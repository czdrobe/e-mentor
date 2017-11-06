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
    public class User : BaseEntity
    {
        public User()
        {
        }


        /// <summary>
        /// 
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Sex { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime Dob { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string County { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string City { get; set; }
        public string Description { get; set; }

        public ICollection<Category> Categories { get; set; }

    }
}
