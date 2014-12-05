using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

namespace WebRole1
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            refreshFileList();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            if (FileUpload1.HasFile)
            {
                String filename = FileUpload1.FileName;
                // Setup the connection to Azure Storage
                var storageAccount = CloudStorageAccount.Parse(RoleEnvironment.GetConfigurationSettingValue("localhost"));
                var blobClient = storageAccount.CreateCloudBlobClient();
                // Get and create the container
                var blobContainer = blobClient.GetContainerReference("quicklap");
                blobContainer.CreateIfNotExists();
                // upload a text blob

                CloudBlobContainer container = blobClient.GetContainerReference("quicklap");
                container.CreateIfNotExists();

                CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference(filename);

                using (var fileStream = FileUpload1.FileContent)
                {
                    blockBlob.UploadFromStream(fileStream);
                } 

                // log a message that can be viewed in the diagnostics tables called WADLogsTable
                System.Diagnostics.Trace.WriteLine("Added blob to Azure Storage");
                refreshFileList();

            }

        }

        protected void FileUpload_Click(object sender, EventArgs e)
        {


        }

        protected void refreshFileList()
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
            CloudConfigurationManager.GetSetting("localhost"));

            // Create the blob client. 
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("quicklap");

            ListBox1.Items.Clear();
            // Loop over items within the container and output the length and URI.
            foreach (IListBlobItem item in container.ListBlobs(null, false))
            {
                if (item.GetType() == typeof(CloudBlockBlob))
                {
                    CloudBlockBlob blob = (CloudBlockBlob)item;
                    

                    System.Diagnostics.Trace.WriteLine("Block blob of length " + blob.Name + " : " + blob.Uri);
                    ListBox1.Items.Add(blob.Name);

                }
                else if (item.GetType() == typeof(CloudPageBlob))
                {
                    CloudPageBlob pageBlob = (CloudPageBlob)item;

                    System.Diagnostics.Trace.WriteLine("Page blob of length " + pageBlob.Properties.Length + " : " + pageBlob.Uri);

                }
                else if (item.GetType() == typeof(CloudBlobDirectory))
                {
                    CloudBlobDirectory directory = (CloudBlobDirectory)item;

                    System.Diagnostics.Trace.WriteLine("Directory: " + directory.Uri);
                }
            }
        }


    }
}