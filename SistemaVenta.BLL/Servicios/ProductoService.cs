using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DAL.Repositorios.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.Model;

namespace SistemaVenta.BLL.Servicios
{
    public class ProductoService : IProductoService
    {
        private readonly IGenericRepository<Producto> _productoRepository;
        private readonly IMapper _mapper;

        public ProductoService(IGenericRepository<Producto> productoRepository, IMapper mapper)
        {
            _productoRepository = productoRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductoDTO>> Lista()
        {
            try
            {
                var queryProducto = await this._productoRepository.Consultar();
                var listaProductos = queryProducto.Include(cat => cat.IdCategoriaNavigation).ToList();
                return this._mapper.Map<List<ProductoDTO>>(listaProductos);
            }
            catch
            {
                throw;
            }
        }

        public async Task<ProductoDTO> Crear(ProductoDTO modelo)
        {
            try
            {
                var productoCreado = await this._productoRepository.Crear(this._mapper.Map<Producto>(modelo));

                if(productoCreado.IdProducto == 0)
                {
                    throw new TaskCanceledException("No se pudo crear");
                }

                return this._mapper.Map<ProductoDTO>(productoCreado);
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Editar(ProductoDTO modelo)
        {
            try
            {
                var productoModelo = this._mapper.Map<Producto>(modelo);
                var productoEncontrado = await this._productoRepository.Obtener(p =>
                        p.IdProducto == productoModelo.IdProducto
                    );

                if(productoEncontrado == null)
                {
                    throw new TaskCanceledException("El producto no existe");
                }

                productoEncontrado.Nombre = productoModelo.Nombre;
                productoEncontrado.IdCategoria = productoModelo.IdCategoria;
                productoEncontrado.Stock = productoModelo.Stock;
                productoEncontrado.Precio = productoModelo.Precio;
                productoEncontrado.EsActivo = productoModelo.EsActivo;

                var respuesta = await this._productoRepository.Editar(productoEncontrado);

                if(!respuesta)
                {
                    throw new TaskCanceledException("No se pudo editar.");
                }

                return respuesta;
            }
            catch 
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var productoEncontrado = await this._productoRepository.Obtener(p => p.IdProducto == id);

                if(productoEncontrado == null)
                {
                    throw new TaskCanceledException("El producto no existe.");
                }

                var respuesta = await this._productoRepository.Eliminar(productoEncontrado);

                if(!respuesta)
                {
                    throw new TaskCanceledException("No se pudo editar.");
                }

                return respuesta;
            }
            catch
            {
                throw;
            }
        }
    }
}
