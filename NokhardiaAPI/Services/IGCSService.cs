namespace NokhardiaAPI.Services
{
	public interface IGCSService
	{
		Task<string> UploadFileAsync(IFormFile file, string folder = "");
	}
}
