﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rentals.Models.InputModel
{
    public class ItemToUserRequest
    {
        public string OauthId { get; set; }
        public int Item { get; set; }
    }
}
