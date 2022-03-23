using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Interfaces
{
    public interface IResources_RequestRepository : IRepositoryBase<ResourcesRequest>
    {
        Task<IEnumerable<ResourcesRequest>> GetAllResourcesRequest(string Condition);

        Task<ResourcesRequest> GetResourcesRequestByID(int resource);
        ResourcesRequest UpdateStockDelete(ResourcesRequest resource);

        void CreateResource_Resquest(ResourcesRequest resources_Request);
        void AcceptRejectRequest(ResourcesRequest resources_Request, int UserRequestID);
    }
}
