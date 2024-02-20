using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.Azure;
using System.Collections;
using System.Text;
using System.Web.Configuration;
namespace ems.storage.Functions
{
    public class azurestorage
    {
        public MemoryStream DownloadStream(string container_name, string blob_filename, string downlod_type)
        {
            if (downlod_type == "L")
            {
                MemoryStream ms = new MemoryStream();
                using (FileStream file = new FileStream(blob_filename, FileMode.Open, System.IO.FileAccess.Read))
                {
                    byte[] bytes = new byte[file.Length];
                    file.Read(bytes, 0, (int)file.Length);
                    ms.Write(bytes, 0, (int)file.Length);
                }
                return ms;
            }

            else
            {
                try
                {
                    MemoryStream memoryStream = new MemoryStream();
                    // Retrieve storage account from connection string.
                    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"].ToString());
                    // Create the blob client.
                    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                    // Retrieve reference to a previously created container.
                    CloudBlobContainer container = blobClient.GetContainerReference(container_name);
                    // Retrieve reference to a blob named "photo1.jpg".
                    CloudBlockBlob blockBlob = container.GetBlockBlobReference(blob_filename);

                    var ls_downfile = new MemoryStream();

                    blockBlob.DownloadToStream(ls_downfile);
                    return ls_downfile;
                }
                catch (Exception ex)
                {
                    var ls_downfile1 = new MemoryStream();
                    return ls_downfile1;
                }
            }
        }


        public bool UploadStream(string container_name, string blob_filename, MemoryStream upload_stream, string upload_type)
        {
            if (upload_type == "L")
            {
                FileStream file = new FileStream(blob_filename, FileMode.Create, FileAccess.Write);
                upload_stream.WriteTo(file);
                file.Close();
                return true;
            }
            else if (upload_type =="B")
            {
                try
                {
                    // Retrieve storage account from connection string.
                    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"].ToString());
                    // Create the blob client.
                    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                    // Retrieve reference to a previously created container.
                    CloudBlobContainer container = blobClient.GetContainerReference(container_name);
                    // Retrieve reference to a blob named "myblob".
                    CloudBlockBlob blockBlob = container.GetBlockBlobReference(blob_filename);
                    // Create or overwrite the "myblob" blob with contents from a local file.
                    if (upload_stream.Length > 0)
                        upload_stream.Position = 0;
                    blockBlob.UploadFromStream(upload_stream);

                    FileStream file = new FileStream(blob_filename, FileMode.Create, FileAccess.Write);
                    upload_stream.WriteTo(file);
                    file.Close();


                    return true;
                }
                catch
                {
                    return false;
                }


            }
            else
            {
                try
                {
                    // Retrieve storage account from connection string.
                    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"].ToString());
                    // Create the blob client.
                    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                    // Retrieve reference to a previously created container.
                    CloudBlobContainer container = blobClient.GetContainerReference(container_name);
                    // Retrieve reference to a blob named "myblob".
                    CloudBlockBlob blockBlob = container.GetBlockBlobReference(blob_filename);
                    // Create or overwrite the "myblob" blob with contents from a local file.
                    if (upload_stream.Length > 0)
                        upload_stream.Position = 0;
                    blockBlob.UploadFromStream(upload_stream);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            

        }

        public bool DeleteBlob(string container_name, string blob_filename)
        {
            CloudStorageAccount _CloudStorageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"].ToString());

            CloudBlobClient _CloudBlobClient = _CloudStorageAccount.CreateCloudBlobClient();

            CloudBlobContainer _CloudBlobContainer = _CloudBlobClient.GetContainerReference(container_name);

            CloudBlockBlob _CloudBlockBlob = _CloudBlobContainer.GetBlockBlobReference(blob_filename);

            _CloudBlockBlob.Delete();

            return true;
        }

        public string UploadBlob(string container_name, string blob_filename, string filepath)
        {
            try
            {
                // Retrieve storage account from connection string.
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"].ToString());
                // Create the blob client.
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                // Retrieve reference to a previously created container.
                CloudBlobContainer container = blobClient.GetContainerReference(container_name);
                // Retrieve reference to a blob named "myblob".
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(blob_filename);
                // Create or overwrite the "myblob" blob with contents from a local file.
                using (FileStream filestream = System.IO.File.OpenRead(filepath))
                {
                    blockBlob.UploadFromStream(filestream);
                }
                return filepath;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public string DownloadBlobText(string container_name, string blob_filename)
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"].ToString());

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference(container_name);

            // Retrieve reference to a blob named "photo1.jpg".
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(blob_filename);
            try
            {
                string text;
                using (var memoryStream = new MemoryStream())
                {
                    blockBlob.DownloadToStream(memoryStream);
                    text = System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
                }
                return text;
            }
            // Save blob contents to a file.

            catch (Exception ex)
            {
                return "";
            }
            finally
            {
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
                System.GC.WaitForFullGCComplete();

            }
        }

        public List<string> DownloadBlobList(string container_name)
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"].ToString());

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference(container_name);


            List<string> BlobList = new List<string>();
            try
            {
                BlobList = container.ListBlobs(null, false).AsEnumerable().Select(row =>
                        (string)(row.Uri.Segments.Last())).ToList();



                //// Retrieve reference to a blob named "photo1.jpg".
                //CloudBlockBlob blockBlob = container.GetBlockBlobReference(blob_filename);
                //try
                //{
                //    string text;
                //    using (var memoryStream = new MemoryStream())
                //    {
                //        blockBlob.DownloadToStream(memoryStream);
                //        text = System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
                //    }
                //    return text;

                // Save blob contents to a file.
                return BlobList;
            }

            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
                System.GC.WaitForFullGCComplete();

            }
        }


        public string Localstoragepath(string localstoragename, string localfilename)
        {
            try
            {
                // Retrieve an object that points to the local storage resource.


                //     Define the file name and path.

                String filePath = HttpContext.Current.Server.MapPath("../../Temp");

                using (FileStream writeStream = File.Create(filePath))
                {
                    Byte[] textToWrite = new UTF8Encoding(true).GetBytes("Testing Web role storage");
                    writeStream.Write(textToWrite, 0, textToWrite.Length);
                }

                filePath = DownloadBlobToPath("eml", localfilename, filePath);

                return filePath;
            }
            catch (Exception ex)
            {
                return "error";
            }
        }


        public string DownloadBlobToPath(string container_name, string blob_filename, string filepath)
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"].ToString());

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference(container_name);

            // Retrieve reference to a blob named "photo1.jpg".
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(blob_filename);
            try
            {
                string text;
                using (var memoryStream = new MemoryStream())
                {
                    blockBlob.DownloadToStream(memoryStream);
                    text = System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
                }
                return text;
            }
            // Save blob contents to a file.

            catch (Exception ex)
            {
                return "";
            }
            finally
            {
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
                System.GC.WaitForFullGCComplete();

            }
        }

