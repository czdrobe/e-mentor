using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace meditatii.Models
{
    public class CityModel
    {

        public string Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string County { get; set; }
        public string Zip { get; set; }
        public int Population { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }

    }
}
