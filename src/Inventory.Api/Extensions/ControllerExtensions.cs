using Microsoft.AspNetCore.Http;
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

        public static IActionResult NotFoundProblemDetails(this ControllerBase cbase, string title, string details)
        {
            return cbase.NotFound(new ProblemDetails{
                Type = "about:blank",
                Status = StatusCodes.Status404NotFound,
                Title = title,
                Detail = details
            });
        }

        public static IActionResult BadRequestProblemDetails(this ControllerBase cbase, string title, string details)
        {
            return cbase.NotFound(new ProblemDetails{
                Type = "about:blank",
                Status = StatusCodes.Status400BadRequest,
                Title = title,
                Detail = details
            });
        }
    }
}
