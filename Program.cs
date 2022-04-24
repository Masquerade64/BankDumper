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

                    string[] MagicNumber = { "70836653", "66757268", "65758075" };  // [0] = FSB5, [1] = BKHD, [2] = AKPK

                    bool found = false;

                    string buffer = infile[i].ToString() + infile[i + 1].ToString() + infile[i + 2].ToString() + infile[i + 3].ToString();

                    if (buffer == MagicNumber[0] || buffer == MagicNumber[1] || buffer == MagicNumber[2])

                    {

                        if (i == 0)
                        
                        {
                            Console.WriteLine("{0} is already a dumped bank,", infile);
                            Environment.Exit(1);
                        }

                        int newlen = infile.Length - i;       // Gets the length of the dumped bank
                        byte[] outfile = new byte[newlen];    // declared an array byte variable, whose index = Length of dumped bank
                        Array.Copy(infile, i, outfile, 0, newlen); // (copies from infile, index i as starting point for header, outfile file to write to, index 0 as starting of of outfile, length to write)
                        File.WriteAllBytes(args[1], outfile); // Writes the all elements of outfile to specified file at args[1]
                        found = true; // a boolean , incase there is some i length before header.         
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
    }
}
