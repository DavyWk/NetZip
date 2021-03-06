﻿using System;
using System.IO.Compression;


namespace NetZip
{
    static class Helpers
    {
        public static bool Contains(this string[] array, string str)
        {
            foreach (var s in array)
                if (s == str)
                    return true;
            return false;
        }

        public static bool Contains(this string[] array, string[] str)
        {
            foreach(var s in str)
                if (array.Contains(s))
                    return true;
            return false;
        }

        public static bool Equal(this string str, string[] array)
        {
            return array.Contains(str);
        }

        public static long GetSize(this ZipArchive archive)
        {
            long ret = 0;
            foreach (var entry in archive.Entries)
                ret += entry.CompressedLength;
            return ret;
        }

        public static long GetDecompressedSize(this ZipArchive archive)
        {
            long ret = 0;
            foreach (var entry in archive.Entries)
                ret += entry.Length;
            return ret;
        }
    }
}
