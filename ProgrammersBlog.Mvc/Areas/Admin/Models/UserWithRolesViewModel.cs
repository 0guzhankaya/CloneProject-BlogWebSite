using ProgrammersBlog.Entities.Concrete;
using System.Collections;
using System.Collections.Generic;

namespace ProgrammersBlog.Mvc.Areas.Admin.Models
{
    public class UserWithRolesViewModel
    {
        // Kullanıcıyı ve kullanıcı rollerini model içerisinde tutmak için.

        public User User { get; set; }
        public IList<string> Roles { get; set; }
    }
}
