using Project_JanSupierz.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_JanSupierz.Repository
{
    internal interface IBloomTDRepository
    {
        Task<List<Tower>> GetTowersAsync();
        Task<Tower> GetTowerAsync();
    }
}
