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
        Task<List<Tower>> GetTowersAsync();
        Task<Tower> GetTowerAsync(string id);
        Task<List<string>> GetTowerTypesAsync();
        Task<List<Tower>> GetTowersAsync(string type);
    }
}
