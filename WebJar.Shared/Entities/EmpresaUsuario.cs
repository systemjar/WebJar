namespace WebJar.Shared.Entities
{
    public class EmpresaUsuario
    {
        public int EmpresaId { get; set; }
        public Empresa? Empresa { get; set; }
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
    }
}