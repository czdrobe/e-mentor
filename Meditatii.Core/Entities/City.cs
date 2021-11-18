using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meditatii.Core.Entities
{
    /// Represents a City
    /// </summary>
    [Serializable]
    public class City : BaseEntity
    {
        public City()
        {
        }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string County { get; set; }
        public string Zip { get; set; }
        public int Population { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }

    }
}
