﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rentals.Models.InputModel
{
    public class FavouritesRequest
    {
        public string OauthId { get; set; }
        public int? Category { get; set; }
    }
}
