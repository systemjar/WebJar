namespace WebJar.Frontend.Pages.Conta.Actualizar
{
    public partial class ActualizarFecha
    {
        private int Month { get; set; } = DateTime.Now.Month;
        private int Year { get; set; } = DateTime.Now.Year;
        private string FormattedDate { get; set; }

        private void ObtenerUltimoDia()
        {
            // Obtener el último día del mes seleccionado
            int lastDay = DateTime.DaysInMonth(Year, Month);

            // Formar la fecha en el formato deseado (dd/MM/yyyy)
            FormattedDate = $"{lastDay:D2}/{Month:D2}/{Year}";
        }
    }
}