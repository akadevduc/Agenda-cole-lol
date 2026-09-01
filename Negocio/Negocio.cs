using System;
using System.Collections.Generic;
using Datos; //lee a Datos

namespace Negocio
{
    public class Persona
    {
        private string _dni;
        private string _nombre;

        public string Dni
        {
            get => _dni;
            set => _dni = string.IsNullOrWhiteSpace(value)
                ? throw new ArgumentException("El DNI no puede estar vacío.")
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
    }

    public class PersonaNegocio
    {
        private PersonaDatos _datos = new PersonaDatos();

        private static readonly string[] CamposValidos = { "DNI", "Apellido", "Nombres", "Calle" };

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
                    Email = r.Email
                });
            }
            return lista;
        }

        public bool AgregarPersona(Persona persona)
        {
            if (persona == null) return false;
            if (string.IsNullOrWhiteSpace(persona.Dni) || string.IsNullOrWhiteSpace(persona.Nombre)) return false;

            if (_datos.BuscarPorDni(persona.Dni) != null) return false;

            return _datos.Add(persona.Dni, persona.Apellido, persona.Nombre, persona.Calle,
                               persona.Piso, persona.Depto, persona.Ciudad, persona.Telefono, persona.Email);
        }

        public bool ModificarPersona(Persona persona)
        {
            if (persona == null) return false;
            if (string.IsNullOrWhiteSpace(persona.Dni)) return false;

           
            return _datos.Modi(persona.Dni, persona.Apellido, persona.Nombre, persona.Calle,
                                persona.Piso, persona.Depto, persona.Ciudad, persona.Telefono, persona.Email);
        }

        public bool EliminarPersona(string dni)
        {
            if (string.IsNullOrWhiteSpace(dni)) return false;

            return _datos.Elim(dni);
        }
    }
}