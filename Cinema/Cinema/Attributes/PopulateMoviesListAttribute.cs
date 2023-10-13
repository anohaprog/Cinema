using Cinema.Interfaces;
using Cinema.Services;
using LightInject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cinema.Attributes
{
    public class PopulateMoviesListAttribute : ActionFilterAttribute
    {
        [Inject]
        public ITicketsService TicketsService { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewData["MoviesList"] = TicketsService.GetAllMovies();
            base.OnActionExecuting(filterContext);
        }
    }
}