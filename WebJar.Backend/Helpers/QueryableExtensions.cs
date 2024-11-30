using WebJar.Shared.DTOs;

namespace WebJar.Backend.Helpers
{
    //Para que sea un metodo de extension la clase tienen que ser static para que no se pueda instanciar
    public static class QueryableExtensions
    {
        // Es generico y es una consulta no materializada, solo definida
        //this es para extender el metodo Paginate
        public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, PaginationDTO pagination)
        {
            return queryable
                .Skip((pagination.Page - 1) * pagination.RecordsNumber)  //Cuantos registros va a saltar
                .Take(pagination.RecordsNumber);  //Cuantos registros se van a tomar en cuente
        }
    }
}