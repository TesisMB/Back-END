using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Interfaces
{
    public interface INotificationsRepository: IRepositoryBase<Notifications>
    {
        void CreateNotifications(Notifications notifications);
    }
}
