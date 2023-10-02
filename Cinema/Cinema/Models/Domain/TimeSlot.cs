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
        public int MovieId { get; set; }
        public int HallId { get; set; } 
        public Format Format { get; set; }
    }
}