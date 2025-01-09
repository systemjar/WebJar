using System.ComponentModel;

namespace WebJar.Shared.Enums
{
    public enum UserType
    {
        [Description("Administrador")] Admin,

        [Description("userConta")] UConta,
        [Description("adminConta")] AConta,

        [Description("userInve")] UInve,
        [Description("adminInve")] AInve,

        [Description("userPagar")] UPagar,
        [Description("adminPagar")] APagar,

        [Description("userCobrar")] UCobrar,
        [Description("adminCobrar")] ACobrar,

        [Description("userIva")] UIva,
        [Description("adminIva")] AIva,

        [Description("userActivos")] UActivos,
        [Description("adminActivos")] AActivos,

        [Description("userFactura")] UFactura,
        [Description("adminFactura")] AFactura,

        [Description("userPuntoV")] UPuntoV,
        [Description("adminPuntoV")] APuntoV,

        [Description("Invitado")] Guest
    }
}