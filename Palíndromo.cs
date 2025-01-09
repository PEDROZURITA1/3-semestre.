using System;

class Program
{
    static void Main()
    {
        Console.Write("Introduce una palabra: ");
        string palabra = Console.ReadLine().ToLower();
        string invertida = string.Join("", palabra.Reverse());

        if (palabra == invertida)
        {
            Console.WriteLine("Es un palíndromo.");
        }
        else
        {
            Console.WriteLine("No es un palíndromo.");
        }
    }
}
