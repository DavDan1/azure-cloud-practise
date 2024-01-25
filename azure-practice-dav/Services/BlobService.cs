using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using System.Threading.Tasks;

public class BlobService
{
  private readonly CloudBlobContainer _blobContainer;

  public BlobService(IConfiguration configuration)
  {
    // Retrieve the Azure Storage connection string from the configuration
    string storageConnectionString = configuration["ConnectionStrings:blobdav"];

    // Create a CloudStorageAccount object using the connection string
    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageConnectionString);

    // Create a CloudBlobClient object using the storage account
    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

    // Create a reference to the blob container
    _blobContainer = blobClient.GetContainerReference("blobdav");

    // Create the container if it doesn't exist
    _blobContainer.CreateIfNotExistsAsync().GetAwaiter().GetResult();
  }

  public async Task<string> UploadFileAsync(string fileName, Stream fileStream)
  {
    // Get a reference to a block blob
    CloudBlockBlob blockBlob = _blobContainer.GetBlockBlobReference(fileName);

    // Upload the file stream to the blob
    await blockBlob.UploadFromStreamAsync(fileStream);

    // Return the URI of the uploaded blob
    return blockBlob.Uri.ToString();
  }
}
