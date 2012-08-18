using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using System.IO;
using System.Runtime.Serialization.Json;

namespace CMS.Web.FrameWork.Helpers
{
    public class ParameterHelper
    {

        public static Parameter GetParameters(string xml)
        {
            Parameter parameter = new Parameter();
            parameter.Groups = new List<ParameterGroup>();
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            XmlNodeList groups = xmlDoc.SelectNodes(@"//group");
            XmlNodeList fields = null;
            XmlNodeList options = null;
            ParameterGroup parameterGroup = null;
            ParameterField parameterField = null;
            string groupName = string.Empty;
            int index = 0;
            string type = string.Empty;
            string name = string.Empty;
            string defaultValue = string.Empty;
            string value = string.Empty;
            string label = string.Empty;
            string description = string.Empty;
            bool ismultiple = false;
            int rows = 0;
            int cols = 0;
            foreach (XmlNode group in groups)
            {
                index++;
                if (group.Attributes != null)
                {
                    groupName = (group.Attributes["name"] != null ? group.Attributes["name"].Value : "Group_" + index.ToString());
                    if (ParameterHelper.IsStringAlphaNumeric(groupName) == false)
                        continue;
                    label = (group.Attributes["label"] != null ? group.Attributes["label"].Value : string.Empty);
                    parameterGroup = new ParameterGroup { GroupName = groupName, Label = label };
                    fields = group.SelectNodes(@"//field");
                    foreach (XmlNode field in fields)
                    {
                        if (field.Attributes != null)
                        {
                            name = (field.Attributes["name"] != null ? field.Attributes["name"].Value : string.Empty);
                            if (ParameterHelper.IsStringAlphaNumeric(name) == false)
                                continue;
                            type = (field.Attributes["type"] != null ? field.Attributes["type"].Value : string.Empty);
                            defaultValue = (field.Attributes["default"] != null ? field.Attributes["default"].Value : string.Empty);
                            label = (field.Attributes["label"] != null ? field.Attributes["label"].Value : string.Empty);
                            description = (field.Attributes["description"] != null ? field.Attributes["description"].Value : string.Empty);
                            ismultiple = (field.Attributes["multiple"] != null ? (field.Attributes["multiple"].Value == "true") : false);
                            int.TryParse((field.Attributes["rows"] != null ? field.Attributes["rows"].Value : "0"), out rows);
                            int.TryParse((field.Attributes["cols"] != null ? field.Attributes["cols"].Value : "0"), out cols);
                            if (string.IsNullOrEmpty(type) == false && string.IsNullOrEmpty(name) == false)
                            {
                                parameterField = null;
                                if (parameterGroup.Fields.ContainsKey(name) == false)
                                {
                                    parameterField = new ParameterField
                                    {
                                        Name = name,
                                        Type = type,
                                        Default = defaultValue,
                                        Value = defaultValue,
                                        Description = description,
                                        Label = label,
                                        Rows = rows,
                                        Cols = cols,
                                        IsMultiple = ismultiple
                                    };
                                    if (parameterField != null)
                                        parameterGroup.Fields.Add(name, parameterField);

                                    options = field.ChildNodes;
                                    name = string.Empty;
                                    value = string.Empty;
                                    foreach (XmlNode option in options)
                                    {
                                        if (option.Name != "option")
                                            continue;
                                        value = (option.Attributes["value"] != null ? option.Attributes["value"].Value : string.Empty);
                                        name = option.InnerText;
                                        if (string.IsNullOrEmpty(name) == false)
                                        {
                                            parameterField.Options.Add(new SelectListItem { Text = name, Value = value });
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (parameter.Groups.Where(p => p.GroupName == parameterGroup.GroupName).Count() == 0)
                        parameter.Groups.Add(parameterGroup);
                }
            }
            return parameter;
        }

        public static Parameter CheckParamters(string xml, string parameters)
        {
            MemoryStream stream1 = new MemoryStream(Encoding.UTF8.GetBytes(parameters));
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Parameter));
            stream1.Position = 0;
            Parameter checkParameter = (Parameter)ser.ReadObject(stream1);
            Parameter parameter = GetParameters(xml);
            foreach (var g in parameter.Groups)
            {
                var findGroup = (from checkGroup in checkParameter.Groups
                                  where checkGroup.GroupName == g.GroupName
                                  select checkGroup).FirstOrDefault();
                if (findGroup != null)
                {
                    foreach (var f in g.Fields)
                    {
                        f.Value.Value = findGroup.Fields.Where(checkField => checkField.Key == f.Key).Select(checkField => checkField.Value.Value).FirstOrDefault();
                    }
                }
            }

            return parameter;
        }


        public static Parameter FillParameterValues(string xml, FormCollection collection)
        {
            Parameter parameter = GetParameters(xml);
            string fieldName = string.Empty;
            foreach (var g in parameter.Groups)
            {
                foreach (var f in g.Fields)
                {
                    fieldName = g.GroupName + "_" + f.Value.Name;
                    if (collection[fieldName] != null)
                    {
                        f.Value.Value = Convert.ToString(collection[fieldName]);
                    }
                }
            }
            return parameter;
        }

        private static bool IsStringAlphaNumeric(string value)
        {
            Regex r = new Regex(@"^[a-zA-Z0-9]*$");
            return r.IsMatch(value);
        }
    }
}
