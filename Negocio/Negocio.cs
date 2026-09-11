using System;
using System.Collections.Generic;
using System.Linq;
using Datos;
using Entidades;

namespace Negocio
{

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

            return resultados;
        }

        public bool AgregarPersona(Persona persona)
        {
            if (persona == null) return false;
            if (string.IsNullOrWhiteSpace(persona.Nombre)) return false;

            var existentes = _datos.Buscar("DNI", persona.Dni.ToString());
            if (existentes != null && existentes.Count > 0) return false;

            return _datos.Add(persona);
        }

        public bool ModificarPersona(Persona personaNueva)
        {
            if (personaNueva == null) return false;
            if (personaNueva.Dni <= 0) return false;

            var actuales = _datos.Buscar("DNI", personaNueva.Dni.ToString());
            if (actuales == null || actuales.Count == 0) return false;
            var actual = actuales[0];

            personaNueva.Apellido = personaNueva.Apellido ?? actual.Apellido;
            personaNueva.Calle = personaNueva.Calle ?? actual.Calle;
            personaNueva.Piso = personaNueva.Piso ?? actual.Piso;
            personaNueva.Depto = personaNueva.Depto ?? actual.Depto;
            personaNueva.Ciudad = personaNueva.Ciudad ?? actual.Ciudad;
            personaNueva.Telefono = personaNueva.Telefono ?? actual.Telefono;
            personaNueva.Email = personaNueva.Email ?? actual.Email;
            personaNueva.CuilCuit = personaNueva.CuilCuit ?? actual.CuilCuit;
            personaNueva.FechaAlta = personaNueva.FechaAlta ?? actual.FechaAlta;
            personaNueva.Nacionalidad = personaNueva.Nacionalidad ?? actual.Nacionalidad;
            personaNueva.Provincia = personaNueva.Provincia ?? actual.Provincia;
            personaNueva.CodigoPostal = personaNueva.CodigoPostal ?? actual.CodigoPostal;
            personaNueva.Barrio = personaNueva.Barrio ?? actual.Barrio;
            personaNueva.TelefonoAlternativo = personaNueva.TelefonoAlternativo ?? actual.TelefonoAlternativo;
            personaNueva.Instagram = personaNueva.Instagram ?? actual.Instagram;
            personaNueva.Profesion = personaNueva.Profesion ?? actual.Profesion;
            personaNueva.Empresa = personaNueva.Empresa ?? actual.Empresa;
            personaNueva.NivelEstudios = personaNueva.NivelEstudios ?? actual.NivelEstudios;
            personaNueva.MetodoPago = personaNueva.MetodoPago ?? actual.MetodoPago;
            personaNueva.Observaciones = personaNueva.Observaciones ?? actual.Observaciones;
            personaNueva.FechaApertura = personaNueva.FechaApertura ?? actual.FechaApertura;

            if (string.IsNullOrWhiteSpace(personaNueva.EstadoCivil)) personaNueva.EstadoCivil = actual.EstadoCivil;
            if (string.IsNullOrWhiteSpace(personaNueva.EstadoPersona)) personaNueva.EstadoPersona = actual.EstadoPersona;
            if (string.IsNullOrWhiteSpace(personaNueva.EstadoCuenta)) personaNueva.EstadoCuenta = actual.EstadoCuenta;

            if (personaNueva.LimiteCredito == 0) personaNueva.LimiteCredito = actual.LimiteCredito;

            return _datos.Modi(personaNueva);
        }

        public void BuscarPersona(string dato)
        {
            Console.Write($"Ingrese {dato}: ");
            string buscar = Console.ReadLine();
            List<Persona> personas = ObtenerPersona(dato, buscar);

            if (personas == null || personas.Count == 0)
            {
                Console.WriteLine("No existe.");
                return;
            }

            for (int i = 0; i < personas.Count; i++)
            {
                Console.WriteLine($"Encontrado: DNI: {personas[i].Dni}");
                Console.WriteLine($"Nombre completo: {personas[i].Nombre}, {personas[i].Apellido}");
                Console.WriteLine($"Calle: {personas[i].Calle}");
                Console.WriteLine($"Depto: {personas[i].Depto}");
                Console.WriteLine($"Piso: {personas[i].Piso}");
                Console.WriteLine($"Ciudad: {personas[i].Ciudad}");
                Console.WriteLine($"Telefono: {personas[i].Telefono}");
                Console.WriteLine($"Email: {personas[i].Email}");
                Console.WriteLine($"CUIL/CUIT: {personas[i].CuilCuit}");
                Console.WriteLine($"Fecha de alta: {personas[i].FechaAlta}");
                Console.WriteLine($"Estado civil: {personas[i].EstadoCivil}");
                Console.WriteLine($"Nacionalidad: {personas[i].Nacionalidad}");
                Console.WriteLine($"Provincia: {personas[i].Provincia}");
                Console.WriteLine($"Codigo postal: {personas[i].CodigoPostal}");
                Console.WriteLine($"Barrio: {personas[i].Barrio}");
                Console.WriteLine($"Telefono alternativo: {personas[i].TelefonoAlternativo}");
                Console.WriteLine($"Instagram: {personas[i].Instagram}");
                Console.WriteLine($"Profesion: {personas[i].Profesion}");
                Console.WriteLine($"Empresa: {personas[i].Empresa}");
                Console.WriteLine($"Nivel de estudios: {personas[i].NivelEstudios}");
                Console.WriteLine($"Estado Persona: {personas[i].EstadoPersona}");
                Console.WriteLine($"Metodo de pago: {personas[i].MetodoPago}");
                Console.WriteLine($"Observaciones: {personas[i].Observaciones}");
                Console.WriteLine($"Fecha apertura: {personas[i].FechaApertura}");
                Console.WriteLine($"Limite credito: {personas[i].LimiteCredito}");
                Console.WriteLine($"Estado de Cuenta: {personas[i].EstadoCuenta}");
            }
        }

        public bool EliminarPersona(int dni)
        {
            if (dni <= 0) return false;

            return _datos.Elim(dni);
        }
    }
}