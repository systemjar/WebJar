using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Security;
using System.Globalization;
using WebJar.Backend.Data;
using WebJar.Backend.Repositories.Interfaces.Conta;

namespace WebJar.Backend.Repositories.Implementations.Conta
{
    public class ActualizarContaRepository : IActualizarContaRepository
    {
        private readonly DataContext _context;

        private DateTime FechaFinalMes { get; set; }

        public ActualizarContaRepository(DataContext context)
        {
            _context = context;
        }

        public async Task ActualizarConta(int empresaId, int elMes, int elYear)
        {
            FechaFinalMes = ObtenerUltimoDia(elMes, elYear);

            using (var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable))
            {
                try
                {
                    await _context.Cuentas.ExecuteUpdateAsync(cuenta =>
                                    cuenta.SetProperty(c => c.Cargos, 0)
                                          .SetProperty(c => c.Abonos, 0)
                                          .SetProperty(c => c.CargosMes, 0)
                                          .SetProperty(c => c.AbonosMes, 0)
                                          .SetProperty(c => c.SaldoMes, 0));

                    //Obtener los Poliza.Detalles y agruparlos por codigo
                    var detallesAgrupados = await _context.Detalles
                                                    .Include(d => d.Poliza)
                                                    .GroupBy(d => d.Codigo)
                                                    .ToListAsync();

                    foreach (var grupo in detallesAgrupados)
                    {
                        var codigo = grupo.Key;

                        //Calcular los valores
                        var cargos = grupo
                                    .Where(d => d.Poliza.Fecha <= FechaFinalMes)
                                    .Sum(d => d.Debe);

                        var abonos = grupo
                                   .Where(d => d.Poliza.Fecha <= FechaFinalMes)
                                   .Sum(d => d.Haber);

                        var cargosMes = grupo
                                    .Where(d => d.Poliza.Fecha.Month == elMes && d.Poliza.Fecha.Year == elYear)
                                    .Sum(d => d.Debe);

                        var abonosMes = grupo
                                    .Where(d => d.Poliza.Fecha.Month == elMes && d.Poliza.Fecha.Year == elYear)
                                    .Sum(d => d.Haber);

                        //Obtener la cuenta a actualizar
                        var queCuenta = await _context.Cuentas
                                            .FirstOrDefaultAsync(c => c.Codigo == codigo);

                        if (queCuenta != null)
                        {
                            //Actualizar valores de la cuenta
                            queCuenta.Cargos = cargos;
                            queCuenta.Abonos = abonos;
                            queCuenta.CargosMes = cargosMes;
                            queCuenta.AbonosMes = abonosMes;

                            //Actualizar saldo inicial del mes
                            if (queCuenta.DebeHaber == "D")
                                queCuenta.SaldoMes = ((cargos - abonos) - (cargosMes - abonosMes));
                            else
                                queCuenta.SaldoMes = ((abonos - cargos) - (abonosMes - cargosMes));

                            //Guardamos los cambios
                            _context.Cuentas.Update(queCuenta);
                        }
                    }

                    #region Rutina niveles anterior

                    ////Actualizar saldos en cuentas mayores
                    //// Obtener la suma de las cuentas hijas agrupadas por CuentaMayor
                    //// Obtener todas las cuentas
                    //var todasLasCuentas = await _context.Cuentas.ToListAsync();

                    //// Ordenar las cuentas por Codigo en orden descendente
                    //var cuentasOrdenadas = todasLasCuentas
                    //    .OrderByDescending(c => c.CodigoMayor)
                    //    .ToList();

                    //// Agrupar las cuentas por CuentaMayor
                    //var cuentasAgrupadas = cuentasOrdenadas
                    //    .Where(c => c.CodigoMayor != null) // Filtrar cuentas que tienen un CuentaMayor
                    //    .GroupBy(c => c.CodigoMayor)
                    //    .ToList();

                    //foreach (var grupo in cuentasAgrupadas)
                    //{
                    //    // Obtener la cuenta padre (cuenta mayor)
                    //    var cuentaPadre = todasLasCuentas
                    //        .FirstOrDefault(c => c.Codigo == grupo.Key);

                    //    if (cuentaPadre != null)
                    //    {
                    //        // Sumar los valores de las cuentas hijas
                    //        cuentaPadre.Cargos = grupo.Sum(c => c.Cargos);
                    //        cuentaPadre.Abonos = grupo.Sum(c => c.Abonos);
                    //        cuentaPadre.CargosMes = grupo.Sum(c => c.CargosMes);
                    //        cuentaPadre.AbonosMes = grupo.Sum(c => c.AbonosMes);
                    //        cuentaPadre.SaldoMes = grupo.Sum(c => c.SaldoMes);

                    //        // Marcar la cuenta padre como modificada
                    //        _context.Cuentas.Update(cuentaPadre);
                    //    }
                    //}

                    #endregion Rutina niveles anterior

                    // Obtener todas las cuentas
                    var todasLasCuentas = await _context.Cuentas.ToListAsync();

                    // Ordenar las cuentas por Codigo en orden descendente
                    var cuentasOrdenadas = todasLasCuentas
                        .OrderByDescending(c => c.CodigoMayor)
                        .ToList();

                    // Agrupar las cuentas por CuentaMayor
                    var cuentasAgrupadas = cuentasOrdenadas
                        .Where(c => c.CodigoMayor != null) // Filtrar cuentas que tienen un CuentaMayor
                        .GroupBy(c => c.CodigoMayor)
                        .ToList();

                    foreach (var grupo in cuentasAgrupadas)
                    {
                        // Obtener la cuenta padre (cuenta mayor)
                        var cuentaPadre = todasLasCuentas
                            .FirstOrDefault(c => c.Codigo == grupo.Key);

                        if (cuentaPadre != null)
                        {
                            // Inicializar acumuladores
                            decimal saldoMesAcumulado = 0;
                            decimal cargosAcumulados = 0;
                            decimal abonosAcumulados = 0;
                            decimal cargosMesAcumulados = 0;
                            decimal abonosMesAcumulados = 0;

                            // Recorrer las cuentas hijas
                            foreach (var cuentaHija in grupo)
                            {
                                // Sumar o restar el SaldoMes dependiendo del DebeHaber
                                if (cuentaHija.DebeHaber == "D")
                                {
                                    saldoMesAcumulado += cuentaHija.SaldoMes;
                                }
                                else if (cuentaHija.DebeHaber == "H")
                                {
                                    saldoMesAcumulado -= cuentaHija.SaldoMes;
                                }

                                // Acumular los demás campos
                                cargosAcumulados += cuentaHija.Cargos;
                                abonosAcumulados += cuentaHija.Abonos;
                                cargosMesAcumulados += cuentaHija.CargosMes;
                                abonosMesAcumulados += cuentaHija.AbonosMes;
                            }

                            // Asignar los valores acumulados a la cuenta padre
                            cuentaPadre.Cargos = cargosAcumulados;
                            cuentaPadre.Abonos = abonosAcumulados;
                            cuentaPadre.CargosMes = cargosMesAcumulados;
                            cuentaPadre.AbonosMes = abonosMesAcumulados;
                            cuentaPadre.SaldoMes = saldoMesAcumulado;

                            // Marcar la cuenta padre como modificada
                            _context.Cuentas.Update(cuentaPadre);
                        }
                    }

                    //Guardar los cambios en la base de dato
                    await _context.SaveChangesAsync();

                    //Confirmar la transaccion
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    // Revertir la transacción en caso de error
                    await transaction.RollbackAsync();
                    throw new Exception("Error al actualizar las cuentas: " + ex.Message);
                }
            }
        }

        private DateTime ObtenerUltimoDia(int queMes, int queYear)
        {
            // Obtener el último día del mes seleccionado
            int lastDay = DateTime.DaysInMonth(queYear, queMes);

            // Formar la fecha en el formato deseado (dd/MM/yyyy)
            string FormattedDate = $"{lastDay:D2}/{queMes:D2}/{queYear}";
            return DateTime.ParseExact(FormattedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        }
    }
}