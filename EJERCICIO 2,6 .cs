using System;

// Clase Nodo
class Nodo {
    public int Valor;
    public Nodo Siguiente;

    public Nodo(int valor) {
        Valor = valor;
        Siguiente = null;
    }
}

// Clase Lista Enlazada
class ListaEnlazada {
    public Nodo Cabeza;

    public ListaEnlazada() {
        Cabeza = null;
    }

    // Método para añadir un elemento al final
    public void Agregar(int valor) {
        Nodo nuevoNodo = new Nodo(valor);
        if (Cabeza == null) {
            Cabeza = nuevoNodo;
        } else {
            Nodo actual = Cabeza;
            while (actual.Siguiente != null) {
                actual = actual.Siguiente;
            }
            actual.Siguiente = nuevoNodo;
        }
    }

    // Método para invertir la lista
    public void Invertir() {
        Nodo previo = null, actual = Cabeza, siguiente = null;
        while (actual != null) {
            siguiente = actual.Siguiente;
            actual.Siguiente = previo;
            previo = actual;
            actual = siguiente;
        }
        Cabeza = previo;
    }

    // Método para imprimir la lista
    public void Imprimir() {
        Nodo actual = Cabeza;
        while (actual != null) {
            Console.Write(actual.Valor + " -> ");
            actual = actual.Siguiente;
        }
        Console.WriteLine("null");
    }
}

// Programa principal para el ejercicio 6
class Estudiante {
    public string Cedula;
    public string Nombre;
    public string Apellido;
    public string Correo;
    public double Nota;
    public Estudiante Siguiente;

    public Estudiante(string cedula, string nombre, string apellido, string correo, double nota) {
        Cedula = cedula;
        Nombre = nombre;
        Apellido = apellido;
        Correo = correo;
        Nota = nota;
        Siguiente = null;
    }
}

class ListaEstudiantes {
    public Estudiante Aprobados;
    public Estudiante Reprobados;

    public ListaEstudiantes() {
        Aprobados = null;
        Reprobados = null;
    }

    public void AgregarEstudiante(string cedula, string nombre, string apellido, string correo, double nota) {
        Estudiante nuevo = new Estudiante(cedula, nombre, apellido, correo, nota);
        if (nota >= 6) {
            nuevo.Siguiente = Aprobados;
            Aprobados = nuevo;
        } else {
            if (Reprobados == null) {
                Reprobados = nuevo;
            } else {
                Estudiante actual = Reprobados;
                while (actual.Siguiente != null) {
                    actual = actual.Siguiente;
                }
                actual.Siguiente = nuevo;
            }
        }
    }

    public void ImprimirEstudiantes(bool aprobados) {
        Estudiante actual = aprobados ? Aprobados : Reprobados;
        Console.WriteLine(aprobados ? "Estudiantes Aprobados:" : "Estudiantes Reprobados:");
        while (actual != null) {
            Console.WriteLine($"Cédula: {actual.Cedula}, Nombre: {actual.Nombre}, Nota: {actual.Nota}");
            actual = actual.Siguiente;
        }
    }

    public int ContarEstudiantes(bool aprobados) {
        Estudiante actual = aprobados ? Aprobados : Reprobados;
        int contador = 0;
        while (actual != null) {
            contador++;
            actual = actual.Siguiente;
        }
        return contador;
    }
}

class Programa {
    static void Main(string[] args) {
        // Ejercicio 2: Invertir lista enlazada
        ListaEnlazada lista = new ListaEnlazada();
        lista.Agregar(1);
        lista.Agregar(2);
        lista.Agregar(3);
        lista.Agregar(4);
        Console.WriteLine("Lista original:");
        lista.Imprimir();

        lista.Invertir();
        Console.WriteLine("Lista invertida:");
        lista.Imprimir();

        // Ejercicio 6: Registro de estudiantes
        ListaEstudiantes registro = new ListaEstudiantes();
        registro.AgregarEstudiante("123", "Pedro", "Zurita", "pedro@mail.com", 8.5);
        registro.AgregarEstudiante("124", "Maria", "Gomez", "maria@mail.com", 5.2);
        registro.AgregarEstudiante("125", "Juan", "Perez", "juan@mail.com", 7.0);

        registro.ImprimirEstudiantes(true); // Aprobados
        registro.ImprimirEstudiantes(false); // Reprobados

        Console.WriteLine($"Total aprobados: {registro.ContarEstudiantes(true)}");
        Console.WriteLine($"Total reprobados: {registro.ContarEstudiantes(false)}");
    }
}
