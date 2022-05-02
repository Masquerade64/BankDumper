using System;
using System.IO;
using System.Text;

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

                    string headerType = Encoding.ASCII.GetString(infile[i..(i + 4)]);

                    if (headerType == "BKHD" || headerType == "AKPK" || headerType == "FSB5")

                    {
                        Console.Clear();

                        if (i == 0)

                        {
                            Console.WriteLine("{0} is already a dumped bank,", args[0]);
                            Environment.Exit(1);
                        }

                        Console.WriteLine("Header Detected Type => " + headerType);

                        int newlen = infile.Length - i;       
                        byte[] outfile = new byte[newlen];  
                        Array.Copy(infile, i, outfile, 0, newlen);
                        File.WriteAllBytes(args[1], outfile);
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
