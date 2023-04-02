using Project_JanSupierz.Model;
using Project_JanSupierz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_JanSupierz.Repository
{
    public interface IBloonsTDRepository
    {
        Task<Tuple<List<Tower>,List<string>>> LoadTowersAsync();
        Task<Tower> GetTowerAsync(string id);
        Task<List<Tower>> GetTowersAsync(string type);
    }
}
