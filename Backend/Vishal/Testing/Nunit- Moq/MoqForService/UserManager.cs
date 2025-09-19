using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoqForService
{
    public class UserManager
    {
        private readonly IUserService _userService;

        public UserManager(IUserService userService)
        {
            _userService = userService;
        }

        public User GetUser(int userId)
        {
            return _userService.GetUserById(userId);
        }
    }
}
