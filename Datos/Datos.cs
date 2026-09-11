using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;
using Entidades;

namespace Datos
{

    public class PersonaDatos
    {
        private const string NombreBase = "BDDLOL";
        private string _conexionStringSinBase = "Server=localhost;Uid=root;Pwd=;";
        private string _conexionString = $"Server=localhost;Database={NombreBase};Uid=root;Pwd=;";

        public PersonaDatos()
        {
            try
            {
                InicializarBaseDeDatos();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("No se pudo conectar a la base de datos. ¿Está prendido XAMPP/MySQL?");
                Console.WriteLine($"Detalle: {ex.Message}");
                Environment.Exit(1);
            }
        }

        private void InicializarBaseDeDatos()
        {
            //se conecta a mysql sin base de datos para crearla si no existe
            using (MySqlConnection conexion = new MySqlConnection(_conexionStringSinBase))
            {
                conexion.Open();

                string crearBase = $"CREATE DATABASE IF NOT EXISTS {NombreBase}";
                using (MySqlCommand comando = new MySqlCommand(crearBase, conexion))
                {
                    comando.ExecuteNonQuery();
                }
            }

            //se conecta a la base de datos para crear las tablas si no existen
            using (MySqlConnection conexion = new MySqlConnection(_conexionString))
            {
                conexion.Open();

                string crearAgenda = @"
                CREATE TABLE IF NOT EXISTS Agenda (
                    Dni      INT PRIMARY KEY,
                    Apellido VARCHAR(100),
                    Nombre   VARCHAR(100),
                    Calle    VARCHAR(100),
                    Piso     VARCHAR(10),
                    Depto    VARCHAR(10),
                    Ciudad   VARCHAR(100),
                    Telefono VARCHAR(30),
                    Email    VARCHAR(100),
                    CuilCuit VARCHAR(20),
                    FechaAlta DATE,
                    Nacionalidad VARCHAR(50),
                    EstadoCivil VARCHAR(30),
                    Provincia VARCHAR(50),
                    CodigoPostal VARCHAR(10),
                    Barrio VARCHAR(100),
                    TelefonoAlternativo VARCHAR(30),
                    Instagram VARCHAR(50),
                    Profesion VARCHAR(100),
                    Empresa VARCHAR(100),
                    NivelEstudios VARCHAR(50),
                    EstadoPersona VARCHAR(20),
                    MetodoPago VARCHAR(30),
                    Observaciones TEXT
                )";

                string crearCuentaCte = @"
                CREATE TABLE IF NOT EXISTS CuentaCte (
                    Id             INT AUTO_INCREMENT PRIMARY KEY,
                    DniPersona     INT NOT NULL,
                    FechaApertura  DATE,
                    LimiteCredito  DECIMAL(10,2),
                    EstadoCuenta   VARCHAR(20),
                    FOREIGN KEY (DniPersona) REFERENCES Agenda(Dni)
                )";

                using (MySqlCommand comando = new MySqlCommand(crearAgenda, conexion))
                {
                    comando.ExecuteNonQuery();
                }

                using (MySqlCommand comando = new MySqlCommand(crearCuentaCte, conexion))
                {
                    comando.ExecuteNonQuery();
                }
            }
        }

        private static readonly Dictionary<string, string> ColumnasBusqueda = new Dictionary<string, string>
        {
            { "DNI", "Dni" },
            { "Apellido", "Apellido" },
            { "Nombres", "Nombre" },
            { "Calle", "Calle" }
        };

        private const string ColumnasSelect =
            "Agenda.Dni, Agenda.Apellido, Agenda.Nombre, Agenda.Calle, Agenda.Piso, Agenda.Depto, " +
            "Agenda.Ciudad, Agenda.Telefono, Agenda.Email, " + "Agenda.CuilCuit, Agenda.FechaAlta, " +
            "Agenda.EstadoCivil, Agenda.Nacionalidad, Agenda.Provincia, " + "Agenda.CodigoPostal, " +
            "Agenda.Barrio, Agenda.TelefonoAlternativo, Agenda.Instagram, " + "Agenda.Profesion, " +
            "Agenda.Empresa, Agenda.NivelEstudios, Agenda.EstadoPersona, " + "Agenda.MetodoPago, " +
            "Agenda.Observaciones, " + "CuentaCte.Id, CuentaCte.FechaApertura, CuentaCte.LimiteCredito, " +
            "CuentaCte.EstadoCuenta";

