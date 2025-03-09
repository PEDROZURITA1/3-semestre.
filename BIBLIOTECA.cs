using System;
using System.Collections.Generic;
using System.Linq;

/**
 * Sistema de Gestión de Biblioteca
 * Esta aplicación demuestra el uso de Dictionary (Mapas) y HashSet (Conjuntos) para gestionar la colección de libros de una biblioteca.
 */
public class LibraryManagementSystem
{
    // Usando Dictionary para almacenar libros con ISBN como clave (equivalente a Map en C#)
    private Dictionary<string, Book> bookCatalog;

    // Usando HashSet para almacenar categorías únicas de libros (equivalente a Set en C#)
    private HashSet<string> categories;

    // Usando Dictionary para rastrear libros prestados con ID de usuario como clave y un conjunto de libros como valor
    private Dictionary<string, HashSet<Book>> borrowedBooks;

    public LibraryManagementSystem()
    {
        // Implementación de Dictionary para el catálogo de libros
        this.bookCatalog = new Dictionary<string, Book>();

        // Implementación de HashSet para categorías
        this.categories = new HashSet<string>();

        // Implementación de Dictionary para libros prestados
        this.borrowedBooks = new Dictionary<string, HashSet<Book>>();
    }

    /**
     * Añadir un nuevo libro al catálogo de la biblioteca
     */
    public void AddBook(string isbn, string title, string author, string category, int year)
    {
        Book book = new Book(isbn, title, author, category, year);
        bookCatalog[isbn] = book;
        categories.Add(category);
        Console.WriteLine("Libro añadido exitosamente: " + title);
    }

    /**
     * Eliminar un libro del catálogo de la biblioteca
     */
    public void RemoveBook(string isbn)
    {
        if (bookCatalog.ContainsKey(isbn))
        {
            Book removedBook = bookCatalog[isbn];
            bookCatalog.Remove(isbn);
            Console.WriteLine("Libro eliminado exitosamente: " + removedBook.Title);

            // Actualizar categorías si es necesario
            UpdateCategories();
        }
        else
        {
            Console.WriteLine("Libro con ISBN " + isbn + " no encontrado.");
        }
    }

    /**
     * Actualizar categorías basado en los libros actuales
     */
    private void UpdateCategories()
    {
        HashSet<string> updatedCategories = new HashSet<string>();
        foreach (Book book in bookCatalog.Values)
        {
            updatedCategories.Add(book.Category);
        }
        this.categories = updatedCategories;
    }

    /**
     * Buscar libros por título (coincidencia parcial)
     */
    public HashSet<Book> SearchByTitle(string titleQuery)
    {
        HashSet<Book> results = new HashSet<Book>();
        foreach (Book book in bookCatalog.Values)
        {
            if (book.Title.ToLower().Contains(titleQuery.ToLower()))
            {
                results.Add(book);
            }
        }
        return results;
    }

    /**
     * Buscar libros por autor
     */
    public HashSet<Book> SearchByAuthor(string authorQuery)
    {
        HashSet<Book> results = new HashSet<Book>();
        foreach (Book book in bookCatalog.Values)
        {
            if (book.Author.ToLower().Contains(authorQuery.ToLower()))
            {
                results.Add(book);
            }
        }
        return results;
    }

