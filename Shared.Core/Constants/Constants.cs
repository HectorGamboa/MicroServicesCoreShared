namespace Shared.Core.Constants
{
    public static class Constants
    {
        #region Messages
        public static string MESSAGE_QUERY = "Consulta exitosa.";
        public static string MESSAGE_QUERY_EMPTY = "No se encontraron registros.";
        public static string MESSAGE_SAVE = "Se registró correctamente.";
        public static string MESSAGE_UPDATE = "Se actualizó correctamente.";
        public static string MESSAGE_DELETE = "Se eliminó correctamente.";
        public static string MESSAGE_EXISTS = "El registro ya existe.";
        public static string MESSAGE_ACTIVATE = "El registro ha sido activado.";
        public static string MESSAGE_TOKEN = "Token generado correctamente.";
        public static string MESSAGE_TOKEN_ERROR = "El usuario y/o contraseña es incorrecta, compruébala.";
        public static string MESSAGE_VALIDATE = "Errores de validación.";
        public static string MESSAGE_FAILED = "Operación fallida.";
        public static string MESSAGE_EXCEPTION = "Hubo un error inesperado, comunicarse con el administrador (admin@gmail.com).";
        public static string MESSAGE_GOOGLE_ERROR = "Su cuenta no se encuentra registrada en el sistema.";
        public static string MESSAGE_AUTH_TYPE_GOOGLE = "Por favor, ingrese con la opción de Google.";
        public static string MESSAGE_AUTH_TYPE = "Su cuenta no se encuentra registrada en el sistema.";
        #endregion

        #region Modules
        public static int Module_Dashboard = 1;
        public static int Module_Users = 2;
        public static int Module_Roles = 3;
        public static int Module_Permissions = 4;
        public static int Module_Modules = 5;
        #endregion

    }
}
