﻿using System.Collections.Generic;
using System.Web.Security;
using PagedList;

namespace HTH8.Areas.MvcMembership.Models.UserAdministration
{
	public class IndexViewModel
	{
		public string Search { get; set; }
		public IPagedList<MembershipUser> Users { get; set; }
		public IEnumerable<string> Roles { get; set; }
		public bool IsRolesEnabled { get; set; }
	}
}