    /**
     * Obtener todos los libros en una categoría específica
     */
    public HashSet<Book> GetBooksByCategory(string category)
    {
        HashSet<Book> results = new HashSet<Book>();
        foreach (Book book in bookCatalog.Values)
        {
            if (book.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
            {
                results.Add(book);
            }
        }
        return results;
    }

    /**
     * Prestar un libro
     */
    public void BorrowBook(string patronId, string isbn)
    {
        if (bookCatalog.ContainsKey(isbn))
        {
            Book book = bookCatalog[isbn];

            if (!book.IsBorrowed)
            {
                // Marcar el libro como prestado
                book.IsBorrowed = true;
                book.BorrowDate = DateTime.Now;

                // Añadir al mapa de libros prestados
                if (!borrowedBooks.ContainsKey(patronId))
                {
                    borrowedBooks[patronId] = new HashSet<Book>();
                }
                borrowedBooks[patronId].Add(book);

                Console.WriteLine("Libro '" + book.Title + "' prestado exitosamente por " + patronId);
            }
            else
            {
                Console.WriteLine("El libro ya está prestado.");
            }
        }
        else
        {
            Console.WriteLine("Libro con ISBN " + isbn + " no encontrado.");
        }
    }

    /**
     * Devolver un libro
     */
    public void ReturnBook(string patronId, string isbn)
    {
        if (borrowedBooks.ContainsKey(patronId) && bookCatalog.ContainsKey(isbn))
        {
            Book book = bookCatalog[isbn];

            HashSet<Book> patronBooks = borrowedBooks[patronId];
            if (patronBooks.Contains(book))
            {
                book.IsBorrowed = false;
                book.BorrowDate = null;
                patronBooks.Remove(book);

                // Eliminar al usuario del mapa si no tiene libros prestados
                if (patronBooks.Count == 0)
                {
                    borrowedBooks.Remove(patronId);
                }

                Console.WriteLine("Libro '" + book.Title + "' devuelto exitosamente por " + patronId);
            }
            else
            {
                Console.WriteLine("Este usuario no ha prestado este libro.");
            }
        }
        else
        {
            Console.WriteLine("ID de usuario o ISBN inválido.");
        }
    }

    /**
     * Obtener todos los libros prestados por un usuario
     */
    public HashSet<Book> GetBorrowedBooksByPatron(string patronId)
    {
        if (borrowedBooks.ContainsKey(patronId))
        {
            return borrowedBooks[patronId];
        }
        else
        {
            return new HashSet<Book>(); // Devolver conjunto vacío si el usuario no tiene libros
        }
    }

    /**
     * Obtener todas las categorías disponibles
     */
    public HashSet<string> GetAllCategories()
    {
        return categories;
    }

    /**
     * Mostrar todos los libros en el catálogo
     */
    public void DisplayAllBooks()
    {
        Console.WriteLine("\n===== CATÁLOGO DE LA BIBLIOTECA =====");
        if (bookCatalog.Count == 0)
        {
            Console.WriteLine("No hay libros en el catálogo.");
        }
        else
        {
            foreach (Book book in bookCatalog.Values)
            {
                Console.WriteLine(book);
            }
        }
        Console.WriteLine("==========================\n");
    }

    /**
     * Mostrar estadísticas de rendimiento
     */
    public void DisplayPerformanceStats()
    {
        Console.WriteLine("\n===== ESTADÍSTICAS DE RENDIMIENTO =====");
        Console.WriteLine("Total de libros en el catálogo: " + bookCatalog.Count);
        Console.WriteLine("Número de categorías: " + categories.Count);
        Console.WriteLine("Número de usuarios con libros prestados: " + borrowedBooks.Count);

        int totalBorrowed = 0;
        foreach (HashSet<Book> books in borrowedBooks.Values)
        {
            totalBorrowed += books.Count;
        }
        Console.WriteLine("Total de libros actualmente prestados: " + totalBorrowed);
        Console.WriteLine("==================================\n");
    }
}

/**
 * Clase Book para almacenar información del libro
 */
public class Book
{
    public string ISBN { get; private set; }
    public string Title { get; private set; }
    public string Author { get; private set; }
    public string Category { get; private set; }
    public int PublicationYear { get; private set; }
    public bool IsBorrowed { get; set; }
    public DateTime? BorrowDate { get; set; }

    public Book(string isbn, string title, string author, string category, int publicationYear)
    {
        ISBN = isbn;
        Title = title;
        Author = author;
        Category = category;
        PublicationYear = publicationYear;
        IsBorrowed = false;
        BorrowDate = null;
    }

