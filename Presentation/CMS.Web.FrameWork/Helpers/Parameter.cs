using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Runtime.Serialization;

namespace CMS.Web.FrameWork.Helpers
{
    [DataContract]
    public class Parameter
    {
        public Parameter()
        {
            this.Groups = new List<ParameterGroup>();
        }

        [DataMember]
        public List<ParameterGroup> Groups { get; set; }
    }

    [DataContract]
    public class ParameterGroup
    {
        public ParameterGroup()
        {
            this.Fields = new Dictionary<string, ParameterField>();
        }

        [DataMember]
        public string GroupName { get; set; }

        [DataMember]
        public string Label { get; set; }

        [DataMember]
        public Dictionary<string, ParameterField> Fields { get; set; }
    }

    [DataContract]
    public class ParameterField
    {
        public ParameterField()
        {
            this.Options = new List<SelectListItem>();
        }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Value { get; set; }

        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public string Default { get; set; }

        [DataMember]
        public string Label { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public bool IsMultiple { get; set; }

        [DataMember]
        public int Rows { get; set; }

        [DataMember]
        public int Cols { get; set; }

        [DataMember]
        public List<SelectListItem> Options { get; set; }

        [DataMember]
        public List<ParameterFiles> Files { get; set; }

    }

    [DataContract]
    public class ParameterFiles
    {
        [DataMember]
        public string FilePath { get; set; }

        [DataMember]
        public string FileName { get; set; }

        [DataMember]
        public long Size { get; set; }
    }



}
