using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace meditatii.Models
{
    public class UsersResultModel
    {
        public int TotalRecords { get; set; }

        public List<Models.UserModel> Users { get; set; }
    }
}
