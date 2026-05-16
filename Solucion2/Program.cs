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
            Console.Clear();
            Console.WriteLine("--- ALTA DE EQUIPO ---");
            //llamo al metodo la seleccionar el club
            string nombreClub = SeleccionarCrearClub();
            Categoria categoria = SeleccionarCategoria();
            string nombreCompleto = GenerarNombreEquipo(nombreClub);
            Equipo nuevo;
            nuevo.NombreClub = nombreClub;
            nuevo.NombreCompleto = nombreCompleto;
            nuevo.Categoria = categoria;
            nuevo.CantidadJugadores = 0;

            listaEquipos.Add(nuevo);
            Console.WriteLine($"Equipo creado: {nombreCompleto}");

        }

        //Metodos auxiliares
        //Usemos static return-type para hacer las funcionalidades y que no quede todo el void
        //Metodo para que seleccione club o ingrese uno nuevo
        //mostrar los club, permitir elegir uno o agregar
        static void MostrarClubes()
        {
            //muestro los clubes recorriendo lista con for y la opcion de nuevo
            for (int i = 0; i < listaClubes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {listaClubes[i]}");
            }
        }
        //metodo general para que selecciones un opcion numerica
        //puedo reutilizar en todos los menus
        //validacion de la selecciones
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

        //metodo quepermite seleccionar o crear club


        static string SeleccionarCrearClub()
        {
            //defino variable para nombre del club
            string nombreClub = "";
            Console.WriteLine("\nClubes disponibles en el sistema:");
            MostrarClubes();
            //variable cantidad de clubes
            int opcionNuevo = listaClubes.Count + 1;
            //muestro opcion de ingresar nuevo club
            Console.WriteLine($"{opcionNuevo}. INGRESAR UN CLUB NUEVO...");
            //permito seleccion de nuevo club
            //uso do-while ya que almenos una vez debe ingresar y validar
            int seleccion = SeleccionarOpcion(1, opcionNuevo);
            if (seleccion == opcionNuevo)
            {
                // ingresa un club nuevo
                Console.Write("Ingrese el nombre del nuevo club: ");
                nombreClub = Console.ReadLine();
                listaClubes.Add(nombreClub);
            }
            else
            {
                // seleccionó uno existente
                nombreClub = listaClubes[seleccion - 1];
            }
            return nombreClub;
        }


        //Metodo para que pueda seleccionar una categoria
        static Categoria SeleccionarCategoria()
        {


        }

        //Generar un nombre de equipo
        //Considerando que es automatica, hay que buscar si el club tiene equipos cuanto y en base a eso darle un nombre
        //Se indico que es tipo club fulano A, club fulano B y asi.
        //queda mas lindo si incorporamos la categoria tambien
        //CLUB + CATEGORIA + LETRA
        static string GenerarNombreEquipo(string nombreClub)
        {
            //ver metodo
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
        static void ModificarEquipo()
        {
            //mostramos clubes, que eliga club y luego los equipos del club
            Console.WriteLine("\nClubes disponibles en el sistema:")
            string clubElegido= ElegirClubExistente()
            //muestro equipos por club
            Console.WriteLine($"Equipos del club : {clubElegido}");
            Console.WriteLine("Selecciones el numero del equipo que desea modificar: ");
            MostrarEquiposPorClub(clubElegido)
            //que eliga una opcion a modificar
            //para eso necesito pasar el valor max y min de la seleccion, entonces debo saber cuantos equipos tengo del club
            int cantEquiposClub = 0;
            for (int i = 0; i < listaEquipos.Count; i++)
            {
                if (listaEquipos[i].NombreClub == clubElegido)
                    cantEquiposClub++;
            }
            int seleccion = SeleccionarOpcion(1, cantEquiposClub);


            //mostrar los jugadores de equipo
            //elegir cual eleminiar del equipo
            //eliminar el equipo de la lista de equipos del jugador y decrementar cantidad de jugadores
            //dar la opcion de borrar todos los jugadores

        }

        //auxiliar para elegir de clubes

        static string SeleccionarClub()
        {
            int seleccion = SeleccionarOpcion(1, listaClubes.Count);
            return listaClubes[seleccion - 1];
        }

        //static string SeleccionarEquipoPorClub()
        //{
        //    //Eligen club entre los existentes

        //    MostrarClubes();
        //    int seleccionClub = SeleccionarOpcion(1, listaClubes.Count);
        //    string opcionClub = listaClubes[seleccionClub - 1];
        //    //----------------
        //    //Mostramos los equipos de ese club
        //    //Genero variable para saber cuantos equipos hay, para luego pasar variable a la selección
        //    int cantEquiposClub = 0;
        //    //genero una lista de los equipos del club, para luego poder acceder por indice al nombre del equipo
        //    List<Equipo> equiposDelClub = new List<Equipo>();
        //    for (int i = 0; i < listaEquipos.Count; i++)
        //    {
        //        if (listaEquipos[i].NombreClub == opcionClub)
        //        {
        //            equiposDelClub.Add(listaEquipos[i]);
        //            cantEquiposClub++;
        //            Console.WriteLine($"{cantEquiposClub}. {listaEquipos[i].NombreCompleto}");
        //        }
        //    }
        //-------------------
        //Elegir equipo del club
        int seleccionEquipo = SeleccionarOpcion(1, equiposDelClub.Count);
            return equiposDelClub[seleccionEquipo - 1].NombreCompleto;
        }


        //-------------------------------------------
        //BAJA EQUIPO
        //-------------------------------------------
        //SE ELIMINA
        static void BajaEquipo()
        {
            opcionClub = ElegirClubExistente();
            



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

        //Validar que el DNI no esté duplicado, devolver booleano
        static bool ValidarDNI()
        {


        }

        /
        static string SeleccionarEquipo()
        {


        }


        //validar que la edad corresponda a la categoria del equipo
        static bool ValidarEdad()
        { 
            

        }

        //-------------------------------------------
        //MODIFICAR JUGADOR
        //-------------------------------------------
        //MODIFICAR edad, equipos, seguro y afiliacion
        //No puede estar en mas de club.
        static void ModificarJugadores()
        {

        }

        static

       //-------------------------------------------
       //BAJA JUGADOR
       //-------------------------------------------
       //SE ELIMINA


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
