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
    public class CategoryGroup : BaseEntity
    {
        public CategoryGroup()
        {
        }


        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ParentId { get; set; }

        public ICollection<Category> Categories { get; set; }

    }
}
