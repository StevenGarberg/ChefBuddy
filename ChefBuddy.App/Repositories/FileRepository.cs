using Amazon.S3;
using Amazon.S3.Model;

namespace ChefBuddy.App.Repositories;

public interface IFileRepository
{
    Task<Guid> UploadFile(IFormFile file);
}

public class FileRepository : IFileRepository
{
    private readonly IConfiguration _configuration;
    private readonly IAmazonS3 _s3Client;

    public FileRepository(IConfiguration configuration, IAmazonS3 s3Client)
    {
        _configuration = configuration;
        _s3Client = s3Client;
    }
    
    public async Task<Guid> UploadFile(IFormFile file)
    {
        var guid = Guid.NewGuid();
        var request = new PutObjectRequest
        {
            BucketName = _configuration["S3:BucketName"],
            Key = $"{_configuration["S3:FilePath"]}{guid}.{file.FileName.Split(".")[1]}",
            InputStream = file.OpenReadStream()
        };
        request.Metadata.Add("Content-Type", file.ContentType);
        await _s3Client.PutObjectAsync(request);
        return guid;
    }
}