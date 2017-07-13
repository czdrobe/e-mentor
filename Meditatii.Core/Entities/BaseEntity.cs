using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meditatii.Core.Entities
{
    public abstract class BaseEntity
    {
        /// <summary>
        /// Id of the entity - always returned from any entity source
        /// </summary>
        public int Id { get; set; }
    }
}
