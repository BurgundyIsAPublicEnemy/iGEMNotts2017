using System;
using System.Drawing;
using System.Linq;
using System.IO;

namespace iGEM2017
{

    class iGEMProgram2017
    {
        private const int threshold = 10;
        public static bool first_file = false;

        static void Main(string[] args)
        {
          //  Console.WriteLine(System.IO.Directory.GetCurrentDirectory());
            File.WriteAllText(@"C:\Users\edpkp\Documents\Visual Studio 2015\Projects\iGEM2017\iGEM2017\bin\Debug\First_Analysis.txt", String.Empty);
            File.WriteAllText(@"C:\Users\edpkp\Documents\Visual Studio 2015\Projects\iGEM2017\iGEM2017\bin\Debug\Second_Analysis.txt", String.Empty);
            string path = @"C:\Users\edpkp\Documents\Visual Studio 2015\Projects\iGEM2017\iGEM2017\bin\Debug\graph1.bmp";
            string path2 = @"C:\Users\edpkp\Documents\Visual Studio 2015\Projects\iGEM2017\iGEM2017\bin\Debug\graph2.bmp";
            //INTERPRET GRAPHS FROM MASS-SPEC (maybe?)

            Bitmap bmpSrc = new Bitmap(path, true);
            DrawImage.ConsoleWriteImage(bmpSrc);
            first_file = true;
            Bitmap bmpSrc1 = new Bitmap(path2, true);
            DrawImage.ConsoleWriteImage(bmpSrc1);

            //---- 
            Console.WriteLine("READING FROM FILE 1 (Original) ");
            string a = File.ReadAllText(@"C:\Users\edpkp\Documents\Visual Studio 2015\Projects\iGEM2017\iGEM2017\bin\Debug\First_Analysis.txt");
            Console.WriteLine("READING FROM FILE 2 (Mutation)");
            string b = File.ReadAllText(@"C:\Users\edpkp\Documents\Visual Studio 2015\Projects\iGEM2017\iGEM2017\bin\Debug\Second_Analysis.txt");
            int distance = Damerau_Levenshtein.Damerau_Levenshtein_Alg(a, b);
            Console.WriteLine("Distance between FILE 1 and FILE 2: " + distance);

            if (distance > threshold)
            {
                Console.WriteLine("PASSWORDS DO NOT MATCH. ACCESS DENIED");
            } else
            {
                Console.WriteLine("THRESHOLD MATCHED. ACCESS ALLOWED");
            }
            Console.WriteLine("Do you wish to view the files? (Debugging) PRESS Y TO WORK IT ");
            string zx = Console.ReadLine();
            if (zx == "Y")
            {
                Console.WriteLine("FILE 1 SAYS: \n " + a);
                Console.WriteLine("FILE 2 SAYS: \n " + b);
            }
            else
            {
                Console.WriteLine("Yeah, me neither. TERMINATING.");
            }
            
            Console.ReadLine();
        }

        


    }

