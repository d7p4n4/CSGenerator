using CSAc4yClass.Class;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGenerator
{
    class GeneratePersistentService
    {

        public static void generatePersistentService(string templateName, string package, string className, List<Ac4yProperty> map, string outputPath)
        {
            string[] text = readIn(templateName);

            string replaced = "";
            string newLine = "";

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i].Contains("#getFirstBy#"))
                {
                    foreach (var pair in map)
                    {
                        newLine = text[i + 1].Replace("#Prop#", pair.Name.Substring(0, 1).ToUpper() + pair.Name.Substring(1))
                                                .Replace("#type#", pair.Type).Replace("#prop#", pair.Name) + 
                                                "\n" + text[i + 2] + "\n" + text[i + 3] + "\n" + text[i + 4] + "\n" + text[i + 5] + "\n";
                        newLine = newLine + text[i + 6].Replace("#className#", className)
                                                        .Replace("#Prop#", pair.Name.Substring(0, 1).ToUpper() + pair.Name.Substring(1))
                                                        .Replace("#prop#", pair.Name) + "\n";
                        for(int x = 7; x < 16; x++)
                        {
                            newLine = newLine + text[i+x] + "\n"; 
                        }

                        replaced = replaced + newLine + "\n\n";
                    }

                    i = i + 16;
                }
                else if (text[i].Equals("#getListBy#"))
                {
                    foreach (var pair in map)
                    {
                        newLine = text[i + 1].Replace("#Prop#", pair.Name.Substring(0, 1).ToUpper() + pair.Name.Substring(1))
                                                .Replace("#type#", pair.Type).Replace("#prop#", pair.Name) +
                                                "\n" + text[i + 2] + "\n" + text[i + 3] + "\n" + text[i + 4] + "\n" + text[i + 5] + "\n";
                        newLine = newLine + text[i + 6].Replace("#className#", className)
                                                        .Replace("#Prop#", pair.Name.Substring(0, 1).ToUpper() + pair.Name.Substring(1))
                                                        .Replace("#prop#", pair.Name) + "\n";
                        for (int x = 7; x < 16; x++)
                        {
                            newLine = newLine + text[i + x] + "\n";
                        }

                        replaced = replaced + newLine + "\n\n";
                    }

                    i = i + 16;
                }
                else
                {
                    replaced = replaced + newLine + text[i] + "\n";
                }

                newLine = "";
            }
            replaced = replaced.Replace("#namespaceName#", package);
            replaced = replaced.Replace("#className#", className);

            writeOut(replaced, className, outputPath);

        }

        public static string[] readIn(string fileName)
        {

            string textFile = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Templates\\", fileName + "PersistentService.csT");

            string[] text = File.ReadAllLines(textFile);

            return text;


        }

        public static void writeOut(string text, string fileName, string outputPath)
        {
            File.WriteAllText(outputPath + fileName + "PersistentService.cs", text);

        }
    }
}
