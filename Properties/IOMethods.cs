using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace BankDumperTool
{
    internal class IOMethods


    {

        public static void FileExistCheck(string[] args)

        {
            if (!File.Exists(args[0]))

            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Input does not exist, please check your files and try again.");
                Console.ResetColor();
                Environment.Exit(1);
            }

        }

        public static void DirectoryExistCheck(string[] args)


        {

            if (!Directory.Exists(args[1]))

            {
                Directory.CreateDirectory(args[1]);
            }

        }

        public static void NoHeadersCaseCheck(List<long> position,string[] args)

        {
            if (position.Count == 0)

            {
                Console.Clear();
                Console.WriteLine("No Headers detected, please check your input and try again.");
                Directory.Delete(args[1]);
                Environment.Exit(1);
            }

        }

        public static byte[] GetEquivalentBytes(string pattern)

        {
            var encodedString = Encoding.ASCII.GetBytes(pattern);
            return encodedString;
        }

        public static string GetEquivalentString(byte[] pattern)

        {
            var encodedBytes = Encoding.ASCII.GetString(pattern);
            return encodedBytes;
        }

        public static bool HeaderEquals(byte[] buffer, byte[] pattern)
        {


            var index = (buffer.Length - 1);
            var patterChecksRemeaning = pattern.Length;


            while (patterChecksRemeaning > 0)
            {
                if (buffer[index] != pattern[index])
                {
                    return false;
                }

                index--;
                patterChecksRemeaning--;
            }

            return true;
        }

        public static bool IsBKHD(byte[] outfile)

        {
            if (outfile[0] == 66 && outfile[1] == 75 && outfile[2] == 72 && outfile[3] == 68)
            {
                return true;
            }

            return false;
        }

        public static bool IsAKPK(byte[] outfile)

        {
            if (outfile[0] == 65 && outfile[1] == 75 && outfile[2] == 80 && outfile[3] == 75)
            
            {
                return true;
            }

            return false;
        }

        public static void Usage()

        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("BDT <input_file> <output_path>");
            Console.WriteLine();
            Console.WriteLine("Masquerade | :(Sad8669 | insomnyawolf");
            Console.ResetColor();
        }
    }
}
