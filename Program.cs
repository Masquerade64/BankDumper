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
                    bool found = false;
                    string buffer = infile[i].ToString() + infile[i + 1].ToString() + infile[i + 2].ToString() + infile[i + 3].ToString();
                    if (buffer == "70836653")
                    {
                        int newlen = infile.Length - i;
                        byte[] outfile = new byte[newlen];
                        Array.Copy(infile, i, outfile, 0, newlen);
                        File.WriteAllBytes(args[1], outfile);
                        found = true;
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
                Console.WriteLine(" " + AppDomain.CurrentDomain.FriendlyName + " <input bank> <output fsb>");
            }
        }
    }
}
