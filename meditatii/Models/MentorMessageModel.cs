﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace meditatii.Models
{
    public class MentorMessageModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int NrOfMessage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool isRead { get; set; }

    }
}