        public bool CheckBlobExist(string container_name, string blob_filename)
        {
            try
            {
                MemoryStream memoryStream = new MemoryStream();
                // Retrieve storage account from connection string.
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"].ToString());
                // Create the blob client.
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                // Retrieve reference to a previously created container.
                CloudBlobContainer container = blobClient.GetContainerReference(container_name);
                // Retrieve reference to a blob named "photo1.jpg".
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(blob_filename);

                if (blockBlob.Exists())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                var ls_downfile1 = new MemoryStream();
                return false;
            }
        }

        //public bool Uploaddocumentfrompath(string container_name, string blob_filename, string upload_file)
        //{
        //    try
        //    {
        //        // Retrieve storage account from connection string.
        //        CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString").ToString());
        //        // Create the blob client.
        //        CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
        //        // Retrieve reference to a previously created container.
        //        CloudBlobContainer container = blobClient.GetContainerReference(container_name);
        //        // Retrieve reference to a blob named "myblob".
        //        CloudBlockBlob blockBlob = container.GetBlockBlobReference(blob_filename);
        //        // Create or overwrite the "myblob" blob with contents from a local file.
        //        blockBlob.UploadFromFile(upload_file);
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }

        //}
        public Dictionary<string, object> ConvertDocumentToByteArray(MemoryStream msstream, string filename, string format)
        {
            var ls_response = new Dictionary<string, object>();
            string downloadfile = string.Empty;
            try
            {
                string message = "";
                string msg_type = "";
                if (msstream.Length > 0)
                {
                    msstream.Position = 0;
                    downloadfile = Convert.ToBase64String(msstream.ToArray());
                    //byte[] encodeddata= Encoding.UTF8.GetBytes(downloadfile);
                    //ls_response.Add("encodedfile", encodeddata);
                    ls_response.Add("file", downloadfile);
                    ls_response.Add("format", format);
                    ls_response.Add("name", filename + "." + format);
                    ls_response.Add("status", true);
                }
                else
                {
                   
                    ls_response.Add("message", message);
                    ls_response.Add("status", false);
                }


            }
            catch (Exception ex)
            {
                
            }
            finally
            {

            }

            return ls_response;
        }
    }
}