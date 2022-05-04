using System;
using System.IO;
using System.Text;

namespace BankDumper
{
    internal class Program
    {
        static async Task Main(string[] args) 
        {
            if (args.Length == 2)
            {

                var fsinfile = File.Open(args[0], FileMode.Open, FileAccess.Read);   // Input File

                byte[] buffer = new byte[fsinfile.Length]; 
               
                
                // Main Loop : Using integer 'i' as an index for arrays and a position finder for I/O streams. 
                for (int i = 0; i < fsinfile.Length; i++)

                {
                    
                    // Nested Loop : To collect all 4-Byte pattern and store each Byte as 'n' index of buffer array
                    for (int n = 0; n < fsinfile.Length; n++)
                    {
                        fsinfile.Seek(n, SeekOrigin.Begin);
                        buffer[n] = Convert.ToByte(fsinfile.ReadByte()); // GetString Method takes array of bytes as input so "Convert.ToByte" was used.
                    }

                    string headerType = Encoding.ASCII.GetString(buffer[i..(i + 4)]);

                    if (headerType == "BKHD" || headerType == "AKPK" || headerType == "FSB5")

                    {
                        Console.Clear();

                        if (i == 0)

                        {

                            Console.WriteLine("{0} is already a dumped bank", args[0]);
                            await Task.Delay(2000); 
                            Environment.Exit(1);
                        }


                        var fsoutfile = File.Open(args[1], FileMode.Create, FileAccess.Write); // Output File

                        

                        Console.WriteLine("Header Detected Type => " + headerType);

                        fsinfile.Seek(i, SeekOrigin.Begin); // This sets the new position, from where fsinfile copies to output.
                        fsinfile.CopyTo(fsoutfile);


                        fsoutfile.Flush();


                        break;
                    }

                }
            }

            else

            {
                Console.WriteLine();
                Console.WriteLine(" " + AppDomain.CurrentDomain.FriendlyName + " <input bank> <output bank>");  // Syntax
            }

        }
    }
}
