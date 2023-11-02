using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SessionWorkshop.Models
{
    public class User
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
    }
}