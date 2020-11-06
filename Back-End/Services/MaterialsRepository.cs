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

        public void Add(Materials materials)
        {
            //Verifico que el Usuario no sea null
            if (materials == null)
            {
                throw new ArgumentNullException(nameof(materials));
            }

            _context.Materials.Add(materials);
        }

        public void Delete(Materials materials)
        {
            if (materials == null) //Verifico que el Usuario no sea null
            {
                throw new ArgumentNullException(nameof(materials));
            }
            //Se retorna al Controller que no hay errores
            _context.Materials.Remove(materials);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {

        }

        public IEnumerable<Materials> GetList()
        {
            //retorno la lista de usuarios con el nombre del rol especifico al que pertence cada uno
            return _context.Materials
                    .ToList();
        }

        public Materials GetListId(int MaterialID)
        {
            if (MaterialID.ToString() == "") // si el usuario esta vacio
            {
                throw new ArgumentNullException(nameof(MaterialID));
            }

            //retorno un Usuario especifico con el nombre del rol al cual pertence el mismo
            return _context.Materials
                 .FirstOrDefault(a => a.MaterialID == MaterialID);
        }

        public bool save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void Update(Materials entity)
        {
            throw new NotImplementedException();
        }
    }
}
