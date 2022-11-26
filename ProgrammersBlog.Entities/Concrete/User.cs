using Microsoft.AspNetCore.Identity;
using ProgrammersBlog.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Entities.Concrete
{
    public class User : IdentityUser<int>
    {
        
        public ICollection<Article> Articles { get; set; } //bir kullanıcı birden fazla makale
        public string Picture { get; set; }
        
    }
}
