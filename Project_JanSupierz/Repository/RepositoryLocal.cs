using Newtonsoft.Json.Linq;
using Project_JanSupierz.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_JanSupierz.Repository
{
    public class ReposioryLocal
    {
        private static List<Tower> _towers;

        public static List<Tower> GetTowers()
        {
            if (_towers != null)
            {
                return _towers;
            }

            _towers = new List<Tower>();

            //Read only once
            string json = File.ReadAllText("../../Resources/Data/towers.json");
            JArray towerArray = JArray.Parse(json);

            foreach (JObject towerObject in towerArray) 
            {
                Tower tower = towerObject.ToObject<Tower>();
                JObject pathObject = towerObject.SelectToken("paths").ToObject<JObject>();

                tower.Paths.Add("path1", pathObject.SelectToken("path1").ToObject<List<PathUpgrade>>());
                tower.Paths.Add("path2", pathObject.SelectToken("path2").ToObject<List<PathUpgrade>>());
                tower.Paths.Add("path3", pathObject.SelectToken("path3").ToObject<List<PathUpgrade>>());

                _towers.Add(tower);
            }

            return _towers;
        }
    }

}
