using Microsoft.EntityFrameworkCore;
using SistemaVenta.DAL.DBContext;
using SistemaVenta.DAL.Repositorios.Contrato;
using System.Linq.Expressions;

namespace SistemaVenta.DAL.Repositorios
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DBVENTAContext _dbcontext;

        public GenericRepository(DBVENTAContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<T> Obtener(Expression<Func<T, bool>> filtro)
        {
            try
            {
                return await this._dbcontext.Set<T>().FirstOrDefaultAsync(filtro);
            }
            catch
            {
                throw;
            }
        }

        public async Task<T> Crear(T modelo)
        {
            try
            {
                this._dbcontext.Set<T>().Add(modelo);
                await this._dbcontext.SaveChangesAsync();
                return modelo;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Existe(Expression<Func<T, bool>> expression)
        {
            try
            {
                return await this._dbcontext.Set<T>().AnyAsync(expression);
            }
            catch 
            {
                throw;
            }
        }

        public async Task<bool> Editar(T modelo)
        {
            try
            {
                // Desvincula la entidad actual de su seguimiento, si ya está en el contexto
                var existeT = await this.Obtener(a => a == modelo);

                if (existeT != null)
                {
                    _dbcontext.Entry(existeT).State = EntityState.Detached;
                }
                _dbcontext.Set<T>().Update(modelo);
                await _dbcontext.SaveChangesAsync(); 
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(T modelo)
        {
            try
            {
                this._dbcontext.Set<T>().Remove(modelo);
                await this._dbcontext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IQueryable<T>> Consultar(Expression<Func<T, bool>> filtro = null)
        {
            try
            {
                IQueryable<T> queryModelo = filtro == null
                    ? this._dbcontext.Set<T>()
                    : this._dbcontext.Set<T>().Where(filtro);
                return queryModelo;
            }
            catch
            {
                throw;
            }
        }

    }
}
