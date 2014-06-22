using System;
using System.Web;
using System.Web.Mvc;

namespace Orcamento.Robo.Web
{
    public static class HtmlHelperExtensions
    {
        public static HtmlString RenderMessages(this HtmlHelper htmlHelper)
        {
            var messages = String.Empty;
            foreach (var messageType in Enum.GetNames(typeof(MessageTypeEnum)))
            {
                var messageTypeKey = "";
                switch (messageType)
                {
                    case "success":
                        messageTypeKey = "alert-success";
                        break;
                    default:
                        messageTypeKey = "alert-danger";
                        break;
                }
                var message = htmlHelper.ViewContext.ViewData.ContainsKey(messageTypeKey)
                                ? htmlHelper.ViewContext.ViewData[messageTypeKey]
                                : htmlHelper.ViewContext.TempData.ContainsKey(messageTypeKey)
                                    ? htmlHelper.ViewContext.TempData[messageTypeKey]
                                    : null;
                if (message != null)
                {
                    var messageBoxBuilder = new TagBuilder("div");
                    messageBoxBuilder.AddCssClass(String.Format("messagebox {0}", messageTypeKey.ToLowerInvariant()));
                    messageBoxBuilder.SetInnerText(message.ToString());
                    messages += messageBoxBuilder.ToString();
                }
            }
            return MvcHtmlString.Create(messages);
        }
    }
}