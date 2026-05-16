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
    
    //Usamos enum para las categorias
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
    
     //declaramos la lista de equipos donde se guardarán, fuera del main para que los metodos y como static para q los metodos puedan acceder
    static List<string> listaClubes = new List<string>();
    static List<Equipo> listaEquipos = new List<Equipo>();
    static List<Jugador> listaJugadores = new List<Jugador>();

    internal class Program
    {

        //--------------
        //Definimos los métodos

        //La documentación va directo antes de los metodos

        //Usariamos static void, que no devuelve nada en sí para el "main" de cada opcion del menu del sistema



        //----------------------------------------------
        //
        //EQUIPOS 
        //
        //-------------------------------------------
        //ALTA EQUIPO
        //-------------------------------------------
        //Metodo principal
        static void AltaEquipo()
        {
            string nombreClub = "";

            if (listaClubes.Count == 0)
            {
                // no hay clubes, debe ingresar uno nuevo
                Console.WriteLine("No hay clubes registrados.");
                nombreClub = IngresarNuevoClub();
            }
            else
            {
                // hay clubes, mostrar y permitir elegir o agregar nuevo
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
            Categoria categoria = SeleccionarCategoria();
            string nombreCompleto = GenerarNombreEquipo(nombreClub, categoria);

            Equipo nuevo;
            nuevo.NombreClub = nombreClub;
            nuevo.NombreCompleto = nombreCompleto;
            nuevo.Categoria = categoria;
            nuevo.CantidadJugadores = 0;

            listaEquipos.Add(nuevo);
            Console.WriteLine($"\nEquipo creado: {nombreCompleto}");
            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }


        //-------------------------------------------
        //BAJA EQUIPO
        //-------------------------------------------
        //SE ELIMINA
        static void BajaEquipo()
        {
            Console.Clear();
            Console.WriteLine("--- BAJA DE EQUIPO ---");

            if (listaEquipos.Count == 0)
            {
                Console.WriteLine("No hay equipos registrados.");
                Console.WriteLine("\nPresione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }

            string nombreEquipoElegido = SeleccionarEquipoPorClub();

            //primer for para recorrer lista de equipos
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
                        if (Confirmar())
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
        static void ModificarEquipo()
        {
            Console.Clear();
            Console.WriteLine("--- MODIFICAR EQUIPO ---");

            if (listaEquipos.Count == 0)
            {
                Console.WriteLine("No hay equipos registrados.");
                Console.WriteLine("\nPresione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }
            //selecciona equipo
            string nombreEquipoElegido = SeleccionarEquipoPorClub();

            //se decide que modificacion realizara
            Console.WriteLine("\n¿Qué desea modificar?");
            Console.WriteLine("1. Eliminar un jugador del equipo");
            Console.WriteLine("2. Eliminar todos los jugadores del equipo");

            int opcion = SeleccionarOpcion(1, 2);
            //Si desea eliminar un jugador de un equipo
            if (opcion == 1)
            {
                //primero, busco si hay jugadores de ese equipo, recorriendo la lista de jugadores y si hay guardo en una lista temporal
                //genero una lista donde voy a guardar temporalmente los jugadores del equipo
                List<Jugador> jugadoresDelEquipo = new List<Jugador>();
                for (int i = 0; i < listaJugadores.Count; i++)
                {
                    if (listaJugadores[i].Equipos.Contains(nombreEquipoElegido))
                    {
                        jugadoresDelEquipo.Add(listaJugadores[i]);
                        //MOSTRAMOS LOS JUGADORES, a medida que vaya printeando e incorporando jugador a la lista va imprimiento el numero
                        Console.WriteLine($"{jugadoresDelEquipo.Count}. {listaJugadores[i].Nombre} {listaJugadores[i].Apellido}");
                    }
                }

                //si no hay jugadores en el equipo, avisa
                if (jugadoresDelEquipo.Count == 0)
                {
                    Console.WriteLine("El equipo no tiene jugadores asignados.");
                }

                //-----------------
                //Se me ocurre que aca se podria incorporar si desea asignar un jugador cargado, solicitando que ingrese DNI
                //SI HAY TIEMPO PODEMOS INCORPORARLO
                //-----------------

                // 
                else
                {
                    //ya se mostraron, permitimos elegir entre los qwue se mostraron
                    int seleccion = SeleccionarOpcion(1, jugadoresDelEquipo.Count);
                    Jugador jugadorElegido = jugadoresDelEquipo[seleccion - 1];

                    Console.WriteLine($"\nVa a eliminar a {jugadorElegido.Nombre} {jugadorElegido.Apellido} del equipo {nombreEquipoElegido}");
                    if (Confirmar())
                    {
                        QuitarEquipoDeJugador(jugadorElegido.DNI, nombreEquipoElegido);

                        // decrementar cantidad jugadores del equipo
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
                if (Confirmar())
                {
                    for (int i = 0; i < listaJugadores.Count; i++)
                    {
                        if (listaJugadores[i].Equipos.Contains(nombreEquipoElegido))
                        {
                            QuitarEquipoDeJugador(listaJugadores[i].DNI, nombreEquipoElegido);
                        }
                    }

                    // poner cantidad jugadores en 0
                    for (int i = 0; i < listaEquipos.Count; i++)
                    {
                        if (listaEquipos[i].NombreCompleto == nombreEquipoElegido)
                        {
                            Equipo eq = listaEquipos[i];
                            eq.CantidadJugadores = 0;
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
        //ALTA JUGADOR
        //-------------------------------------------

        static void AltaJugador()
        {
            //ingrersar los datos nombre, apellido
            //DNI validar no este duplicado
            //edad
            //matchear con categoria
            //clubes filtrados con equipos de esta categoria
            //seleccion de equipo
            //seguro
            //afiliado

            static void AltaJugador()
            {
                Console.Clear();
                Console.WriteLine("--- ALTA DE JUGADOR ---");

                if (listaEquipos.Count == 0)
                {
                    Console.WriteLine("No hay equipos registrados, debe dar de alta un equipo primero.");
                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                    return;
                }

                // ingreso y validacion de DNI
                int dni = IngresarEntero("Ingrese DNI: ", 1, 99999999);
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

                // obtener categoria por edad
                Categoria categoriaJugador = ObtenerCategoriaPorEdad(edad);
                Console.WriteLine($"Categoría correspondiente: {categoriaJugador}");

                // elegir club y equipo
                Console.WriteLine("\nSeleccione el club:");
                string nombreClub = ElegirClubExistente();

                string nombreEquipo = SeleccionarEquipoPorClubYCategoria(nombreClub, categoriaJugador);
                if (nombreEquipo == "")
                {
                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                    return;
                }

                // seguro y afiliacion
                Console.Write("¿Tiene seguro? ");
                bool seguro = Confirmar();

                Console.Write("¿Tiene afiliación? ");
                bool afiliacion = Confirmar();

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
        }


        //-------------------------------------------
        //MODIFICAR JUGADOR
        //-------------------------------------------
        //MODIFICAR edad, equipos, seguro y afiliacion
        //No puede estar en mas de club.
        static void ModificarJugadores()
        {


        }


        //-------------------------------------------
        //BAJA JUGADOR
        //-------------------------------------------
        

        //-----------------------------------
        //METODOS AUXILIARES
        //-----------------------------------
        //Usemos static return-type para hacer las funcionalidades y que no quede todo el void
        //

        //Auxiliar para confirmar
        static bool Confirmar()
        {
            string input = "";
            do
            {
                Console.Write("¿Confirma? (S/N): ");
                input = Console.ReadLine().ToUpper();

                if (input != "S" && input != "N")
                    Console.WriteLine("Error: ingrese S o N.");

            } while (input != "S" && input != "N");

            return input == "S";
        }

        //Auxiliar para seleccionar opcion numerica
        static int SeleccionarOpcion(int min, int max)
        {
            int seleccion = 0;
            do
            {
                Console.Write("\nSeleccione una opción: ");
                bool esNumero = int.TryParse(Console.ReadLine(), out seleccion);

                if (!esNumero)
                    Console.WriteLine("Error: ingrese un número.");
                else if (seleccion < min || seleccion > max)
                    Console.WriteLine("Error: opción inválida.");

            } while (seleccion < min || seleccion > max);

            return seleccion;
        }

        //Auxiliar ingreso de string
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

        //Auxiliar para mostrar los clubes enlistados

        static void MostrarClubes()
        {
            for (int i = 0; i < listaClubes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {listaClubes[i]}");
            }
        }

        static string ElegirClubExistente()
        {
            MostrarClubes();
            int seleccion = SeleccionarOpcion(1, listaClubes.Count);
            return listaClubes[seleccion - 1];
        }

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

        static string IngresarNuevoClub()
        {
            string nombreClub = "";
            do
            {
                nombreClub = IngresarString("Ingrese el nombre del nuevo club: ");
                Console.WriteLine($"El club a ingresar es: {nombreClub}");
            } while (!Confirmar());

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
        static Categoria ObtenerCategoriaPorEdad(int edad)
        {
            if (edad < 13)
                return Categoria.Infantiles;
            else if (edad >= 13 && edad < 16)
                return Categoria.Cadetes;
            else if (edad >= 16 && edad < 18)
                return Categoria.Juveniles;
            else if (edad >= 18 && edad <= 34)
                return Categoria.Primera;
            else
                return Categoria.Veteranos;
        }


        //Seleccionar equipo segun club y categoria
        static string SeleccionarEquipoPorClubYCategoria(string nombreClub, Categoria categoria)
        {
            List<Equipo> equiposFiltrados = new List<Equipo>();
            for (int i = 0; i < listaEquipos.Count; i++)
            {
                if (listaEquipos[i].NombreClub == nombreClub && listaEquipos[i].Categoria == categoria)
                {
                    equiposFiltrados.Add(listaEquipos[i]);
                    Console.WriteLine($"{equiposFiltrados.Count}. {listaEquipos[i].NombreCompleto}");
                }
            }

            if (equiposFiltrados.Count == 0)
            {
                Console.WriteLine("No hay equipos disponibles para esta categoría.");
                return "";
            }

            int seleccion = SeleccionarOpcion(1, equiposFiltrados.Count);
            return equiposFiltrados[seleccion - 1].NombreCompleto;
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
                    case 12: PromedioEdad(); break;
                    case 13: CantidadPorCategoria(); break;
                    case 14: EquiposIncompletos(); break;
                    case 0: Console.WriteLine("\nSaliendo..."); break;
                }

            } while (opcion != 0);
        }


    }
}
