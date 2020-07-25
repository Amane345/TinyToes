using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TinyToes.Models
{
    public class User : IdentityUser
    {
       [NotMapped]
        public IList<string> RoleNames { get; set; }
    }
}
