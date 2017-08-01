using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ActiveDirectoryAPI.Model
{
    public class GroupForCreation
    {
        [Required(ErrorMessage = "Group Name is required.")]
        [MaxLength(50)]
        public string GroupName { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }
    }
}
