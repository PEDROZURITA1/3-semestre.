using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // Crear el catálogo de revistas
        List<string> catalogo = new List<string>
        {
            "HARRY POTTER",
            "HISTORIA DEL MUNDO",
            "National Geographic",
            "The New Yorker",
            "Time",
            "Forbes",
            "Wired",
            "EL COMERCIO",
            "Popular Science",
            "Scientific American"
        };

        // Menú de opciones
        while (true)
        {
            Console.WriteLine("\nMenú:");
            Console.WriteLine("1. Buscar título (Búsqueda Iterativa)");
            Console.WriteLine("2. Buscar título (Búsqueda Recursiva)");
            Console.WriteLine("3. Salir");
            Console.Write("Seleccione una opción: ");
            string opcion = Console.ReadLine();

            if (opcion == "1")
            {
                Console.Write("Ingrese el título a buscar: ");
                string titulo = Console.ReadLine();
                bool encontrado = BusquedaIterativa(catalogo, titulo);
                Console.WriteLine(encontrado ? "Encontrado" : "No encontrado");
            }
            else if (opcion == "2")
            {
                Console.Write("Ingrese el título a buscar: ");
                string titulo = Console.ReadLine();
                bool encontrado = BusquedaRecursiva(catalogo, titulo, 0);
                Console.WriteLine(encontrado ? "Encontrado" : "No encontrado");
            }
            else if (opcion == "3")
            {
                break;
            }
            else
            {
                Console.WriteLine("Opción no válida. Intente de nuevo.");
            }
        }
    }

    // Búsqueda Iterativa
    static bool BusquedaIterativa(List<string> catalogo, string titulo)
    {
        foreach (string t in catalogo)
        {
            if (t.Equals(titulo, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }
        return false;
    }

    // Búsqueda Recursiva
    static bool BusquedaRecursiva(List<string> catalogo, string titulo, int index)
    {
        if (index >= catalogo.Count)
        {
            return false;
        }
        if (catalogo[index].Equals(titulo, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }
        return BusquedaRecursiva(catalogo, titulo, index + 1);
    }
}
