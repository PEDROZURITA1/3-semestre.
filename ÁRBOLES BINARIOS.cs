using System;

class Nodo
{
    public int dato;
    public Nodo izquierda, derecha;

    public Nodo(int dato)
    {
        this.dato = dato;
        izquierda = derecha = null;
    }
}

class ArbolBinario
{
    private Nodo raiz;

    public ArbolBinario()
    {
        raiz = null;
    }

    public void Insertar(int dato)
    {
        raiz = InsertarRec(raiz, dato);
    }

    private Nodo InsertarRec(Nodo raiz, int dato)
    {
        if (raiz == null)
        {
            raiz = new Nodo(dato);
            return raiz;
        }
        if (dato < raiz.dato)
            raiz.izquierda = InsertarRec(raiz.izquierda, dato);
        else if (dato > raiz.dato)
            raiz.derecha = InsertarRec(raiz.derecha, dato);

        return raiz;
    }

    public void InOrden()
    {
        InOrdenRec(raiz);
    }

    private void InOrdenRec(Nodo raiz)
    {
        if (raiz != null)
        {
            InOrdenRec(raiz.izquierda);
            Console.Write(raiz.dato + " ");
            InOrdenRec(raiz.derecha);
        }
    }

    public void PreOrden()
    {
        PreOrdenRec(raiz);
    }

    private void PreOrdenRec(Nodo raiz)
    {
        if (raiz != null)
        {
            Console.Write(raiz.dato + " ");
            PreOrdenRec(raiz.izquierda);
            PreOrdenRec(raiz.derecha);
        }
    }

    public void PostOrden()
    {
        PostOrdenRec(raiz);
    }

    private void PostOrdenRec(Nodo raiz)
    {
        if (raiz != null)
        {
            PostOrdenRec(raiz.izquierda);
            PostOrdenRec(raiz.derecha);
            Console.Write(raiz.dato + " ");
        }
    }

    public bool Buscar(int dato)
    {
        return BuscarRec(raiz, dato);
    }

    private bool BuscarRec(Nodo raiz, int dato)
    {
        if (raiz == null)
            return false;
        if (raiz.dato == dato)
            return true;
        if (dato < raiz.dato)
            return BuscarRec(raiz.izquierda, dato);
        return BuscarRec(raiz.derecha, dato);
    }
}

class Program
{
    static void Main()
    {
        ArbolBinario arbol = new ArbolBinario();
        int opcion;

        do
        {
            Console.WriteLine("\n--- Menú Árbol Binario ---");
            Console.WriteLine("1. Insertar nodo");
            Console.WriteLine("2. Buscar nodo");
            Console.WriteLine("3. Recorrido InOrden");
            Console.WriteLine("4. Recorrido PreOrden");
            Console.WriteLine("5. Recorrido PostOrden");
            Console.WriteLine("6. Salir");
            Console.Write("Seleccione una opción: ");
            opcion = int.Parse(Console.ReadLine());

            switch (opcion)
            {
                case 1:
                    Console.Write("Ingrese un número: ");
                    int dato = int.Parse(Console.ReadLine());
                    arbol.Insertar(dato);
                    break;
                case 2:
                    Console.Write("Ingrese un número a buscar: ");
                    int buscar = int.Parse(Console.ReadLine());
                    Console.WriteLine(arbol.Buscar(buscar) ? "Nodo encontrado" : "Nodo no encontrado");
                    break;
                case 3:
                    Console.Write("Recorrido InOrden: ");
                    arbol.InOrden();
                    Console.WriteLine();
                    break;
                case 4:
                    Console.Write("Recorrido PreOrden: ");
                    arbol.PreOrden();
                    Console.WriteLine();
                    break;
                case 5:
                    Console.Write("Recorrido PostOrden: ");
                    arbol.PostOrden();
                    Console.WriteLine();
                    break;
                case 6:
                    Console.WriteLine("Saliendo...");
                    break;
                default:
                    Console.WriteLine("Opción inválida");
                    break;
            }
        } while (opcion != 6);
    }
}
