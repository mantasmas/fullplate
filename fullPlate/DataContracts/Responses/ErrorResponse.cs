using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fullPlate.DataContracts.Responses
{
    public class ErrorResponse
    {
        public string errorMessage { get; set; }
        public string errorCause { get; set; }
    }
}
