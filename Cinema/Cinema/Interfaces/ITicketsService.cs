using Cinema.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Interfaces
{
    internal interface ITicketsService
    {
        Movie GetMovieById(int Id);
        Movie[] GetAllMovies();

        Hall GetHallById(int Id);
        Hall[] GetAllHalls();

        TimeSlot GetTimeSlotById(int Id);
        TimeSlot[] GetAllTimeSlots();

        bool UpdateMovie(Movie updatedMovie);
    }
}
