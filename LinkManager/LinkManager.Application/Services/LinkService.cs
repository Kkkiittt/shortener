using LinkManager.Application.Dtos;
using LinkManager.Application.Interfaces.Repositories;
using LinkManager.Application.Interfaces.Services;
using LinkManager.Domain.Entities;

using Shortener.Shared.Enums;
using Shortener.Shared.Exceptions;
using Shortener.Shared.Helpers;
using Shortener.Shared.Interfaces;

namespace LinkManager.Application.Services;

public class LinkService : ILinkService
{
	private readonly IClientValidator _validator;
	private readonly ILinkRepository _linkRepo;
	private readonly IUserIdentifier _user;

	public LinkService(IClientValidator validator, ILinkRepository linkRepo, IUserIdentifier userIdentifier)
	{
		_validator = validator;
		_linkRepo = linkRepo;
		_user = userIdentifier;
	}

	public async Task<string> CreateLinkAsync(LinkCreateDto dto)
	{
		int linkCount = await _linkRepo.GetLinkCountAsync(_user.Id);
		ClientWriteCheckDto checkDto = new(_user.SubscriptionId, linkCount, dto.Lifetime);

		var res = await _validator.ValidateCreateAsync(checkDto);
		if(!res)
			throw new ShortenerPermissionException("Permission denied", "Subscription");

		string? passw = null;
		if(dto.Password != null)
			passw = Hasher.Hash(dto.Password);

		Link link = new Link(_user.Id, dto.LongLink, dto.Lifetime, passw)
		{
			Clicks = 0,
			Created = DateTime.UtcNow,
			Updated = DateTime.UtcNow,
		};

		_linkRepo.CreateLink(link);
		await _linkRepo.SaveChangesAsync();
		return Encoder.Encode(link.Id);
	}

	public async Task<bool> DeleteLinkAsync(string shortLink)
	{
		var res = await _validator.ValidateDeleteAsync(_user.SubscriptionId);
		if(!res)
			throw new ShortenerPermissionException("Permission denied", "Subscription");

		long id = Encoder.Decode(shortLink);
		Link? link = await _linkRepo.GetLinkAsync(id);

		if(link == null)
			throw new ShortenerNotFoundException("Link not found", "Link", shortLink);

		if(link.UserId != _user.Id && !_user.IsAdmin)
			throw new ShortenerPermissionException("Access denied", "User role");

		_linkRepo.DeleteLink(link);
		return await _linkRepo.SaveChangesAsync();
	}

	public async Task<string> GetLinkAsync(string shortLink, string? password = null)
	{
		long id = Encoder.Decode(shortLink);
		Link? link = await _linkRepo.GetLinkAsync(id);

		if(link == null || link.Created.AddDays(link.LifeTime) < DateTime.UtcNow)
			throw new ShortenerNotFoundException("Link not found", "Link", shortLink);//if link expired, say that it is not existing

		if(link.PasswordHash != null)//if password is not null only then check it
		{
			if(password == null)
				throw new ShortenerArgumentException("Password required", "Password");

			if(!Hasher.Verify(password, link.PasswordHash))
				throw new ShortenerPermissionException("Access denied", "Password");
		}

		link.Clicks++;
		_linkRepo.UpdateLink(link);
		//_ = Task.Run(async () =>
		//{
		//	var scope = _repo;
		await _linkRepo.SaveChangesAsync();
		//});

		return link.Url;
	}

	public async Task<LinkUpdateDto> GetTemplateAsync(string shortLink)
	{
		long id = Encoder.Decode(shortLink);
		Link? link = await _linkRepo.GetLinkAsync(id);

		if(link == null)
			throw new ShortenerNotFoundException("Link not found", "Link", shortLink);

		if(link.UserId != _user.Id && !_user.IsAdmin)
			throw new ShortenerPermissionException("Access denied", "User role");

		LinkUpdateDto dto = new(shortLink, link.Url, "", link.LifeTime);
		return dto;
	}

	public async Task<LinkGetFullDto> GetLinkFullInfoAsync(long id)
	{
		if(!_user.IsAdmin)
			throw new ShortenerPermissionException("Access denied", "User role");

		Link? link = await _linkRepo.GetLinkAsync(id);

		if(link == null)
			throw new ShortenerNotFoundException("Link not found", "Link", id.ToString());

		LinkGetFullDto dto = (LinkGetFullDto)link;

		return dto;
	}

