using Negocio;

internal class Program
{
    private void BuscarPersona(string dato, PersonaNegocio negocio)
    {
        Console.Write($"Ingrese {dato}: ");
        string buscar = Console.ReadLine();
        List<Persona> personas = negocio.ObtenerPersona(dato, buscar);

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
        }
    }

    private static void Main()
    {
        var lol = new Program();
        PersonaNegocio negocio = new PersonaNegocio();
        bool salir = false;

        while (!salir)
        {
            Console.WriteLine("--- Agenda ---");
            Console.WriteLine("1. Buscar");
            Console.WriteLine("2. Agregar");
            Console.WriteLine("3. Modificar");
            Console.WriteLine("4. Eliminar");
            Console.WriteLine("5. Salir");
            Console.Write("Opcion: ");
            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    Console.WriteLine("--- Buscar por.. ---");
                    Console.WriteLine("1. DNI");
                    Console.WriteLine("2. APELLIDO");
                    Console.WriteLine("3. NOMBRES");
                    Console.WriteLine("4. CALLE");
                    Console.WriteLine("5. Salir");
                    Console.Write("Opcion: ");
                    string opcion2 = Console.ReadLine();

                    switch (opcion2)
                    {
                        case "1":
                            lol.BuscarPersona("DNI", negocio);
                            break;

                        case "2":
                            lol.BuscarPersona("Apellido", negocio);
                            break;

                        case "3":
                            lol.BuscarPersona("Nombres", negocio);
                            break;

                        case "4":
                            lol.BuscarPersona("Calle", negocio);
                            break;

                        case "5":
                            Console.WriteLine("Volviendo al menu principal.");
                            break;

                        default:
                            Console.WriteLine("Opción invalida.");
                            break;
                    }
                    break; // <- faltaba: sin esto caía directo al case "2"

                case "2":
                    Persona nuevaPersona = new Persona();
                    Console.Write("DNI: ");
                    nuevaPersona.Dni = Console.ReadLine();
                    Console.Write("Nombre: ");
                    nuevaPersona.Nombre = Console.ReadLine();
                    Console.Write("Apellido: ");
                    nuevaPersona.Apellido = Console.ReadLine();
                    Console.Write("Calle: ");
                    nuevaPersona.Calle = Console.ReadLine();
                    Console.Write("Piso: ");
                    nuevaPersona.Piso = Console.ReadLine();
                    Console.Write("Depto: ");
                    nuevaPersona.Depto = Console.ReadLine();
                    Console.Write("Ciudad: ");
                    nuevaPersona.Ciudad = Console.ReadLine();
                    Console.Write("Telefono: ");
                    nuevaPersona.Telefono = Console.ReadLine();
                    Console.Write("Email: ");
                    nuevaPersona.Email = Console.ReadLine();

                    bool agregado = negocio.AgregarPersona(nuevaPersona);
                    Console.WriteLine(agregado ? "Agregado con exito." : "No se pudo agregar.");
                    break;

                case "3":
                    Persona personaModificar = new Persona();
                    Console.Write("DNI a modificar: ");
                    personaModificar.Dni = Console.ReadLine();
                    Console.Write("Nuevo Nombre: (dejar en blanco para mantener el valor) ");
                    personaModificar.Nombre = Console.ReadLine();
                    Console.Write("Nuevo Apellido: ");
                    personaModificar.Apellido = Console.ReadLine();
                    Console.Write("Nuevo Calle: ");
                    personaModificar.Calle = Console.ReadLine();
                    Console.Write("Nuevo Piso: ");
                    personaModificar.Piso = Console.ReadLine();
                    Console.Write("Nuevo Depto: ");
                    personaModificar.Depto = Console.ReadLine();
                    Console.Write("Nuevo Ciudad: ");
                    personaModificar.Ciudad = Console.ReadLine();
                    Console.Write("Nuevo Telefono: ");
                    personaModificar.Telefono = Console.ReadLine();
                    Console.Write("Nuevo Email: ");
                    personaModificar.Email = Console.ReadLine();

                    bool modificado = negocio.ModificarPersona(personaModificar);
                    Console.WriteLine(modificado ? "Modificado con exito." : "No se pudo modificar.");
                    break;

                case "4":
                    Console.Write("Ingrese DNI a eliminar: ");
                    string dniEliminar = Console.ReadLine();
                    bool eliminado = negocio.EliminarPersona(dniEliminar);
                    Console.WriteLine(eliminado ? "Eliminado con exito." : "No se pudo eliminar.");
                    break;

                case "5":
                    salir = true;
                    break;

                default:
                    Console.WriteLine("Opción invalida.");
                    break;
            }
        }
    }
}