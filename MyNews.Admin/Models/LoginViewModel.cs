using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyNews.Admin.Models
{
    public class LoginViewModel
    {

        [EmailAddress]
        [StringLength(200)]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Required]
        [StringLength(256)]
        public string Password { get; set; }



    }
}
