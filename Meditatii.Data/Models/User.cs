using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meditatii.Data.Models
{
    public class User
    {
        public User()
        {
            Categories = new HashSet<Category>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

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
        public DateTime? Dob { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string County { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string City { get; set; }

        public string Description { get; set; }

        public string ProfileImageUrl { get; set; }

        public int? Rating { get; set; }

        public decimal? Price { get; set; }

        public virtual ICollection<Category> Categories { get; set; }

        public virtual ICollection<Cycle> Cycles { get; set; }

        public virtual ICollection<Roles> Roles { get; set; }
    }
}
