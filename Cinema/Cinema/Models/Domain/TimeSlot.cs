using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.Models.Domain
{
    public class TimeSlot
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public decimal Cost { get; set; }
        public Movie Movie { get; set; }
        public Hall Hall { get; set; } 
        public Format Format { get; set; }
    }
}