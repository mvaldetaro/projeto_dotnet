using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Web.Business
{
    public class Blob
    {
        public static string UploadBlob(HttpPostedFileBase file)
        {
            var BlobUrl = "";

            if (file != null && file.ContentLength > 0)
            {
                CloudStorageAccount storageAccount = null;
                CloudBlobContainer cloudBlobContainer = null;

                string storageConnectionString = ConfigurationManager.AppSettings["StorageConnectionString"];

                if (CloudStorageAccount.TryParse(storageConnectionString, out storageAccount))
                {
                    try
                    {
                        CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
                        cloudBlobContainer = cloudBlobClient.GetContainerReference("hortagram");

                        CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(Guid.NewGuid().ToString() + ".jpg");

                        cloudBlockBlob.UploadFromFileAsync(file.FileName);

                        BlobUrl = cloudBlockBlob.Uri.AbsoluteUri;
                    }
                    catch (StorageException e)
                    {
                        var erro = e.Message;
                    }

                    return BlobUrl;
                }
                else
                {
                    return BlobUrl;
                }

            }

            return BlobUrl;
        }
    }
}