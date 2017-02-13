using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Autoshop.Models.Autoshop
{
    public class Rule
    {
        public virtual Car CreateObject(string label, int seed)
        {
            return new Car()
            {
                Label = label,
                Price = (seed % 11 + 5) * 20,
                Balance = seed % 100 + 1
            };
        }

        public string Type { get; set; }
        public IEnumerable<IEnumerable<string>> SubLabels { get; set; }
    }

    public class SedanRule : Rule
    {
        public DateTime? StartDate { get; set; }
        public int DateMinDaysRange { get; set; }
        public int DateMaxDaysRange { get; set; }

        public IEnumerable<string> Colors { get; set; }

        public SedanRule()
        {
            Colors = new string[0];
        }

        public DateTime? GenerateWarrantyPeriod(int seed)
        {
            int days = DateMaxDaysRange - DateMinDaysRange != 0 ? (seed % (DateMaxDaysRange - DateMinDaysRange)) + DateMinDaysRange + 1 : 0;
            return StartDate == null ? null : (DateTime?)StartDate.Value.AddDays(days);
        }

        public int GenerateMaxWeight(int seed)
        {
            return seed % 2 == 1 ? 100 : 10000;
        }

        public int GenerateEngineSize(int seed)
        {
            return seed & 5;
        }

        public string GenerateColor(int seed)
        {
            if (Colors.Count() == 0) return null;
            int index = (seed / 17) % Colors.Count();
            return Colors.Skip(index).Take(1).FirstOrDefault();
        }

        public override Car CreateObject(string label, int seed)
        {
            return new Sedan()
            {
                Label = label,
                Price = (seed % 11 + 5) * 20,
                Balance = seed % 100 + 1,
                EngineSize = GenerateEngineSize(seed),
                Color = GenerateColor(seed),
                MaxWeight = GenerateMaxWeight(seed),
                WarrantyPeriod = GenerateWarrantyPeriod(seed)
            };
        }
    }
    public class AutoshopGenerator
    {
        private Dictionary<string, Car> _store = new Dictionary<string, Car>();
        private Random rand;

        public AutoshopGenerator(int seed = 1)
        {
            rand = new Random(seed);
            Rules = new List<Rule>();
        }
        
        private List<Rule> Rules { get; set; }

        public void AddRule(Rule rule)
        {
            Rules.Add(rule);
        }

        public Car Generate()
        {
            string label;
            int genValue;
            Rule rule;

            do
            {
                genValue = rand.Next();
                if (Rules.Count == 0) throw new Exception();
                rule = Rules[genValue % Rules.Count];

                List<string> labelParts = new List<string>();
                labelParts.Add(rule.Type);

                int divider = 3;

                foreach (IEnumerable<string> subLabels in rule.SubLabels)
                {
                    int index = (genValue / divider) % subLabels.Count();
                    labelParts.Add(subLabels.Skip(index).FirstOrDefault());
                    divider++;
                }

                label = String.Join(" ", labelParts);
            } while (_store.ContainsKey(label));
            
            ;
            return (_store[label] = rule.CreateObject(label, genValue));
        }
    }
}