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
            Console.WriteLine($"Estado: {personas[i].EstadoPersona}");
            Console.WriteLine($"Metodo de pago: {personas[i].MetodoPago}");
            Console.WriteLine($"Observaciones: {personas[i].Observaciones}");
            Console.WriteLine($"Fecha apertura: {personas[i].FechaApertura}");
            Console.WriteLine($"Limite credito: {personas[i].LimiteCredito}");
            Console.WriteLine($"Estado: {personas[i].EstadoCuenta}");
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
                    try
                    {
                        Persona nuevaPersona = new Persona();
                        Console.Write("DNI: ");
                        int.TryParse(Console.ReadLine(), out int dniNuevo);
                        nuevaPersona.Dni = dniNuevo;
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
                        Console.Write("CUIL/CUIT: ");
                        nuevaPersona.CuilCuit = Console.ReadLine();
                        Console.Write("Fecha de alta (YYYY-MM-DD): ");
                        nuevaPersona.FechaAlta = Console.ReadLine();
                        Console.Write("Estado civil: (Casado/Soltero)");
                        nuevaPersona.EstadoCivil = Console.ReadLine();
                        Console.Write("Nacionalidad: ");
                        nuevaPersona.Nacionalidad = Console.ReadLine();
                        Console.Write("Provincia: ");
                        nuevaPersona.Provincia = Console.ReadLine();
                        Console.Write("Codigo postal: ");
                        nuevaPersona.CodigoPostal = Console.ReadLine();
                        Console.Write("Barrio: ");
                        nuevaPersona.Barrio = Console.ReadLine();
                        Console.Write("Telefono alternativo: ");
                        nuevaPersona.TelefonoAlternativo = Console.ReadLine();
                        Console.Write("Instagram: ");
                        nuevaPersona.Instagram = Console.ReadLine();
                        Console.Write("Profesion/Ocupacion: ");
                        nuevaPersona.Profesion = Console.ReadLine();
                        Console.Write("Empresa/Lugar de trabajo: ");
                        nuevaPersona.Empresa = Console.ReadLine();
                        Console.Write("Nivel de estudios: ");
                        nuevaPersona.NivelEstudios = Console.ReadLine();
                        Console.Write("Estado de la persona (Activo/Inactivo): ");
                        nuevaPersona.EstadoPersona = Console.ReadLine();
                        Console.Write("Metodo de pago preferido: ");
                        nuevaPersona.MetodoPago = Console.ReadLine();
                        Console.Write("Observaciones/Notas: ");
                        nuevaPersona.Observaciones = Console.ReadLine();
                        Console.Write("Fecha apertura cuenta (YYYY-MM-DD): ");
                        nuevaPersona.FechaApertura = Console.ReadLine();
                        Console.Write("Limite credito: ");
                        decimal.TryParse(Console.ReadLine(), out decimal limite);
                        nuevaPersona.LimiteCredito = limite;
                        Console.Write("Estado de la cuenta (Activo/Suspendido): ");
                        nuevaPersona.EstadoCuenta = Console.ReadLine();

                        bool agregado = negocio.AgregarPersona(nuevaPersona);
                        Console.WriteLine(agregado ? "Agregado con exito." : "No se pudo agregar.");
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Dato inválido: {ex.Message}");
                    }
                    break;

                case "3":
                    try
                    {
                        Persona personaModificar = new Persona();
                        Console.Write("DNI a modificar: ");
                        int.TryParse(Console.ReadLine(), out int dniModi);
                        personaModificar.Dni = dniModi;
                        Console.Write("Nuevo Nombre: ");
                        personaModificar.Nombre = Console.ReadLine();
                        Console.Write("Nuevo Apellido: ");
                        personaModificar.Apellido = Console.ReadLine();
                        Console.Write("Nueva Calle: ");
                        personaModificar.Calle = Console.ReadLine();
                        Console.Write("Nuevo Piso: ");
                        personaModificar.Piso = Console.ReadLine();
                        Console.Write("Nuevo Depto: ");
                        personaModificar.Depto = Console.ReadLine();
                        Console.Write("Nueva Ciudad: ");
                        personaModificar.Ciudad = Console.ReadLine();
                        Console.Write("Nuevo Telefono: ");
                        personaModificar.Telefono = Console.ReadLine();
                        Console.Write("Nuevo Email: ");
                        personaModificar.Email = Console.ReadLine();
                        Console.Write("Nuevo CUIL/CUIT: ");
                        personaModificar.CuilCuit = Console.ReadLine();
                        Console.Write("Nueva Fecha de alta: ");
                        personaModificar.FechaAlta = Console.ReadLine();
                        Console.Write("Nuevo Estado civil (Casado/Soltero): ");
                        personaModificar.EstadoCivil = Console.ReadLine();
                        Console.Write("Nueva Nacionalidad: ");
                        personaModificar.Nacionalidad = Console.ReadLine();
                        Console.Write("Nueva Provincia: ");
                        personaModificar.Provincia = Console.ReadLine();
                        Console.Write("Nuevo Codigo postal: ");
                        personaModificar.CodigoPostal = Console.ReadLine();
                        Console.Write("Nuevo Barrio: ");
                        personaModificar.Barrio = Console.ReadLine();
                        Console.Write("Nuevo Telefono alternativo: ");
                        personaModificar.TelefonoAlternativo = Console.ReadLine();
                        Console.Write("Nuevo Instagram: ");
                        personaModificar.Instagram = Console.ReadLine();
                        Console.Write("Nueva Profesion: ");
                        personaModificar.Profesion = Console.ReadLine();
                        Console.Write("Nueva Empresa: ");
                        personaModificar.Empresa = Console.ReadLine();
                        Console.Write("Nuevo Nivel de estudios: ");
                        personaModificar.NivelEstudios = Console.ReadLine();
                        Console.Write("Nuevo Estado de la persona (Activo/Inactivo): ");
                        personaModificar.EstadoPersona = Console.ReadLine();
                        Console.Write("Nuevo Metodo de pago: ");
                        personaModificar.MetodoPago = Console.ReadLine();
                        Console.Write("Nuevas Observaciones: ");
                        personaModificar.Observaciones = Console.ReadLine();
                        Console.Write("Nueva Fecha apertura: ");
                        personaModificar.FechaApertura = Console.ReadLine();
                        Console.Write("Nuevo Limite credito: ");
                        decimal.TryParse(Console.ReadLine(), out decimal limiteModi);
                        personaModificar.LimiteCredito = limiteModi;
                        Console.Write("Nuevo Estado de la cuenta (Activo/Suspendido): ");
                        personaModificar.EstadoCuenta = Console.ReadLine();

                        bool modificado = negocio.ModificarPersona(personaModificar);
                        Console.WriteLine(modificado ? "Modificado con exito." : "No se pudo modificar.");
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine($"Dato inválido: {ex.Message}");
                    }
                    break;

                case "4":
                    Console.Write("Ingrese DNI a eliminar: ");
                    int.TryParse(Console.ReadLine(), out int dniEliminar);
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