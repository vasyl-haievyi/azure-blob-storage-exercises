using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace AzureBlobLearning.Services
{
	public interface IAzureBlobService
	{
		Task UploadAsync(IFormFileCollection files);
		Task<IEnumerable<Uri>> ListAsync();
		Task DeleteAsync(string fileUri);
		Task DeleteAllAsync();
	}

	public class AzureBlobService : IAzureBlobService
	{
		private readonly IAzureBlobConnectionFactory _azureBlobConnectionFactory;

		public AzureBlobService(IAzureBlobConnectionFactory azureBlobConnectionFactory)
		{
			_azureBlobConnectionFactory = azureBlobConnectionFactory;
		}

		public async Task UploadAsync(IFormFileCollection files)
		{
			var blobContainer = await _azureBlobConnectionFactory.GetBlobContainer();

			foreach (var file in files)
			{
				//TODO: implement file uploading to the blob storage
			}
		}

		public async Task<IEnumerable<Uri>> ListAsync()
		{
			var blobContainer = await _azureBlobConnectionFactory.GetBlobContainer();
			var allBlobs = new List<Uri>();
			
			//TODO: implement retrieving uri's of all blobs in the container.

			return allBlobs;
		}

		public async Task DeleteAsync(string fileUri)
		{
			var blobContainer = await _azureBlobConnectionFactory.GetBlobContainer();

			//TODO: implement deleting of blob with name specifiend withing fileUri from container.
		}

		public async Task DeleteAllAsync()
		{
			var blobContainer = await _azureBlobConnectionFactory.GetBlobContainer();

			//TODO: implement deleting all blobs in the container.
		}
	}
}
