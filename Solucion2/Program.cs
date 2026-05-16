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
        public List<Equipo> Equipos;
        public bool Seguro;
        public bool Afiliacion;
        //No utilizamos equipos asignados ya que cada jugador
        //puede estar en más de un equipo, y usar listas dentro
        //de una estructura puede traer conflictos ya que es un tipo por referencia
        // y la estructura es un tipo por valor
    }
    
     //declaramos la lista de equipos donde se guardarán, fuera del main para que los metodos y como static para q los metodos puedan acceder
    static List<string> listaClubes = new List<string>();
    static List<Equipo> listaEquipos = new List<Equipo>();

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
        //MODIFICAR EQUIPO
        //-------------------------------------------
        //Aca tenemos que definir que modificar, si modificamos la categoria podemos generar incompatibilidad de edades
        //por ahora podemos dejar solo nombre de club, que modifca nombre de equipo? 
        //cantidad de jugadores, que implica
        //Considerando que los nombres se asignan automaticaticamente, no se deberian poder modificar.
        //Dejamos que solo se puedan modificar lo jugadores?
        //baja de jugadores del equipo
         //mostrar los jugadores de equipo
         //elegir cual eleminiar del equipo
         //eliminar el equipo de la lista de equipos del jugador y decrementar cantidad de jugadores
         //dar la opcion de borrar todos los jugadores
        

        




        //-------------------------------------------
        //BAJA EQUIPO
        //-------------------------------------------
        //SE ELIMINA
        static void BajaEquipo()
        {
            if (listaClub.Count > 0)
            {
                SeleccionarEquipoPorClub()
            }
            



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
                Console.Write("Ingrese el nombre del nuevo club: ");
                nombreClub = Console.ReadLine();
                Console.WriteLine($"El club a ingresar es: {nombreClub}");
            } while (!Confirmar());

            listaClubes.Add(nombreClub);
            return nombreClub;
        }



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





        //auxiliar para elegir de clubes


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
