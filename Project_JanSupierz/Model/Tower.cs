using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_JanSupierz.Model
{
    public class Cost : ICloneable
    {
        public int Easy { get; set; }
        public int Medium { get; set; }
        public int Hard { get; set; }
        public int Impoppable { get; set; }

        public object Clone()
        {
            Cost newCost = new Cost
            {
                Easy = Easy,
                Medium = Medium,
                Hard = Hard,
                Impoppable = Impoppable
            };

            return newCost;
        }
    }

    public class Statistics : ICloneable
    {
        public string Damage { get; set; }
        public string Pierce { get; set; }
        public string AttackSpeed { get; set; }
        public string Range { get; set; }
        public string Type { get; set; }

        public object Clone()
        {
            Statistics newStats = new Statistics
            {
                Damage = Damage,
                Pierce = Pierce,
                AttackSpeed = AttackSpeed,
                Range = Range,
                Type = Type
            };

            return newStats;
        }
    }

    public class Tower : ICloneable
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }

        public int Footprint { get; set; }
        public string DefaultHotkey { get; set; }

        public Cost Cost { get; set; }
        public Statistics Stats { get; set; }

        [JsonIgnore]
        public List<List<Upgrade>> Paths { get; set; } = new List<List<Upgrade>>();

        public object Clone()
        {
            // Create a new instance of the Tower class
            Tower newTower = new Tower
            {
                Id = Id,
                Name = Name,
                Description = Description,
                Type = Type,
                Footprint = Footprint,
                DefaultHotkey = DefaultHotkey,
                Cost = (Cost)Cost.Clone(),
                Stats = (Statistics)Stats.Clone(),
                Paths = new List<List<Upgrade>>()
            };

            // Create deep copies of the Lists in the Paths property
            foreach (List<Upgrade> path in Paths)
            {
                List<Upgrade> newPath = new List<Upgrade>();

                foreach (Upgrade upgrade in path)
                {
                    newPath.Add((Upgrade)upgrade.Clone());
                }

                newTower.Paths.Add(newPath);
            }

            return newTower;
        }
    }

    public class Upgrade : ICloneable
    {
        [JsonIgnore]
        public string Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public Cost Cost { get; set; }
        public int UnlockXp { get; set; }
        public List<string> Effects { get; set; }

        public object Clone()
        {
            Upgrade newUpgrade = new Upgrade
            {
                Id = Id,
                Name = Name,
                Description = Description,
                Cost = (Cost)Cost.Clone(),
                UnlockXp = UnlockXp,
                Effects = new List<string>()
            };

            // Create deep copy of the Effects list
            foreach (string effect in Effects)
            {
                newUpgrade.Effects.Add(effect);
            }

            return newUpgrade;
        }
    }
}
