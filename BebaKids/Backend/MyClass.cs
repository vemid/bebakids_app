﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BebaKids.Backend
{
    public class MyClass
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public bool AllowedSMS { get; set; }
        public string TimeSpan { get; set; }
        public decimal Price { get; set; }
        public string TypeName { get; set; }
    }
}
