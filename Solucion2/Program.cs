//Grupo 18
//Integrantes: Quinteros Ivana, Zapata Santiago y Bayer Jeremias
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solucion2
{
    /*Definición de tipos
    Realizamos esta definición fuera del programa para separar visualmente 
    las definiciones de tipos de la lógica del programa*/
    
    //Usamos enum para las categorias, ya que son fijas
    enum Categoria
    {
        Infantiles,
        Cadetes,
        Juveniles,
        Primera,
        Veteranos
    }

    //Utilizamos estructuras para definir Equipos y Jugadores
    struct Equipo
    {
        public string NombreClub;
        public string NombreCompleto;
        public Categoria Categoria;
        public int CantidadJugadores;
    }
    struct Jugador
    {
        public int DNI;
        public string Nombre;
        public string Apellido;
        public int Edad;
        public List<string> Equipos;
        public bool Seguro;
        public bool Afiliacion;
    }
    
     
    internal class Program
    {
        //declaramos la lista generales donde tendremos agrupado clubes, equipos y jugadores de la liga
        static List<string> listaClubes = new List<string>();
        static List<Equipo> listaEquipos = new List<Equipo>();
        static List<Jugador> listaJugadores = new List<Jugador>();

        //Definimos los métodos con las funcionalidades generales primero
        //
        //-------------------------------------------
        //
        //EQUIPOS 
        //
        //-------------------------------------------
        //ALTA EQUIPO
        //-------------------------------------------       
        /// <summary>
        /// Permite registrar un nuevo equipo en el sistema. Solicita seleccionar un club existente 
        /// o ingresar uno nuevo, elige la categoría y genera automáticamente el nombre del equipo.
        /// </summary>
        static void AltaEquipo()
        {
            Console.Clear();
            Console.WriteLine("--- ALTA EQUIPO ---");
            string nombreClub = "";

            if (listaClubes.Count == 0)
            {
                //Si no hay clubes aviso y permito que cargen un nuevo club
                Console.WriteLine("No hay clubes registrados.");
                nombreClub = IngresarNuevoClub();
            }
            else
            {
                //Si hay clubes, los mostramos y permitir elegir entre ello o agregar nuevo
                Console.WriteLine("\nClubes disponibles:");
                MostrarClubes();
                Console.WriteLine("0. + INGRESAR UN CLUB NUEVO...");

                int opcion = SeleccionarOpcion(0, listaClubes.Count);

                if (opcion == 0)
                {
                    nombreClub = IngresarNuevoClub();
                }
                else
                {
                    nombreClub = listaClubes[opcion - 1];
                }
            }
           
            //Permito que seleccionen la categoria que a la que va a pertenecer
            Categoria categoria = SeleccionarCategoria();

            //Genero el nombes
            string nombreCompleto = GenerarNombreEquipo(nombreClub, categoria);

            //Creo el equipo y lo agrego a la lista
            Equipo nuevo;
            nuevo.NombreClub = nombreClub;
            nuevo.NombreCompleto = nombreCompleto;
            nuevo.Categoria = categoria;
            nuevo.CantidadJugadores = 0;
            listaEquipos.Add(nuevo);

            //Muestro el equipo creado
            Console.WriteLine($"\nEquipo creado: {nombreCompleto}");
            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }


        //-------------------------------------------
        //BAJA EQUIPO
        //-------------------------------------------
        //SE ELIMINA
        /// <summary>
        /// Permite eliminar un equipo del sistema. Solo se puede dar de baja si el equipo
        /// no tiene jugadores asignados. Solicita confirmación antes de eliminar.
        /// </summary>
        static void BajaEquipo()
        {
            Console.Clear();
            Console.WriteLine("--- BAJA DE EQUIPO ---");

            //Si no hay equipos aviso
            if (!HayEquipos()) return;

            //permito que seleccionen el equipo, por el club
            string nombreEquipoElegido = SeleccionarEquipoPorClub();

            //recorro lista de 
            for (int i = 0; i < listaEquipos.Count; i++)
            {
                //busco el equipo con el nombre elegido
                if (listaEquipos[i].NombreCompleto == nombreEquipoElegido)
                {
                    //corroboro si tiene 0 o mas jugadores
                    if (listaEquipos[i].CantidadJugadores > 0)
                    {
                        Console.WriteLine("No se puede eliminar, tiene jugadores asignados.");
                    }
                    else
                    {
                        Console.WriteLine($"\nVa a eliminar el equipo: {nombreEquipoElegido}");
                        if (Confirmar("¿Confirma la eliminación? S/N"))
                        {
                            listaEquipos.RemoveAt(i);
                            Console.WriteLine("Equipo eliminado exitosamente.");
                        }
                        else
                        {
                            Console.WriteLine("Baja cancelada.");
                        }
                    }
                    break;
                }
            }

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        //-------------------------------------------
        //MODIFICAR EQUIPO
        //-------------------------------------------
        //Se decidio que considerando que los nombres de los equipos estan asociados a
        //club y categoria designado a la hora de crearlo, no tiene sentido que esos valores se puedan modificar
        //por tanto se definió solo poder eleminar un jugador del equipo o eliminar a todos los jugadores
        /// <summary>
        /// Permite modificar un equipo existente. Ofrece la opción de eliminar un jugador
        /// individual del equipo o eliminar todos los jugadores a la vez. Decrementando la cantidad de jugadores del equipo
        /// y borrando el equipo de la lista de equipos del jugador
        /// </summary>
        static void ModificarEquipo()
        {
            Console.Clear();
            Console.WriteLine("--- MODIFICAR EQUIPO ---");
            //Aviso si no hay equipos que se puedan modificar
            if (!HayEquipos()) return;
            //Seleccipon de club y equipo de dicho club
            string nombreEquipoElegido = SeleccionarEquipoPorClub();

            //Se solicita que indique que modificación realizará
            Console.WriteLine("\n¿Qué desea modificar?");
            Console.WriteLine("1. Eliminar un jugador del equipo");
            Console.WriteLine("2. Eliminar todos los jugadores del equipo");

            //-----------------
            //Se me ocurre que aca se podria incorporar si desea asignar un jugador cargado, solicitando que ingrese DNI
            //SI HAY TIEMPO PODEMOS INCORPORARLO
            //-----------------

            int opcion = SeleccionarOpcion(1, 2);
           
            //Si desea eliminar un jugador de un equipo
            if (opcion == 1)
            {
                //Lidta de jugadores para guardar los jugadores del equipo
                List<Jugador> jugadoresDelEquipo = new List<Jugador>();
                //recorro la lista de jugadores, si son del equipo los guardo en la lista creada
                for (int i = 0; i < listaJugadores.Count; i++)
                {
                    if (listaJugadores[i].Equipos.Contains(nombreEquipoElegido))
                    {
                        //agregamos a la lista
                        jugadoresDelEquipo.Add(listaJugadores[i]);
                        //Mostramos los jugadores que encontramos
                        Console.WriteLine($"{jugadoresDelEquipo.Count}. {listaJugadores[i].Nombre} {listaJugadores[i].Apellido} DNI: {listaJugadores[i].DNI}");
                    }
                }

                //si no hay jugadores en el equipo osea lista vacia, avisa
                if (jugadoresDelEquipo.Count == 0)
                {
                    Console.WriteLine("El equipo no tiene jugadores asignados.");
                }

                //en caso de que se encontraron y mostraron jugadores deben elegir el que desean eliminar
                else
                {
                    //Permitimos elegir entre los que se mostraron
                    int seleccion = SeleccionarOpcion(1, jugadoresDelEquipo.Count);
                    Jugador jugadorElegido = jugadoresDelEquipo[seleccion - 1];

                    //Confirmamos elimionacion o cancelamos
                    Console.WriteLine($"\nVa a eliminar a {jugadorElegido.Nombre} {jugadorElegido.Apellido} del equipo {nombreEquipoElegido}");
                    if (Confirmar("¿Confirma la eliminación? S/N"))
                    {
                        //Quitamos el equipo de la lista de equipos de jugador
                        QuitarEquipoDeJugador(jugadorElegido.DNI, nombreEquipoElegido);
                        //Decrementar cantidad de jugadores del equipo
                        DecrementarJugadoresEquipo(nombreEquipoElegido);
                        Console.WriteLine("Jugador eliminado del equipo exitosamente.");
                    }
                    else
                    {
                        Console.WriteLine("Modificación cancelada.");
                    }
                }
            }
            else if (opcion == 2)
            {
                Console.WriteLine($"\nVa a eliminar todos los jugadores del equipo {nombreEquipoElegido}");
                //confirmamos que vaya a eliminar
                if (Confirmar("¿Confirma la eliminación? S/N"))
                {
                    //Recorro lista de jugadores de la liga
                    for (int i = 0; i < listaJugadores.Count; i++)
                    {
                        //Identifico si el jugador pertene al equipo
                        if (listaJugadores[i].Equipos.Contains(nombreEquipoElegido))
                        {
                            //Elimino al equipo de la lista de equipos del jugador
                            QuitarEquipoDeJugador(listaJugadores[i].DNI, nombreEquipoElegido);
                        }
                    }

                    //Ponemos cantidad de jugadores del equipo en cero
                    //Recorro la lista de equipos
                    for (int i = 0; i < listaEquipos.Count; i++)
                    {
                        //Busco el equipo
                        if (listaEquipos[i].NombreCompleto == nombreEquipoElegido)
                        {
                            //Realizo una copia del equipo
                            Equipo eq = listaEquipos[i];
                            //Modifico cantidad de jugadores a 0
                            eq.CantidadJugadores = 0;
                            //Remplazo equipo original de la lista por la copia modificada
                            listaEquipos[i] = eq;
                            break;
                        }
                    }
                    Console.WriteLine("Todos los jugadores eliminados del equipo exitosamente.");
                }
                else
                {
                    Console.WriteLine("Modificación cancelada.");
                }
            }

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        //-------------------------------------------
        //
        //JUGADORES
        //
        //-------------------------------------------
        //-------------------------------------------
        //ALTA JUGADOR
        //-------------------------------------------

        /// <summary>
        /// Permite registrar un nuevo jugador en el sistema. Solicita y valida DNI, nombre,
        /// apellido y edad. Determina la categoría automáticamente según la edad, permite
        /// seleccionar el equipo correspondiente e indicar si tiene seguro y afiliación.
        /// </summary>
        static void AltaJugador()
        {
            Console.Clear();
            Console.WriteLine("--- ALTA DE JUGADOR ---");

            //Si no hay equipos doy aviso
            if (!HayEquipos()) return;

            // ingreso y validacion de DNI
            int dni = IngresarEntero("Ingrese DNI: ", 999999, 99999999);
            if (DNIExiste(dni))
            {
                Console.WriteLine("Error: el DNI ya está registrado.");
                Console.WriteLine("\nPresione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }

            // ingreso de datos personales
            string nombre = IngresarString("Ingrese Nombre: ");
            string apellido = IngresarString("Ingrese Apellido: ");
            int edad = IngresarEntero("Ingrese Edad: ", 1, 99);

            // obtener y mostrar categorias por edad
            List<Categoria> categoriasJugador = ObtenerCategoriasPorEdad(edad);
            Console.WriteLine("Categorías correspondientes:");
            foreach (Categoria cat in categoriasJugador)
            {
                Console.WriteLine($"- {cat}");
            }

            
            // elegir club y equipo
            Console.WriteLine("\nSeleccione el Club al que pertenece");
            string nombreClub = ElegirClubExistente();

            Console.WriteLine("\nSeleccione el Equipo al que pertenece");

            string nombreEquipo = SeleccionarEquipoPorClubYCategoria(nombreClub, categoriasJugador);
            if (nombreEquipo == "")
            {
                Console.WriteLine("\nPresione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }

            // seguro y afiliacion
            bool seguro = Confirmar("¿Tiene seguro? S/N:");

            bool afiliacion = Confirmar("¿Tiene afiliación? S/N");

            // crear jugador
            Jugador j;
            j.DNI = dni;
            j.Nombre = nombre;
            j.Apellido = apellido;
            j.Edad = edad;
            j.Equipos = new List<string>();
            j.Seguro = seguro;
            j.Afiliacion = afiliacion;

            // agregar jugador y asignar equipo
            listaJugadores.Add(j);
            listaJugadores[listaJugadores.Count - 1].Equipos.Add(nombreEquipo);
            IncrementarJugadoresEquipo(nombreEquipo);

            Console.WriteLine($"\nJugador {nombre} {apellido} registrado exitosamente en {nombreEquipo}.");
            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }




        //-------------------------------------------
        //BAJA JUGADOR
        //-------------------------------------------
        static void BajaJugador()
        {
            Console.Clear();
            Console.WriteLine("--- BAJA DE JUGADOR ---");

            if (!HayJugadores()) return;

            int indice = BuscarJugador();
            if (indice == -1)
            {
                Console.WriteLine("\nJugador no encontrado.");
                Console.WriteLine("\nPresione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }

            if (indice == -1)
            {
                Console.WriteLine("\nJugador no encontrado.");
                Console.WriteLine("\nPresione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }

            Jugador jugador = listaJugadores[indice];
            Console.WriteLine($"\nJugador encontrado: {jugador.Nombre} {jugador.Apellido} | DNI: {jugador.DNI}");
            Console.WriteLine($"Va a eliminar al jugador {jugador.Nombre} {jugador.Apellido}");

            if (Confirmar("¿Confirma la eliminación? (S/N): "))
            {
                for (int i = 0; i < jugador.Equipos.Count; i++)
                {
                    DecrementarJugadoresEquipo(jugador.Equipos[i]);
                }
                listaJugadores.RemoveAt(indice);
                Console.WriteLine("Jugador eliminado exitosamente.");
            }
            else
            {
                Console.WriteLine("Baja cancelada.");
            }

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        //-------------------------------------------
        //MODIFICAR JUGADOR
        //-------------------------------------------
        //MODIFICAR edad, equipos, seguro y afiliacion
        //No puede estar en mas de club.
        /// <summary>
        /// Permite modificar los datos de un jugador existente: edad, equipos asignados,
        /// seguro y afiliación. Un jugador no puede pertenecer a más de un club.
        /// </summary>
        static void ModificarJugador()
        {
            Console.Clear();
            Console.WriteLine("--- MODIFICAR JUGADOR ---");

            if (!HayJugadores()) return;
            //Buscar jugador por dni o apellido 
            int indice = BuscarJugador();
            if (indice == -1)
            {
                Console.WriteLine("\nJugador no encontrado.");
                Console.WriteLine("\nPresione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }

            //mostramos jugador entontrado y daots
            Jugador jugador = listaJugadores[indice];
            Console.WriteLine($"\nJugador encontrado: {jugador.Nombre} {jugador.Apellido} | Edad: {jugador.Edad}");
            Console.WriteLine($"\nDNI: {jugador.DNI}");

            if (jugador.Seguro)
                Console.WriteLine("Seguro: SI");
            else
                Console.WriteLine("Seguro: NO");
            if (jugador.Afiliacion)
                Console.WriteLine("Afiliacion: SI");
            else
                Console.WriteLine("Afiliacion: NO");

            //Damos opciones para modificar
            Console.WriteLine("\n¿Qué desea modificar?");
            Console.WriteLine("1. Modificar Seguro");
            Console.WriteLine("2. Modificar Afiliacion");
            Console.WriteLine("3. Agregar equipo");
            Console.WriteLine("4. Modificar Edad");  
            Console.WriteLine("-----------------");
            Console.WriteLine("0. Volver al menu principal");

            int opcion = SeleccionarOpcion(0, 4);  
           
            switch (opcion)
            {
                case 1:
                    if (jugador.Seguro)
                        Console.WriteLine("Seguro actual: SI");
                    else
                        Console.WriteLine("Seguro actual: NO");
                    jugador.Seguro = Confirmar("¿Tiene seguro? (S/N): ");
                    listaJugadores[indice] = jugador;
                    Console.WriteLine("Seguro actualizado.");
                    break;

                case 2:
                    if (jugador.Afiliacion)
                        Console.WriteLine("Afiliacion actual: SI");
                    else
                        Console.WriteLine("Afiliacion actual: NO");
                    jugador.Afiliacion = Confirmar("¿Tiene afiliación? (S/N): ");
                    listaJugadores[indice] = jugador;
                    Console.WriteLine("Afiliación actualizada.");
                    break;

                case 3:
                    AgregarEquipoAJugador(indice);
                    break;
                case 4:
                    Console.WriteLine($"Edad actual: {jugador.Edad}");
                    int nuevaEdad = IngresarEntero("Ingrese la nueva edad: ", 1, 99);

                    // Categorías antes y después del cambio
                    List<Categoria> categoriasAntes = ObtenerCategoriasPorEdad(jugador.Edad);
                    List<Categoria> categoriasDespues = ObtenerCategoriasPorEdad(nuevaEdad);

                    // Detectar equipos que ya no son válidos con la nueva edad
                    List<string> equiposInvalidos = new List<string>();
                    foreach (string nombreEq in jugador.Equipos)
                    {
                        // Buscar la categoría del equipo en la lista
                        for (int i = 0; i < listaEquipos.Count; i++)
                        {
                            if (listaEquipos[i].NombreCompleto == nombreEq &&
                                !categoriasDespues.Contains(listaEquipos[i].Categoria))
                            {
                                equiposInvalidos.Add(nombreEq);
                                break;
                            }
                        }
                    }

                    // Advertir si algún equipo quedará inválido
                    if (equiposInvalidos.Count > 0)
                    {
                        Console.WriteLine("\nATENCIÓN: con la nueva edad, el jugador ya no");
                        Console.WriteLine("cumple los requisitos de los siguientes equipos:");
                        foreach (string eq in equiposInvalidos)
                            Console.WriteLine($"  - {eq}");

                        Console.WriteLine("Estos equipos serán quitados al jugador.");

                        if (Confirmar("¿Confirma el cambio de edad? S/N"))
                        {
                            // Quitar los equipos inválidos
                            foreach (string eq in equiposInvalidos)
                            {
                                QuitarEquipoDeJugador(jugador.DNI, eq);
                                DecrementarJugadoresEquipo(eq);
                            }
                            jugador.Edad = nuevaEdad;
                            listaJugadores[indice] = jugador;
                            Console.WriteLine("Edad actualizada y equipos inválidos removidos.");
                        }
                        else
                        {
                            Console.WriteLine("Modificación cancelada.");
                        }
                    }
                    else
                    {
                        // No hay equipos afectados, actualizar directo
                        jugador.Edad = nuevaEdad;
                        listaJugadores[indice] = jugador;
                        Console.WriteLine("Edad actualizada correctamente.");
                    }
                    break;

                case 0:
                    Console.WriteLine("Volviendo al menú principal.");
                    break;
            }

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        //-------------------------------------------
        //
        //LISTAS
        //
        //-------------------------------------------
        //JUGADORES ASEGURADOS
        /// <summary>
        /// Permite mostrar una lista de los jugadores asegurados
        /// </summary>
        static void ListarAsegurados()
        {
            Console.Clear();
            Console.WriteLine("--- JUGADORES ASEGURADOS ---");

            //primero chequeo que haya jugadores
            if (!HayJugadores()) return;

            // si hay filto los asegurados
            //genero mi lista donde guardar los asegurados
            List<Jugador> asegurados = new List<Jugador>();
            //recorro lista de jugadores y me quedo con los asegurados
            for (int i = 0; i < listaJugadores.Count; i++)
            {
                if (listaJugadores[i].Seguro)
                    asegurados.Add(listaJugadores[i]);
            }
            //si no hay asegurados aviso
            if (asegurados.Count == 0)
            {
                Console.WriteLine("No hay jugadores asegurados.");
            }
            //si hay asegurados entonces recorro la lista de asegurados y los muestro
            else
            {
                foreach (Jugador j in asegurados)
                {
                    Console.WriteLine($"{j.Nombre} {j.Apellido} | Equipos: ");
                    foreach (string equipo in j.Equipos)
                    {
                        Console.WriteLine($"  - {equipo}");
                    }
                }
            }

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        //JUGADORES POR EDAD
        /// <summary>
        /// Lista los jugadores por edad de forma creciente
        /// </summary>
        static void ListarPorEdad()
        {
            Console.Clear();
            Console.WriteLine("--- JUGADORES POR CATEGORIA ---");

            //primero chequeo que haya jugadores
            if (!HayJugadores()) return;

            //Si hay jugadores, los voy a tener que guardar en un stack por edad, ingresando el mas viejo al mas joven
            //Para eso debo recorrer la lista de jugadores desde la edad mas alta a la mas baja e ir guardando en el stack
            //entoces debo ir guardandolos en el stack

            //obtengo las edades
            int edadMin = ObtenerEdadMinima();
            int edadMax = ObtenerEdadMaxima();

            Stack<Jugador> stack = new Stack<Jugador>();
            //recorro lista desde el mas grande, guardo en stack y decremento la edad
            for (int edad = edadMax; edad >= edadMin; edad--)
            {
                for (int i = 0; i < listaJugadores.Count; i++)
                {
                    if (listaJugadores[i].Edad == edad)
                        stack.Push(listaJugadores[i]);
                }
            }

            foreach (Jugador j in stack)
            {
                Console.WriteLine($"Edad: {j.Edad} | {j.Nombre} {j.Apellido}");
            }

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();

        }

        //JUGADORES POR CATEGORIA
        //Entendemos que aca listamos segun la categoria a la que PUEDEN pertenecer por edad,
        //No se muestran los jugadores que juegan en cada categoria, de querer realizarlo de esa forma debería modificarse
        /// <summary>
        /// Lista los jugadores que pueden pertenecer a cierta categoria por edad
        /// </summary>
        static void ListarPorCategoria()
        {
            Console.Clear();
            Console.WriteLine("--- JUGADORES POR CATEGORÍA ---");

            if (!HayJugadores()) return;

            Categoria categoria = SeleccionarCategoria();
            List<Jugador> jugadoresFiltrados = ObtenerJugadoresPorCategoria(categoria);

            if (jugadoresFiltrados.Count == 0)
            {
                Console.WriteLine("No hay jugadores en esta categoría.");
            }
            else
            {
                foreach (Jugador j in jugadoresFiltrados)
                {
                    Console.WriteLine($"{j.Nombre} {j.Apellido} | Edad: {j.Edad}");
                }
            }

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        //-----------------------------------
        //
        //REPORTES GENERALES
        //
        //-----------------------------------

        //JUGADOR MAS JOVEN
        static void JugadorMasJoven()
        {
            Console.Clear();
            Console.WriteLine("--- JUGADOR MÁS JOVEN ---");

            if (!HayJugadores()) 
                return;

            int edadMin = ObtenerEdadMinima();

            for (int i = 0; i < listaJugadores.Count; i++)
            {
                if (listaJugadores[i].Edad == edadMin)
                    Console.WriteLine($"{listaJugadores[i].Nombre} {listaJugadores[i].Apellido} | Edad: {edadMin}");
            }

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        //JUGADOR MAS VIEJO
        /// <summary>
        /// Muestra el jugador con mas edad de la liga
        /// </summary>
        static void JugadorMasViejo()
        {
            Console.Clear();
            Console.WriteLine("--- JUGADOR MÁS VIEJO ---");

            if (!HayJugadores()) 
                return;


            int edadMax = ObtenerEdadMaxima();

            for (int i = 0; i < listaJugadores.Count; i++)
            {
                if (listaJugadores[i].Edad == edadMax)
                    Console.WriteLine($"{listaJugadores[i].Nombre} {listaJugadores[i].Apellido} | Edad: {edadMax}");
            }

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        //CANTIDAD DE JUGADORES POR CATEGORIA
        /// <summary>
        /// Muestra los jugadores de la liga que pueden pertenecer a cada categoria
        /// </summary>
        static void CantidadPorCategoria()
        {  
            Console.Clear();
            Console.WriteLine("--- CANTIDAD DE JUGADORES POR CATEGORIA ---");

            if (!HayJugadores())
                return;
            foreach (Categoria categoria in Enum.GetValues(typeof(Categoria)))
            {
                List<Jugador> jugadoresFiltrados = ObtenerJugadoresPorCategoria(categoria);
                Console.WriteLine($"{categoria}: {jugadoresFiltrados.Count} jugadores");
            }
            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();

        }
        //PROMEDIO DE EDAD DE LA LIGA
        /// <summary>
        /// Muestra el promedio de edad de la liga
        /// </summary>
        static void CalcularPromedioEdad()
        {
            Console.Clear();
            Console.WriteLine("--- PROMEDIO DE EDAD ---");

            if (!HayJugadores()) return;

            int sumaEdades = 0;
            foreach (Jugador jug in listaJugadores)
            {
                sumaEdades += jug.Edad;
            }

            double promedio = (double)sumaEdades / listaJugadores.Count;
            Console.WriteLine($"El promedio general de edad en la liga es: {promedio:F2} años.");

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }
        //EQUIPOS INCOMPLETOS
        /// <summary>
        /// Muestra los equipos incompletos que hay en la liga
        /// </summary>
        static void EquiposIncompletos()
        {
            Console.Clear();
            Console.WriteLine("--- EQUIPOS INCOMPLETOS ---");

            if (!HayEquipos()) return;

            bool hayIncompletos = false;

            foreach (Equipo eq in listaEquipos)
            {
                if (eq.Categoria == Categoria.Veteranos && eq.CantidadJugadores < 10)
                {
                    Console.WriteLine($"{eq.NombreCompleto} | Jugadores: {eq.CantidadJugadores}/10");
                    hayIncompletos = true;
                }
                else if (eq.Categoria != Categoria.Veteranos && eq.CantidadJugadores < 9)
                {
                    Console.WriteLine($"{eq.NombreCompleto} | Jugadores: {eq.CantidadJugadores}/9");
                    hayIncompletos = true;
                }
            }

            if (!hayIncompletos)
                Console.WriteLine("Todos los equipos cumplen el mínimo de jugadores requeridos.");

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        //-----------------------------------
        //METODOS AUXILIARES
        //-----------------------------------
        //Usemos static return-type para hacer las funcionalidades y que no quede todo el void
        //

        //Auxiliar para confirmar
        /// <summary>
        /// Solicita confirmación al usuario mediante S/N antes de ejecutar una acción crítica.
        /// </summary>
        /// <returns>True si el usuario confirmó con S, false si ingresó N</returns>
        static bool Confirmar(string mensaje)
        {
            string input = "";
            do
            {
                Console.WriteLine(mensaje);
                input = Console.ReadLine().ToUpper();

                if (input != "S" && input != "N")
                Console.WriteLine("Error: ingrese S o N.");

            } while (input != "S" && input != "N");

            return input == "S";
        }

        //Auxiliar para seleccionar opcion numerica
        /// <summary>
        /// Solicita al usuario que ingrese una opción numérica dentro de un rango válido.
        /// Repite la solicitud hasta recibir un valor correcto.
        /// </summary>
        /// <param name="min">Valor mínimo aceptado</param>
        /// <param name="max">Valor máximo aceptado</param>
        /// <returns>Opción numérica seleccionada por el usuario</returns>
        static int SeleccionarOpcion(int min, int max)
        {
            int seleccion;
            bool valido;

            do
            {
                Console.Write("\nSeleccione una opción: ");
                valido = int.TryParse(Console.ReadLine(), out seleccion) && seleccion >= min && seleccion <= max;

                if (!valido)
                    Console.WriteLine($"Error: ingrese un número entre {min} y {max}.");

            } while (!valido);

            return seleccion;
        }

        //Auxiliar ingreso de string
        /// <summary>
        /// Solicita al usuario el ingreso de una cadena de texto no vacía.
        /// Repite la solicitud si el campo queda vacío.
        /// </summary>
        /// <param name="mensaje">Texto que se muestra al usuario como indicación</param>
        /// <returns>Cadena ingresada por el usuario en mayúsculas</returns>
        static string IngresarString(string mensaje)
        {
            string input = "";
            do
            {
                Console.Write(mensaje);
                input = Console.ReadLine().ToUpper();

                if (input == "")
                    Console.WriteLine("Error: no puede estar vacío.");

            } while (input == "");

            return input;
        }

        //Auxiliar para ingresar y validar enteros pasando max y minimo para usarlo en diferentes lugares
        /// <summary>
        /// Solicita al usuario el ingreso de un número entero dentro de un rango definido.
        /// Valida que sea un número y que esté dentro del mínimo y máximo indicados.
        /// </summary>
        /// <param name="mensaje">Texto que se muestra al usuario como indicación</param>
        /// <param name="min">Valor mínimo aceptado</param>
        /// <param name="max">Valor máximo aceptado</param>
        /// <returns>Entero válido ingresado por el usuario</returns>
        static int IngresarEntero(string mensaje, int min, int max)
        {
            int valor = 0;
            bool esNumero;
            do
            {
                Console.Write(mensaje);
                esNumero = int.TryParse(Console.ReadLine(), out valor);

                if (!esNumero)
                    Console.WriteLine("Error: ingrese un número válido.");
                else if (valor < min || valor > max)
                    Console.WriteLine($"Error: debe ser entre {min} y {max}.");

            } while (!esNumero || valor < min || valor > max);

            return valor;
        }

        //Auxiliar comprobar si hay jugadores y equipo 
        /// <summary>
        /// Revisa si hay jugadores cargados
        /// </summary>
        /// <returns>verdadero si hay jugadores, falso si no hay jugadores</returns>
        static bool HayJugadores()
        {
            if (listaJugadores.Count == 0)
            {
                Console.WriteLine("No hay jugadores registrados.");
                Console.WriteLine("Debera realizar el alta de un jugador para realizar la acción.");
                Console.WriteLine("\nPresione cualquier tecla para continuar...");
                Console.ReadKey();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Revisa si hay equipos cargados
        /// </summary>
        /// <returns>verdadero si hay equipos, falso si no hay equipos</returns>
        static bool HayEquipos()
        {
            if (listaEquipos.Count == 0)
            {
                Console.WriteLine("No hay equipos registrados.");
                Console.WriteLine("Debera realizar el alta de un equipo para realizar la acción.");
                Console.WriteLine("\nPresione cualquier tecla para continuar...");
                Console.ReadKey();
                return false;
            }
            return true;
        }

        //Auxiliar para mostrar los clubes enlistados
        /// <summary>
        /// Muestra por consola la lista numerada de todos los clubes registrados en el sistema.
        /// </summary>
        static void MostrarClubes()
        {
            Console.WriteLine("\nClubes en sistema");
            for (int i = 0; i < listaClubes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {listaClubes[i]}");
            }
        }

        /// <summary>
        /// Muestra los clubes disponibles y permite al usuario seleccionar uno de la lista.
        /// </summary>
        /// <returns>Nombre del club seleccionado</returns>
        static string ElegirClubExistente()
        {
            MostrarClubes();
            int seleccion = SeleccionarOpcion(1, listaClubes.Count);
            return listaClubes[seleccion - 1];
        }

        /// <summary>
        /// Muestra las categorías disponibles y permite al usuario seleccionar una.
        /// </summary>
        /// <returns>Categoría seleccionada por el usuario</returns>
        static Categoria SeleccionarCategoria()
        {
            Console.WriteLine("\nCategorías disponibles:");
            Console.WriteLine("1. Infantiles (hasta 13 años)");
            Console.WriteLine("2. Cadetes (13 a 16 años)");
            Console.WriteLine("3. Juveniles (16 a 18 años)");
            Console.WriteLine("4. Primera (mayores de 18)");
            Console.WriteLine("5. Veteranos (mayores de 35)");

            int opcion = SeleccionarOpcion(1, 5);
            return (Categoria)(opcion - 1);
        }

        /// <summary>
        /// Solicita el nombre de un nuevo club, pide confirmación y lo agrega a la lista de clubes.
        /// </summary>
        /// <returns>Nombre del nuevo club ingresado</returns>
        static string IngresarNuevoClub()
        {
            string nombreClub = "";
            do
            {
                nombreClub = IngresarString("Ingrese el nombre del nuevo club: ");
                Console.WriteLine($"El club a ingresar es: {nombreClub}");
            } while (!Confirmar("¿Confirma el ingreso? S/N"));

            listaClubes.Add(nombreClub);
            return nombreClub;
        }


        /// <summary>
        /// Metodo que genera el nombre del equipo automaticamente
        /// </summary>
        /// <param name="nombreClub">Nombre del club al que pertenecera el equipo</param>
        /// <param name="categoria">Categoria a la que va a pertenecer el equipo</param>
        /// <returns>Nuevo Nombre de Equipo = Club + Cat + Letra</returns>
        static string GenerarNombreEquipo(string nombreClub, Categoria categoria)
        {
            int cantidadExistentes = 0;
            for (int i = 0; i < listaEquipos.Count; i++)
            {
                if (listaEquipos[i].NombreClub == nombreClub && listaEquipos[i].Categoria == categoria)
                cantidadExistentes++;
            }

            char sufijo = (char)('A' + cantidadExistentes);
            return $"{nombreClub} {categoria} {sufijo}";

        }

        /// <summary>
        /// Metodo para elegir un equipo pertenieciente a un club, permite primero ver y elegir club, luego muestra equipos de ese club y permite seleccionar uno
        /// </summary>
        /// <returns>Equipo Seleccionado</returns>
        static string SeleccionarEquipoPorClub()
        {
            Console.WriteLine("Seleccione un club");
            Console.WriteLine("Clubes en sistema:");
            MostrarClubes();
            int seleccionClub = SeleccionarOpcion(1, listaClubes.Count);
            string opcionClub = listaClubes[seleccionClub - 1];

            List<Equipo> equiposDelClub = new List<Equipo>();
            for (int i = 0; i < listaEquipos.Count; i++)
            {
                if (listaEquipos[i].NombreClub == opcionClub)
                {
                    equiposDelClub.Add(listaEquipos[i]);
                    Console.WriteLine($"{equiposDelClub.Count}. {listaEquipos[i].NombreCompleto}");
                }
            }

            int seleccionEquipo = SeleccionarOpcion(1, equiposDelClub.Count);
            return equiposDelClub[seleccionEquipo - 1].NombreCompleto;
        }

        //Auxiliar eliminar equipo de un jugador
        /// <summary>
        /// Quita la referencia a un equipo de la lista de equipos de un jugador, buscándolo por DNI.
        /// </summary>
        /// <param name="DNI">DNI del jugador al que se le quitará el equipo</param>
        static void QuitarEquipoDeJugador(int DNI, string nombreEquipo)
        {
            for (int i = 0; i < listaJugadores.Count; i++)
            {
                if (listaJugadores[i].DNI == DNI)
                {
                    listaJugadores[i].Equipos.Remove(nombreEquipo);
                    break; 
                } 
            }
        }

        //Axiliar para decrementar en uno cantidad de jugadores en un equipo
        /// <summary>
        /// Decrementa en uno el contador de jugadores del equipo indicado.
        /// Realiza una copia de la estructura para poder modificarla y la sobreescribe en la lista.
        /// </summary>
        /// <param name="nombreEquipo">Nombre del equipo cuyo contador se decrementará</param>
        static void DecrementarJugadoresEquipo(string nombreEquipo)
        {
            for (int i = 0; i < listaEquipos.Count; i++)
            {
                //recorro lista de equipos para buscar el equipo que quiero modificar
                if (listaEquipos[i].NombreCompleto == nombreEquipo)
                {
                    //ya que la estructura no permite modificar un valor, realizo una copia
                    Equipo eq = listaEquipos[i];
                    //decremento cantidad de jugagadores
                    eq.CantidadJugadores--;
                    //sobreescribo con la copia modificada
                    listaEquipos[i] = eq;
                    break;
                }
            }
        }
        //Axiliar para incrementar en uno cantidad de jugadores en un equipo
        /// <summary>
        /// Incrementa en uno el contador de jugadores del equipo indicado.
        /// Realiza una copia de la estructura para poder modificarla y la sobreescribe en la lista.
        /// </summary>
        /// <param name="nombreEquipo">Nombre del equipo cuyo contador se incrementará</param>
        static void IncrementarJugadoresEquipo(string nombreEquipo)
        {
            for (int i = 0; i < listaEquipos.Count; i++)
            {
                if (listaEquipos[i].NombreCompleto == nombreEquipo)
                {
                    Equipo eq = listaEquipos[i];
                    eq.CantidadJugadores++;
                    listaEquipos[i] = eq;
                    break;
                }
            }
        }

        //Auxiliar que no este cargado ya ese DNI
        /// <summary>
        /// Verifica si ya existe un jugador registrado con el DNI indicado.
        /// </summary>
        /// <param name="dni">DNI a verificar</param>
        /// <returns>True si el DNI ya está registrado, false si no existe</returns>
        static bool DNIExiste(int dni)
        {
            foreach (Jugador j in listaJugadores)
            {
                if (j.DNI == dni)
                    return true;
            }
            return false;
        }

        //Obtenemos la categoria con la edad
        //Aca definimos entonces que cuando decimos hasta es que si tioene 15 y 9 meses juega pero
        //una vez cumplido 16 entra en siguiente categoria
        /// <summary>
        /// Determina la categoría correspondiente a un jugador según su edad.
        /// Infantiles: menores de 13 | Cadetes: 13 a 15 | Juveniles: 16 a 17 |
        /// Primera: 18 a 34 | Veteranos: 35 o más.
        /// </summary>
        /// <param name="edad">Edad del jugador</param>
        /// <returns>Categoría correspondiente según la edad</returns>
        static List<Categoria> ObtenerCategoriasPorEdad(int edad)
        {
            List<Categoria> categorias = new List<Categoria>();

            if (edad <= 13)
                categorias.Add(Categoria.Infantiles);
            if (edad >= 13 && edad <= 16)
                categorias.Add(Categoria.Cadetes);
            if (edad >= 16 && edad <= 18)
                categorias.Add(Categoria.Juveniles);
            if (edad >= 18 && edad <= 34)
                categorias.Add(Categoria.Primera);
            if (edad >= 35)
                categorias.Add(Categoria.Veteranos);

            return categorias;
        }


        //Seleccionar equipo segun club y categoria
        /// <summary>
        /// Filtra y muestra los equipos disponibles de un club para una categoría específica,
        /// y permite al usuario seleccionar uno.
        /// </summary>
        /// <param name="nombreClub">Nombre del club a filtrar</param>
        /// <param name="categoria">Categoría por la que se filtrarán los equipos</param>
        /// <returns>Nombre del equipo seleccionado, o cadena vacía si no hay equipos disponibles</returns>
        static string SeleccionarEquipoPorClubYCategoria(string nombreClub, List<Categoria> categorias)
        {
            Console.WriteLine("\nEquipos en sistema:");
            List<Equipo> equiposFiltrados = new List<Equipo>();
            for (int i = 0; i < listaEquipos.Count; i++)
            {
                if (listaEquipos[i].NombreClub == nombreClub && categorias.Contains(listaEquipos[i].Categoria))
                {
                    equiposFiltrados.Add(listaEquipos[i]);
                    Console.WriteLine($"{equiposFiltrados.Count}. {listaEquipos[i].NombreCompleto}");
                }
            }

            if (equiposFiltrados.Count == 0)
            {
                Console.WriteLine("No hay equipos disponibles para estas categorías.");
                return "";
            }

            int seleccion = SeleccionarOpcion(1, equiposFiltrados.Count);
            return equiposFiltrados[seleccion - 1].NombreCompleto;
        }

        //Auxiliar para buscar jugador por dni
        /// <summary>
        /// Busca un jugador en la lista por su DNI y devuelve su índice.
        /// </summary>
        /// <param name="dni">DNI del jugador a buscar</param>
        /// <returns>Índice del jugador en la lista, o -1 si no se encuentra</returns>
        static int BuscarJugadorPorDNI(int dni)
        {
            for (int i = 0; i < listaJugadores.Count; i++)
            {
                if (listaJugadores[i].DNI == dni)
                    return i;
            }
            return -1;
        }
        /// <summary>
        /// Busca a jugadores con un cierto apellido
        /// </summary>
        /// <param name="apellido">Apellido del DNI a buscar</param>
        /// <returns>Lista de jugadores con un apellido</returns>
        static List<int> BuscarJugadorPorApellido(string apellido)
        {
            List<int> coincidencias = new List<int>();
            for (int i = 0; i < listaJugadores.Count; i++)
            {
                if (listaJugadores[i].Apellido == apellido)
                    coincidencias.Add(i);
            }
            return coincidencias;
        }

        //auxliar para agregar equipo a jugador
        /// <summary>
        /// Incorpora a un jugador un equipo a elección, busca la categoria o categorias del jugador,
        /// permite buscar los equipos
        /// </summary>
        /// <param name="indice">Índice del jugador en lista de jugadores</param>
        static void AgregarEquipoAJugador(int indice)
        {
            Jugador j = listaJugadores[indice];
            //Buscamos a que categoria pertenece el jugador y las mostramos
            List<Categoria> categoriasJugador = ObtenerCategoriasPorEdad(j.Edad);
            Console.WriteLine("Categorías:");
            foreach (Categoria cat in categoriasJugador)
            {
                Console.WriteLine($"- {cat}");
            }
            //Identificamos si ya pertene a un club
            string nombreClub = "";
            //En caso que el jugador este en un equipo, identifica a que club pertenece
            if (j.Equipos.Count > 0)
                nombreClub = ObtenerClubDeEquipo(j.Equipos[0]);
            //Caso contrario, permite elegir un club
            else
                nombreClub = ElegirClubExistente();

            //Al tener club, buscamos los equipos que pertenec a ese club y la categoria del jugador
            string nombreEquipo = SeleccionarEquipoPorClubYCategoria(nombreClub, categoriasJugador);
            if (nombreEquipo == "")
            {
                Console.WriteLine("No hay equipos disponibles para esta categoría.");
                return;
            }

            if (j.Equipos.Contains(nombreEquipo))
                Console.WriteLine("El jugador ya pertenece a ese equipo.");
            else
            {
                listaJugadores[indice].Equipos.Add(nombreEquipo);
                IncrementarJugadoresEquipo(nombreEquipo);
                Console.WriteLine($"Jugador agregado a {nombreEquipo} exitosamente.");
            }
        }

        /// <summary>
        /// Recorre la lista de equipos por nombres y busca el club al que pertece ese equipo
        /// </summary>
        /// <param name="nombreEquipo">Equipo del cual queremos obtener su club</param>
        /// <returns>Nombre del club al que pertenece un equipo, si no lo encuentra, devuelve un string vacio</returns>
        static string ObtenerClubDeEquipo(string nombreEquipo)
        {
            for (int i = 0; i < listaEquipos.Count; i++)
            {
                if (listaEquipos[i].NombreCompleto == nombreEquipo)
                    return listaEquipos[i].NombreClub;
            }
            return "";
        }

        //Auxiliar para edad minima y maxima
        /// <summary>
        /// Obtiene la edad minima de todos los jugadores de la liga
        /// </summary>
        /// <returns>Edad minima (int)</returns>
        static int ObtenerEdadMinima()
        {
            int edadMin = listaJugadores[0].Edad;
            for (int i = 1; i < listaJugadores.Count; i++)
            {
                if (listaJugadores[i].Edad < edadMin)
                    edadMin = listaJugadores[i].Edad;
            }
            return edadMin;
        }

        /// <summary>
        /// Obtiene la edad minima de todos los jugadores de la liga
        /// </summary>
        /// <returns>Edad máxima (int)</returns>
        static int ObtenerEdadMaxima()
        {
            int edadMax = listaJugadores[0].Edad;
            for (int i = 1; i < listaJugadores.Count; i++)
            {
                if (listaJugadores[i].Edad > edadMax)
                    edadMax = listaJugadores[i].Edad;
            }
            return edadMax;
        }

        /// <summary>
        /// Obtiene todos los jugadores de la liga que pertenecen a una categoria
        /// </summary>
        /// <param name="categoria">Categoria de la cual queremos obtener los jugadores</param>
        /// <returns>Lista de jugadores de una categoria</returns>
        static List<Jugador> ObtenerJugadoresPorCategoria(Categoria categoria)
        {
            List<Jugador> jugadoresFiltrados = new List<Jugador>();
            for (int i = 0; i < listaJugadores.Count; i++)
            {
                if (ObtenerCategoriasPorEdad(listaJugadores[i].Edad).Contains(categoria))
                    jugadoresFiltrados.Add(listaJugadores[i]);
            }
            return jugadoresFiltrados;
        }

        /// <summary>
        /// Permite buscar un jugador por DNI o apellido
        /// </summary>
        /// <returns>indice del jugador en lista de jugadores</returns>
        static int BuscarJugador()
        {
            Console.WriteLine("¿Cómo desea buscar al jugador?");
            Console.WriteLine("1. Buscar por DNI");
            Console.WriteLine("2. Buscar por Apellido");
            Console.WriteLine("0. Volver al menú principal");

            int opcionBusqueda = SeleccionarOpcion(0, 2);
            int indice = -1;

            switch (opcionBusqueda)
            {
                case 1:
                    int dni = IngresarEntero("Ingrese DNI: ", 1, 99999999);
                    indice = BuscarJugadorPorDNI(dni);
                    break;

                case 2:
                    string apellido = IngresarString("Ingrese el apellido: ");
                    List<int> coincidencias = BuscarJugadorPorApellido(apellido);

                    if (coincidencias.Count == 0)
                        indice = -1;
                    else if (coincidencias.Count == 1)
                        indice = coincidencias[0];
                    else
                    {
                        Console.WriteLine($"\nSe encontraron {coincidencias.Count} jugadores:");
                        for (int i = 0; i < coincidencias.Count; i++)
                        {
                            Jugador jugador = listaJugadores[coincidencias[i]];
                            Console.WriteLine($"{i + 1}. {jugador.Nombre} {jugador.Apellido} | DNI: {jugador.DNI}");
                        }
                        int seleccion = SeleccionarOpcion(1, coincidencias.Count);
                        indice = coincidencias[seleccion - 1];
                    }
                    break;

                case 0:
                    return -1;
            }

            return indice;
        }
        //--------------------------------------
        //MENU PRINCIPAL
        //--------------------------------------

        static void Main(string[] args)
        {
            int opcion = -1;
            do
            {
                Console.Clear();
                Console.WriteLine("========================================");
                Console.WriteLine("       LIGA DEPORTIVA - GESTIÓN         ");
                Console.WriteLine("========================================");

                Console.WriteLine("\n--- GESTIÓN DE EQUIPOS ---");
                Console.WriteLine("1. Alta de Equipo");
                Console.WriteLine("2. Baja de Equipo");
                Console.WriteLine("3. Modificar Equipo");

                Console.WriteLine("\n--- GESTIÓN DE JUGADORES ---");
                Console.WriteLine("4. Alta de Jugador");
                Console.WriteLine("5. Baja de Jugador");
                Console.WriteLine("6. Modificar Jugador");

                Console.WriteLine("\n--- LISTADOS DE JUGADORES ---");
                Console.WriteLine("7. Jugadores Asegurados");
                Console.WriteLine("8. Jugadores por Edad");
                Console.WriteLine("9. Jugadores por Categoría");

                Console.WriteLine("\n--- REPORTES GENERALES ---");
                Console.WriteLine("10. Jugador más joven");
                Console.WriteLine("11. Jugador más viejo");
                Console.WriteLine("12. Promedio de edad general");
                Console.WriteLine("13. Cantidad de jugadores por categoría");

                Console.WriteLine("\n--- CONTROL DE EQUIPOS ---");
                Console.WriteLine("14. Equipos incompletos");

                Console.WriteLine("\n0. Salir");
                Console.WriteLine("========================================");

                opcion = SeleccionarOpcion(0, 14);

                switch (opcion)
                {
                    case 1: AltaEquipo(); break;
                    case 2: BajaEquipo(); break;
                    case 3: ModificarEquipo(); break;
                    case 4: AltaJugador(); break;
                    case 5: BajaJugador(); break;
                    case 6: ModificarJugador(); break;
                    case 7: ListarAsegurados(); break;
                    case 8: ListarPorEdad(); break;
                    case 9: ListarPorCategoria(); break;
                    case 10: JugadorMasJoven(); break;
                    case 11: JugadorMasViejo(); break;
                    case 12: CalcularPromedioEdad(); break;
                    case 13: CantidadPorCategoria(); break;
                    case 14: EquiposIncompletos(); break;
                    case 0: Console.WriteLine("\nSaliendo..."); break;
                }

            } while (opcion != 0);
        }


    }
}

    