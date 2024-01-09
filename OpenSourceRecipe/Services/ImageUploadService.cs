using Firebase.Storage;

namespace FirebaseStorageService.Storage;
public class FirebaseStorageService
{
    private readonly string _bucket;
    private readonly string _apiKey;

    public FirebaseStorageService(IConfiguration configuration)
    {
        _bucket = configuration["Firebase:StorageBucket"];
        _apiKey = configuration["Firebase:ApiKey"];
    }

    public async Task<string> UploadFileAsync(Stream fileStream, string fileName)
    {
        var task = new FirebaseStorage(_bucket, new FirebaseStorageOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(_apiKey),
                ThrowOnCancel = true // when you cancel the upload, exception is thrown. By default no exception is thrown
            })
            .Child("images")
            .Child(fileName)
            .PutAsync(fileStream);

        var downloadUrl = await task;
        return downloadUrl;
    }
}
