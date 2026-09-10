using System;
using System.Collections.Generic;
using System.Linq; // hacía falta para el .Contains(campo, StringComparer...) de CamposValidos
using Datos; //lee a Datos

namespace Negocio
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

    }

    public class PersonaNegocio
    {
        private PersonaDatos _datos = new PersonaDatos();

        private static readonly string[] CamposValidos = { "DNI", "Apellido", "Nombres", "Calle" };

        private FilaPersona ConvertirAFilaPersona(Persona persona)
        {
            return new FilaPersona
            {
                Dni = persona.Dni,
                Apellido = persona.Apellido,
                Nombre = persona.Nombre,
                Calle = persona.Calle,
                Piso = persona.Piso,
                Depto = persona.Depto,
                Ciudad = persona.Ciudad,
                Telefono = persona.Telefono,
                Email = persona.Email,
                CuilCuit = persona.CuilCuit,
                FechaAlta = persona.FechaAlta,
                Nacionalidad = persona.Nacionalidad,
                EstadoCivil = persona.EstadoCivil,
                Provincia = persona.Provincia,
                CodigoPostal = persona.CodigoPostal,
                Barrio = persona.Barrio,
                TelefonoAlternativo = persona.TelefonoAlternativo,
                Instagram = persona.Instagram,
                Profesion = persona.Profesion,
                Empresa = persona.Empresa,
                NivelEstudios = persona.NivelEstudios,
                EstadoPersona = persona.EstadoPersona,
                MetodoPago = persona.MetodoPago,
                Observaciones = persona.Observaciones,

                FechaApertura = persona.FechaApertura,
                LimiteCredito = persona.LimiteCredito,
                Estado = persona.EstadoCuenta
            };
        }

        public List<Persona> ObtenerPersona(string campo, string valor)
        {
            if (string.IsNullOrWhiteSpace(campo) || string.IsNullOrWhiteSpace(valor)) return null;
            if (!CamposValidos.Contains(campo, StringComparer.OrdinalIgnoreCase)) return null;

            var resultados = _datos.Buscar(campo, valor);

            if (resultados == null || resultados.Count == 0) return null;

            var lista = new List<Persona>();
            foreach (var r in resultados)
            {
                lista.Add(new Persona
                {
                    Dni = r.Dni,
                    Apellido = r.Apellido,
                    Nombre = r.Nombre,
                    Calle = r.Calle,
                    Piso = r.Piso,
                    Depto = r.Depto,
                    Ciudad = r.Ciudad,
                    Telefono = r.Telefono,
                    Email = r.Email,
                    CuilCuit = r.CuilCuit,
                    FechaAlta = r.FechaAlta,
                    EstadoCivil = r.EstadoCivil,
                    Nacionalidad = r.Nacionalidad,
                    Provincia = r.Provincia,
                    CodigoPostal = r.CodigoPostal,
                    Barrio = r.Barrio,
                    TelefonoAlternativo = r.TelefonoAlternativo,
                    Instagram = r.Instagram,
                    Profesion = r.Profesion,
                    Empresa = r.Empresa,
                    NivelEstudios = r.NivelEstudios,
                    EstadoPersona = r.EstadoPersona,
                    MetodoPago = r.MetodoPago,
                    Observaciones = r.Observaciones,

                    FechaApertura = r.FechaApertura,
                    LimiteCredito = r.LimiteCredito,
                    EstadoCuenta = r.Estado
                });
            }
            return lista;
        }

        public bool AgregarPersona(Persona persona)
        {
            if (persona == null) return false;
            if (string.IsNullOrWhiteSpace(persona.Nombre)) return false;

            var existentes = _datos.Buscar("DNI", persona.Dni.ToString());
            if (existentes != null && existentes.Count > 0) return false;

            return _datos.Add(ConvertirAFilaPersona(persona));
        }

        public bool ModificarPersona(Persona persona)
        {
            if (persona == null) return false;
            if (persona.Dni <= 0) return false;

            return _datos.Modi(ConvertirAFilaPersona(persona));
        }

        public bool EliminarPersona(int dni)
        {
            if (dni <= 0) return false;

            return _datos.Elim(dni);
        }
    }
}