using System;
using System.Collections.Generic;
using System.Linq;

namespace GrafosPractica
{
    // Clase para representar un grafo mediante lista de adyacencia
    public class Grafo<T> where T : IComparable<T>
    {
        private Dictionary<T, List<Arista<T>>> listaAdyacencia;
        
        public Grafo()
        {
            listaAdyacencia = new Dictionary<T, List<Arista<T>>>();
        }
        
        // Método para agregar un vértice al grafo
        public void AgregarVertice(T vertice)
        {
            if (!listaAdyacencia.ContainsKey(vertice))
                listaAdyacencia.Add(vertice, new List<Arista<T>>());
        }
        
        // Método para agregar una arista entre dos vértices
        public void AgregarArista(T origen, T destino, int peso = 1, bool esDirigido = false)
        {
            // Verificar si los vértices existen, si no, crearlos
            if (!listaAdyacencia.ContainsKey(origen))
                AgregarVertice(origen);
                
            if (!listaAdyacencia.ContainsKey(destino))
                AgregarVertice(destino);
                
            // Agregar la arista del origen al destino
            listaAdyacencia[origen].Add(new Arista<T>(origen, destino, peso));
            
            // Si es no dirigido, agregar también la arista del destino al origen
            if (!esDirigido)
                listaAdyacencia[destino].Add(new Arista<T>(destino, origen, peso));
        }
        
        // Método para obtener todos los vértices del grafo
        public List<T> ObtenerVertices()
        {
            return listaAdyacencia.Keys.ToList();
        }
        
        // Método para obtener todas las aristas del grafo
        public List<Arista<T>> ObtenerAristas()
        {
            List<Arista<T>> aristas = new List<Arista<T>>();
            
            foreach (var vertice in listaAdyacencia.Keys)
            {
                aristas.AddRange(listaAdyacencia[vertice]);
            }
            
            return aristas;
        }
        
        // Método para obtener los vecinos de un vértice
        public List<T> ObtenerVecinos(T vertice)
        {
            if (!listaAdyacencia.ContainsKey(vertice))
                return new List<T>();
                
            return listaAdyacencia[vertice].Select(a => a.Destino).ToList();
        }
        
        // Método para calcular el grado de un vértice
        public int CalcularGrado(T vertice)
        {
            if (!listaAdyacencia.ContainsKey(vertice))
                return 0;
                
            return listaAdyacencia[vertice].Count;
        }
        
        // Método para calcular la centralidad de grado
        public Dictionary<T, double> CalcularCentralidadGrado()
        {
            Dictionary<T, double> centralidad = new Dictionary<T, double>();
            int n = listaAdyacencia.Count;
            
            foreach (var vertice in listaAdyacencia.Keys)
            {
                double grado = CalcularGrado(vertice);
                // Normalizar por el número máximo de conexiones posibles (n-1)
                centralidad[vertice] = grado / (n - 1);
            }
            
            return centralidad;
        }
        
        // Método para calcular la centralidad de cercanía
        public Dictionary<T, double> CalcularCentralidadCercania()
        {
            Dictionary<T, double> centralidad = new Dictionary<T, double>();
            int n = listaAdyacencia.Count;
            
            foreach (var vertice in listaAdyacencia.Keys)
            {
                // Calcular las distancias más cortas desde este vértice a todos los demás
                var distancias = CalcularDistanciasMinimas(vertice);
                
                // Sumar todas las distancias (excluyendo infinitos)
                double sumaDistancias = 0;
                int verticesAlcanzables = 0;
                
                foreach (var dist in distancias)
                {
                    if (dist.Value < double.MaxValue && !dist.Key.Equals(vertice))
                    {
                        sumaDistancias += dist.Value;
                        verticesAlcanzables++;
                    }
                }
                
                // Calcular la centralidad de cercanía
                if (verticesAlcanzables > 0 && sumaDistancias > 0)
                    centralidad[vertice] = (double)(verticesAlcanzables) / sumaDistancias;
                else
                    centralidad[vertice] = 0;
            }
            
            return centralidad;
        }
        
