using Eventfy.Models.DTOs;
using Eventfy.Models;

namespace Eventfy.Interface.Interface_Services
{
    public interface ILocalServices
    {
        Task<IEnumerable<Local>> GetAllLocalsAsync();
        Task<Local> GetLocalByIdAsync(int id);
        Task<Local> CreateLocalAsync(LocalDto localDto);
        Task<Local> UpdateLocalAsync(LocalDto localDto);
        Task<bool> DeleteLocal(int id);
    }
}
