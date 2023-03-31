﻿using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Project_JanSupierz.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Net;

namespace Project_JanSupierz.Repository
{
    internal class BloonsTDApiRepository : IBloonsTDRepository
    {
        private List<Tower> _towers;

        public async Task<Tower> GetTowerAsync()
        {
            if(_towers == null)
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

            string endpoint = "https://statsnite.com/api/btd/v3/towers";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync(endpoint);

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new HttpRequestException(response.ReasonPhrase);
                    }

                    string json = await response.Content.ReadAsStringAsync();

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
                            //Add zeros
                            string zerosFront = "".PadLeft(2 - index, '0');
                            string zerosEnd = "".PadLeft(index, '0');

                            int counter = 1;
                            tower.Paths[index].ForEach(pathUpgrade => pathUpgrade.Id = $"{tower.Id}/{zerosFront}{counter++.ToString()}{zerosEnd}");
                        }

                        _towers.Add(tower);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Loading towers failed!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                //await Task.Run(() =>
                //{
                //    foreach (Tower tower in _towers)
                //    {
                //        foreach (List<UpgradePath> towerPath in tower.Paths)
                //        {
                //            foreach (UpgradePath upgradePath in towerPath)
                //            {
                //                string url = $"https://statsnite.com/images/btd/towers/{upgradePath.Id}.png"; // the URL of the image to download
                //                string savePath = $@"../../Resources/Towers/{upgradePath.Id}.png"; // the path to save the downloaded image
                //
                //                if (!Directory.Exists($@"../..Resources/Towers/{tower.Id})"))
                //                {
                //                    Directory.CreateDirectory($@"../../Resources/Towers/{upgradePath.Id}");
                //                }
                //
                //                using (WebClient clientt = new WebClient())
                //                {
                //                    clientt.DownloadFile(url, savePath);
                //                }
                //            }
                //        }
                //    }
                //});


                return _towers;
            }
        }
    }
}
