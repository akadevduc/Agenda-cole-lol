namespace Entidades
{
    public class Persona
    {
        private int _dni;
        private string _nombre;
        private string _estadoPersona;
        private string _estadoCivil;
        private string _estadoCuenta;

        public int Dni
        {
            get => _dni;
            set => _dni = value <= 0
                ? throw new ArgumentException("El DNI debe ser un número positivo.")
                : value;
        }

        public string Nombre
        {
            get => _nombre;
            set => _nombre = string.IsNullOrWhiteSpace(value)
                ? throw new ArgumentException("El nombre no puede estar vacío.")
                : value;
        }

        public string Apellido { get; set; }
        public string Calle { get; set; }
        public string Piso { get; set; }
        public string Depto { get; set; }
        public string Ciudad { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string CuilCuit { get; set; }
        public string FechaAlta { get; set; }
        public string EstadoCivil
        {
            get => _estadoCivil;
            set
            {
                if (!string.Equals(value, "Casado", StringComparison.OrdinalIgnoreCase) &&
                    !string.Equals(value, "Soltero", StringComparison.OrdinalIgnoreCase))
                    throw new ArgumentException("El estado civil debe ser 'Casado' o 'Soltero'.");
                _estadoCivil = value;
            }
        }
        public string Nacionalidad { get; set; }
        public string Provincia { get; set; }
        public string CodigoPostal { get; set; }
        public string Barrio { get; set; }
        public string TelefonoAlternativo { get; set; }
        public string Instagram { get; set; }
        public string Profesion { get; set; }
        public string Empresa { get; set; }
        public string NivelEstudios { get; set; }
        public string EstadoPersona
        {
            get => _estadoPersona;
            set
            {
                if (!string.Equals(value, "Activo", StringComparison.OrdinalIgnoreCase) &&
                    !string.Equals(value, "Inactivo", StringComparison.OrdinalIgnoreCase))
                    throw new ArgumentException("El estado de la persona debe ser 'Activo' o 'Inactivo'.");
                _estadoPersona = value;
            }
        }
        public string MetodoPago { get; set; }
        public string Observaciones { get; set; }

        public string FechaApertura { get; set; }
        public decimal LimiteCredito { get; set; }
        public string EstadoCuenta
        {
            get => _estadoCuenta;
            set
            {
                if (!string.Equals(value, "Activo", StringComparison.OrdinalIgnoreCase) &&
                    !string.Equals(value, "Suspendido", StringComparison.OrdinalIgnoreCase))
                    throw new ArgumentException("El estado de la cuenta debe ser 'Activo' o 'Suspendido'.");
                _estadoCuenta = value;
            }
        }
        public int IdCuenta { get; set; }
    }
}