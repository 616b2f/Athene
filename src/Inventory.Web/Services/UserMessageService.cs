using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Athene.Inventory.Web.Services;
using Athene.Inventory.Web.Models;
using Athene.Inventory.Web.Areas.Librarian.Models.BooksViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Athene.Inventory.Web.Services
{
	public class UserMessageService
	{
		public void SetMessageIntoMessageContainer(Controller vc, string messageText)
		{
			vc.TempData.Add("MessageValue", messageText);
		}
	}
}
