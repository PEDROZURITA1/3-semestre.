using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        List<string> asignaturas = new List<string> { "Matemáticas", "Física", "Química", "Historia", "Lengua" };
        Dictionary<string, double> calificaciones = new Dictionary<string, double>();

        foreach (string asignatura in asignaturas)
        {
            Console.Write($"Introduce la nota de {asignatura}: ");
            double nota = double.Parse(Console.ReadLine());
            calificaciones[asignatura] = nota;
        }

        Console.WriteLine("\nNotas obtenidas:");
        foreach (var asignatura in calificaciones)
        {
            Console.WriteLine($"En {asignatura.Key} has sacado {asignatura.Value}");
        }
    }
}
