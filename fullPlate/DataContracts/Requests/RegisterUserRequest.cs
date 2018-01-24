using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fullPlate.DataContracts.Requests
{
    public class RegisterUserRequest
    {
        public string email { get; set; }
        public string username { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string password { get; set; }
    }
}
