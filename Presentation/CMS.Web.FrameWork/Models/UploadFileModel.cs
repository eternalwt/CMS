using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Web.FrameWork.Models
{
    public class UploadFileModel
    {
        public string FilePath { get; set; }

        public string FileName { get; set; }

        public long Size { get; set; }
    }
}
