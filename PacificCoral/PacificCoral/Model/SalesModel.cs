﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacificCoral.Model
{
    public class SalesModel
    {
        public string Code { get; set; }
        public string Shrimp { get; set; }
        public string Sep1 { get; set; }
        public string Sep2 { get; set; }
        public string GL { get; set; }

        public string Title { get; set; }
        public string IndicatorUrl { get; set; }
        public bool Prospect { get; set; }
        public bool Deviated { get; set; }
        public bool Sub { get; set; }
    }
}
