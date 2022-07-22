using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIRest.Helper
{
    public class ErrorResponse
    {
        public int NoError { get; set; }
        public string Message { get; set; }

        public ErrorResponse()
        {

        }

        public string MyProperty { get; set; }
    }


}
