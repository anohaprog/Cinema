using Cinema.Attributes;
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
        public ActionResult GetMovieTimeslotsList(int movieId)
        {
            var model = _ticketsService.GetTimeSlotsByMovieId(movieId);
            return View("TimeslotsList", model);
        }
        public ActionResult MoviesList()
        {
            var movies = _ticketsService.GetAllMovies();
            return View("MoviesList", movies);
        }
        public ActionResult HallsList()
        {
            var halls = _ticketsService.GetAllHalls();
            return View("HallsList", halls);
        }
        public ActionResult TimeslotsList()
        {
            var timeslots = _ticketsService.GetAllTimeSlots();
            return View("TimeslotsList", timeslots);
        }

        [HttpGet]
        public ActionResult EditMovie(int movieId)
        {
            var movie = _ticketsService.GetMovieById(movieId);
            return View("EditMovie", movie);
        }

        [HttpPost]
        public ActionResult EditMovie(Movie model)
        {
            if (ModelState.IsValid)
            {
                var updateResult = _ticketsService.UpdateMovie(model);
                if (updateResult)
                {
                    return RedirectToAction("MoviesList");
                }
                return Content("Update failed.");
            }

            return View("EditMovie", model);
        }

        [HttpGet]
        public ActionResult EditHall(int hallId)
        {
            var movie = _ticketsService.GetHallById(hallId);
            return View("EditHall", movie);
        }

        [HttpPost]
        public ActionResult EditHall(Hall model)
        {
            if (ModelState.IsValid)
            {
                var updateResult = _ticketsService.UpdateHall(model);
                if (updateResult)
                {
                    return RedirectToAction("HallsList");
                }
                return Content("Update failed.");
            }

            return View("EditHall", model);
        }

        [HttpGet]
        [PopulateHallsList,PopulateMoviesList]
        public ActionResult EditTimeslot(int timeslotId)
        {
            var timeslot = _ticketsService.GetTimeSlotById(timeslotId);
            return View("EditTimeslot", timeslot);
        }

        [HttpPost]
        public ActionResult EditTimeslot(TimeSlot model)
        {
            if (ModelState.IsValid)
            {
                var updateResult = _ticketsService.UpdateTimeslot(model);
                if (updateResult)
                {
                    return RedirectToAction("TimeslotsList");
                }
                return Content("Update failed.");
            }

            return View("EditTimeslot", model);
        }
        [HttpGet]
        public ActionResult AddMovie()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddMovie(Movie newMovie)
        {
            if (ModelState.IsValid)
            {
                var result = _ticketsService.CreateMovie(newMovie);
                if (result) 
                    return RedirectToAction("MoviesList");

                return Content("Update failed.");
            }
            return View(newMovie);
        }
        [HttpGet]
        [PopulateHallsList, PopulateMoviesList]
        public ActionResult AddTimeslot()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddTimeslot(TimeSlot newTimeslot)
        {
            if (ModelState.IsValid)
            {
                var result = _ticketsService.CreateTimeslot(newTimeslot);
                if (result)
                    return RedirectToAction("TimeslotsList");

                return Content("Update failed.");
            }
            return View(newTimeslot);
        }

    }
}