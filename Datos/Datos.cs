using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Datos
{
    public class PersonaDatos
    {
        private string _conexionString = "Server=localhost;Database=BDDLOL;Uid=root;Pwd=;";

        private static readonly Dictionary<string, string> ColumnasBusqueda = new Dictionary<string, string>
        {
            { "DNI", "Dni" },
            { "Apellido", "Apellido" },
            { "Nombres", "Nombre" },
            { "Calle", "Calle" }
        };

        private const string ColumnasSelect =
            "Dni, Apellido, Nombre, Calle, Piso, Depto, Ciudad, Telefono, Email";

        private (string Dni, string Apellido, string Nombre, string Calle, string Piso,
                  string Depto, string Ciudad, string Telefono, string Email) LeerFila(MySqlDataReader reader)
        {
            return (
                reader["Dni"].ToString(),
                reader["Apellido"].ToString(),
                reader["Nombre"].ToString(),
                reader["Calle"].ToString(),
                reader["Piso"].ToString(),
                reader["Depto"].ToString(),
                reader["Ciudad"].ToString(),
                reader["Telefono"].ToString(),
                reader["Email"].ToString()
            );
        }

        public (string Dni, string Apellido, string Nombre, string Calle, string Piso,
                 string Depto, string Ciudad, string Telefono, string Email)? BuscarPorDni(string dni)
        {
            string query = $"SELECT {ColumnasSelect} FROM agenda WHERE Dni = @Dni";

            using (MySqlConnection conexion = new MySqlConnection(_conexionString))
            {
                MySqlCommand comando = new MySqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@Dni", dni);

                conexion.Open();

                using (MySqlDataReader reader = comando.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return LeerFila(reader);
                    }
                }
            }

            return null;
        }

        public List<(string Dni, string Apellido, string Nombre, string Calle, string Piso,
                      string Depto, string Ciudad, string Telefono, string Email)> Buscar(string campo, string valor)
        {
            if (!ColumnasBusqueda.TryGetValue(campo, out string columna)) return null;

            var resultados = new List<(string, string, string, string, string, string, string, string, string)>();

            string query = $"SELECT {ColumnasSelect} FROM agenda WHERE {columna} LIKE @Valor";

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

        public bool Add(string dni, string apellido, string nombre, string calle, string piso,
                         string depto, string ciudad, string telefono, string email)
        {
            string query = "INSERT INTO agenda (Dni, Apellido, Nombre, Calle, Piso, Depto, Ciudad, Telefono, Email) " +
                            "VALUES (@Dni, @Apellido, @Nombre, @Calle, @Piso, @Depto, @Ciudad, @Telefono, @Email)";

            using (MySqlConnection conexion = new MySqlConnection(_conexionString))
            {
                MySqlCommand comando = new MySqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@Dni", dni);
                comando.Parameters.AddWithValue("@Apellido", apellido);
                comando.Parameters.AddWithValue("@Nombre", nombre);
                comando.Parameters.AddWithValue("@Calle", calle);
                comando.Parameters.AddWithValue("@Piso", piso);
                comando.Parameters.AddWithValue("@Depto", depto);
                comando.Parameters.AddWithValue("@Ciudad", ciudad);
                comando.Parameters.AddWithValue("@Telefono", telefono);
                comando.Parameters.AddWithValue("@Email", email);

                conexion.Open();

                int filasAfectadas = comando.ExecuteNonQuery();
                return filasAfectadas > 0;
            }
        }

        public bool Modi(string dni, string apellido, string nombre, string calle, string piso,
                          string depto, string ciudad, string telefono, string email)
        {
            string query = "UPDATE agenda SET Apellido = @Apellido, Nombre = @Nombre, Calle = @Calle, " +
                            "Piso = @Piso, Depto = @Depto, Ciudad = @Ciudad, Telefono = @Telefono, Email = @Email " +
                            "WHERE Dni = @Dni";

            using (MySqlConnection conexion = new MySqlConnection(_conexionString))
            {
                MySqlCommand comando = new MySqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@Dni", dni);
                comando.Parameters.AddWithValue("@Apellido", apellido);
                comando.Parameters.AddWithValue("@Nombre", nombre);
                comando.Parameters.AddWithValue("@Calle", calle);
                comando.Parameters.AddWithValue("@Piso", piso);
                comando.Parameters.AddWithValue("@Depto", depto);
                comando.Parameters.AddWithValue("@Ciudad", ciudad);
                comando.Parameters.AddWithValue("@Telefono", telefono);
                comando.Parameters.AddWithValue("@Email", email);

                conexion.Open();

                int filasAfectadas = comando.ExecuteNonQuery();
                return filasAfectadas > 0;
            }
        }

        public bool Elim(string dni)
        {
            string query = "DELETE FROM agenda WHERE Dni = @Dni";

            using (MySqlConnection conexion = new MySqlConnection(_conexionString))
            {
                MySqlCommand comando = new MySqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@Dni", dni);

                conexion.Open();

                int filasAfectadas = comando.ExecuteNonQuery();
                return filasAfectadas > 0;
            }
        }
    }
}