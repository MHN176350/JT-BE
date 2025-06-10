using Microsoft.Extensions.Configuration;
using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;
using System.Text;
using System.Text.RegularExpressions;

public class StorageServices
{
    private readonly IConfiguration _configuration;
    private readonly IMinioClient _minioClient;
    private readonly string _bucketName;

    public StorageServices(IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

        var endpoint = _configuration["Minio:Endpoint"] ?? "http://localhost:9000";
        var accessKey = _configuration["Minio:AccessKey"];
        var secretKey = _configuration["Minio:SecretKey"];
        _bucketName = _configuration["Minio:BucketName"];

        _minioClient = new MinioClient()
            .WithEndpoint(endpoint.Replace("http://", "").Replace("https://", ""))
            .WithCredentials(accessKey, secretKey)
            .WithSSL(endpoint.StartsWith("https"))
            .Build();
    }

    public async Task<string> upLoadImg(int id, string base64)
    {
        if (string.IsNullOrWhiteSpace(base64))
            throw new ArgumentException("Base64 image is empty");

        var match = Regex.Match(base64, @"data:image/(?<type>.+?);base64,(?<data>.+)");
        string base64Data;
        string extension = "png";

        if (match.Success)
        {
            base64Data = match.Groups["data"].Value;
            extension = match.Groups["type"].Value;
        }
        else
        {
            base64Data = base64;
        }

        byte[] imageBytes = Convert.FromBase64String(base64Data);

        string objectName = $"UserPicture/{id}_{Guid.NewGuid()}.{extension}";

        bool found = await _minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(_bucketName));
        if (!found)
        {
            await _minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(_bucketName));
        }

        using var stream = new MemoryStream(imageBytes);

        await _minioClient.PutObjectAsync(new PutObjectArgs()
            .WithBucket(_bucketName)
            .WithObject(objectName)
            .WithStreamData(stream)
            .WithObjectSize(stream.Length)
            .WithContentType($"image/{extension}"));

        string endpoint = _configuration["Minio:Endpoint"]!.TrimEnd('/');
        return objectName;
    }
    public async Task<string> GetImageUrl(string objectName)
    {
        if (string.IsNullOrWhiteSpace(objectName))
            throw new ArgumentException("Object name cannot be empty");
        try
        {
            var presignedUrl = await _minioClient.PresignedGetObjectAsync(new PresignedGetObjectArgs()
                .WithBucket(_bucketName)
                .WithObject(objectName)
                .WithExpiry(60 * 120));
            return presignedUrl;
        }
        catch (MinioException ex)
        {
            throw new Exception($"Error getting image URL: {ex.Message}", ex);
        }
    }
}

