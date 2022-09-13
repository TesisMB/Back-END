using Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Interfaces
{
    public interface IResources_RequestRepository : IRepositoryBase<ResourcesRequest>
    {
        Task<IEnumerable<ResourcesRequest>> GetAllResourcesRequest(int userId, string Condition, string state);
        Task<IEnumerable<ResourcesRequest>> GetAllResourcesRequest(int userId, string Condition);
        Task<IEnumerable<ResourcesRequest>> GetAllResourcesRequestPDF(int userId, string Condition, int emergency, DateTime dateConvert, DateTime dateConvertEnd);

        Task<ResourcesRequest> GetResourcesRequestByID(int resource);
        ResourcesRequest UpdateStockDelete(ResourcesRequest resource);

        void CreateResource_Resquest(ResourcesRequest resources_Request);
        void AcceptRejectRequest(ResourcesRequest resources_Request, int UserRequestID);
    }
}
