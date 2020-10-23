using Back_End.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_End.Services
{

    public class MaterialsRepository : ICruzRojaRepository<Materials>, IDisposable
    {
        public readonly CruzRojaContext2 _context;

        public MaterialsRepository(CruzRojaContext2 context)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
        }
        public void Add(Materials entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Materials entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Materials> GetList()
        {
            throw new NotImplementedException();
        }

        public Materials GetListId(int EntityID)
        {
            throw new NotImplementedException();
        }

        public bool save()
        {
            throw new NotImplementedException();
        }

        public void Update(Materials entity)
        {
            throw new NotImplementedException();
        }
    }
}
