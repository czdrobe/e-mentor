﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meditatii.Data.Models
{
    public class Category
    {
        public Category()
        {
            
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public int ParentId { get; set; }

    }
}
