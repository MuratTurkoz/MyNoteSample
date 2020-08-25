using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNoteSample.Common
{
    public class DefaultCommon : ICommon
    {
        public string GetUsername()
        {
            return "_system";
        }
    }
}
