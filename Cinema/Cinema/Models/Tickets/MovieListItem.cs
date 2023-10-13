﻿using Cinema.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.Models.Tickets
{
    public class MovieListItem
    {
        public Movie Movie { get; set; }
        public TimeslotTag[] AvailableTimeslots { get; set; }
    }
}