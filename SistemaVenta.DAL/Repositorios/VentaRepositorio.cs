using SistemaVenta.DAL.DBContext;
using SistemaVenta.DAL.Repositorios.Contrato;
using SistemaVenta.Model;
using System.Data;

namespace SistemaVenta.DAL.Repositorios
{
    public class VentaRepositorio : GenericRepository<Venta>, IVentaRepository
    {
        private readonly DBVENTAContext _dbcontext;

        public VentaRepositorio(DBVENTAContext dbcontext) : base(dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<Venta> Registrar(Venta modelo)
        {
            Venta ventaGenerada = new Venta();

            using(var transaction = this._dbcontext.Database.BeginTransaction())
            {
                try
                {
                    // Productos dentro de la venta
                    foreach(DetalleVenta dv in modelo.DetalleVenta)
                    {
                        Producto productoEncontrado = this._dbcontext.Productos.Where(p => p.IdProducto == dv.IdProducto).First();
                        productoEncontrado.Stock -= dv.Cantidad;

                        this._dbcontext.Productos.Update(productoEncontrado);
                    }
                    await this._dbcontext.SaveChangesAsync();

                    // Numero Documento
                    NumeroDocumento correlativo = this._dbcontext.NumeroDocumentos.First();
                    correlativo.UltimoRegistro++;
                    correlativo.FechaRegistro = DateTime.Now;

                    this._dbcontext.NumeroDocumentos.Update(correlativo);
                    await this._dbcontext.SaveChangesAsync();

                    int cantidadDigitos = 4;
                    string ceros = string.Concat(Enumerable.Repeat("0", cantidadDigitos));
                    string numeroVenta = ceros + correlativo.UltimoRegistro.ToString();

                    // 00001
                    numeroVenta = numeroVenta.Substring(numeroVenta.Length - cantidadDigitos, cantidadDigitos);

                    modelo.NumeroDocumento = numeroVenta;

                    await this._dbcontext.AddAsync(modelo);
                    await this._dbcontext.SaveChangesAsync();

                    ventaGenerada = modelo;

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
                return ventaGenerada;
            }
        }
    }
}