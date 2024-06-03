using System;
using System.IO;

namespace REE.Unpacker
{
    class Program
    {
        public static String m_Title = "RE Engine UnPAK - Desktop";

        static void Main(String[] args)
        {
            string updateDate = "06/03/2024";
            string versionNumber = "1.0.0";
            string m_ListFile = "";
            string m_PakFile = "";
            string m_Output = null;
            bool isUnpack = true;

            Console.Title = m_Title;
            Console.WriteLine("/////////////////////////////////////////");
            Console.WriteLine("-- " + m_Title + "\n");
            Console.WriteLine("-- Desktop version by: SilverEzredes");
            Console.WriteLine("-- Updated: " + updateDate);
            Console.WriteLine("-- Version: " + versionNumber);
            Console.WriteLine("-- (c) 2024 Ekey (h4x0r)");
            Console.WriteLine("/////////////////////////////////////////\n");

            while (isUnpack)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Enter .list file name:");
                Console.ResetColor();
                m_ListFile = Console.ReadLine();
                if (!File.Exists(PakList.m_Path + m_ListFile))
                {
                    Utils.iSetError("[ERROR]: Input LIST file [" + PakList.m_Path + m_ListFile + "] does not exist");
                    Console.ReadLine();
                    return;
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nEnter .pak file path:");
                Console.ResetColor();
                m_PakFile = Console.ReadLine();
                if (!File.Exists("Zstandard.Net.dll") || !File.Exists("libzstd.dll"))
                {
                    Utils.iSetError("[ERROR]: Unable to find ZSTD modules");
                    Console.ReadLine();
                    return;
                }
                if (!File.Exists(m_PakFile))
                {
                    Utils.iSetError("[ERROR]: Input PAK file [" + m_PakFile + "] does not exist");
                    Console.ReadLine();
                    return;
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nEnter output folder path:");
                Console.ResetColor();
                m_Output = Console.ReadLine();

                if (m_Output.Length < 2)
                {
                    m_Output = Path.GetDirectoryName(m_PakFile) + @"\" + Path.GetFileNameWithoutExtension(m_PakFile) + @"\";
                }
                else
                {
                    m_Output = Utils.iCheckArgumentsPath(m_Output);
                }

                PakList.iLoadProject(m_ListFile);
                PakUnpack.iDoIt(m_PakFile, m_Output);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n[FINISHED]");
                Console.ResetColor();
                Console.WriteLine("  " + Path.GetFileNameWithoutExtension(m_PakFile));

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n[OUTPUT PATH]");
                Console.ResetColor();
                Console.WriteLine("  " + m_Output);

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\nDo you want to unpack another archive? (yes/no):");
                string unpackAgain = Console.ReadLine().Trim().ToLower();

                if (unpackAgain != "yes")
                {
                    isUnpack = false;
                }
            }
        }
    }
}
