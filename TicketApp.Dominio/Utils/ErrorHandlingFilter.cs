using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Web.Http;
using TicketApp.Dominio.DTO;

namespace TicketApp.Dominio.Utils
{
    public class ErrorHandlingFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var resp = ((HttpResponseException)context.Exception).Response;
            context.Result = new ObjectResult(new ResultDTO { Message = resp.ReasonPhrase, IsTrue = false });
            context.HttpContext.Response.StatusCode = (int)resp.StatusCode;
            context.ExceptionHandled = true; //optional 
            base.OnException(context);
        }
    }
}
