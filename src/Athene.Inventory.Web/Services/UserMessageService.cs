using System;
using Microsoft.AspNetCore.Mvc;
using Athene.Inventory.Web.Extensions;

namespace Athene.Inventory.Web.Services
{
    [Obsolete("Please use SetUserMessage in Athene.Inventory.Web.Extensions")]
    public static class UserMessageService
    {
        public static void SetMessageIntoMessageContainer(this Controller vc, string messageText, UserMessageType messageType)
        {
            vc.ViewBag.MessageValue = messageText;
            vc.ViewBag.MessageType = messageType;
        }

    }
}
