using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
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

        //String connectionString = "localhost";
        string search = "";

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


                CloudBlobContainer accountContainer = blobClient.GetContainerReference("listing");
                accountContainer.CreateIfNotExists();

                CloudBlockBlob userBlob =  accountContainer.GetBlockBlobReference(filename);
                
                if (uploadType.Text.Contains("0"))
                {
                    userBlob.UploadText("public");
                }
                else if (uploadType.Text.Contains("1"))
                {
                    userBlob.UploadText("unlisted");
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

            //Literal1.Text = TextBox1.Text;
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
            CloudConfigurationManager.GetSetting(connectionString));
            // Create the blob client. 
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("quicklap");

            // Retrieve reference to a previously created container.
            
            CloudBlobContainer listContainer = blobClient.GetContainerReference("listing");

            

            if (container.ListBlobs(null, false) != null)
            {
                // Loop over items within the container and output the length and URI.
                foreach (IListBlobItem item in container.ListBlobs(null, false))
                {

                    if (item.GetType() == typeof(CloudBlockBlob))
                    {


                        CloudBlockBlob blob = (CloudBlockBlob)item;

                        string text = "";
                        if (listContainer.GetBlockBlobReference(blob.Name).Exists())
                        {
                            using (var memoryStream = new MemoryStream())
                            {
                                //text
                                listContainer.GetBlockBlobReference(blob.Name).DownloadToStream(memoryStream);
                                text = System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
                            }

                        }

                        //System.Diagnostics.Trace.WriteLine("Block blob of length " + blob.Name + " : " + blob.Uri);
                        if (!fileListBox.Items.Contains(new ListItem(blob.Name)))
                            {
                                //check if plubic mode
                                if (!uploadType.Text.Contains("1"))
                                {
                                    //item is public
                                    if (!text.Equals("unlisted"))
                                    {
                                        fileListBox.Items.Add(new ListItem(blob.Name));
                                    }
                                }
                                else
                                {
                                    //Remove unlisted items
                                    fileListBox.Items.Remove(new ListItem(blob.Name));
                                }

                            }
                        //Remove Items
                        if (!uploadType.Text.Contains("1"))
                        {
                            if (!blob.Name.Contains(TextBox1.Text) && !TextBox1.Text.Equals(""))
                            {
                                if (fileListBox.Items.Contains(new ListItem(blob.Name)))
                                     fileListBox.Items.Remove(new ListItem(blob.Name));
                            }
                        }
                        else
                        {
                            if (fileListBox.Items.Contains(new ListItem(blob.Name)))
                                 fileListBox.Items.Remove(new ListItem(blob.Name));

                            if (text.Contains("unlisted"))
                            {
                                if (blob.Name.Contains(TextBox1.Text) && TextBox1.Text.Length > 3)
                                {
                                    if (!fileListBox.Items.Contains(new ListItem(blob.Name)))
                                        fileListBox.Items.Add(new ListItem(blob.Name));
                                }
                            }
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

        //download
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

               // blockBlob.DownloadToFile(@"C:\Users\Joshua\Desktop\" + selectedItem, FileMode.Create);

                MemoryStream other = new MemoryStream();

                Response.ClearContent();

                Response.ContentType = blockBlob.Properties.ContentType; //contentType: html, pdf, jpg, doc, txt, etc...
                Response.AddHeader("content-disposition", "attachment; filename=" + blockBlob.Name);

                blockBlob.DownloadToStream(other);//from blob to stream
                other.WriteTo(Response.OutputStream);//stream to browser download

                Response.Flush();
                Response.End();

            }
        }

        protected void private_Click(object sender, EventArgs e)
        {
            uploadType.Text = ""+1;
        }

        protected void public_Click(object sender, EventArgs e)
        {
            uploadType.Text = ""+0;
        }

        protected void refreshButton(object sender, EventArgs e)
        {
            search = TextBox1.Text;
            refreshFileList();
        }


    }
}