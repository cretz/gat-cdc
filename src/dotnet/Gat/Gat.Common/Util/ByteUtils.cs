using System;
using System.Collections.Generic;
using System.Text;
using Gat.Common.Error;

namespace Gat.Common.Util
{
    public static class ByteUtils
    {
        /// <summary>
        /// Get a byte array for a primitive (or a string)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] ToByteArray(object obj)
        {
            //we only handle primitives
            if (obj.GetType().IsPrimitive)
            {
                if (obj is byte) { return new byte[] { (byte)obj }; }
                else if (obj is bool) { return BitConverter.GetBytes((bool)obj); }
                else if (obj is char) { return BitConverter.GetBytes((char)obj); }
                else if (obj is double) { return BitConverter.GetBytes((double)obj); }
                else if (obj is float) { return BitConverter.GetBytes((float)obj); }
                else if (obj is int) { return BitConverter.GetBytes((int)obj); }
                else if (obj is long) { return BitConverter.GetBytes((long)obj); }
                else if (obj is short) { return BitConverter.GetBytes((short)obj); }
                else if (obj is uint) { return BitConverter.GetBytes((uint)obj); }
                else if (obj is ulong) { return BitConverter.GetBytes((ulong)obj); }
                else if (obj is ushort) { return BitConverter.GetBytes((ushort)obj); }
                else { throw new GatException("Unrecognized primitive: " + obj.GetType()); }
            }
            else if (obj is string)
            {
                return Encoding.UTF8.GetBytes((string)obj);
            }
            else
            {
                throw new GatException("Unrecognized type to convert to bytes: " + obj.GetType());
            }
        }
    }
}
