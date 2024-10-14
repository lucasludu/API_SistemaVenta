using AutoMapper;
using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DAL.Repositorios.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.Model;
using System.Globalization;

namespace SistemaVenta.BLL.Servicios
{
    public class DashboardService : IDashboardService
    {
        private readonly IVentaRepository _ventaRepositorio;
        private readonly IGenericRepository<Producto> _productoRepositorio;
        private readonly IMapper _mapper;

        private const int DiasUltimaSemana = -7;

        public DashboardService(IVentaRepository ventaRepositorio, IGenericRepository<Producto> productoRepositorio, IMapper mapper)
        {
            _ventaRepositorio = ventaRepositorio;
            _productoRepositorio = productoRepositorio;
            _mapper = mapper;
        }

        /// <summary>
        /// Ver Parte 05 1:00:00
        /// https://www.youtube.com/watch?v=J9XqtohVnWk&list=PLx2nia7-PgoA1-y-qrxCQii0-EmC4JZ5e&index=5
        /// </summary>
        /// <param name="tablaVenta"></param>
        /// <param name="restarCantidadDias"></param>
        /// <returns></returns>
        private IQueryable<Venta> RetornarVentas(IQueryable<Venta> tablaVenta, int restarCantidadDias)
        {
            DateTime? ultimaFecha = tablaVenta.OrderByDescending(v => v.FechaRegistro).Select(v => v.FechaRegistro).First();

            ultimaFecha = ultimaFecha.Value.AddDays(restarCantidadDias);

            return tablaVenta.Where(v => v.FechaRegistro.Value.Date >= ultimaFecha.Value.Date);
        }

        private async Task<int> TotalVentasUltimaSemana()
        {
            int total = 0;
            IQueryable<Venta> _ventaQuery = await this._ventaRepositorio.Consultar();

            if(_ventaQuery.Count() > 0)
            {
                var tablaVenta = this.RetornarVentas(_ventaQuery, DiasUltimaSemana);
                total = tablaVenta.Count();
            }
            return total;
        }

        private async Task<string> TotalIngresosUltimaSemana()
        {
            decimal resultado = 0;
            IQueryable<Venta> _ventaQuery = await this._ventaRepositorio.Consultar();

            if(_ventaQuery.Count() > 0)
            {
                var tablaVenta = this.RetornarVentas(_ventaQuery, DiasUltimaSemana);
                resultado = tablaVenta.Select(v => v.Total).Sum(v => v.Value);
            }

            return Convert.ToString(resultado, new CultureInfo("es-AR"));
        }

        private async Task<int> TotalProductos()
        {
            IQueryable<Producto> _productoQuery = await this._productoRepositorio.Consultar();
            int total = _productoQuery.Count();

            return total;
        }

        private async Task<Dictionary<string, int>> VentasUltimaSemana()
        {
            Dictionary<string, int> resultado = new Dictionary<string, int>();

            IQueryable<Venta> _ventaQuery = await this._ventaRepositorio.Consultar();

            if(_ventaQuery.Count() > 0)
            {
                var tablaVenta = this.RetornarVentas(_ventaQuery, DiasUltimaSemana);
                resultado = tablaVenta
                    .GroupBy(v => v.FechaRegistro.Value.Date)
                    .OrderBy(g => g.Key)
                    .Select(dv => new {
                        fecha = dv.Key.ToString("dd/MM/yyyy"), 
                        total = dv.Count()
                    })
                    .ToDictionary(keySelector: r => r.fecha, elementSelector: r => r.total);
            }
            return resultado;
        }

        public async Task<DashboardDTO> Resumen()
        {
            DashboardDTO vmDashboard = new DashboardDTO();

            try
            {
                vmDashboard.TotalVentas = await this.TotalVentasUltimaSemana();
                vmDashboard.TotalIngresos = await this.TotalIngresosUltimaSemana();
                vmDashboard.TotalProdcutos = await this.TotalProductos();

                List<VentasSemanalDTO> listaVentaSemana = new List<VentasSemanalDTO>();

                foreach(KeyValuePair<string, int> item in await this.VentasUltimaSemana())
                {
                    listaVentaSemana.Add(new VentasSemanalDTO()
                    {
                        Fecha = item.Key,
                        Total = item.Value
                    });
                }
                vmDashboard.VentasUltimaSemana = listaVentaSemana;
            }
            catch 
            {
                throw;
            }

            return vmDashboard;
        }
    }
}
