using UnityEngine;
using System;
using System.Collections;

namespace NetWorkFrame
{
    public class MiniConverter
    {
        public static int BytesToInt(byte[] bytes, int startIndex)
        {
            Array.Reverse(bytes, startIndex, 4);
            return BitConverter.ToInt32(bytes, startIndex);
        }

        public static Char BytesToInt8(byte[] bytes, int startIndex)
        {
            //Array.Reverse(bytes, startIndex, 1);
            return BitConverter.ToChar(bytes, startIndex);
        }

        public static byte[] IntToBytes(int value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            return bytes;
        }

        public static byte[] Int8ToBytes(char value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Reverse(bytes);
            return bytes;
        }
        
        public static long TimeJavaToCSharp(long java_time)
        {
            DateTime dt_1970 = new DateTime(1970, 1, 1, 0, 0, 0);
            long ticks_1970 = dt_1970.Ticks;
            long time_ticks = ticks_1970 + java_time * 10000;
            DateTime dt = new DateTime(time_ticks).AddHours(8);
            return dt.Ticks;
        }
    }
}


