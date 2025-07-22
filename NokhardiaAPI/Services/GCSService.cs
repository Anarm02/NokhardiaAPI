
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using NokhardiaAPI.DTO_s;

namespace NokhardiaAPI.Services
{
	public class GCSService : IGCSService
	{
		private readonly string _bucketName;
		private readonly StorageClient _storageClient;
		public GCSService(GCSOptions options)
		{
			_bucketName = options.BucketName ?? throw new ArgumentNullException(nameof(options.BucketName));

			var credentialsPath = Path.Combine(Directory.GetCurrentDirectory(), options.CredentialsPath);
			if (!File.Exists(credentialsPath))
				throw new FileNotFoundException("GCS credentials file not found", credentialsPath);

			var credential = GoogleCredential.FromFile(credentialsPath);
			_storageClient = StorageClient.Create(credential);
		}
		public async Task<string> UploadFileAsync(IFormFile file, string folder = "")
		{
			if (file == null || file.Length == 0)
				throw new ArgumentException("File is empty");

			var objectName = $"{folder}/{Guid.NewGuid()}_{file.FileName}".Trim('/');
			using var stream = file.OpenReadStream();

			await _storageClient.UploadObjectAsync(_bucketName, objectName, file.ContentType, stream);

			return $"https://storage.googleapis.com/{_bucketName}/{objectName}";
		}
	}
}
