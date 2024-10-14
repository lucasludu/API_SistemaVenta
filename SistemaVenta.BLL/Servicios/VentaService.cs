using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DAL.Repositorios.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.Model;
using System.Globalization;

namespace SistemaVenta.BLL.Servicios
{
    public class VentaService : IVentaService
    {
        private readonly IVentaRepository _ventaRepositorio;
        private readonly IGenericRepository<DetalleVenta> _detalleVentaRepositorio;
        private readonly IMapper _mapper;

        public VentaService(IVentaRepository ventaRepositorio, IGenericRepository<DetalleVenta> detalleVentaRepositorio, IMapper mapper)
        {
            this._ventaRepositorio = ventaRepositorio;
            this._detalleVentaRepositorio = detalleVentaRepositorio;
            this._mapper = mapper;
        }

        public async Task<VentaDTO> Registrar(VentaDTO modelo)
        {
            try
            {
                var ventaGenerada = await this._ventaRepositorio.Registrar(_mapper.Map<Venta>(modelo));

                if(ventaGenerada.IdVenta == 0)
                {
                    throw new TaskCanceledException("No se pudo crear.");
                }

                return this._mapper.Map<VentaDTO>(ventaGenerada);
            }
            catch 
            {
                throw;
            }
        }

        public async Task<List<VentaDTO>> Historial(string buscarPor, string numeroVenta, string fechaInicio, string fechaFin)
        {
            var query = await this._ventaRepositorio.Consultar();
            var listaResultado = new List<Venta>();
            try
            {
                if(buscarPor == "fecha")
                {
                    var fecha_inicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-AR"));
                    var fecha_fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-AR"));

                    listaResultado = await query
                        .Where(v =>
                            v.FechaRegistro.Value.Date >= fecha_inicio.Date &&
                            v.FechaRegistro.Value.Date <= fecha_fin.Date 
                        )
                        .Include(dv => dv.DetalleVenta)
                        .ThenInclude(p => p.IdProductoNavigation)
                        .ToListAsync();
                }
                else
                {
                   listaResultado = await query
                        .Where(v => v.NumeroDocumento == numeroVenta)
                        .Include(dv => dv.DetalleVenta)
                        .ThenInclude(p => p.IdProductoNavigation)
                        .ToListAsync();
                }
            }
            catch
            {
                throw;
            }
            return this._mapper.Map<List<VentaDTO>>(listaResultado);
        }

        public async Task<List<ReporteDTO>> Reporte(string fechaInicio, string fechaFin)
        {
            var query = await this._detalleVentaRepositorio.Consultar();
            var listaResultado = new List<DetalleVenta>();

            try
            {
                var fecha_inicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-AR"));
                var fecha_fin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-AR"));

                listaResultado = await query
                    .Include(p => p.IdProductoNavigation)
                    .Include(v => v.IdVentaNavigation)
                    .Where(dv =>
                        dv.IdVentaNavigation.FechaRegistro.Value.Date >= fecha_inicio.Date &&
                        dv.IdVentaNavigation.FechaRegistro.Value.Date <= fecha_fin.Date
                    )
                    .ToListAsync();
            }
            catch
            {
                throw;
            }
            return this._mapper.Map<List<ReporteDTO>>(listaResultado);
        }
    }
}
