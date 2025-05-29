using LinkManager.Application.Dtos;

namespace LinkManager.Application.Interfaces.Services;

public interface ILinkManager
{
	public Task<string> GetLinkAsync(string shortLink, string? password = null);

	public Task<string> CreateLinkAsync(LinkCreateDto dto);

	public Task<bool> DeleteAsync(string shortLink);

	public Task<bool> UpdateAsync(LinkUpdateDto dto);

	public Task<LinkGetDto> GetLinkInfoAsync(string shortLink);

	public Task<LinkGetFullDto> GetLinkFullInfoAsync(long id);

	public Task<List<LinkGetFullDto>> GetLinksFullInfoAsync(int page, int pageSize);

	public Task<List<LinkGetFullDto>> GetLinksFullInfoByUserAsync(long userId);

	public Task<List<LinkGetDto>> GetLinksInfoByUserAsync(long userId);
}
