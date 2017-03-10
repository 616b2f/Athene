using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace Athene.Inventory.Web.Services
{
	public class UserMessageService
	{
		public void SetMessageIntoMessageContainer(HttpContext httpContext, string messageText)
		{
			httpContext.Session.SetString("MessageValue", messageText);
		}
	}
}
