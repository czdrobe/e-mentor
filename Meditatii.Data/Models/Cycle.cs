using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meditatii.Data.Models
{
    public class Cycle
    {
        public Cycle()
        {
            Ads = new HashSet<Ad>();
            
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Ad> Ads { get; set; }
    }
}
