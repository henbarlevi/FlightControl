﻿using FlightControl.Contract.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightControl.BL.EventArguments
{
    //argument
    public class AirportEventArgs : EventArgs
    {
        public AirportDTO ChangedAirport { get; set; }
    }
}
