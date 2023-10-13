using Cinema.Interfaces;
using Cinema.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cinema.Controllers
{
    public class TicketsController: Controller
    {
        private readonly ITicketsService _ticketsService;
        public TicketsController(ITicketsService ticketsService)
        {
            _ticketsService = ticketsService;   
        }
        public ActionResult GetMovies()
        {
            var allMovies = _ticketsService.GetFullMoviesInfo();
            return View("~/Views/Tickets/MoviesList.cshtml", allMovies);
        }

        public ActionResult GetHallInfo(int timeslotId)
        {
            return View("~/Views/Tickets/HallInfo.cshtml");
        }
    }
    
}