using Back_End.Entities;
using Contracts.Interfaces;
using Entities.Models;

namespace Repository
{
    public class NotificationsRepository : RepositoryBase<Notifications>, INotificationsRepository
    {
        public NotificationsRepository(CruzRojaContext cruzRojaContext) : base(cruzRojaContext)
        {

        }

        public void CreateNotifications(Notifications notifications)
        {
            Create(notifications);
        }
    }
}
