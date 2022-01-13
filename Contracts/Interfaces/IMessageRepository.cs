using Entities.Models;

namespace Contracts.Interfaces
{
    public interface IMessageRepository: IRepositoryBase<Messages>
    {
        void CreateMaterial(Messages messages);

    }
}
