using System;
using System.Collections.Generic;
using System.Text;

namespace Spectre.Models
{
    public sealed class UserListEntry
    {
        public string UserName { get; }

        public bool Transient { get; }

        public UserListEntry(string userName, bool transient)
        {
            UserName = userName;
            Transient = transient;
        }

        public override string ToString()
        {
            return UserName;
        }
    }
}
