using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;

namespace CodingTest
{
    class Program
    {
        public static void OutputToFile(Dictionary<string, Product> products, ushort earliestYear, ushort latestYear)
        {
            using StreamWriter outputFile = new StreamWriter("Output.csv");

            outputFile.WriteLine(earliestYear.ToString() + ", " + (latestYear - earliestYear + 1).ToString());

            foreach (var product in products)
                outputFile.WriteLine(product.Value.ToString(earliestYear, latestYear));
        }

        public static string TakeFileNameInput()
        {
            string defaultFileName = "SampleInputData.csv";
            string fileName = "";

            while (!File.Exists(fileName))
            {
                if (!String.IsNullOrEmpty(fileName))
                        Console.WriteLine(Environment.NewLine + "The file does not exist - try again!");

                Console.WriteLine(Environment.NewLine + "Enter file name [blank to use a default sample file]:");

                fileName = Console.ReadLine();

                if (String.IsNullOrEmpty(fileName))
                    fileName = defaultFileName;
            }

            return fileName;
        }

        static void Main(string[] args)
        {
            Dictionary<string, Product> products = new Dictionary<string, Product>();

            ushort earliestYear = ushort.MaxValue;
            ushort latestYear = ushort.MinValue;

            using (var csv = new CsvReader(new StreamReader(TakeFileNameInput()), CultureInfo.InvariantCulture))
            {
                while (csv.Read())
                {
                    Record record;
                    try
                    {
                        record = csv.GetRecord<Record>();

                        if (record.DevelopementYear < earliestYear)
                            earliestYear = record.DevelopementYear;

                        if (record.DevelopementYear > latestYear)
                            latestYear = record.DevelopementYear;

                        if (!products.ContainsKey(record.Product))
                            products.Add(record.Product, new Product(record.Product, record.OriginYear, record.DevelopementYear, record.IncrementalValue));
                        else
                            products[record.Product].AddDevelopementYearRecord(record.OriginYear, record.DevelopementYear, record.IncrementalValue);

                    }
                    catch (CsvHelperException ex)
                    {
                        Console.WriteLine("{0}\nIgnoring the invalid row", ex.Message);
                    }
                }
            }

            OutputToFile(products, earliestYear, latestYear);
        }
    }
}
