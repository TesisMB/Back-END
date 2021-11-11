using Back_End.Entities;
using Contracts.Interfaces;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class NotificationsRepository:RepositoryBase<Notifications>, INotificationsRepository
    {
        public NotificationsRepository(CruzRojaContext cruzRojaContext): base(cruzRojaContext)
        {

        }

        public void CreateNotifications(Notifications notifications)
        {
            Create(notifications);
        }
    }
}
