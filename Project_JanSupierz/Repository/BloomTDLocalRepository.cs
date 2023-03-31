using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Project_JanSupierz.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Project_JanSupierz.Repository
{
    internal class BloomTDLocalRepository : IBloomTDRepository
    {
        private static List<Tower> _towers;

        public async Task<Tower> GetTowerAsync()
        {
            if( _towers == null )
            {
                await GetTowersAsync();
            }

            return _towers.FirstOrDefault();
        }

        public async Task<List<Tower>> GetTowersAsync()
        {
            //Read only once
            if (_towers != null)
            {
                return _towers;
            }

            _towers = new List<Tower>();

            //Change path if in design mode
            string path = (DesignerProperties.GetIsInDesignMode(new DependencyObject())) ? "Resources/data/towers.json" : "../../Resources/data/towers.json";

            using (StreamReader sr = new StreamReader(path))
            {
                string json = await sr.ReadToEndAsync();

                JArray towerArray = JArray.Parse(json);

                //Load Upgrades
                foreach (JObject towerObject in towerArray)
                {
                    Tower tower = towerObject.ToObject<Tower>();
                    JObject pathObject = towerObject.SelectToken("paths").ToObject<JObject>();

                    //Add upgrade paths
                    tower.Paths.Add(pathObject.SelectToken("path1").ToObject<List<UpgradePath>>());
                    tower.Paths.Add(pathObject.SelectToken("path2").ToObject<List<UpgradePath>>());
                    tower.Paths.Add(pathObject.SelectToken("path3").ToObject<List<UpgradePath>>());

                    //Save id for the upgrade images
                    for (int index = 0; index < tower.Paths.Count; index++)
                    {
                        string zerosFront = "".PadLeft(2 - index, '0');
                        string zerosEnd = "".PadLeft(index, '0');

                        int counter = 1;
                        tower.Paths[index].ForEach(pathUpgrade => pathUpgrade.Id = $"{tower.Id}/{zerosFront}{counter++.ToString()}{zerosEnd}");
                    }

                    _towers.Add(tower);
                }
            }

            return _towers;
        }
    }
}
