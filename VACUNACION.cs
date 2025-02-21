using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        // Crear un conjunto de 500 ciudadanos
        HashSet<string> ciudadanos = new HashSet<string>();
        for (int i = 1; i <= 500; i++)
        {
            ciudadanos.Add($"Ciudadano {i}");
        }

        // Crear conjuntos ficticios de vacunados
        HashSet<string> vacunadosPfizer = new HashSet<string>();
        HashSet<string> vacunadosAstrazeneca = new HashSet<string>();

        // Asignar 75 ciudadanos vacunados con Pfizer
        var random = new Random();
        var ciudadanosList = ciudadanos.ToList();
        for (int i = 0; i < 75; i++)
        {
            int index = random.Next(ciudadanosList.Count);
            vacunadosPfizer.Add(ciudadanosList[index]);
            ciudadanosList.RemoveAt(index); // Evitar duplicados
        }

        // Asignar 75 ciudadanos vacunados con AstraZeneca
        for (int i = 0; i < 75; i++)
        {
            int index = random.Next(ciudadanosList.Count);
            vacunadosAstrazeneca.Add(ciudadanosList[index]);
            ciudadanosList.RemoveAt(index); // Evitar duplicados
        }

        // Operaciones de conjuntos
        HashSet<string> noVacunados = new HashSet<string>(ciudadanos);
        noVacunados.ExceptWith(vacunadosPfizer);
        noVacunados.ExceptWith(vacunadosAstrazeneca);

        HashSet<string> dosVacunas = new HashSet<string>(vacunadosPfizer);
        dosVacunas.IntersectWith(vacunadosAstrazeneca);

        HashSet<string> soloPfizer = new HashSet<string>(vacunadosPfizer);
        soloPfizer.ExceptWith(vacunadosAstrazeneca);

        HashSet<string> soloAstrazeneca = new HashSet<string>(vacunadosAstrazeneca);
        soloAstrazeneca.ExceptWith(vacunadosPfizer);

        // Generar reporte
        using (StreamWriter sw = new StreamWriter("ReporteVacunacion.txt"))
        {
            sw.WriteLine("=== Ciudadanos no vacunados ===");
            foreach (var ciudadano in noVacunados)
            {
                sw.WriteLine(ciudadano);
            }

            sw.WriteLine("\n=== Ciudadanos con dos vacunas ===");
            foreach (var ciudadano in dosVacunas)
            {
                sw.WriteLine(ciudadano);
            }

            sw.WriteLine("\n=== Ciudadanos solo con Pfizer ===");
            foreach (var ciudadano in soloPfizer)
            {
                sw.WriteLine(ciudadano);
            }

            sw.WriteLine("\n=== Ciudadanos solo con AstraZeneca ===");
            foreach (var ciudadano in soloAstrazeneca)
            {
                sw.WriteLine(ciudadano);
            }
        }

        Console.WriteLine("Reporte generado en 'ReporteVacunacion.txt'");
    }
}
