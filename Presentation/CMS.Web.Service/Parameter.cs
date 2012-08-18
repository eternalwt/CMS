using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Web.Service
{
    public class Parameters
    {
        public List<ParameterGroup> Groups { get; set; }
    }


    public class ParameterGroup
    {
        public Dictionary<string, ParameterField> Fields { get; set; }
    }

    public class ParameterField
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public Dictionary<string, string> Options { get; set; }

    }

}
