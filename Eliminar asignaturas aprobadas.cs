using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        List<string> asignaturas = new List<string> { "Matemáticas", "Física", "Química", "Historia", "Lengua" };
        Dictionary<string, double> calificaciones = new Dictionary<string, double>();
        List<string> asignaturasRepetidas = new List<string>();

        foreach (string asignatura in asignaturas)
        {
            Console.Write($"Introduce la nota de {asignatura}: ");
            double nota = double.Parse(Console.ReadLine());
            if (nota < 5)
            {
                asignaturasRepetidas.Add(asignatura);
            }
        }

        Console.WriteLine("\nAsignaturas que debes repetir:");
        foreach (string asignatura in asignaturasRepetidas)
        {
            Console.WriteLine(asignatura);
        }
    }
}
