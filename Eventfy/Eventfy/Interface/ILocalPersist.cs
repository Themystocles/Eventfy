using Eventfy.Models;

namespace Eventfy.Interface
{
    public interface ILocalPersist
    {
        Task<IEnumerable<Local>> GetAllLocalAsync();
        Task<Local> GetLocalByIdAsync(int id);
        Task<Local> CreateLocalAsync(Local local);
        Task<Local> UpdateLocalAsync(Local local);
        Task<Local> DeleteLocalAsync(int id);

    }
}
