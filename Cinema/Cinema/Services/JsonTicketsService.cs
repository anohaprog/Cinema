﻿using Cinema.Interfaces;
using Cinema.Models;
using Cinema.Models.Domain;
using Cinema.Models.Tickets;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Cinema.Services
{
    public class JsonTicketsService : ITicketsService
    {
        private const string PathToJson = "/Files/Data.json";
        private HttpContext Context { get; set; }

        public JsonTicketsService()
        {
            Context = HttpContext.Current;
        }

        public Movie GetMovieById(int Id)
        {
            var fullModel = GetDataFromFile();
            return fullModel.Movies.FirstOrDefault(x => x.Id == Id);
        }

        public Movie[] GetAllMovies()
        {
            var fullModel = GetDataFromFile();
            return fullModel.Movies;
        }

        public Hall GetHallById(int Id)
        {
            var fullModel = GetDataFromFile();
            return fullModel.Halls.FirstOrDefault(x => x.Id == Id);
        }

        public Hall[] GetAllHalls()
        {
            var fullModel = GetDataFromFile();
            return fullModel.Halls;
        }
        public TimeSlot GetTimeSlotById(int Id)
        {
            var fullModel = GetDataFromFile();
            return fullModel.TimeSlots.FirstOrDefault(x => x.Id == Id);
        }
        public TimeSlot[] GetAllTimeSlots()
        {
            var fullModel = GetDataFromFile();
            return fullModel.TimeSlots;
        }

        public bool UpdateMovie(Movie updatedMovie)
        {
            var fullModel = GetDataFromFile();
            var movieToUpdate = fullModel.Movies.FirstOrDefault(x => x.Id == updatedMovie.Id);

            if (movieToUpdate == null)
                return false;

            movieToUpdate.Title = updatedMovie.Title;
            movieToUpdate.Director = updatedMovie.Director;
            movieToUpdate.Duration = updatedMovie.Duration;
            movieToUpdate.MinAge = updatedMovie.MinAge;
            movieToUpdate.Rating = updatedMovie.Rating;
            movieToUpdate.ImgUrl = updatedMovie.ImgUrl;
            movieToUpdate.ReleaseDate = updatedMovie.ReleaseDate;
            if (updatedMovie.Genres != null)
            {
                movieToUpdate.Genres = updatedMovie.Genres;
            }

            SaveToFile(fullModel);
            return true;
        }
        public bool UpdateHall(Hall updatedHall)
        {
            var fullModel = GetDataFromFile();
            var hallToUpdate = fullModel.Halls.FirstOrDefault(x => x.Id == updatedHall.Id);

            if (hallToUpdate == null)
                return false;

            hallToUpdate.Name = updatedHall.Name;
            hallToUpdate.Places = updatedHall.Places;

            SaveToFile(fullModel);
            return true;
        }

        public bool UpdateTimeslot(TimeSlot updatedTimeslot)
        {
            var fullModel = GetDataFromFile();
            var timeslotToUpdate = fullModel.TimeSlots.FirstOrDefault(x => x.Id == updatedTimeslot.Id);

            if (timeslotToUpdate == null)
                return false;

            timeslotToUpdate.Format = updatedTimeslot.Format;
            timeslotToUpdate.StartTime = updatedTimeslot.StartTime;
            timeslotToUpdate.Cost = updatedTimeslot.Cost;
            timeslotToUpdate.MovieId = updatedTimeslot.MovieId;
            timeslotToUpdate.HallId = updatedTimeslot.HallId;

            SaveToFile(fullModel);
            return true;
        }
        public TimeSlot[] GetTimeSlotsByMovieId(int movieId)
        {
            var fullModel = GetDataFromFile();
            return fullModel.TimeSlots.Where(x => x.MovieId == movieId).ToArray();
        }
        private void SaveToFile(FileModel model)
        {
            var jsonFilePath = Context.Server.MapPath(PathToJson);
            var serializedModel = JsonConvert.SerializeObject(model);
            System.IO.File.WriteAllText(jsonFilePath, serializedModel);
        }

        private FileModel GetDataFromFile()
        {
            var jsonFilePath = Context.Server.MapPath(PathToJson);
            if (!System.IO.File.Exists(jsonFilePath))
                return null;

            var json = System.IO.File.ReadAllText(jsonFilePath);
            var fileModel = JsonConvert.DeserializeObject<FileModel>(json);
            return fileModel;
        }

        public bool CreateMovie(Movie newMovie)
        {
            var fullModel = GetDataFromFile();
            try
            {
                var newMovieId = fullModel.Movies.Max(m => m.Id) + 1;
                newMovie.Id = newMovieId;
                var existingMoviesList = fullModel.Movies.ToList();
                existingMoviesList.Add(newMovie);
                fullModel.Movies = existingMoviesList.ToArray();
                SaveToFile(fullModel);
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        public bool CreateTimeslot(TimeSlot newTimeslot)
        {
            var fullModel = GetDataFromFile();
            try
            {
                var newTimeslotId = fullModel.TimeSlots.Max(m => m.Id) + 1;
                newTimeslot.Id = newTimeslotId;
                var existingTimeslotList = fullModel.TimeSlots.ToList();
                existingTimeslotList.Add(newTimeslot);
                fullModel.TimeSlots = existingTimeslotList.ToArray();
                SaveToFile(fullModel);
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        public bool RemoveMovie(int movieId)
        {
            var fullModel = GetDataFromFile();
            var moviesList = fullModel.Movies.ToList();
            var movieToDelete = moviesList.FirstOrDefault(x => x.Id == movieId);
            if (movieToDelete == null)
                return false;

            var timeslotsToDelete = fullModel.TimeSlots.Where(x => x.MovieId == movieId).ToList();
            if (timeslotsToDelete.Any())
            {
                foreach (var timeslot in timeslotsToDelete)
                {
                    RemoveTimeslot(timeslot.Id);
                }
            }
            moviesList.Remove(movieToDelete);
            fullModel.Movies = moviesList.ToArray();
            SaveToFile(fullModel);

            return true;
        }
        public bool RemoveTimeslot(int timeslotId)
        {
            var fullModel = GetDataFromFile();
            try
            {
                var timeslotsList = fullModel.TimeSlots.ToList();
                var timeslotToDelete = timeslotsList.FirstOrDefault(x => x.Id == timeslotId);
                timeslotsList.Remove(timeslotToDelete);
                fullModel.TimeSlots = timeslotsList.ToArray();
                SaveToFile(fullModel);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public bool RemoveHall(int hallId)
        {
            var fullModel = GetDataFromFile();
            try
            {
                var hallsList = fullModel.Halls.ToList();
                var hallToDelete = hallsList.FirstOrDefault(x => x.Id == hallId);
                hallsList.Remove(hallToDelete);
                fullModel.Halls = hallsList.ToArray();
                SaveToFile(fullModel);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public MovieListItem[] GetFullMoviesInfo()
        {
            var allMovies = GetAllMovies();
            var resultModel = new List<MovieListItem>();
            foreach (var movie in allMovies)
            {
                resultModel.Add(new MovieListItem
                {
                    Movie = movie,
                    AvailableTimeslots = GetTimeslotTagsByMovieId(movie.Id)
                });
            }
            return resultModel.ToArray();
        }

        public TimeslotTag[] GetTimeslotTagsByMovieId(int movieId)
        {
            var allTimeslots = GetTimeSlotsByMovieId(movieId);
            var resultModel = new List<TimeslotTag>();
            foreach (var timeslot in allTimeslots)
            {
                resultModel.Add(new TimeslotTag
                {
                    TimeslotId = timeslot.Id,
                    StartTime = timeslot.StartTime,
                    Cost = timeslot.Cost
                });
            }

            return resultModel.ToArray();
        }

    }
}