using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CMS.Web.FrameWork.Helpers {
	public class ValidationHelper {
		/// <summary>
		/// Get any errors associated with the model also investigating any rules dictated by attached Metadata buddy classes.
		/// </summary>
		/// <param name="instance"></param>
		/// <returns></returns>
		public static IEnumerable<ErrorInfo> Validate(object instance) {
			var metadataAttrib = instance.GetType().GetCustomAttributes(typeof(MetadataTypeAttribute), true).OfType<MetadataTypeAttribute>().FirstOrDefault();
			var buddyClassOrModelClass = metadataAttrib != null ? metadataAttrib.MetadataClassType : instance.GetType();
			var buddyClassProperties = TypeDescriptor.GetProperties(buddyClassOrModelClass).Cast<PropertyDescriptor>();
			var modelClassProperties = TypeDescriptor.GetProperties(instance.GetType()).Cast<PropertyDescriptor>();

			List<ErrorInfo> errors = (from buddyProp in buddyClassProperties
									  join modelProp in modelClassProperties on buddyProp.Name equals modelProp.Name
									  from attribute in buddyProp.Attributes.OfType<ValidationAttribute>()
									  where !attribute.IsValid(modelProp.GetValue(instance))
									  select new ErrorInfo(buddyProp.Name, attribute.FormatErrorMessage(attribute.ErrorMessage))).ToList();
			// Add in the class level custom attributes
			IEnumerable<ErrorInfo> classErrors = from attribute in TypeDescriptor.GetAttributes(buddyClassOrModelClass).OfType<ValidationAttribute>()
												 where !attribute.IsValid(instance)
												 select new ErrorInfo("ClassLevelCustom", attribute.FormatErrorMessage(attribute.ErrorMessage));

			errors.AddRange(classErrors);
			return errors.AsEnumerable();
		}

		public static IEnumerable<ErrorInfo> GetErrorInfo(ModelStateDictionary modelStateDictionary) {
			List<ErrorInfo> errors = new List<ErrorInfo>();
			if (modelStateDictionary != null) {
				foreach (string key in modelStateDictionary.Keys) {
					ModelState ms = modelStateDictionary[key];
					if (ms.Errors.Count > 0) {
						foreach (ModelError error in ms.Errors) {
							errors.Add(new ErrorInfo(key, error.ErrorMessage));
						}
					}
				}
			}
			return errors;
		}
	}
}
