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

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            if (FileUpload1.HasFile)
            {

                // Setup the connection to Azure Storage
                var storageAccount = CloudStorageAccount.Parse(RoleEnvironment.GetConfigurationSettingValue("localhost"));
                var blobClient = storageAccount.CreateCloudBlobClient();
                // Get and create the container
                var blobContainer = blobClient.GetContainerReference("quicklap");
                blobContainer.CreateIfNotExists();
                // upload a text blob

                CloudBlobContainer container = blobClient.GetContainerReference("quicklap");
                container.CreateIfNotExists();

                CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference("quicklap");

                using (var fileStream = FileUpload1.FileContent)
                {
                    blockBlob.UploadFromStream(fileStream);
                } 

                // log a message that can be viewed in the diagnostics tables called WADLogsTable
                System.Diagnostics.Trace.WriteLine("Added blob to Azure Storage");

            }

        }

        protected void FileUpload_Click(object sender, EventArgs e)
        {


        }




    }
}