        private Persona LeerFila(MySqlDataReader reader)
        {
            return new Persona
            {
                Dni = Convert.ToInt32(reader["Dni"]),
                Apellido = reader["Apellido"].ToString(),
                Nombre = reader["Nombre"].ToString(),
                Calle = reader["Calle"].ToString(),
                Piso = reader["Piso"].ToString(),
                Depto = reader["Depto"].ToString(),
                Ciudad = reader["Ciudad"].ToString(),
                Telefono = reader["Telefono"].ToString(),
                Email = reader["Email"].ToString(),
                IdCuenta = reader["Id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Id"]),
                FechaApertura = reader["FechaApertura"] == DBNull.Value ? null : reader["FechaApertura"].ToString(),
                LimiteCredito = reader["LimiteCredito"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["LimiteCredito"]),
                EstadoCuenta = reader["EstadoCuenta"] == DBNull.Value ? null : reader["EstadoCuenta"].ToString(),
                CuilCuit = reader["CuilCuit"].ToString(),
                FechaAlta = reader["FechaAlta"].ToString(),
                EstadoCivil = reader["EstadoCivil"].ToString(),
                Nacionalidad = reader["Nacionalidad"].ToString(),
                Provincia = reader["Provincia"].ToString(),
                CodigoPostal = reader["CodigoPostal"].ToString(),
                Barrio = reader["Barrio"].ToString(),
                TelefonoAlternativo = reader["TelefonoAlternativo"].ToString(),
                Instagram = reader["Instagram"].ToString(),
                Profesion = reader["Profesion"].ToString(),
                Empresa = reader["Empresa"].ToString(),
                NivelEstudios = reader["NivelEstudios"].ToString(),
                EstadoPersona = reader["EstadoPersona"].ToString(),
                MetodoPago = reader["MetodoPago"].ToString(),
                Observaciones = reader["Observaciones"].ToString(),
            };
        }

        public List<Persona> Buscar(string campo, string valor)
        {
            if (!ColumnasBusqueda.TryGetValue(campo, out string columna)) return null;

            var resultados = new List<Persona>();

            string query = $@"SELECT {ColumnasSelect} FROM Agenda 
                            LEFT JOIN CuentaCte ON Agenda.Dni = CuentaCte.DniPersona 
                            WHERE Agenda.{columna} LIKE @Valor";

            using (MySqlConnection conexion = new MySqlConnection(_conexionString))
            {
                MySqlCommand comando = new MySqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@Valor", $"%{valor}%");

                conexion.Open();

                using (MySqlDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        resultados.Add(LeerFila(reader));
                    }
                }
            }

