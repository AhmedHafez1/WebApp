﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Supplier
    {
        public long SupplierId { get; set; }
        public string? Name { get; set; }
        public string? City { get; set; }
        public IEnumerable<Product>? Products { get; set; }
    }
}
