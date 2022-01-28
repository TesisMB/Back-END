using Back_End.Entities;
using Contracts.Interfaces;
using Entities.Models;

namespace Repository
{
    public class MessageRepository: RepositoryBase<Messages>, IMessageRepository
    {
        public MessageRepository(CruzRojaContext context): base(context)
        {

        }

        public void CreateMessage(Messages messages)
        {
            Create(messages);
        }
    }
}
