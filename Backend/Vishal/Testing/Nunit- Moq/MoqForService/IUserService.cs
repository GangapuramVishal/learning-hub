using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoqForService
{
    public interface IUserService
    {
        User GetUserById(int userId);
    }
}
