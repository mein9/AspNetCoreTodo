using AspNetCoreTodo.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace AspNetCoreTodo.Models
{
	public class ManageUsersViewModel
	{
		public IdentityUser[] Administrators { get; set; }
		public IdentityUser[] Everyone { get; set; }
	}
}