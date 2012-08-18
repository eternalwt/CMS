using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using CMS.Web.FrameWork.Models;
using System.Web;

namespace CMS.Web.FrameWork.Helpers
{
    public static class UploadFileHelper
    {

        private static IFileUpload _FileUpload = null;

        static UploadFileHelper()
        {
           _FileUpload = (IFileUpload)ConfigurationManager.GetSection("ServerFileUpload");
        }

        public static UploadFileModel Upload(HttpPostedFileBase uploadFile, string appSettingName, params object[] args)
        {
            return _FileUpload.UploadFile(uploadFile, appSettingName, args);
        }

        public static bool DeleteFile(string fileName)
        {
            return _FileUpload.DeleteFile(fileName);
        }
           
    }
}
