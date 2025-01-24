using System;
using System.Collections.Generic;

class Program
{
    static bool VerificarBalanceo(string expresion)
    {
        Stack<char> pila = new Stack<char>();

        foreach (char caracter in expresion)
        {
            if (caracter == '(' || caracter == '{' || caracter == '[')
            {
                pila.Push(caracter); // Agregar a la pila si es un paréntesis abierto.
            }
            else if (caracter == ')' || caracter == '}' || caracter == ']')
            {
                if (pila.Count == 0) return false; // No hay apertura para cerrar.

                char apertura = pila.Pop(); // Retirar el último elemento.

                // Verificar si coinciden los paréntesis.
                if ((caracter == ')' && apertura != '(') ||
                    (caracter == '}' && apertura != '{') ||
                    (caracter == ']' && apertura != '['))
                {
                    return false;
                }
            }
        }

        return pila.Count == 0; // Verificar si quedan elementos sin cerrar.
    }

    static void Main()
    {
        string formula = "{7+(8*5)-[(9-7)+(4+1)]}";
        Console.WriteLine($"La fórmula está balanceada: {VerificarBalanceo(formula)}");
    }
}
