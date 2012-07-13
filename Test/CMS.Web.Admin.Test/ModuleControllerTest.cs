using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CMS.Web.Admin.Controllers;
using System.Web.Mvc;

namespace CMS.Web.Admin.Test {

	[TestClass]
	public class ModuleControllerTest {


		[TestMethod]
		public void Index() {


			// Arrange
			using (ModuleController controller = new ModuleController()) {

				FormCollection collection = new FormCollection();

				collection.Add("ModuleID", "0");

				// Act
				JsonResult result = controller.Create(collection) as JsonResult;

				// Assert
				Assert.AreEqual("Welcome to ASP.NET MVC!", result);
			}
		}
	}
}
