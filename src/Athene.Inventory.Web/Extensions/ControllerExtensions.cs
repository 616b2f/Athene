using Microsoft.AspNetCore.Mvc;

namespace Athene.Inventory.Web.Extensions
{
    public static class ControllerExtensions
    {
        public static void SetUserMessage(this Controller vc, UserMessageType messageType, string messageText)
        {
            // vc.ViewBag.MessageType = messageType;
            // vc.ViewBag.MessageValue = messageText; 
            vc.TempData["messageValue"] = messageText;
            vc.TempData["messageType"] = messageType;
        }
    }
}
