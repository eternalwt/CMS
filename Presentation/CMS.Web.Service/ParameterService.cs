using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace CMS.Web.Service
{
    public class ParameterService
    {
        private string _xml;

        public ParameterService(string xml)
        {
            _xml = xml;
        }

        public List<ParameterGroup> GetParameters()
        {
            List<ParameterGroup> parameterGroups = new List<ParameterGroup>();
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(_xml);
            XmlNodeList groups = xmlDoc.SelectNodes(@"//group");
            foreach (XmlNode group in groups)
            {
                if (group.Attributes != null && group.Attributes["name"] != null)
                {

                }
            }
            return parameterGroups;
        }
    }
}
