using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
        public ActionResult S3Signature()
        {
            FroalaEditor.S3Config config = new FroalaEditor.S3Config
            {
                // The name of your bucket.
                Bucket = "mailimageuploads",

                // S3 region. If you are using the default us-east-1, it this can be ignored.
                Region = "us-east-1",

                // The folder where to upload the images.
                KeyStart = "uploads",

                // File access.
                Acl = "AllowPublicRead",

                // AWS keys.
                AccessKey = "AKIATLKVFRS7KDZXJHRN",
                SecretKey = "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"
            };

            return Json(FroalaEditor.S3.GetHash(config), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UploadImage()
        {
            string uploadPath = "/Public/";
            HttpPostedFile MyFile = System.Web.HttpContext.Current.Request.Files["file"];
            string Extension = Path.GetExtension(MyFile.FileName);
            if(Extension.Contains(";base64"))
            {
                string json = ""; 
                Extension = Extension.Replace(";base64", "");

                //Determining file name. You can format it as you wish.
                string FileName = Path.GetFileName(MyFile.FileName);

                // Generate random name.
                FileName = Guid.NewGuid().ToString().Substring(0, 8);

                // Determining file size.
                int FileSize = MyFile.ContentLength;

                // Creating a byte array corresponding to file size.
                byte[] FileByteArray = new byte[FileSize];

                // Basic validation for file extension
                string[] AllowedExtension = { ".gif", ".jpeg", ".jpg", ".png", ".svg", ".blob" };

                // Basic validation for mime type
                string[] AllowedMimeType = { "image/gif", "image/jpeg", "image/pjpeg", "image/x-png", "image/png", "image/svg+xml", "application/octet-stream" };

                if (AllowedExtension.Contains(Extension) && AllowedMimeType.Contains(MimeMapping.GetMimeMapping(MyFile.FileName)))
                {
                    // Posted file is being pushed into byte array.
                    MyFile.InputStream.Read(FileByteArray, 0, FileSize);

                    // Uploading properly formatted file to server.
                    MyFile.SaveAs(ConfigurationManager.AppSettings["path"] + uploadPath + FileName + Extension);

                    Hashtable resp = new Hashtable();
                    string urlPath = MapURL(uploadPath) + FileName + Extension;

                    // Make the response an json object
                    resp.Add("link", urlPath.Replace("//", "/"));
                    json = JsonConvert.SerializeObject(resp);

                    // Clear and send the response back to the browser.
                    Response.Clear();
                    Response.ContentType = "application/json; charset=utf-8";
                    Response.Write(json);
                    Response.End();
                    return Json(json);
                }
                else
                {
                    return Json(json);
                    // Handle upload errors.
                }
            }
            else
            {
                try
                {
                    FroalaEditor.ImageOptions options = new FroalaEditor.ImageOptions
                    {

                        Fieldname = "file",
                        Validation = new FroalaEditor.ImageValidation(new string[] { "gif", "jpeg", "jpg", "png", "svg", "blob" }, new string[] { "image/gif", "image/jpeg", "image/pjpeg", "image/x-png", "image/png", "image/svg+xml" })
                    };

                    return Json(FroalaEditor.Image.Upload(System.Web.HttpContext.Current, uploadPath, options));
                }
                catch (Exception e)
                {
                    return Json(e);
                }
            }
            
        }
         
        private string MapURL(string path)
        {
            string appPath = Server.MapPath("/").ToLower();
            return string.Format("/{0}", path.ToLower().Replace(appPath, "").Replace(@"\", "/"));
        }
    }
}

