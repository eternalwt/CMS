using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using CMS.Web.FrameWork.Models;
using System.Web;
using System.IO;

namespace CMS.Web.FrameWork.Helpers
{
    public class ServerFileUpload : ConfigurationSection, IFileUpload
    {
        [ConfigurationProperty("UploadPathKeys")]
        public UploadPathKeyCollection UploadPathKeys
        {
            get { return ((UploadPathKeyCollection)(base["UploadPathKeys"])); }
        }

        public UploadFileModel UploadFile(HttpPostedFileBase uploadFile, string appSettingName, params object[] args)
        {
            string rootPath = HttpContext.Current.Server.MapPath("~/");
            string uploadFilePath = Path.Combine(rootPath, string.Format(this.UploadPathKeys[appSettingName].Value, args));
            string directoryName = Path.GetDirectoryName(uploadFilePath);
            UploadFileModel uploadFileModel = null;
            if (Directory.Exists(directoryName) == false)
            {
                Directory.CreateDirectory(directoryName);
            }
            if (File.Exists(uploadFilePath))
            {
                File.Delete(uploadFilePath);
            }
            uploadFile.SaveAs(uploadFilePath);
            FileInfo fileInfo = new FileInfo(uploadFilePath);
            uploadFileModel = new UploadFileModel
            {
                FileName = fileInfo.Name,
                FilePath = directoryName.Replace(rootPath, ""),
                Size = fileInfo.Length
            };
            return uploadFileModel;
        }

        public bool DeleteFile(string fileName)
        {
            bool result = false;
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
                result = true;
            }
            return result;
        }
    }
}
