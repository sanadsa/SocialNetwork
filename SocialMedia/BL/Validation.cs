using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BL
{
    public class Validation : IValidation
    {
        public bool CheckNewUserFields(string email, string username, string password)
        {
            if (Regex.IsMatch(username, "[A-Za-z][A-Za-z0-9._]{5,14}"))
                if (Regex.IsMatch(password, "[A-Za-z][A-Za-z0-9._!@#$]{5,14}"))
                    if (Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
                        return true;
            return false;
        }
    }
}
