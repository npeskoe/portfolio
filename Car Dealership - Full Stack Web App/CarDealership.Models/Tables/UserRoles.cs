﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Models.Tables
{
    public class UserRoles
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Discriminator { get; set; }
    }
}
