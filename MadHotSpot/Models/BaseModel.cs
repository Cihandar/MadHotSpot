﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MadHotSpot.Models
{
    public class BaseModel
    {
        public Guid Id { get; set; }
        public Guid FirmaId { get; set; }
        public int FirmaKodu { get; set; }
        public bool Silindi { get; set; }
        
    }
}
