using System.Threading.Tasks;

namespace TP.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
