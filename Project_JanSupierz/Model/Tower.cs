using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_JanSupierz.Model
{
    public class Cost
    {
        public int Easy { get; set; }
        public int Medium { get; set; }
        public int Hard { get; set; }
        public int Impoppable { get; set; }
    }

    public class Statistics
    {
        public string Damage { get; set; }
        public string Pierce { get; set; }
        public string AttackSpeed { get; set; }
        public string Range { get; set; }
        public string Type { get; set; }
    }

    public class Tower
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }

        public int Footprint { get; set; }
        public string DefaultHotkey { get; set; }

        public Cost Cost { get; set; }
        public Statistics Stats { get; set; }
    }
}
