using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksDone.Domain.Enums
{
    /// <summary>
    /// Role of a user with different access rights.
    /// </summary>
    public enum UserRole
    {
        /// <summary>
        /// Guest with extra-limited access - can only be assigned tasks, has no access to tasks of other users
        /// </summary>
        Guest,
        /// <summary>
        /// Regular developer, can create and be assigned tasks, can access other users' tasks
        /// </summary>
        Developer,
        /// <summary>
        /// Administrator - can delete tasks, access private projects and change user roles
        /// </summary>
        Administrator
    }
}
