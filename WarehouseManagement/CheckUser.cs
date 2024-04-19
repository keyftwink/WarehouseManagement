using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laboratorka
{
    public class CheckUser
    {
        public string Login { get; set; }
        public bool IsAdmin { get;}
        public bool IsOperator { get; }
        public string Status => IsAdmin ? "Admin" : IsOperator ? "Operator" : "Subscriber";
        public CheckUser(string login, bool isAdmin, bool isOperator)
        {
            Login = login.Trim();
            IsAdmin = isAdmin;
            IsOperator = isOperator;
        }
    
    }
}
