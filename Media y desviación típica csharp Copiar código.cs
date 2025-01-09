using System;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.Write("Introduce una muestra de números separados por comas: ");
        string[] entrada = Console.ReadLine().Split(',');
        double[] numeros = Array.ConvertAll(entrada, double.Parse);

        double media = numeros.Average();
        double varianza = numeros.Select(n => Math.Pow(n - media, 2)).Average();
        double desviacionTipica = Math.Sqrt(varianza);

        Console.WriteLine($"Media: {media}");
        Console.WriteLine($"Desviación típica: {desviacionTipica}");
    }
}
