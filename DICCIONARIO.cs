# Diccionario base de palabras
diccionario = {
    "time": "tiempo",
    "person": "persona",
    "year": "año",
    "way": "camino/forma",
    "day": "día",
    "thing": "cosa",
    "man": "hombre",
    "world": "mundo",
    "life": "vida",
    "hand": "mano",
    "part": "parte",
    "child": "niño/a",
    "eye": "ojo",
    "woman": "mujer",
    "place": "lugar",
    "work": "trabajo",
    "week": "semana",
    "case": "caso",
    "point": "punto/tema",
    "government": "gobierno",
    "company": "empresa/compañía"
}

# Función para traducir una frase
def traducir_frase(frase):
    palabras = frase.split()
    frase_traducida = []
    for palabra in palabras:
        # Limpiar la palabra de signos de puntuación
        palabra_limpia = palabra.strip(".,;!?")
        # Traducir la palabra si está en el diccionario
        if palabra_limpia.lower() in diccionario:
            traduccion = diccionario[palabra_limpia.lower()]
            frase_traducida.append(traduccion)
        elif palabra_limpia.lower() in {v: k for k, v in diccionario.items()}:
            traduccion = {v: k for k, v in diccionario.items()}[palabra_limpia.lower()]
            frase_traducida.append(traduccion)
        else:
            frase_traducida.append(palabra)
    return " ".join(frase_traducida)

# Función para agregar nuevas palabras al diccionario
def agregar_palabra(palabra, traduccion):
    diccionario[palabra.lower()] = traduccion.lower()
    print(f"Palabra '{palabra}' agregada al diccionario.")

# Menú principal
while True:
    print("\nMENU")
    print("=======================================================")
    print("1. Traducir una frase")
    print("2. Ingresar más palabras al diccionario")
    print("0. Salir")
    opcion = input("Seleccione una opción: ")

    if opcion == "1":
        frase = input("Ingrese la frase: ")
        frase_traducida = traducir_frase(frase)
        print("Su frase traducida es:", frase_traducida)
    elif opcion == "2":
        palabra = input("Ingrese la palabra en inglés: ")
        traduccion = input(f"Ingrese la traducción al español de '{palabra}': ")
        agregar_palabra(palabra, traduccion)
    elif opcion == "0":
        print("Saliendo del programa...")
        break
    else:
        print("Opción no válida. Por favor, seleccione una opción válida.")
