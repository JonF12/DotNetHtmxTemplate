using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


namespace DotNetHtmxTypescriptTemplate.Utils
{
    public class HtmxRequestAttribute : ActionFilterAttribute
    {
        private readonly string viewName;

        public HtmxRequestAttribute(string viewName)
        {
            this.viewName = viewName;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var request = context.HttpContext.Request;

            // Check for the HTMX-specific header to determine if it's an initial load or a subsequent load
            // this is so we can avoid rendering the base layout.
            // is there a better way to do this? Probably.
            //
            // if you need to pass in a model you can move this logic out into the controller instead of attribute
            // repetition is fine who cares
            if (request.Headers.ContainsKey("HX-Request"))
            {
                context.Result = new PartialViewResult
                {
                    ViewName = viewName
                };
            }
            else
            {
                context.Result = new ViewResult
                {
                    ViewName = viewName
                };
            }

            base.OnActionExecuting(context);
        }
    }
}
