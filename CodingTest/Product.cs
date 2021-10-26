using System.Collections.Generic;
using System.Linq;

namespace CodingTest
{
    using DevelopementYears = Dictionary<ushort, decimal>;
    using OriginYears = Dictionary<ushort, Dictionary<ushort, decimal>>;

    public class Product
    {
        private string m_name;
        private OriginYears m_originYears;

        public OriginYears OriginYears { get => m_originYears; set => m_originYears = value; }

        public class OriginYear
        {
            private DevelopementYears m_developementYears = new DevelopementYears();

            public DevelopementYears DevelopementYears { get => m_developementYears; set => m_developementYears = value; }

            public void AddDevYear(ushort year, decimal value)
            {
                DevelopementYears.Add( year , value );
            }
        }

        public Product(string name, ushort originYear, ushort developementYear, decimal value)
        {
            m_name = name;
            OriginYears = new OriginYears { [originYear] = new DevelopementYears { [developementYear] = value } };
        }

        public void AddDevelopementYearRecord(ushort originYear, ushort developementYear, decimal value)
        {
            if (!m_originYears.ContainsKey(originYear))
                m_originYears.Add(originYear, new DevelopementYears { [ developementYear ] = value});
            else
                m_originYears[originYear].Add(developementYear, value);
        }

        public string ToString(ushort earliestYear, ushort latestYear)
        {
            string output = m_name + ", ";

            for (var originYear = earliestYear; originYear <= latestYear; ++originYear)
            {
                if (m_originYears.ContainsKey(originYear))
                {
                    decimal accumulatedValue = 0;

                    for (var developementYear = earliestYear; developementYear <= latestYear; ++developementYear)
                    {
                        if (m_originYears[originYear].ContainsKey(developementYear))
                            accumulatedValue += m_originYears[originYear][developementYear];

                        // Output value without trailing .0
                        // NOTE: This is where my solution purposely deviates from the example data:
                        // I found it counter-intuitive to produce '0' for the dev years where no origin data record exists,
                        // but not output anything for missing dev years in existing origin years.
                        // So this solution outputs value for all origin years / dev years, displaying 0s where no record exsist
                        // or repeats the accumulated value, where appropiate.
                        output += accumulatedValue.ToString("G29") + ", ";
                    }
                }
                else
                {
                    output += string.Concat(Enumerable.Repeat("0, ", latestYear - earliestYear + 1));
                }
            }

            return output.TrimEnd(',', ' ');
        }
    }
}