    public override string ToString()
    {
        string status = IsBorrowed ? "Prestado" : "Disponible";
        string date = (BorrowDate != null) ? ((DateTime)BorrowDate).ToString("yyyy-MM-dd") : "N/A";

        return string.Format("ISBN: {0} | Título: {1} | Autor: {2} | Categoría: {3} | Año: {4} | Estado: {5} | Fecha de préstamo: {6}",
                ISBN, Title, Author, Category, PublicationYear, status, date);
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        Book book = (Book)obj;
        return ISBN.Equals(book.ISBN);
    }

    public override int GetHashCode()
    {
        return ISBN.GetHashCode();
    }
}

/**
 * Clase Program con el método Main
 */
public class Program
{
    public static void Main(string[] args)
    {
        // Tiempo de inicio para medir el rendimiento
        DateTime startTime = DateTime.Now;

        LibraryManagementSystem library = new LibraryManagementSystem();
        bool running = true;

        // Añadir algunos libros de ejemplo
        library.AddBook("9780747532743", "Harry Potter y la piedra filosofal", "J.K. Rowling", "Ficción", 1997);
        library.AddBook("9780060935467", "Matar a un ruiseñor", "Harper Lee", "Ficción", 1960);
        library.AddBook("9780345391803", "Guía del autoestopista galáctico", "Douglas Adams", "Ciencia Ficción", 1979);
        library.AddBook("9780486280615", "Las aventuras de Sherlock Holmes", "Arthur Conan Doyle", "Misterio", 1892);
        library.AddBook("9780547928227", "El Hobbit", "J.R.R. Tolkien", "Fantasía", 1937);
        library.AddBook("9780486275437", "Orgullo y prejuicio", "Jane Austen", "Romance", 1813);
        library.AddBook("9780307743657", "El resplandor", "Stephen King", "Terror", 1977);
        library.AddBook("9780307277671", "El código Da Vinci", "Dan Brown", "Thriller", 2003);

        while (running)
        {
            Console.WriteLine("\n===== SISTEMA DE GESTIÓN DE BIBLIOTECA =====");
            Console.WriteLine("1. Añadir un libro");
            Console.WriteLine("2. Eliminar un libro");
            Console.WriteLine("3. Mostrar todos los libros");
            Console.WriteLine("4. Buscar libros por título");
            Console.WriteLine("5. Buscar libros por autor");
            Console.WriteLine("6. Mostrar libros por categoría");
            Console.WriteLine("7. Prestar un libro");
            Console.WriteLine("8. Devolver un libro");
            Console.WriteLine("9. Mostrar libros prestados por usuario");
            Console.WriteLine("10. Mostrar todas las categorías");
            Console.WriteLine("11. Mostrar estadísticas de rendimiento");
            Console.WriteLine("0. Salir");
            Console.Write("Ingrese su opción: ");

            string input = Console.ReadLine();
            if (!int.TryParse(input, out int choice))
            {
                Console.WriteLine("Entrada inválida. Por favor, ingrese un número.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    Console.Write("Ingrese ISBN: ");
                    string isbn = Console.ReadLine();
                    Console.Write("Ingrese título: ");
                    string title = Console.ReadLine();
                    Console.Write("Ingrese autor: ");
                    string author = Console.ReadLine();
                    Console.Write("Ingrese categoría: ");
                    string category = Console.ReadLine();
                    Console.Write("Ingrese año de publicación: ");
                    if (int.TryParse(Console.ReadLine(), out int year))
                    {
                        library.AddBook(isbn, title, author, category, year);
                    }
                    else
                    {
                        Console.WriteLine("Formato de año inválido.");
                    }
                    break;

                case 2:
                    Console.Write("Ingrese ISBN del libro a eliminar: ");
                    string isbnToRemove = Console.ReadLine();
                    library.RemoveBook(isbnToRemove);
                    break;

                case 3:
                    library.DisplayAllBooks();
                    break;

                case 4:
                    Console.Write("Ingrese título a buscar: ");
                    string titleQuery = Console.ReadLine();
                    HashSet<Book> titleResults = library.SearchByTitle(titleQuery);

                    Console.WriteLine("\n===== RESULTADOS DE BÚSQUEDA =====");
                    if (titleResults.Count == 0)
                    {
                        Console.WriteLine("No se encontraron libros que coincidan con '" + titleQuery + "'");
                    }
                    else
                    {
                        foreach (Book book in titleResults)
                        {
                            Console.WriteLine(book);
                        }
                    }
                    break;

                case 5:
                    Console.Write("Ingrese autor a buscar: ");
                    string authorQuery = Console.ReadLine();
                    HashSet<Book> authorResults = library.SearchByAuthor(authorQuery);

                    Console.WriteLine("\n===== RESULTADOS DE BÚSQUEDA =====");
                    if (authorResults.Count == 0)
                    {
                        Console.WriteLine("No se encontraron libros del autor '" + authorQuery + "'");
                    }
                    else
                    {
                        foreach (Book book in authorResults)
                        {
                            Console.WriteLine(book);
                        }
                    }
                    break;

                case 6:
                    Console.Write("Ingrese categoría: ");
                    string categoryQuery = Console.ReadLine();
                    HashSet<Book> categoryResults = library.GetBooksByCategory(categoryQuery);

                    Console.WriteLine("\n===== CATEGORÍA: " + categoryQuery.ToUpper() + " =====");
                    if (categoryResults.Count == 0)
                    {
                        Console.WriteLine("No se encontraron libros en la categoría '" + categoryQuery + "'");
                    }
                    else
                    {
                        foreach (Book book in categoryResults)
                        {
                            Console.WriteLine(book);
                        }
                    }
                    break;

                case 7:
                    Console.Write("Ingrese ID de usuario: ");
                    string patronId = Console.ReadLine();
                    Console.Write("Ingrese ISBN del libro a prestar: ");
                    string isbnToBorrow = Console.ReadLine();

                    library.BorrowBook(patronId, isbnToBorrow);
                    break;

                case 8:
                    Console.Write("Ingrese ID de usuario: ");
                    string returnPatronId = Console.ReadLine();
                    Console.Write("Ingrese ISBN del libro a devolver: ");
                    string isbnToReturn = Console.ReadLine();

                    library.ReturnBook(returnPatronId, isbnToReturn);
                    break;

                case 9:
                    Console.Write("Ingrese ID de usuario: ");
                    string patronToCheck = Console.ReadLine();
                    HashSet<Book> patronBooks = library.GetBorrowedBooksByPatron(patronToCheck);

                    Console.WriteLine("\n===== LIBROS PRESTADOS POR " + patronToCheck + " =====");
                    if (patronBooks.Count == 0)
                    {
                        Console.WriteLine("No hay libros prestados actualmente por este usuario.");
                    }
                    else
                    {
                        foreach (Book book in patronBooks)
                        {
                            Console.WriteLine(book);
                        }
                    }
                    break;

                case 10:
                    HashSet<string> allCategories = library.GetAllCategories();

                    Console.WriteLine("\n===== TODAS LAS CATEGORÍAS =====");
                    if (allCategories.Count == 0)
                    {
                        Console.WriteLine("No hay categorías disponibles.");
                    }
                    else
                    {
                        foreach (string cat in allCategories)
                        {
                            Console.WriteLine("- " + cat);
                        }
                    }
                    break;

                case 11:
                    library.DisplayPerformanceStats();
                    // Calcular y mostrar el tiempo de ejecución
                    TimeSpan executionTime = DateTime.Now - startTime;
                    Console.WriteLine("Tiempo de ejecución del programa: " + executionTime.TotalSeconds + " segundos");
                    break;

                case 0:
                    running = false;
                    Console.WriteLine("Saliendo del Sistema de Gestión de Biblioteca. ¡Adiós!");
                    break;

                default:
                    Console.WriteLine("Opción inválida. Por favor, intente de nuevo.");
                    break;
            }
        }
    }
}
