﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rentals_API_NET6.Models.DatabaseModel
{
    public enum ItemState
    {
        Rented,
        Available,
        Unavailable,
    }
    public enum RentingState
    {
        InProgress,
        Ended,
        Cancelled,
        WillStart
    }
}