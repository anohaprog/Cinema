using Cinema.Models.Domain;
using Cinema.Models.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Interfaces
{
    public interface ITicketsService
    {
        Movie GetMovieById(int Id);
        Movie[] GetAllMovies();
        MovieListItem[] GetFullMoviesInfo();

        Hall GetHallById(int Id);
        Hall[] GetAllHalls();

        TimeSlot GetTimeSlotById(int Id);
        TimeSlot[] GetAllTimeSlots();
        TimeSlot[] GetTimeSlotsByMovieId(int movieId);
        TimeslotTag[] GetTimeslotTagsByMovieId(int movieId);

        bool UpdateMovie(Movie updatedMovie);
        bool UpdateHall(Hall hall);
        bool UpdateTimeslot(TimeSlot timeSlot);

        bool CreateMovie(Movie newMovie);
        bool CreateTimeslot(TimeSlot newTimeslot);
        bool RemoveMovie(int movieId);
        bool RemoveTimeslot(int timeslotId);
        bool RemoveHall(int hallId);
    }
}
