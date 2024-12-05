using System;

namespace FigurasGeometricas
{
    // Clase base abstracta para representar una figura geométrica
    public abstract class FiguraGeometrica
    {
        // Método abstracto para calcular el área
        public abstract double CalcularArea();

        // Método abstracto para calcular el perímetro
        public abstract double CalcularPerimetro();
    }

    // Clase para representar un círculo
    public class Circulo : FiguraGeometrica
    {
        private double radio;

        // Constructor para inicializar el radio del círculo
        public Circulo(double radio)
        {
            this.radio = radio;
        }

        // Método para calcular el área del círculo
        public override double CalcularArea()
        {
            return Math.PI * radio * radio;
        }

        // Método para calcular el perímetro del círculo (circunferencia)
        public override double CalcularPerimetro()
        {
            return 2 * Math.PI * radio;
        }
    }

    // Clase para representar un cuadrado
    public class Cuadrado : FiguraGeometrica
    {
        private double lado;

        // Constructor para inicializar el lado del cuadrado
        public Cuadrado(double lado)
        {
            this.lado = lado;
        }

        // Método para calcular el área del cuadrado
        public override double CalcularArea()
        {
            return lado * lado;
        }

        // Método para calcular el perímetro del cuadrado
        public override double CalcularPerimetro()
        {
            return 4 * lado;
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            // Crear un círculo con radio 5
            Circulo circulo = new Circulo(5);

            // Crear un cuadrado con lado 3
            Cuadrado cuadrado = new Cuadrado(3);

            // Calcular y mostrar el área y perímetro de cada figura
            Console.WriteLine("Área del círculo: " + circulo.CalcularArea());
            Console.WriteLine("Perímetro del círculo: " + circulo.CalcularPerimetro());

            Console.WriteLine("Área del cuadrado: " + cuadrado.CalcularArea());
            Console.WriteLine("Perímetro del cuadrado: " + cuadrado.CalcularPerimetro());
        }
    }
}
