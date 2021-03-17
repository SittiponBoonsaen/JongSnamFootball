﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JongSnamFootball.Entities.Request
{
    public class UpdateUserRequest
    {
        [Required]
        [MaxLength(50)]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [MaxLength(150)]
        [DataType(DataType.Text)]
        public string Address { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 9,
            ErrorMessage = "ContactMobile should be minimum 8 characters and a maximum of 10 characters")]
        [Phone]
        public string ContactMobile { get; set; }

        public string ImageProfile { get; set; }
    }
}