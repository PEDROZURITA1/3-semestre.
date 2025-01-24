using System;

class Program
{
    // Función recursiva para resolver Torres de Hanoi.
    static void TorresDeHanoi(int discos, char origen, char destino, char auxiliar)
    {
        if (discos == 1)
        {
            Console.WriteLine($"Mover disco 1 de {origen} a {destino}");
            return;
        }

        // Paso 1: Mover n-1 discos de origen a auxiliar.
        TorresDeHanoi(discos - 1, origen, auxiliar, destino);

        // Paso 2: Mover el disco n de origen a destino.
        Console.WriteLine($"Mover disco {discos} de {origen} a {destino}");

        // Paso 3: Mover n-1 discos de auxiliar a destino.
        TorresDeHanoi(discos - 1, auxiliar, destino, origen);
    }

    static void Main()
    {
        int numeroDiscos = 3;
        Console.WriteLine("Secuencia de movimientos para las Torres de Hanoi:");
        TorresDeHanoi(numeroDiscos, 'A', 'C', 'B');
    }
}
