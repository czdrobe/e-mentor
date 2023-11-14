using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace meditatii.Models
{
    public class AdModel
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public int Duration { get; set; }

        public DateTime Added { get; set; }

        public string DurationName{ get; set; }

        public int Price { get; set; }

        public virtual ICollection<CycleModel> Cycles { get; set; }

        public string Description { get; set; }

        public virtual CategoryModel Category { get; set; }

        public virtual UserModel Teacher { get; set; }

        public int TeacherId { get; set; }

    }
}
