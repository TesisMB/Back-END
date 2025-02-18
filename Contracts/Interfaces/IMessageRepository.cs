﻿using Entities.Models;

namespace Contracts.Interfaces
{
    public interface IMessageRepository: IRepositoryBase<Messages>
    {
        void CreateMessage(Messages messages);

    }
}
