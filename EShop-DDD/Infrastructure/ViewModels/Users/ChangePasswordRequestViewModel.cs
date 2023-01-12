using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Users
{
    public class ChangePasswordRequestViewModel:object
    {
        public ChangePasswordRequestViewModel():base()
        {

        }

        public Guid? Id { get; set; }

        public string NewPassword { get; set; }
    }
}
