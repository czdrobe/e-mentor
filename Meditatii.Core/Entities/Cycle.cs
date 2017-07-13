using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meditatii.Core.Entities
{
    /// Represents a User
    /// </summary>
    [Serializable]
    public class Cycle : BaseEntity
    {
        public Cycle()
        {
        }


        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

    }
}
