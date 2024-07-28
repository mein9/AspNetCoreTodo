using AspNetCoreTodo.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace AspNetCoreTodo.Models
{
	public class ManageUsersViewModel
	{
		public required IdentityUser[] Administrators { get; set; }
		public required IdentityUser[] Everyone { get; set; }
	}
}