        // Método auxiliar para calcular las distancias mínimas (Dijkstra)
        private Dictionary<T, double> CalcularDistanciasMinimas(T origen)
        {
            Dictionary<T, double> distancias = new Dictionary<T, double>();
            HashSet<T> visitados = new HashSet<T>();
            
            // Inicializar todas las distancias como infinito
            foreach (var vertice in listaAdyacencia.Keys)
            {
                distancias[vertice] = double.MaxValue;
            }
            
            // La distancia al vértice origen es 0
            distancias[origen] = 0;
            
            while (visitados.Count < listaAdyacencia.Count)
            {
                // Encontrar el vértice no visitado con la distancia mínima
                T verticeActual = default(T);
                double distanciaMinima = double.MaxValue;
                
                foreach (var vertice in listaAdyacencia.Keys)
                {
                    if (!visitados.Contains(vertice) && distancias[vertice] < distanciaMinima)
                    {
                        verticeActual = vertice;
                        distanciaMinima = distancias[vertice];
                    }
                }
                
                // Si no se pudo encontrar un vértice, romper el ciclo
                if (EqualityComparer<T>.Default.Equals(verticeActual, default(T)))
                    break;
                
                visitados.Add(verticeActual);
                
                // Actualizar las distancias de los vecinos del vértice actual
                foreach (var arista in listaAdyacencia[verticeActual])
                {
                    if (!visitados.Contains(arista.Destino))
                    {
                        double nuevaDistancia = distancias[verticeActual] + arista.Peso;
                        
                        if (nuevaDistancia < distancias[arista.Destino])
                            distancias[arista.Destino] = nuevaDistancia;
                    }
                }
            }
            
            return distancias;
        }
        
        // Método para imprimir el grafo
        public void ImprimirGrafo()
        {
            foreach (var vertice in listaAdyacencia.Keys)
            {
                Console.Write($"{vertice}: ");
                
                foreach (var arista in listaAdyacencia[vertice])
                {
                    Console.Write($"{arista.Destino}({arista.Peso}) ");
                }
                
                Console.WriteLine();
            }
        }
    }
    
    // Clase para representar una arista del grafo
    public class Arista<T> where T : IComparable<T>
    {
        public T Origen { get; set; }
        public T Destino { get; set; }
        public int Peso { get; set; }
        
        public Arista(T origen, T destino, int peso = 1)
        {
            Origen = origen;
            Destino = destino;
            Peso = peso;
        }
        
        public override string ToString()
        {
            return $"({Origen}, {Destino}, {Peso})";
        }
    }
    
    // Programa principal para demostrar el uso del grafo
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== PRÁCTICA EXPERIMENTAL: IMPLEMENTACIÓN Y REPRESENTACIÓN DE GRAFOS ===\n");
            
            // Ejemplo 1: Grafo de red social
            Console.WriteLine("EJEMPLO 1: GRAFO DE RED SOCIAL\n");
            var grafoRedSocial = new Grafo<string>();
            
            // Agregar vértices (personas)
            string[] personas = { "Ana", "Juan", "Maria", "Pedro", "Carlos", "Sofia" };
            foreach (var persona in personas)
            {
                grafoRedSocial.AgregarVertice(persona);
            }
            
            // Agregar conexiones (amistades)
            grafoRedSocial.AgregarArista("Ana", "Juan");
            grafoRedSocial.AgregarArista("Ana", "Maria");
            grafoRedSocial.AgregarArista("Juan", "Pedro");
            grafoRedSocial.AgregarArista("Maria", "Pedro");
            grafoRedSocial.AgregarArista("Maria", "Sofia");
            grafoRedSocial.AgregarArista("Pedro", "Carlos");
            grafoRedSocial.AgregarArista("Carlos", "Sofia");
            
            // Imprimir la representación del grafo
            Console.WriteLine("Representación del grafo de red social (lista de adyacencia):");
            grafoRedSocial.ImprimirGrafo();
            
