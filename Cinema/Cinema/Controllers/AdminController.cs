using Cinema.Interfaces;
using Cinema.Models;
using Cinema.Models.Domain;
using Cinema.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cinema.Controllers
{
    public class AdminController : Controller
    {
        private readonly ITicketsService _ticketsService;
        public AdminController()
        {
            _ticketsService = new JsonTicketsService(System.Web.HttpContext.Current);
        }
        public ActionResult FindMovieById(int id)
        {
            var movie = _ticketsService.GetMovieById(id);
            if (movie == null)
                return Content("Movie with such ID does not exists", "application/json");

            var movieJson = JsonConvert.SerializeObject(movie);
            return Content(movieJson, "application/json");
        }
        public ActionResult FindHallById(int id)
        {
            var hall = _ticketsService.GetHallById(id);
            if (hall == null)
                return Content("Hall with such ID does not exists", "application/json");

            var hallJson = JsonConvert.SerializeObject(hall);
            return Content(hallJson, "application/json");
        }

        public ActionResult FindTimeSlotById(int id)
        {
            var timeSlot = _ticketsService.GetTimeSlotById(id);
            if (timeSlot == null)
                return Content("TimeSlot with such ID does not exists", "application/json");

            var timeSlotJson = JsonConvert.SerializeObject(timeSlot);
            return Content(timeSlotJson, "application/json");
        }
    }
}