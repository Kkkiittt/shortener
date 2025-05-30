
using LinkManager.Application.Dtos;
using LinkManager.Application.Helpers;
using LinkManager.Application.Interfaces.Repositories;
using LinkManager.Application.Interfaces.Services;
using LinkManager.Domain.Entities;

namespace LinkManager.Application.Services;

public class LinkService : ILinkService
{
	//private readonly IClientValidator _validator;
	private readonly ILinkRepository _repo;
	private readonly IUserIdentifier _user;

	public LinkService(/* IClientValidator validator,*/ ILinkRepository repo, IUserIdentifier userIdentifier)
	{
		//_validator = validator;
		_repo = repo;
		_user = userIdentifier;
	}

	public async Task<string> CreateLinkAsync(LinkCreateDto dto)
	{
		string? passw = null;
		if(dto.Password != null)
			passw = Hasher.Hash(dto.Password);

		Link link = new Link(_user.Id, dto.LongLink, dto.Lifetime, passw)
		{
			Clicks = 0,
			Created = DateTime.UtcNow,
			Updated = DateTime.UtcNow,
		};

		_repo.CreateLink(link);
		await _repo.SaveChangesAsync();
		return Encoder.Encode(link.Id);
	}

	public async Task<bool> DeleteLinkAsync(string shortLink)
	{
		long id = Encoder.Decode(shortLink);
		Link? link = await _repo.GetLinkAsync(id);

		if(link == null)
			throw new Exception("Link not found");

		if(link.UserId != _user.Id && !_user.IsAdmin)
			throw new Exception("Access denied");

		_repo.DeleteLink(link);
		return await _repo.SaveChangesAsync();
	}

	public async Task<string> GetLinkAsync(string shortLink, string? password = null)
	{
		long id = Encoder.Decode(shortLink);
		Link? link = await _repo.GetLinkAsync(id);

		if(link == null || link.Created.AddDays(link.LifeTime) < DateTime.UtcNow)
			throw new Exception("Link not found");

		if(link.PasswordHash != null)
		{
			if(password == null)
				throw new Exception("Password required");

			if(!Hasher.Verify(password, link.PasswordHash))
				throw new Exception("Wrong password");
		}

		link.Clicks++;
		_repo.UpdateLink(link);
		//_ = Task.Run(async () =>
		//{
		//	var scope = _repo;
		await _repo.SaveChangesAsync();
		//});

		return link.Url;
	}

	public async Task<LinkUpdateDto> GetTemplateAsync(string shortLink)
	{
		long id = Encoder.Decode(shortLink);
		Link? link = await _repo.GetLinkAsync(id);

		if(link == null)
			throw new Exception("Link not found");

		if(link.UserId != _user.Id && !_user.IsAdmin)
			throw new Exception("Access denied");

		LinkUpdateDto dto = new(shortLink, link.Url, "", link.LifeTime);
		return dto;
	}

	public async Task<LinkGetFullDto> GetLinkFullInfoAsync(long id)
	{
		if(!_user.IsAdmin)
			throw new Exception("Access denied");

		Link? link = await _repo.GetLinkAsync(id);

		if(link == null)
			throw new Exception("Link not found");

		LinkGetFullDto dto = (LinkGetFullDto)link;

		return dto;
	}

	public async Task<LinkGetDto> GetLinkInfoAsync(string shortLink)
	{
		long id = Encoder.Decode(shortLink);
		Link? link = await _repo.GetLinkAsync(id);

		if(link == null)
			throw new Exception("Link not found");

		if(link.UserId != _user.Id && !_user.IsAdmin)
			throw new Exception("Access denied");

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
			throw new Exception("Access denied");

		if(page < 1 || pageSize < 1)
			throw new Exception("Invalid parameters");

		if(pageSize > 50)
			pageSize = 50;

		int skip = (page - 1) * pageSize;
		List<Link> links = await _repo.GetLinksAsync(skip, pageSize);

		return links.Select(x => (LinkGetFullDto)x).ToList();
	}

	public async Task<List<LinkGetFullDto>> GetLinksFullInfoByUserAsync(long userId)
	{
		if(!_user.IsAdmin)
			throw new Exception("Access denied");

		List<Link> links = await _repo.GetLinksAsync(userId);

		return links.Select(x => (LinkGetFullDto)x).ToList();
	}

	public async Task<List<LinkGetDto>> GetLinksInfoByUserAsync(long userId)
	{
		if(!_user.IsAdmin && userId != _user.Id)
			throw new Exception("Access denied");

		List<Link> links = await _repo.GetLinksAsync(userId);

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
		long id = Encoder.Decode(dto.ShortLink);
		Link? link = await _repo.GetLinkAsync(id);

		if(link == null)
			throw new Exception("Link not found");

		if(link.UserId != _user.Id)
			throw new Exception("Access denied");

		link.Url = dto.LongLink;
		link.LifeTime = dto.Lifetime;

		if(dto.Password == null)
			link.PasswordHash = null;
		else if(dto.Password != "")
			link.PasswordHash = Hasher.Hash(dto.Password);

		link.Updated = DateTime.UtcNow;

		_repo.UpdateLink(link);
		return await _repo.SaveChangesAsync();
	}
}
