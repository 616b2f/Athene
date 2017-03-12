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
using System.ComponentModel.DataAnnotations;

namespace Athene.Inventory.Web.Services
{
	public static class UserMessageService
	{
		public static void SetMessageIntoMessageContainer(this Controller vc, string messageText, UserMessageType messageType)
		{
			vc.ViewBag.MessageValue = messageText;
			vc.ViewBag.MessageType = messageType;
		}
	}

	public enum UserMessageType
	{
		[Display(Name = "Erfolgreich")]
		Success,
		[Display(Name = "Warnung")]
		Warning,
		[Display(Name = "Fehler")]
		Error,
		[Display(Name = "Infor")]
		Info
	}
}
