using LinkManager.Application.Dtos;

namespace LinkManager.Application.Interfaces.Services;

public interface ILinkService
{
	public Task<string> GetLinkAsync(string shortLink, string? password = null);

	public Task<string> CreateLinkAsync(LinkCreateDto dto);

	public Task<bool> DeleteLinkAsync(string shortLink);

	public Task<bool> UpdateLinkAsync(LinkUpdateDto dto);

	public Task<LinkUpdateDto> GetTemplateAsync(string shortLink);

	public Task<LinkGetDto> GetLinkInfoAsync(string shortLink);

	public Task<LinkGetFullDto> GetLinkFullInfoAsync(long id);

	public Task<List<LinkGetFullDto>> GetLinksFullInfoAsync(int page, int pageSize);

	public Task<List<LinkGetFullDto>> GetLinksFullInfoByUserAsync(long userId);

	public Task<List<LinkGetDto>> GetLinksInfoByUserAsync(long userId);
}
