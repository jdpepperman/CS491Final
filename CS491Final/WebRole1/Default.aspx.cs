using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

namespace WebRole1
{
    public partial class _Default : Page
    {
        String selectedItem = "";
        String connectionString = "cloudcomputing";

        protected void Page_Load(object sender, EventArgs e)
        {
            refreshFileList();
            this.Header.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            if (FileUpload1.HasFile)
            {
                String filename = FileUpload1.FileName;
                // Setup the connection to Azure Storage
                var storageAccount = CloudStorageAccount.Parse(RoleEnvironment.GetConfigurationSettingValue(connectionString));
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

         
                    Literal lit = new Literal();
                    lit.Text = @"<div class='alert alert-dismissable alert-success'> <button type='button' class='close' data-dismiss='alert'>×</button>
                 <strong>Success!</strong> File <a href='#' class='alert-link'>" + filename + "</a> was uploaded.</div>";

                    Panel1.Controls.Add(lit);
                
               
            }
            

        }

        //delete
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (selectedItem != "")
            {
                // Retrieve storage account from connection string.
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                    CloudConfigurationManager.GetSetting(connectionString));

                // Create the blob client.
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                // Retrieve reference to a previously created container.
                CloudBlobContainer container = blobClient.GetContainerReference("quicklap");

                // Retrieve reference to a blob named "myblob.txt".
                //System.Diagnostics.Trace.WriteLine("Blob name to DELETE: " + selectedItem);
                //Literal1.Text = selectedItem;
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(selectedItem);

                // Delete the blob.
                blockBlob.Delete();
                fileListBox.Items.Clear();
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
            CloudConfigurationManager.GetSetting(connectionString));

            // Create the blob client. 
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("quicklap");

            if (container.ListBlobs(null, false) != null)
            {
                // Loop over items within the container and output the length and URI.
                foreach (IListBlobItem item in container.ListBlobs(null, false))
                {
                    if (item.GetType() == typeof(CloudBlockBlob))
                    {
                        CloudBlockBlob blob = (CloudBlockBlob)item;

                    //System.Diagnostics.Trace.WriteLine("Block blob of length " + blob.Name + " : " + blob.Uri);
                        if (!fileListBox.Items.Contains(new ListItem(blob.Name)))
                            {
                                fileListBox.Items.Add(new ListItem(blob.Name));
                            }

                        

                    }
                    else if (item.GetType() == typeof(CloudPageBlob))
                    {
                        CloudPageBlob pageBlob = (CloudPageBlob)item;

                        //System.Diagnostics.Trace.WriteLine("Page blob of length " + pageBlob.Properties.Length + " : " + pageBlob.Uri);

                    }
                    else if (item.GetType() == typeof(CloudBlobDirectory))
                    {
                        CloudBlobDirectory directory = (CloudBlobDirectory)item;

                        //System.Diagnostics.Trace.WriteLine("Directory: " + directory.Uri);
                    }
                }
            }
           
        }

        protected void fileListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fileListBox.SelectedItem != null) selectedItem = fileListBox.SelectedItem.Text;
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            if (selectedItem != "")
            {
                // Retrieve storage account from connection string.
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                    CloudConfigurationManager.GetSetting(connectionString));

                // Create the blob client.
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                // Retrieve reference to a previously created container.
                CloudBlobContainer container = blobClient.GetContainerReference("quicklap");

                // Retrieve reference to a blob named "photo1.jpg".
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(selectedItem);

                String pathUser = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                String pathDownload = Path.Combine(pathUser, "Downloads");


                pathDownload = TextBox1.Text;
                Literal1.Text = TextBox1.Text;

                String filepath = Path.Combine(pathDownload, selectedItem);//, selectedItem);v 

                // Save blob contents to a file.
                using (var fileStream = System.IO.File.OpenWrite(@filepath))
                {
                    blockBlob.DownloadToStream(fileStream);
                }
            }
        }

        protected void btnRandom_Click(object sender, EventArgs e)
        {
             
            Literal lit = new Literal();
            lit.Text = @"
            <input id='FileInput' runat='server' type='file' />";

            FileUpload newFileupload = new FileUpload();
           // newFileupload.ApplyStyle();

            Panel1.Controls.Add(lit);

        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }


    }
}