            // Calcular métricas de centralidad
            Console.WriteLine("\nMétricas de centralidad para el grafo de red social:");
            
            Console.WriteLine("\nCentralidad de grado (normalizada):");
            var centralidadGrado = grafoRedSocial.CalcularCentralidadGrado();
            foreach (var par in centralidadGrado.OrderByDescending(x => x.Value))
            {
                Console.WriteLine($"{par.Key}: {par.Value:F4}");
            }
            
            Console.WriteLine("\nCentralidad de cercanía:");
            var centralidadCercania = grafoRedSocial.CalcularCentralidadCercania();
            foreach (var par in centralidadCercania.OrderByDescending(x => x.Value))
            {
                Console.WriteLine($"{par.Key}: {par.Value:F4}");
            }
            
            // Ejemplo 2: Grafo de ciudades y distancias
            Console.WriteLine("\n\nEJEMPLO 2: GRAFO DE CIUDADES Y DISTANCIAS\n");
            var grafoCiudades = new Grafo<string>();
            
            // Agregar vértices (ciudades)
            string[] ciudades = { "Quito", "Guayaquil", "Cuenca", "Ambato", "Puyo", "Tena" };
            foreach (var ciudad in ciudades)
            {
                grafoCiudades.AgregarVertice(ciudad);
            }
            
            // Agregar conexiones (rutas) con sus respectivas distancias
            grafoCiudades.AgregarArista("Quito", "Guayaquil", 400);
            grafoCiudades.AgregarArista("Quito", "Ambato", 140);
            grafoCiudades.AgregarArista("Guayaquil", "Cuenca", 200);
            grafoCiudades.AgregarArista("Ambato", "Puyo", 100);
            grafoCiudades.AgregarArista("Puyo", "Tena", 80);
            grafoCiudades.AgregarArista("Tena", "Quito", 190);
            
            // Imprimir la representación del grafo
            Console.WriteLine("Representación del grafo de ciudades (lista de adyacencia):");
            grafoCiudades.ImprimirGrafo();
            
            // Calcular métricas de centralidad
            Console.WriteLine("\nMétricas de centralidad para el grafo de ciudades:");
            
            Console.WriteLine("\nCentralidad de grado (normalizada):");
            var centralidadGradoCiudades = grafoCiudades.CalcularCentralidadGrado();
            foreach (var par in centralidadGradoCiudades.OrderByDescending(x => x.Value))
            {
                Console.WriteLine($"{par.Key}: {par.Value:F4}");
            }
            
            Console.WriteLine("\nCentralidad de cercanía:");
            var centralidadCercaniaCiudades = grafoCiudades.CalcularCentralidadCercania();
            foreach (var par in centralidadCercaniaCiudades.OrderByDescending(x => x.Value))
            {
                Console.WriteLine($"{par.Key}: {par.Value:F4}");
            }
            
            // Análisis de algoritmos
            Console.WriteLine("\n=== ANÁLISIS DE ALGORITMOS UTILIZADOS ===\n");
            Console.WriteLine("1. Algoritmo para calcular centralidad de grado:");
            Console.WriteLine("   - Complejidad: O(V), donde V es el número de vértices");
            Console.WriteLine("   - Ventajas: Simple de calcular y entender, útil para identificar nodos con muchas conexiones directas");
            Console.WriteLine("   - Desventajas: No considera la estructura global del grafo, solo las conexiones inmediatas\n");
            
            Console.WriteLine("2. Algoritmo para calcular centralidad de cercanía (basado en Dijkstra):");
            Console.WriteLine("   - Complejidad: O(V^2), donde V es el número de vértices (implementación simple)");
            Console.WriteLine("   - Ventajas: Considera la estructura global del grafo, identifica nodos que tienen acceso rápido a otros nodos");
            Console.WriteLine("   - Desventajas: Mayor complejidad computacional, puede ser costoso para grafos grandes\n");
            
            Console.WriteLine("Pulse cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}
