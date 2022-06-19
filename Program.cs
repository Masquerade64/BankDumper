using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;

namespace BankDumperTool
{
    internal class Program
    {
        static void Main(string[] args)
        {


            Console.Clear();
            Console.Title = "BDT";


            if (args.Length != 2)

            {
                IOMethods.Usage();
                Environment.Exit(1);
            }

            // Instance of Stopwatch Class, to measure the performance of the whole process.
            Stopwatch timeCounter = new Stopwatch();


            // Checks whether input file exist or not, if not, the program will exit.
            IOMethods.FileExistCheck(args);
            // Checks whether output path exist or not, if not, then it creates the path
            IOMethods.DirectoryExistCheck(args);


            // Input File
            var bigStream = File.Open(args[0], FileMode.Open, FileAccess.Read);


            // Variables and Magic Numbers //
            // ---------------------------------------------- //
            int currentByte;
            byte[] buffer = new byte[4]; // Sliding Window
            byte[] wwiseBKHD = { 66, 75, 72, 68 };  // BKHD
            byte[] wwiseAKPK = { 65, 75, 80, 75 };  // AKPK
            byte[] fmodFSB5 = { 70, 83, 66, 53 };  // BKHD
            // ---------------------------------------------- //


            // To save streams position
            List<long> position = new List<long>();

            // Starts ticking
            timeCounter.Start();



            while ((currentByte = bigStream.ReadByte()) != -1)

            {

                for (int i = 0; i < (buffer.Length - 1); i++)

                {


                    buffer[i] = buffer[i + 1];
                }

                buffer[(buffer.Length - 1)] = (byte)currentByte;


                if (IOMethods.HeaderEquals(buffer, wwiseAKPK) || IOMethods.HeaderEquals(buffer, wwiseBKHD) || IOMethods.HeaderEquals(buffer, fmodFSB5))

                {
                    if (IOMethods.IsBKHD(buffer))

                    {
                        Console.WriteLine("-> BKHD Header Detected at offset(d): " + (bigStream.Position - buffer.Length));
                    }

                    else if (IOMethods.IsAKPK(buffer))

                    {
                        Console.WriteLine("-> AKPK Header Detected at offset(d): " + (bigStream.Position - buffer.Length));
                    }

                    else

                    {
                        Console.WriteLine("-> FSB5 Header Detected at offset(d): " + (bigStream.Position - buffer.Length));
                    }

                    position.Add(bigStream.Position - buffer.Length);
                }

            }

            // Checks if any header was detected or not
            IOMethods.NoHeadersCaseCheck(position,args);


            // You will need length as last element element to extract them correctly.
            position.Add(bigStream.Length);

            // Closes the current stream, so that it can be read again by infile variable
            bigStream.Close();

            // The same algorithm used for extraction
            byte[] infile = File.ReadAllBytes(args[0]);

            for (int k = 1; k < position.Count; k++)

            {
                long length = position[k] - position[k - 1];
                byte[] outfile = new byte[length];
                Array.Copy(infile, position[k - 1], outfile, 0, length);


                if (IOMethods.IsBKHD(outfile))

                {
                    File.WriteAllBytes(args[1] + @"\" + k + ".bnk", outfile);
                }


                else if (IOMethods.IsAKPK(outfile))

                {
                    File.WriteAllBytes(args[1] + @"\" + k + ".pck", outfile);
                }

                else

                {
                    File.WriteAllBytes(args[1] + @"\" + k + ".fsb", outfile);
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("File: " + Path.GetFileName(args[0]) + k + "| Status: Extracted! | Length: " + length);
                Console.ResetColor();

            }


            // Stops Ticking
            timeCounter.Stop();

            // Displays time taken during the whole process.
            Console.WriteLine("This process took " + timeCounter.ElapsedMilliseconds / 1000.0 + " seconds!");

            Console.WriteLine();
            Console.WriteLine("Masquerade | :( Sad8669 | insomnyawolf");

        }

    }
}
