using System;

using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

namespace CodingTest
{
    public class Record
    {
        private string m_product;
        private ushort m_originYear;
        private ushort m_developementYear;
        private Decimal m_incrementalValue;

        public Record()
        {
        }

        [Name(" Origin Year")]
        public ushort OriginYear { get => m_originYear; set => m_originYear = value; }


        [Name(" Development Year")]
        public ushort DevelopementYear
        {
            get => m_developementYear;
            set
            {
                if (value >= this.OriginYear)
                {
                    m_developementYear = value;
                }
                else
                {
                    // TODO: Decide what to do:
                    // - disregard the value for the year, most likely
                }
            }
        }

        [Name(" Incremental Value")]
        public decimal IncrementalValue { get => m_incrementalValue; set => m_incrementalValue = value; }

        [Name("Product")]
        public string Product { get => m_product; set => m_product = value; }
    }
}
