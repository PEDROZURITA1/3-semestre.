using System;
using System.Collections.Generic;

namespace ThemeParkQueueSystem
{
    public class Visitor
    {
        public string Name { get; set; }
        public int TicketNumber { get; set; }
        public int AssignedSeat { get; set; }

        public Visitor(string name, int ticketNumber)
        {
            Name = name;
            TicketNumber = ticketNumber;
            AssignedSeat = -1;
        }
    }

    public class Attraction
    {
        private Queue<Visitor> visitorQueue;
        private bool[] seats;
        private const int TotalSeats = 30;
        private const int MaxQueueSize = 100; // Tamaño máximo de la cola
        private int currentTicketNumber;

        public Attraction()
        {
            visitorQueue = new Queue<Visitor>();
            seats = new bool[TotalSeats];
            currentTicketNumber = 1;
        }

        public void AddVisitor(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("El nombre no puede estar vacío.");
                return;
            }

            if (visitorQueue.Count >= MaxQueueSize)
            {
                Console.WriteLine("La cola está llena. No se pueden agregar más visitantes.");
                return;
            }

            var visitor = new Visitor(name, currentTicketNumber++);
            visitorQueue.Enqueue(visitor);
            Console.WriteLine($"Visitante {name} agregado a la cola con número de ticket {visitor.TicketNumber}.");
        }

        public void AssignSeats()
        {
            if (visitorQueue.Count == 0)
            {
                Console.WriteLine("No hay visitantes en la cola.");
                return;
            }

            while (visitorQueue.Count > 0)
            {
                int availableSeat = Array.FindIndex(seats, seat => !seat);
                if (availableSeat == -1)
                {
                    Console.WriteLine("No hay asientos disponibles.");
                    break;
                }

                var visitor = visitorQueue.Dequeue();
                visitor.AssignedSeat = availableSeat + 1;
                seats[availableSeat] = true;
                Console.WriteLine($"Asiento {visitor.AssignedSeat} asignado a {visitor.Name} (Ticket #{visitor.TicketNumber}).");
            }
        }

        public void ReleaseSeat(int seatNumber)
        {
            if (seatNumber < 1 || seatNumber > TotalSeats || !seats[seatNumber - 1])
            {
                Console.WriteLine("Número de asiento inválido o ya está libre.");
                return;
            }

            seats[seatNumber - 1] = false;
            Console.WriteLine($"Asiento {seatNumber} liberado.");
        }

        public void ShowQueueStatus()
        {
            Console.WriteLine("\n--- Estado de la Cola ---");
            if (visitorQueue.Count == 0)
            {
                Console.WriteLine("La cola está vacía.");
                return;
            }

            Console.WriteLine($"Visitantes en cola: {visitorQueue.Count}");
            foreach (var visitor in visitorQueue)
            {
                Console.WriteLine($"- {visitor.Name} (Ticket #{visitor.TicketNumber})");
            }
        }

        public void ShowSeatsStatus()
        {
            Console.WriteLine("\n--- Estado de los Asientos ---");
            for (int i = 0; i < TotalSeats; i++)
            {
                Console.WriteLine($"Asiento {i + 1}: {(seats[i] ? "Ocupado" : "Libre")}");
            }
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var attraction = new Attraction();
            bool running = true;

            while (running)
            {
                try
                {
                    Console.WriteLine("\n=== Sistema de Cola del Parque de Diversiones ===");
                    Console.WriteLine("-----------------------------------------------");
                    Console.WriteLine("1. Agregar visitante");
                    Console.WriteLine("2. Asignar asientos");
                    Console.WriteLine("3. Ver estado de la cola");
                    Console.WriteLine("4. Ver estado de los asientos");
                    Console.WriteLine("5. Liberar asiento");
                    Console.WriteLine("6. Salir");
                    Console.Write("Seleccione una opción: ");

                    string option = Console.ReadLine();

                    switch (option)
                    {
                        case "1":
                            Console.Write("Ingrese el nombre del visitante: ");
                            string name = Console.ReadLine();
                            attraction.AddVisitor(name);
                            break;

                        case "2":
                            attraction.AssignSeats();
                            break;

                        case "3":
                            attraction.ShowQueueStatus();
                            break;

                        case "4":
                            attraction.ShowSeatsStatus();
                            break;

                        case "5":
                            Console.Write("Ingrese el número del asiento a liberar: ");
                            if (int.TryParse(Console.ReadLine(), out int seatNumber))
                            {
                                attraction.ReleaseSeat(seatNumber);
                            }
                            else
                            {
                                Console.WriteLine("Entrada inválida. Debe ingresar un número.");
                            }
                            break;

                        case "6":
                            running = false;
                            Console.WriteLine("Saliendo del sistema...");
                            break;

                        default:
                            Console.WriteLine("Opción no válida. Por favor, seleccione una opción del 1 al 6.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ocurrió un error inesperado: {ex.Message}");
                }
            }
        }
    }
}
