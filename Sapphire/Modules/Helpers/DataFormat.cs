using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sapphire
{
    sealed class Format
    {
        public struct LoginData
        {
            public string url { get; set; }
            public string login { get; set; }
            public string password { get; set; }
            public string browser { get; set; }
        }
    }
}
