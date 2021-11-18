using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meditatii.Data.Models
{
    public class City
    {
        public City()
        {
            Users = new HashSet<User>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string County { get; set; }
        public string Zip { get; set; }
        public int Population { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
