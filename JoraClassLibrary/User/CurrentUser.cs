 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoraClassLibrary.User.User
{
    public class CurrentUser
    {
        private static CurrentUser _CU;
        private static readonly object _lock = new object();
        public User currentUser { get; private set; }
        private CurrentUser() { }

        public static CurrentUser Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_CU == null)
                    {
                        _CU = new CurrentUser();
                    }
                    return _CU;
                }
            }
        }
        internal void SetCurrentUser(User logUser)
        {
            currentUser = logUser;
        }

    }
}
