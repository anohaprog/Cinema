using Cinema.Interfaces;
using Cinema.Models;
using Cinema.Models.Domain;
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

        public JsonTicketsService(HttpContext context)
        {
            Context = context;
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
    }
}