            return resultados;
        }

        public bool Add(Persona filaPersona)
        {
            string queryAgenda = "INSERT INTO Agenda (Dni, Apellido, Nombre, Calle, Piso, Depto, Ciudad, Telefono, Email, " +
                                  "CuilCuit, FechaAlta, EstadoCivil, Nacionalidad, Provincia, CodigoPostal, Barrio, " +
                                  "TelefonoAlternativo, Instagram, Profesion, Empresa, NivelEstudios, EstadoPersona, MetodoPago, Observaciones) " +
                                  "VALUES (@Dni, @Apellido, @Nombre, @Calle, @Piso, @Depto, @Ciudad, @Telefono, @Email, " +
                                  "@CuilCuit, @FechaAlta, @EstadoCivil, @Nacionalidad, @Provincia, @CodigoPostal, @Barrio, " +
                                  "@TelefonoAlternativo, @Instagram, @Profesion, @Empresa, @NivelEstudios, @EstadoPersona, @MetodoPago, @Observaciones)";

            string queryCuenta = "INSERT INTO CuentaCte (DniPersona, FechaApertura, LimiteCredito, EstadoCuenta) " +
                                  "VALUES (@Dni, @FechaApertura, @LimiteCredito, @EstadoCuenta)";

            using (MySqlConnection conexion = new MySqlConnection(_conexionString))
            {
                conexion.Open();

                using (MySqlTransaction transaccion = conexion.BeginTransaction())
                {
                    try
                    {
                        MySqlCommand comandoAgenda = new MySqlCommand(queryAgenda, conexion, transaccion);
                        comandoAgenda.Parameters.AddWithValue("@Dni", filaPersona.Dni);
                        comandoAgenda.Parameters.AddWithValue("@Apellido", filaPersona.Apellido);
                        comandoAgenda.Parameters.AddWithValue("@Nombre", filaPersona.Nombre);
                        comandoAgenda.Parameters.AddWithValue("@Calle", filaPersona.Calle);
                        comandoAgenda.Parameters.AddWithValue("@Piso", filaPersona.Piso);
                        comandoAgenda.Parameters.AddWithValue("@Depto", filaPersona.Depto);
                        comandoAgenda.Parameters.AddWithValue("@Ciudad", filaPersona.Ciudad);
                        comandoAgenda.Parameters.AddWithValue("@Telefono", filaPersona.Telefono);
                        comandoAgenda.Parameters.AddWithValue("@Email", filaPersona.Email);
                        comandoAgenda.Parameters.AddWithValue("@CuilCuit", filaPersona.CuilCuit);
                        comandoAgenda.Parameters.AddWithValue("@FechaAlta", filaPersona.FechaAlta);
                        comandoAgenda.Parameters.AddWithValue("@EstadoCivil", filaPersona.EstadoCivil);
                        comandoAgenda.Parameters.AddWithValue("@Nacionalidad", filaPersona.Nacionalidad);
                        comandoAgenda.Parameters.AddWithValue("@Provincia", filaPersona.Provincia);
                        comandoAgenda.Parameters.AddWithValue("@CodigoPostal", filaPersona.CodigoPostal);
                        comandoAgenda.Parameters.AddWithValue("@Barrio", filaPersona.Barrio);
                        comandoAgenda.Parameters.AddWithValue("@TelefonoAlternativo", filaPersona.TelefonoAlternativo);
                        comandoAgenda.Parameters.AddWithValue("@Instagram", filaPersona.Instagram);
                        comandoAgenda.Parameters.AddWithValue("@Profesion", filaPersona.Profesion);
                        comandoAgenda.Parameters.AddWithValue("@Empresa", filaPersona.Empresa);
                        comandoAgenda.Parameters.AddWithValue("@NivelEstudios", filaPersona.NivelEstudios);
                        comandoAgenda.Parameters.AddWithValue("@EstadoPersona", filaPersona.EstadoPersona);
                        comandoAgenda.Parameters.AddWithValue("@MetodoPago", filaPersona.MetodoPago);
                        comandoAgenda.Parameters.AddWithValue("@Observaciones", filaPersona.Observaciones);
                        comandoAgenda.ExecuteNonQuery();

                        MySqlCommand comandoCuenta = new MySqlCommand(queryCuenta, conexion, transaccion);
                        comandoCuenta.Parameters.AddWithValue("@Dni", filaPersona.Dni);
                        comandoCuenta.Parameters.AddWithValue("@FechaApertura", filaPersona.FechaApertura);
                        comandoCuenta.Parameters.AddWithValue("@LimiteCredito", filaPersona.LimiteCredito);
                        comandoCuenta.Parameters.AddWithValue("@EstadoCuenta", filaPersona.EstadoCuenta);
                        comandoCuenta.ExecuteNonQuery();

                        transaccion.Commit();
                        return true;
                    }
                    catch
                    {
                        transaccion.Rollback();
                        return false;
                    }
                }
            }
        }

        public bool Modi(Persona filaPersona)
        {
            string queryAgenda = "UPDATE Agenda SET Apellido = @Apellido, Nombre = @Nombre, Calle = @Calle, " +
                                  "Piso = @Piso, Depto = @Depto, Ciudad = @Ciudad, Telefono = @Telefono, Email = @Email, " +
                                  "CuilCuit = @CuilCuit, FechaAlta = @FechaAlta, EstadoCivil = @EstadoCivil, " +
                                  "Nacionalidad = @Nacionalidad, Provincia = @Provincia, CodigoPostal = @CodigoPostal, " +
                                  "Barrio = @Barrio, TelefonoAlternativo = @TelefonoAlternativo, Instagram = @Instagram, " +
                                  "Profesion = @Profesion, Empresa = @Empresa, NivelEstudios = @NivelEstudios, " +
                                  "EstadoPersona = @EstadoPersona, MetodoPago = @MetodoPago, Observaciones = @Observaciones " +
                                  "WHERE Dni = @Dni";

            string queryCuenta = "UPDATE CuentaCte SET FechaApertura = @FechaApertura, " +
                                  "LimiteCredito = @LimiteCredito, EstadoCuenta = @EstadoCuenta WHERE DniPersona = @Dni";

            using (MySqlConnection conexion = new MySqlConnection(_conexionString))
            {
                conexion.Open();

                using (MySqlTransaction transaccion = conexion.BeginTransaction())
                {
                    try
                    {
                        MySqlCommand comandoAgenda = new MySqlCommand(queryAgenda, conexion, transaccion);
                        comandoAgenda.Parameters.AddWithValue("@Dni", filaPersona.Dni);
                        comandoAgenda.Parameters.AddWithValue("@Apellido", filaPersona.Apellido);
                        comandoAgenda.Parameters.AddWithValue("@Nombre", filaPersona.Nombre);
                        comandoAgenda.Parameters.AddWithValue("@Calle", filaPersona.Calle);
                        comandoAgenda.Parameters.AddWithValue("@Piso", filaPersona.Piso);
                        comandoAgenda.Parameters.AddWithValue("@Depto", filaPersona.Depto);
                        comandoAgenda.Parameters.AddWithValue("@Ciudad", filaPersona.Ciudad);
                        comandoAgenda.Parameters.AddWithValue("@Telefono", filaPersona.Telefono);
                        comandoAgenda.Parameters.AddWithValue("@Email", filaPersona.Email);
                        comandoAgenda.Parameters.AddWithValue("@CuilCuit", filaPersona.CuilCuit);
                        comandoAgenda.Parameters.AddWithValue("@FechaAlta", filaPersona.FechaAlta);
                        comandoAgenda.Parameters.AddWithValue("@EstadoCivil", filaPersona.EstadoCivil);
                        comandoAgenda.Parameters.AddWithValue("@Nacionalidad", filaPersona.Nacionalidad);
                        comandoAgenda.Parameters.AddWithValue("@Provincia", filaPersona.Provincia);
                        comandoAgenda.Parameters.AddWithValue("@CodigoPostal", filaPersona.CodigoPostal);
                        comandoAgenda.Parameters.AddWithValue("@Barrio", filaPersona.Barrio);
                        comandoAgenda.Parameters.AddWithValue("@TelefonoAlternativo", filaPersona.TelefonoAlternativo);
                        comandoAgenda.Parameters.AddWithValue("@Instagram", filaPersona.Instagram);
                        comandoAgenda.Parameters.AddWithValue("@Profesion", filaPersona.Profesion);
                        comandoAgenda.Parameters.AddWithValue("@Empresa", filaPersona.Empresa);
                        comandoAgenda.Parameters.AddWithValue("@NivelEstudios", filaPersona.NivelEstudios);
                        comandoAgenda.Parameters.AddWithValue("@EstadoPersona", filaPersona.EstadoPersona);
                        comandoAgenda.Parameters.AddWithValue("@MetodoPago", filaPersona.MetodoPago);
                        comandoAgenda.Parameters.AddWithValue("@Observaciones", filaPersona.Observaciones);
                        int filasAgenda = comandoAgenda.ExecuteNonQuery();

                        MySqlCommand comandoCuenta = new MySqlCommand(queryCuenta, conexion, transaccion);
                        comandoCuenta.Parameters.AddWithValue("@Dni", filaPersona.Dni);
                        comandoCuenta.Parameters.AddWithValue("@FechaApertura", filaPersona.FechaApertura);
                        comandoCuenta.Parameters.AddWithValue("@LimiteCredito", filaPersona.LimiteCredito);
                        comandoCuenta.Parameters.AddWithValue("@EstadoCuenta", filaPersona.EstadoCuenta);
                        comandoCuenta.ExecuteNonQuery();

                        transaccion.Commit();
                        return filasAgenda > 0;
                    }
                    catch
                    {
                        transaccion.Rollback();
                        return false;
                    }
                }
            }
        }

        public bool Elim(int dni)
        {
            string queryCuenta = "DELETE FROM CuentaCte WHERE DniPersona = @Dni";
            string queryAgenda = "DELETE FROM Agenda WHERE Dni = @Dni";

            using (MySqlConnection conexion = new MySqlConnection(_conexionString))
            {
                conexion.Open();

                using (MySqlTransaction transaccion = conexion.BeginTransaction())
                {
                    try
                    {
                        MySqlCommand comandoCuenta = new MySqlCommand(queryCuenta, conexion, transaccion);
                        comandoCuenta.Parameters.AddWithValue("@Dni", dni);
                        comandoCuenta.ExecuteNonQuery();

                        MySqlCommand comandoAgenda = new MySqlCommand(queryAgenda, conexion, transaccion);
                        comandoAgenda.Parameters.AddWithValue("@Dni", dni);
                        int filasAfectadas = comandoAgenda.ExecuteNonQuery();

                        transaccion.Commit();
                        return filasAfectadas > 0;
                    }
                    catch
                    {
                        transaccion.Rollback();
                        return false;
                    }
                }
            }
        }
    }
}