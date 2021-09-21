using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Interfaces
{
    public interface IResources_RequestRepository: IRepositoryBase<Resources_Request>
    {
        Task<IEnumerable<Resources_Request>> GetAllResourcesRequest();

        void CreateResource_Resquest(Resources_Request resources_Request);
    }
}