	public async Task<LinkGetDto> GetLinkInfoAsync(string shortLink)
	{
		var res = await _validator.ValidateInformateAsync(_user.SubscriptionId);
		if(!res)
			throw new ShortenerPermissionException("Permission denied", "Subscription");

		long id = Encoder.Decode(shortLink);
		Link? link = await _linkRepo.GetLinkAsync(id);

		if(link == null)
			throw new ShortenerNotFoundException("Link not found", "Link", shortLink);

		if(link.UserId != _user.Id && !_user.IsAdmin)
			throw new ShortenerPermissionException("Access denied", "User role");

		LinkGetDto dto = new()
		{
			ShortLink = shortLink,
			Clicks = link.Clicks,
			Created = link.Created,
			Lifetime = link.LifeTime,
			Url = link.Url,
			HasPassword = link.PasswordHash != null
		};

		return dto;
	}

	public async Task<List<LinkGetFullDto>> GetLinksFullInfoAsync(int page, int pageSize)
	{
		if(!_user.IsAdmin)
			throw new ShortenerPermissionException("Access denied", "User role");

		if(page < 1)
			throw new ShortenerArgumentException("Invalid page", "Page");

		if(pageSize < 1)
			throw new ShortenerArgumentException("Invalid page size", "PageSize");

		if(pageSize > 50)
			pageSize = 50;//so too much data can not be retrieved

		int skip = (page - 1) * pageSize;
		List<Link> links = await _linkRepo.GetLinksAsync(skip, pageSize);

		return links.Select(x => (LinkGetFullDto)x).ToList();
	}

	public async Task<List<LinkGetFullDto>> GetLinksFullInfoByUserAsync(long userId)
	{
		if(!_user.IsAdmin)
			throw new ShortenerPermissionException("Access denied", "User role");

		List<Link> links = await _linkRepo.GetLinksAsync(userId);

		return links.Select(x => (LinkGetFullDto)x).ToList();
	}

	public async Task<List<LinkGetDto>> GetLinksInfoByUserAsync(long userId)
	{
		var res = await _validator.ValidateInformateAsync(_user.SubscriptionId);
		if(!res)
			throw new ShortenerPermissionException("Permission denied", "Subscription");

		if(!_user.IsAdmin && userId != _user.Id)
			throw new ShortenerPermissionException("Access denied", "User role");

		List<Link> links = await _linkRepo.GetLinksAsync(userId);

		return links.Select(x =>
		{
			LinkGetDto dto = new()
			{
				ShortLink = Encoder.Encode(x.Id),
				Clicks = x.Clicks,
				Created = x.Created,
				Lifetime = x.LifeTime,
				Url = x.Url,
				HasPassword = x.PasswordHash != null,
			};
			return dto;
		}).ToList();
	}

	public async Task<bool> UpdateLinkAsync(LinkUpdateDto dto)
	{
		int linkCount = await _linkRepo.GetLinkCountAsync(_user.Id);
		ClientWriteCheckDto checkDto = new(_user.SubscriptionId, linkCount, dto.Lifetime);

		var res = await _validator.ValidateUpdateAsync(checkDto);
		if(!res)
			throw new ShortenerPermissionException("Permission denied", "Subscription");

		long id = Encoder.Decode(dto.ShortLink);
		Link? link = await _linkRepo.GetLinkAsync(id);

		if(link == null)
			throw new ShortenerNotFoundException("Link not found", "Link", dto.ShortLink);

		if(link.UserId != _user.Id)
			throw new ShortenerPermissionException("Access denied", "User role");

		link.Url = dto.LongLink;
		link.LifeTime = dto.Lifetime;

		if(dto.Password == null)//if password is null set link passwordless
			link.PasswordHash = null;
		else if(dto.Password != "")//if password remained "" as given in template, do not change anything
			link.PasswordHash = Hasher.Hash(dto.Password);

		link.Updated = DateTime.UtcNow;

		_linkRepo.UpdateLink(link);
		return await _linkRepo.SaveChangesAsync();
	}
}
