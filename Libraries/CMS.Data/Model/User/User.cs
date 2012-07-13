using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace CMS.Data {
	public partial class User : BaseEntity {

		public virtual int UserID { get; set; }

		[Required(ErrorMessage = "Role is required")]
		public virtual int RoleID { get; set; }

		[Required(ErrorMessage = "Username is required")]
		[StringLength(256, ErrorMessage = "UserName must be under 256 characters")]
		public virtual string UserName { get; set; }

		[Required(ErrorMessage = "Password is required")]
		[StringLength(128, ErrorMessage = "Password must be under 128 characters")]
		public virtual string Password { get; set; }

		[Required(ErrorMessage = "Password Salt is required")]
		[StringLength(128, ErrorMessage = "Password Salt must be under 128 characters")]
		public virtual string PasswordSalt { get; set; }

		[Required(ErrorMessage = "Email is required")]
		[StringLength(256, ErrorMessage = "Email must be under 256 characters")]
		public virtual string Email { get; set; }

		[Required(ErrorMessage = "FirstName is required")]
		[StringLength(50, ErrorMessage = "FirstName must be under 50 characters")]
		public virtual string FirstName { get; set; }

		[StringLength(50, ErrorMessage = "MiddleName must be under 50 characters")]
		public virtual string MiddleName { get; set; }

		[Required(ErrorMessage = "LastName is required")]
		[StringLength(50, ErrorMessage = "LastName must be under 50 characters")]
		public virtual string LastName { get; set; }

		[StringLength(50, ErrorMessage = "LastName must be under 50 characters")]
		public virtual string Phone { get; set; }
		
		public virtual string Parameters { get; set; }

		public Role Role { get; set; }

	}
}
