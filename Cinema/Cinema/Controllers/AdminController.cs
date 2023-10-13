using Cinema.Attributes;
using Cinema.Interfaces;
using Cinema.Models;
using Cinema.Models.Domain;
using Cinema.Services;
using LightInject;
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
        [Inject]
        public ITicketsService TicketsService { get; set; }

        public ActionResult FindMovieById(int id)
        {
            var movie = TicketsService.GetMovieById(id);
            if (movie == null)
                return Content("Movie with such ID does not exists", "application/json");

            var movieJson = JsonConvert.SerializeObject(movie);
            return Content(movieJson, "application/json");
        }
        public ActionResult FindHallById(int id)
        {
            var hall = TicketsService.GetHallById(id);
            if (hall == null)
                return Content("Hall with such ID does not exists", "application/json");

            var hallJson = JsonConvert.SerializeObject(hall);
            return Content(hallJson, "application/json");
        }

        public ActionResult FindTimeSlotById(int id)
        {
            var timeSlot = TicketsService.GetTimeSlotById(id);
            if (timeSlot == null)
                return Content("TimeSlot with such ID does not exists", "application/json");

            var timeSlotJson = JsonConvert.SerializeObject(timeSlot);
            return Content(timeSlotJson, "application/json");
        }
        public ActionResult GetMovieTimeslotsList(int movieId)
        {
            return View("TimeslotsList", ProccessTimeslots(TicketsService.GetTimeSlotsByMovieId(movieId)));
        }
        public ActionResult MoviesList()
        {
            var movies = TicketsService.GetAllMovies();
            return View("MoviesList", movies);
        }
        public ActionResult HallsList()
        {
            var halls = TicketsService.GetAllHalls();
            return View("HallsList", halls);
        }
        [HttpGet]
        public ActionResult TimeslotsList()
        {
            return View("TimeslotsList", ProccessTimeslots(TicketsService.GetAllTimeSlots()));
        }
        private TimeslotGridRow[] ProccessTimeslots(TimeSlot[] timeslots)
        {
            var movies = TicketsService.GetAllMovies();
            var halls = TicketsService.GetAllHalls();

            return timeslots.Select(timeslot => new TimeslotGridRow()
            {
                StartTime = timeslot.StartTime,
                Cost = timeslot.Cost,
                Format = timeslot.Format,
                Id = timeslot.Id,
                Hall = halls.First(x=>x.Id == timeslot.HallId),
                Movie = movies.First(x => x.Id == timeslot.MovieId)
            }).ToArray();
        }

        [HttpGet]
        public ActionResult EditMovie(int movieId)
        {
            var movie = TicketsService.GetMovieById(movieId);
            return View("EditMovie", movie);
        }

        [HttpPost]
        public ActionResult EditMovie(Movie model)
        {
            if (ModelState.IsValid)
            {
                var updateResult = TicketsService.UpdateMovie(model);
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
            var movie = TicketsService.GetHallById(hallId);
            return View("EditHall", movie);
        }

        [HttpPost]
        public ActionResult EditHall(Hall model)
        {
            if (ModelState.IsValid)
            {
                var updateResult = TicketsService.UpdateHall(model);
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
            var timeslot = TicketsService.GetTimeSlotById(timeslotId);
            return View("EditTimeslot", timeslot);
        }

        [HttpPost]
        public ActionResult EditTimeslot(TimeSlot model)
        {
            if (ModelState.IsValid)
            {
                var updateResult = TicketsService.UpdateTimeslot(model);
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
                var result = TicketsService.CreateMovie(newMovie);
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
                var result = TicketsService.CreateTimeslot(newTimeslot);
                if (result)
                    return RedirectToAction("TimeslotsList");

                return Content("Update failed.");
            }
            return View(newTimeslot);
        }
        [HttpGet]
        public ActionResult RemoveMovie()
        {
            return RedirectToAction("MoviesList");
        }
        [HttpGet]
        public ActionResult RemoveTimeslot(int timeslotId)
        {
            if (TicketsService.RemoveTimeslot(timeslotId))
                return RedirectToAction("TimeslotsList");

            return Content("Delete failed.");
        }
        [HttpGet]
        public ActionResult RemoveHall(int hallId)
        {
            if (TicketsService.RemoveHall(hallId))
                return RedirectToAction("HallsList");

            return Content("Delete failed.");
        }
    }
}