# Azure Blob Storage Exercises

## Exercise 1 - Azure portal
Prerequisites: 
- Azure subscription - [link](https://azure.microsoft.com/free/?WT.mc_id=A261C142F "Free account")
- Azure storage accoung - [link](https://docs.microsoft.com/en-us/azure/storage/common/storage-account-create?tabs=azure-portal "Create storage account")
  
Task: 
- Create container and upload any image.
- Open uploaded image through web browser.

<details>
<summary>Solution</summary>

- Go to Azure Portal.
- Go to Storage Explorer.
- Select storage account you created.
- Right click on Blob containers menu item. Thank select Create blob container option.
- Enter name e.g. images. Select public access level (Blob is enough for this exercise).
- Refresh view. Select created container. Click upload and upload an image.
- Select uploaded blob. Click ```Copy URL``` button. 
- Paste url in web browser. Look at you wonderful blob :)
</details>

<br/>
<br/>

## Exercise 2 - Connection string
Prerequisites:
- Exercise 1

Task:
- From Azure Portal copy you connection string.
- Paste it into the settings.json file as a value for the "StorageConnectionString" key.
  
<details>
<summary>Solution</summary>

- Go to Azure Portal.
- Go to Storage accounts. Select you storage account.
- Click the "Access keys" menu item in "Security + networking section".
- Click show keys.
- Cope key1 Connection string value.
- Paste copied string into the settings.json file as a value for the "StorageConnectionString" key.
</details>

<br/>
<br/>

## Exercise 3 - Upload blob
Prerequisites:
- Exercise 2

Task:
- Implement missing part of the ```UploadAsync``` method in the ```AzureBlobService.cs``` file

Useful links:
- [access block blob](https://docs.microsoft.com/en-us/dotnet/api/microsoft.azure.storage.blob.cloudblobcontainer.getblockblobreference?view=azure-dotnet-legacy#Microsoft_Azure_Storage_Blob_CloudBlobContainer_GetBlockBlobReference_System_String_)
- [upload blob](https://docs.microsoft.com/en-us/dotnet/api/microsoft.azure.storage.blob.cloudblockblob.uploadfromstreamasync?view=azure-dotnet-legacy#Microsoft_Azure_Storage_Blob_CloudBlockBlob_UploadFromStreamAsync_System_IO_Stream_)
  
<details>
<summary>Solution</summary>

```c#
var blob = blobContainer.GetBlockBlobReference(file.FileName);
using (var stream = file.OpenReadStream())
{
    await blob.UploadFromStreamAsync(stream);
}
```
</details>

<br/>
<br/>

## Exercise 4 - List blobs
Prerequisites:
- Exercise 2

Task:
- Implement missing part of the ```ListAsync``` method in the ```AzureBlobService.cs``` file

Useful links:
- [list blobs](https://docs.microsoft.com/en-us/dotnet/api/microsoft.azure.storage.blob.cloudblobcontainer.listblobssegmentedasync?view=azure-dotnet-legacy#Microsoft_Azure_Storage_Blob_CloudBlobContainer_ListBlobsSegmentedAsync_Microsoft_Azure_Storage_Blob_BlobContinuationToken_)
  
<details>
<summary>Solution</summary>

```c#
BlobContinuationToken blobContinuationToken = null;
do
{
	var response = await blobContainer.ListBlobsSegmentedAsync(blobContinuationToken);
	foreach (IListBlobItem blob in response.Results)
    {
	    if (blob.GetType() == typeof(CloudBlockBlob))
			allBlobs.Add(blob.Uri);
	}
	blobContinuationToken = response.ContinuationToken;
} while (blobContinuationToken != null);
```
</details>

<br/>
<br/>

## Exercise 5 - Delete blob
Prerequisites:
- Exercise 2

Task:
- Implement missing part of the ```DeleteAsync``` method in the ```AzureBlobService.cs``` file

Useful links:
- [access block blob](https://docs.microsoft.com/en-us/dotnet/api/microsoft.azure.storage.blob.cloudblobcontainer.getblockblobreference?view=azure-dotnet-legacy#Microsoft_Azure_Storage_Blob_CloudBlobContainer_GetBlockBlobReference_System_String_)
- [delete block blob](https://docs.microsoft.com/en-us/dotnet/api/microsoft.azure.storage.blob.cloudblob.deleteifexistsasync?view=azure-dotnet-legacy#Microsoft_Azure_Storage_Blob_CloudBlob_DeleteIfExistsAsync)
  
<details>
<summary>Solution</summary>

```c#
Uri uri = new Uri(fileUri);
string filename = Path.GetFileName(uri.LocalPath);

var blob = blobContainer.GetBlockBlobReference(filename);
await blob.DeleteIfExistsAsync();
```
</details>

<br/>
<br/>

## Exercise 5 - Delete all blobs
Prerequisites:
- Exercise 2

Task:
- Implement missing part of the ```DeleteAllAsync``` method in the ```AzureBlobService.cs``` file

Useful links:
- [list blobs](https://docs.microsoft.com/en-us/dotnet/api/microsoft.azure.storage.blob.cloudblobcontainer.listblobssegmentedasync?view=azure-dotnet-legacy#Microsoft_Azure_Storage_Blob_CloudBlobContainer_ListBlobsSegmentedAsync_Microsoft_Azure_Storage_Blob_BlobContinuationToken_)
- [delete block blob](https://docs.microsoft.com/en-us/dotnet/api/microsoft.azure.storage.blob.cloudblob.deleteifexistsasync?view=azure-dotnet-legacy#Microsoft_Azure_Storage_Blob_CloudBlob_DeleteIfExistsAsync)
  
<details>
<summary>Solution</summary>

```c#
BlobContinuationToken blobContinuationToken = null;
do
{
	var response = await blobContainer.ListBlobsSegmentedAsync(blobContinuationToken);
	foreach (IListBlobItem blob in response.Results)
	{
		if (blob.GetType() == typeof(CloudBlockBlob))
			await((CloudBlockBlob)blob).DeleteIfExistsAsync();
	}
	blobContinuationToken = response.ContinuationToken;
} while (blobContinuationToken != null);
```
</details>


# Run the application
- Go to ```AzureBlobLearning/AzureBlobLearning/AzureBlobLearning```.
- Run ```dotnet build```.
- Run ```dotnet run```.
- In web browser open the <https://localhost:5001>