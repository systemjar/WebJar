using WebJar.Backend.UnitOfWork.Interfaces;
using WebJar.Shared.Entities;
using WebJar.Shared.Entities.Conta;
using WebJar.Shared.Enums;

namespace WebJar.Backend.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUsuariosUnitOfWork _usuariosUnitOfWork;

        public SeedDb(DataContext context, IUsuariosUnitOfWork usuariosUnitOfWork)
        {
            _context = context;
            _usuariosUnitOfWork = usuariosUnitOfWork;
        }

        public async Task SeedAsync()
        {
            //Metodo que sirve para asegurarse de que haya base de datos, corre un update-database que corre todas las migraciones pendientes
            await _context.Database.EnsureCreatedAsync();

            //Chequear roles
            await CheckRolesAsync();

            //Crear un usuario admin para poder acceder al sistema
            await CheckUsersAsync("10111965", "Jorge", "Alcántara", "jar@yopmail.com", "322 311 4620", "Direccion", UserType.Admin);

            await CheckTiposConta();
        }

        //Esta implementado en usuariosUnitOfWork
        private async Task CheckRolesAsync()
        {
            await _usuariosUnitOfWork.CheckRoleAsync(UserType.Admin.ToString());
            await _usuariosUnitOfWork.CheckRoleAsync(UserType.Guest.ToString());
        }

        private async Task<Usuario> CheckUsersAsync(string document, string firstName, string lastName, string email, string phoneNumber, string address, UserType userType)
        {
            var user = await _usuariosUnitOfWork.GetUserAsync(email);

            if (user == null)
            {
                //string filePath;
                //if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                //{
                //    filePath = $"{Environment.CurrentDirectory}\\Images\\users\\{image}";
                //}
                //else
                //{
                //    filePath = $"{Environment.CurrentDirectory}/Images/users/{image}";
                //}
                //var fileBytes = File.ReadAllBytes(filePath);
                //var imagePath = await _fileStorage.SaveFileAsync(fileBytes, "jpg", "users");

                user = new Usuario
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phoneNumber,
                    Address = address,
                    Document = document,
                    UserType = userType,
                    //Photo = imagePath,
                };

                await _usuariosUnitOfWork.AddUserAsync(user, "123456");
                await _usuariosUnitOfWork.AddUserToRoleAsync(user, userType.ToString());

                var token = await _usuariosUnitOfWork.GenerateEmailConfirmationTokenAsync(user);
                await _usuariosUnitOfWork.ConfirmEmailAsync(user, token);
            }
            return user;
        }

        private async Task CheckTiposConta()
        {
            if (!_context.TiposConta.Any())
            {
                _context.TiposConta.Add(new TipoConta { Nombre = "CHEQUE" });
                _context.TiposConta.Add(new TipoConta { Nombre = "POLIZA" });
            }
            await _context.SaveChangesAsync();
        }
    }
}