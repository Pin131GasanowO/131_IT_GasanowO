using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Autoshop.Models.Autoshop
{
    public class AutoshopGenerator
    {
        private Dictionary<string, Car> _store = new Dictionary<string, Car>();
        private Random random;

        public AutoshopGenerator(int seed = 1)
        {
            random = new Random(seed);
            Rules = new List<Rule>();
        }

        public class Rule
        {
            public string Type { get; set; }
            public IEnumerable<IEnumerable<string>> SubLabels { get; set; } 
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
                genValue = random.Next();
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

            Car p = new Car() { 
                Label = label,
                Price = 100,
                Balance = 100
            };

            _store[label] = p;
            return p;
        }
    }
}