    class DrawImage
    {
        static int[] cColors = { 0x000000, 0x000080, 0x008000, 0x008080, 0x800000, 0x800080, 0x808000, 0xC0C0C0, 0x808080, 0x0000FF, 0x00FF00, 0x00FFFF, 0xFF0000, 0xFF00FF, 0xFFFF00, 0xFFFFFF };
        public static void ConsoleWritePixel(Color cValue)
        {

            Color[] cTable = cColors.Select(x => Color.FromArgb(x)).ToArray();
            char[] rList = new char[] { (char)9617, (char)9618, (char)9619, (char)9608 }; // 1/4, 2/4, 3/4, 4/4
            int[] bestHit = new int[] { 0, 0, 4, int.MaxValue }; //ForeColor, BackColor, Symbol, Score

            for (int rChar = rList.Length; rChar > 0; rChar--)
            {
                for (int cFore = 0; cFore < cTable.Length; cFore++)
                {
                    for (int cBack = 0; cBack < cTable.Length; cBack++)
                    {
                        int R = (cTable[cFore].R * rChar + cTable[cBack].R * (rList.Length - rChar)) / rList.Length;
                        int G = (cTable[cFore].G * rChar + cTable[cBack].G * (rList.Length - rChar)) / rList.Length;
                        int B = (cTable[cFore].B * rChar + cTable[cBack].B * (rList.Length - rChar)) / rList.Length;
                        int iScore = (cValue.R - R) * (cValue.R - R) + (cValue.G - G) * (cValue.G - G) + (cValue.B - B) * (cValue.B - B);
                        if (!(rChar > 1 && rChar < 4 && iScore > 50000)) // rule out too weird combinations
                        {
                            if (iScore < bestHit[3])
                            {
                                bestHit[3] = iScore; //Score
                                bestHit[0] = cFore;  //ForeColor
                                bestHit[1] = cBack;  //BackColor
                                bestHit[2] = rChar;  //Symbol
                            }
                        }
                    }
                }
            }
            Console.ForegroundColor = (ConsoleColor)bestHit[0];
            Console.BackgroundColor = (ConsoleColor)bestHit[1];

            if (iGEMProgram2017.first_file == false)
            {
                using (StreamWriter sw = File.AppendText(@"C:\Users\edpkp\Documents\Visual Studio 2015\Projects\iGEM2017\iGEM2017\bin\Debug\First_Analysis.txt"))
                {
                    sw.Write(rList[bestHit[2] - 1]);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(@"C:\Users\edpkp\Documents\Visual Studio 2015\Projects\iGEM2017\iGEM2017\bin\Debug\Second_Analysis.txt"))
                {
                    sw.Write(rList[bestHit[2] - 1]);
                }
            }

            Console.Write(rList[bestHit[2] - 1]);
        }

        public static void ConsoleWriteImage(Bitmap source)
        {
            //calibration 
            int sMax = 40;
            decimal percent = Math.Min(decimal.Divide(sMax, source.Width), decimal.Divide(sMax, source.Height));
            Size dSize = new Size((int)(source.Width * percent), (int)(source.Height * percent));
            Bitmap bmpMax = new Bitmap(source, dSize.Width * 2, dSize.Height);
            for (int i = 0; i < dSize.Height; i++)
            {
                for (int j = 0; j < dSize.Width; j++)
                {
                    ConsoleWritePixel(bmpMax.GetPixel(j * 2, i));
                    ConsoleWritePixel(bmpMax.GetPixel(j * 2 + 1, i));
                }
                System.Console.WriteLine();
            }
            Console.ResetColor();
        }
    }

    class Damerau_Levenshtein
        {

        internal static int Damerau_Levenshtein_Alg(string original, string modified)
        {
            //taken from https://en.wikipedia.org/wiki/Damerau%E2%80%93Levenshtein_distance and github 
            int len_orig = original.Length;
            int len_diff = modified.Length;

            var matrix = new int[len_orig + 1, len_diff + 1];
            for (int i = 0; i <= len_orig; i++)
                matrix[i, 0] = i;
            for (int j = 0; j <= len_diff; j++)
                matrix[0, j] = j;

            for (int i = 1; i <= len_orig; i++)
            {
                for (int j = 1; j <= len_diff; j++)
                {
                    int cost = modified[j - 1] == original[i - 1] ? 0 : 1;
                    var vals = new int[] {
                matrix[i - 1, j] + 1,
                matrix[i, j - 1] + 1,
                matrix[i - 1, j - 1] + cost
            };
                    matrix[i, j] = vals.Min();
                    if (i > 1 && j > 1 && original[i - 1] == modified[j - 2] && original[i - 2] == modified[j - 1])
                        matrix[i, j] = Math.Min(matrix[i, j], matrix[i - 2, j - 2] + cost);
                }
            }
            return matrix[len_orig, len_diff];

            throw new NotImplementedException();
        }

    }
}
