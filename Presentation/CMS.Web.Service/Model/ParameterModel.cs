using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMS.Web.Service.Model
{
    public class Parameters
    {

        public List<ParameterGroup> Groups { get; set; }
    }


    public class ParameterGroup
    {
        public string Name { get; set; }

        public List<ParameterField> Fields { get; set; }
    }

    public class ParameterField
    {
        public string Name { get; set; }

    }
}
