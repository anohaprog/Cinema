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