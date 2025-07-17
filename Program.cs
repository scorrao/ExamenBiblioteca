using System.Security.Cryptography.X509Certificates;

Biblioteca biblioteca = new Biblioteca();
void MostrarMenu()
{
    Console.Clear();
    Console.WriteLine("1 Registrar Ejemplar");
    Console.WriteLine("2 Registrar Prestamo");
    Console.WriteLine("3 Registrar Devolucion");
    Console.WriteLine("4 Consultar Disponibilidad");
    Console.WriteLine("5 Listado Prestamos Pendientes");
    Console.WriteLine("6 Listado Ejemplares Prestados");
    Console.WriteLine("7 Salir");

}

void RegistrarEjemplar()
{
    // Tomar datos
    //     Preguntar por el codigo
    //     Validar que no se encuentre repetido
    //     Preguntar el tipo
    //     Tomar el resto de los datos
    // Guargar en la lista
    Console.WriteLine("Ingrese el codigo de ejemplar:");
    int codigo = int.Parse(Console.ReadLine());
    //Si Esta Repetido
    if (Existe(codigo))
    {
        Console.WriteLine("El codigo ingresado ya existe");
        return;
    }
    Console.WriteLine("Ingrese el tipo del ejemplar:");
    Console.WriteLine("1. Libro:");
    Console.WriteLine("2. DVD:");
    Console.WriteLine("3. Revista:");
    int tipo = int.Parse(Console.ReadLine());
    Ejemplar? e = null;
    switch (tipo)
    {
        case 1:
            e = new Libro();
            break;
        case 2:
            e = new DVD();
            break;
        case 3:
            e = new Revista();
            break;
        default:
            return;
    }
    Console.WriteLine("Ingrese el Titulo");
    e.Titulo = Console.ReadLine();
    Console.WriteLine("Ingrese el Genero");
    e.Genero = Console.ReadLine();
    Console.WriteLine("Ingrese el Autor");
    e.Autor = Console.ReadLine();
    if (e is Libro l)
    {
        Console.WriteLine("Ingrese el ISBN");
        l.ISBN = Console.ReadLine();
        Console.WriteLine("Ingrese el Año Publicacion");
        l.AnioPublicacion = Console.ReadLine();
    }
    if (e is DVD d)
    {
        Console.WriteLine("Ingrese la duracion");
        d.Duracion = int.Parse(Console.ReadLine());
    }
    if (e is Revista r)
    {
        Console.WriteLine("Ingrese el Numero");
        r.Numero = int.Parse(Console.ReadLine());
        Console.WriteLine("Ingrese la Fecha");
        r.Fecha = DateTime.Parse(Console.ReadLine());
    }
    biblioteca.Ejemplares.Add(e);
}

bool Existe(int codigo)
{
    foreach (Ejemplar e in biblioteca.Ejemplares)
    {
        if (e.Codigo == codigo)
            return true;
    }
    return false;
    return biblioteca.Ejemplares.Any(e => e.Codigo == codigo);
}

void RegistrarPrestamo()
{
    /*Tomar datos  Ejemplar
     * Verificar si el ejemplar existe
     * Verificar si el ejemplar está disponible
     * Tomar resto de los datos
     * Guardar en la lista   
    */
    Console.WriteLine("Ingrese el codigo de ejemplar");
    int codigo = int.Parse(Console.ReadLine());
    if (EstaDisponible(codigo))
    {
        Prestamo p = new Prestamo();
        p.CodigoEjemplar = codigo;
        Console.WriteLine("Ingrese el nombre del socio");
        p.Socio = Console.ReadLine();

        Console.WriteLine("Ingrese la fecha del prestamo");
        p.Fecha = DateTime.Parse(Console.ReadLine());

        p.FechaDevolucion = p.Fecha.AddDays(7);
        //p.Fecha = DateTime.Now;

        biblioteca.Prestamos.Add(p);
    }

}

bool EstaDisponible(int codigo)
{
    if (Existe(codigo))
    {
        foreach (Prestamo p in biblioteca.Prestamos)
        {
            if (p.CodigoEjemplar == codigo && p.Devuelto == false)
            {
                return false;
            }
        }
        return true;
        return !biblioteca.Prestamos.Any(p => p.CodigoEjemplar == codigo && p.Devuelto == false);
    }
    else
        return false;
}


void RegistrarDevolucion()
{
    /*
    * Tomar el codigo ejemplar
    * Verificar si existe
    * Verficar que está prestado
    * Registrar que volvió
    */

    int codigo = int.Parse(Console.ReadLine());
    if (Existe(codigo))
    {
        if (!EstaDisponible(codigo))
        {
        //     foreach (Prestamo p in biblioteca.Prestamos)
        //     {
        //         if (p.CodigoEjemplar == codigo && p.Devuelto == false)
        //         {
        //             p.Devuelto = true;
        //             p.FechaDevolucion = DateTime.Now;
        //             break;
        //         }
        //     }
            Prestamo p2 = biblioteca.Prestamos
            .First(p => p.CodigoEjemplar == codigo && !p.Devuelto);
            p2.Devuelto = true;
            p2.FechaDevolucion = DateTime.Now;
        }
        else
        {
            Console.WriteLine("El ejemplar no está prestado");
        }
    }
    else
        Console.WriteLine("El ejemplar no existe");
}
void ConsultarDisponibilidad()
{
    Console.WriteLine("Ingrese el codigo de ejemplar");
    int codigo = int.Parse(Console.ReadLine());
    if (EstaDisponible(codigo))
        Console.WriteLine("El ejemplar esta disponible");
    else
        Console.WriteLine("El ejemplar no esta disponible");
}
void ListadoPrestamosPendientes()
{
    foreach (Prestamo p in biblioteca.Prestamos)
    {
        if (p.Devuelto == false && p.FechaDevolucion < DateTime.Now)
        {
            var ejemplar = biblioteca.Ejemplares.First(x => x.Codigo == p.CodigoEjemplar);
            Console.WriteLine(ejemplar);
        }
    }
}

void ListadoEjemplaresPrestados()
{
    foreach (Ejemplar e in biblioteca.Ejemplares)
    {
        if(EstaDisponible(e.Codigo) == false)
            Console.WriteLine(e);
    }
}

MostrarMenu();
int i = int.Parse(Console.ReadLine());
while (true)
{
    switch (i)
    {
        case 1:
            RegistrarEjemplar();
            break;
        case 2:
            RegistrarPrestamo();
            break;
        case 3:
            RegistrarDevolucion();
            break;
        case 4:
            ConsultarDisponibilidad();
            break;
        case 5:
            ListadoPrestamosPendientes();
            break;
        case 6:
            ListadoEjemplaresPrestados();
            break;
        case 7:
            Environment.Exit(0);
            break;
        default:
            Console.WriteLine("Opcion Incorrecta");
            break;
    }
    Console.ReadKey();
    MostrarMenu();
i = int.Parse(Console.ReadLine());

}