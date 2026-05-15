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
            string nombreClub = SeleccionarClub();
            Categoria categoria = SeleccionarCategoria();
            string nombreCompleto = GenerarNombreEquipo(nombreClub, categoria);
            Equipo nuevo;
            nuevo.NombreClub = nombreClub;
            nuevo.NombreCompleto = nombreCompleto;
            nuevo.Categoria = categoria;

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

        static string SeleccionarClub()
        {
            string nombreClub = "";
            int opcionNuevo = listaClubes.Count + 1;
            Console.WriteLine("\nClubes disponibles en el sistema:");
            MostrarClubes();
            //muestro opcion de ingresar nuevo club
            Console.WriteLine($"{opcionNuevo}. + INGRESAR UN CLUB NUEVO...");
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
        //declaramos la lista de equipos donde se guardarán, fuera del main para que los metodos y como static para q los metodos puedan acceder
        static List<string> listaClubes = new List<string>();
        static List<Equipo> listaEquipos = new List<Equipo>()

        //Metodo para que pueda seleccionar una categoria
        static Categoria SeleccionarCategoria()
        {


        }

        //Generar un nombre de equipo
        //Considerando que es automatica, hay que buscar si el club tiene equipos cuanto y en base a eso darle un nombre
        //Se indico que es tipo club fulano A, club fulano B y asi.
        //queda mas lindo si incorporamos la categoria tambien
        //CLUB + CATEGORIA + LETRA
        static string GenerarNombreEquipo(string nombreClub, Categoria categoria)
        {
            int cantidadExistentes = 0;
            for (int i = 0; i < listaEquipos.Count; i++)
            {
                if (listaEquipos[i].NombreClub == nombreClub && listaEquipos[i].Categoria == categoria)
                    cantidadExistentes++;
            }

            char sufijo = (char)('A' + cantidadExistentes);
            return $"{nombreClub} {sufijo}";
        }

        //-------------------------------------------
        //MODIFICAR EQUIPO
        //-------------------------------------------
        //Aca tenemos que definir que modificar, si modificamos la categoria podemos generar incompatibilidad de edades
        //por ahora podemos dejar solo nombre de clñub, que modifca nombre de equipo? 
        //
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

        }

        //auxiliar para elegir de clubes

        static string SeleccionarEquipoPorClub()
        {
            //Eligen club entre los existentes

            MostrarClubes();
            int seleccionClub = SeleccionarOpcion(1, listaClubes.Count);
            string opcionClub = listaClubes[seleccionClub - 1];
            //----------------
            //Mostramos los equipos de ese club
            //Genero variable para saber cuantos equipos hay, para luego pasar variable a la selección
            int cantEquiposClub = 0;
            //genero una lista de los equipos del club, para luego poder acceder por indice al nombre del equipo
            List<Equipo> equiposDelClub = new List<Equipo>();
            for (int i = 0; i < listaEquipos.Count; i++)
            {
                if (listaEquipos[i].NombreClub == opcionClub)
                {
                    equiposDelClub.Add(listaEquipos[i]);
                    cantEquiposClub++;
                    Console.WriteLine($"{cantEquiposClub}. {listaEquipos[i].NombreCompleto}");
                }
            }
            //-------------------
            //Elegir equipo del club
            int seleccionEquipo = SeleccionarOpcion(1, equiposDelClub.Count);
            return equiposDelClub[seleccionEquipo - 1].NombreCompleto;
        }


        //-------------------------------------------
        //BAJA EQUIPO
        //-------------------------------------------
        static void BajaEquipo()
        {
            opcionClub = ElegirClubExistente();
            



        }


        static void AltaJugador()
        {

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



        static void Main(string[] args)
        {


        }


    }
}
