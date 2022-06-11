using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace BankDumper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.Title = "Bank Dumping Tool | BDT";

            if (args.Length == 3)  // Syntax : BDT <infile> <outfile> [.ext]

            {

                List<int> position = new List<int>(); // Used to store starting offset/position
                byte[] infile = File.ReadAllBytes(args[0]);

                FileExistCheck(args); // Checks whether <output_path> is a directory or a file

                for (int i = 0; i < infile.Length - 4; i++)

                {

                    string headerType = Encoding.ASCII.GetString(infile[i..(i + 4)]);
                    HeaderCheck(headerType, i, position);

                }

                // End position = Length of the file, so should be the last element of 'position' List.
                position.Add(infile.Length);


                DirectoryCheck(args); // Checks whether <output_path> exists

                Extracting(position, infile, args);

                Credits();

            }

            else if (args.Length == 2)   // Syntax : BDT <infile> <outfile> 

            {

                List<int> position = new List<int>();  // Used to store starting offset/position
                byte[] infile = File.ReadAllBytes(args[0]);

                FileExistCheck(args); // Checks whether <output_path> is a directory or a file

                for (int i = 0; i < infile.Length - 4; i++)

                {

                    string headerType = Encoding.ASCII.GetString(infile[i..(i + 4)]);
                    HeaderCheck(headerType, i, position);

                }

                // End position = Length of the file, so should be the last element of 'position' List.
                position.Add(infile.Length);
                
                
                DirectoryCheck(args); // Checks whether <output_path> exists

                for (int k = 1; k < position.Count; k++)

                {
                    int length = position[k] - position[k - 1];
                    byte[] outfile = new byte[length];
                    Array.Copy(infile, position[k - 1], outfile, 0, length);
                    File.WriteAllBytes(args[1] + @"\" + k + ".dat", outfile);
                    Console.WriteLine("->>> " + (k - 1) + ".dat" + " of length " + length + " ...... Extracted!");

                }

                Credits();
            }

            else

            {
                Console.WriteLine();
                Console.WriteLine(AppDomain.CurrentDomain.FriendlyName + " <input_bank> <output_directory>"); 
                Credits();
            }

        }


        static void Extracting(List<int> position, byte[] infile, string[] args)

        {
            for (int k = 1; k < position.Count; k++)

            {
                int length = position[k] - position[k - 1];
                byte[] outfile = new byte[length];
                Array.Copy(infile, position[k - 1], outfile, 0, length);
                File.WriteAllBytes(args[1] + @"\" + k + args[2], outfile);
                Console.WriteLine("->>> " + (k - 1) + args[2] + " of length " + length + " ...... Extracted!");

            }
        }

        static void Credits()

        {

            Console.WriteLine();
            Console.WriteLine("Masquerade | :(Sad8669");
            Console.WriteLine();

        }

        static void HeaderCheck(string headerType, int i,List<int> position)

        {

            if (headerType == "BKHD" || headerType == "AKPK" || headerType == "FSB5")

            {
                Console.WriteLine("->" + headerType + " Header found at offset(d): " + i);
                position.Add(i);

            }

        }

        static void DirectoryCheck(string[] args)

        {
            if (!Directory.Exists(args[1]))

            {
                Directory.CreateDirectory(args[1]);
            }

        }

        static void FileExistCheck(string[] args)

        {

            if (File.Exists(args[1]))  

            {
                Console.WriteLine("Output path is not in correct format!");
                Environment.Exit(1);
            }

        }
    }

}
