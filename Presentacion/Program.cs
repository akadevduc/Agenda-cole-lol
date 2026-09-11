using Entidades;
using Negocio;

internal class Program
{
    private static void Main()
    {
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
                            negocio.BuscarPersona("DNI");
                            break;

                        case "2":
                            negocio.BuscarPersona("Apellido");
                            break;

                        case "3":
                            negocio.BuscarPersona("Nombres");
                            break;

                        case "4":
                            negocio.BuscarPersona("Calle");
                            break;

                        case "5":
                            Console.WriteLine("Volviendo al menu principal.");
                            break;

                        default:
                            Console.WriteLine("Opción invalida.");
                            break;
                    }
                    break;

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

                        Console.Write("Nuevo Nombre: (dejar en blanco para mantener el valor) ");
                        string nombre = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(nombre)) personaModificar.Nombre = nombre;

                        Console.Write("Nuevo Apellido: ");
                        string apellido = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(apellido)) personaModificar.Apellido = apellido;

                        Console.Write("Nueva Calle: ");
                        string calle = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(calle)) personaModificar.Calle = calle;

                        Console.Write("Nuevo Piso: ");
                        string piso = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(piso)) personaModificar.Piso = piso;

                        Console.Write("Nuevo Depto: ");
                        string depto = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(depto)) personaModificar.Depto = depto;

                        Console.Write("Nueva Ciudad: ");
                        string ciudad = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(ciudad)) personaModificar.Ciudad = ciudad;

                        Console.Write("Nuevo Telefono: ");
                        string telefono = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(telefono)) personaModificar.Telefono = telefono;

                        Console.Write("Nuevo Email: ");
                        string email = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(email)) personaModificar.Email = email;

                        Console.Write("Nuevo CUIL/CUIT: ");
                        string cuilCuit = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(cuilCuit)) personaModificar.CuilCuit = cuilCuit;

                        Console.Write("Nueva Fecha de alta: ");
                        string fechaAlta = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(fechaAlta)) personaModificar.FechaAlta = fechaAlta;

                        Console.Write("Nuevo Estado civil (Casado/Soltero): ");
                        string estadoCivil = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(estadoCivil)) personaModificar.EstadoCivil = estadoCivil;

                        Console.Write("Nueva Nacionalidad: ");
                        string nacionalidad = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(nacionalidad)) personaModificar.Nacionalidad = nacionalidad;

                        Console.Write("Nueva Provincia: ");
                        string provincia = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(provincia)) personaModificar.Provincia = provincia;

                        Console.Write("Nuevo Codigo postal: ");
                        string codigoPostal = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(codigoPostal)) personaModificar.CodigoPostal = codigoPostal;

                        Console.Write("Nuevo Barrio: ");
                        string barrio = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(barrio)) personaModificar.Barrio = barrio;

                        Console.Write("Nuevo Telefono alternativo: ");
                        string telefonoAlt = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(telefonoAlt)) personaModificar.TelefonoAlternativo = telefonoAlt;

                        Console.Write("Nuevo Instagram: ");
                        string instagram = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(instagram)) personaModificar.Instagram = instagram;

                        Console.Write("Nueva Profesion: ");
                        string profesion = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(profesion)) personaModificar.Profesion = profesion;

                        Console.Write("Nueva Empresa: ");
                        string empresa = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(empresa)) personaModificar.Empresa = empresa;

                        Console.Write("Nuevo Nivel de estudios: ");
                        string nivelEstudios = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(nivelEstudios)) personaModificar.NivelEstudios = nivelEstudios;

                        Console.Write("Nuevo Estado de la persona (Activo/Inactivo): ");
                        string estadoPersona = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(estadoPersona)) personaModificar.EstadoPersona = estadoPersona;

                        Console.Write("Nuevo Metodo de pago: ");
                        string metodoPago = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(metodoPago)) personaModificar.MetodoPago = metodoPago;

                        Console.Write("Nuevas Observaciones: ");
                        string observaciones = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(observaciones)) personaModificar.Observaciones = observaciones;

                        Console.Write("Nueva Fecha apertura: ");
                        string fechaApertura = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(fechaApertura)) personaModificar.FechaApertura = fechaApertura;

                        Console.Write("Nuevo Limite credito (dejar en blanco para mantener el valor): ");
                        string limiteTexto = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(limiteTexto) && decimal.TryParse(limiteTexto, out decimal limiteModi))
                            personaModificar.LimiteCredito = limiteModi;

                        Console.Write("Nuevo Estado de la cuenta (Activo/Suspendido): ");
                        string estadoCuenta = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(estadoCuenta)) personaModificar.EstadoCuenta = estadoCuenta;

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