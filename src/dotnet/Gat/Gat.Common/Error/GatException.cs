using System;
using System.Collections.Generic;
using System.Text;

namespace Gat.Common.Error
{
    public class GatException : Exception
    {
        public GatException(string message) : base(message) { }
    }
}
