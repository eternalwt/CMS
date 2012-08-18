using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.Web.FrameWork.Models;
using System.Web;

namespace CMS.Web.FrameWork.Helpers
{
    interface IFileUpload
    {
        UploadFileModel UploadFile(HttpPostedFileBase uploadFile, string appSettingName, params object[] args);
        bool DeleteFile(string fileName);
    }
}
