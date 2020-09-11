using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Talentos.Senai.Utilities
{
    public class General
    {
        public object returnResponse(object objectForReturn)
        {
            return new
            {
                objectForReturn
            };
        }
    }
}
