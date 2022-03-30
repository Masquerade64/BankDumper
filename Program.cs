using System;
using System.IO;

namespace BankDumper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 2)
            {
                byte[] infile = File.ReadAllBytes(args[0]);

                for (int i = 0; i < infile.Length; i++) 
                
                {

                    string[] MagicNumber = { "70836653", "66757268", "65758075" }; // [0] = FSB5, [1] = BKHD, [2] = AKPK ....... upto [n-1] = Header, n belongs to postive integers

                    bool found = false;

                    string buffer = infile[i].ToString() + infile[i + 1].ToString() + infile[i + 2].ToString() + infile[i + 3].ToString();

                    for (int n = 0; n <= 2; n++)
                    {
                        if (buffer == MagicNumber[n])

                        {

                            if (i == 0)

                            {
                                GetHeader(args, buffer, i);
                                Environment.Exit(1);
                            }

                            GetHeader(args, buffer, i);
                            SetLength(args, infile, i);
                            found = true;

                        }
                    }

                    if (found == true)

                    {

                        break;
                    }
                }
            }
            
            else
            
            {
                Console.WriteLine();
                Console.WriteLine(" " + AppDomain.CurrentDomain.FriendlyName + " <input bank> <output bank>");
            }

        }

        public static void SetLength(string[] args, byte[] infile, int i) // (Full Length - Before Header Length) = DumpedBank, all bytes are then written to outfile 
        {
            int newlen = infile.Length - i;
            byte[] outfile = new byte[newlen];
            Array.Copy(infile, i, outfile, 0, newlen);
            File.WriteAllBytes(args[1], outfile);
        }
        public static void GetHeader(string[] args, string buffer, int i) // Calculates Header values from 'buffer' string
        {
            var firstbyte = char.ConvertFromUtf32(Convert.ToInt32(buffer[0].ToString() + buffer[1].ToString()));
            var secondbyte = char.ConvertFromUtf32(Convert.ToInt32(buffer[2].ToString() + buffer[3].ToString()));
            var thirdbyte = char.ConvertFromUtf32(Convert.ToInt32(buffer[4].ToString() + buffer[5].ToString()));
            var fourthbyte = char.ConvertFromUtf32(Convert.ToInt32(buffer[6].ToString() + buffer[7].ToString()));
            string headertype = (firstbyte + secondbyte + thirdbyte + fourthbyte);
            Console.WriteLine("Input File: {0}", args[0]);
            Console.WriteLine("Header Found: {0}", headertype);
            Console.WriteLine("Length Before Header: {0}", i);
        }
    }
}
