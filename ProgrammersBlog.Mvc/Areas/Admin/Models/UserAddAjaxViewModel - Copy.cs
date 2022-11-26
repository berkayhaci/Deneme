using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ProgrammersBlog.Entities.DTOs;

namespace ProgrammersBlog.Mvc.Areas.Admin.Models
{
    public class UserAddAjaxViewModel
    {
        public UserAddDto CategoryAddDto { get; set; }
        public string UserAddPartial { get; set; }
        public UserDto CategoryDto { get; set; }
    }
}
