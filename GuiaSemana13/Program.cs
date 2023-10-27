//using System;
//using GuiaSemana13;

//using (var contextdb = new Context())
//{
//    contextdb.Database.EnsureCreated();

//    bool agregarMasRegistros = true;

//    while (agregarMasRegistros)
//    {
//        Console.Write("Ingresa los nombres: ");
//        string nombre = Console.ReadLine();

//        Console.Write("Ingresa los apellidos: ");
//        string apellido = Console.ReadLine();

//        Console.Write("Ingrese el sexo: ");
//        string sexo = Console.ReadLine();

//        Console.Write("Ingrese la edad: ");
//        int edad = int.Parse(Console.ReadLine());

//        var estudiante = new Student()
//        {
//            Nombres = nombre,
//            Apellidos = apellido,
//            Sexo = sexo,
//            Edad = edad
//        };

//        contextdb.Add(estudiante);
//        contextdb.SaveChanges();

//        Console.Write("¿Desea agregar más registros? (S/N): ");
//        string respuesta = Console.ReadLine();

//        agregarMasRegistros = respuesta.Trim().Equals("S", StringComparison.OrdinalIgnoreCase);
//    }

//    Console.WriteLine("Datos de la tabla estudiante:");
//    foreach (var estudiante in contextdb.Estudiante)
//    {
//        Console.WriteLine($"ID: {estudiante.Id}, Nombre: {estudiante.Nombres}, Apellido: {estudiante.Apellidos}, Sexo: {estudiante.Sexo}, Edad: {estudiante.Edad}");
//    }

using System;
using GuiaSemana13;
using Microsoft.EntityFrameworkCore;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        using (var contextdb = new Context())
        {
            contextdb.Database.EnsureCreated();

            Console.WriteLine("Lista de Estudiantes:");
            var estudiantes = contextdb.Estudiante.ToList();
            foreach (var estudiante in estudiantes)
            {
                Console.WriteLine($"ID: {estudiante.Id}, Nombre: {estudiante.Nombres}, Apellidos: {estudiante.Apellidos}, Edad: {estudiante.Edad}, Sexo: {estudiante.Sexo}");
            }

            Console.Write("Ingresar el ID del estudiante que se desee modificar o eliminar ->  ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var estudianteAModificar = contextdb.Estudiante.SingleOrDefault(e => e.Id == id);

                if (estudianteAModificar != null)
                {
                    Console.WriteLine("Opciones disponibles: MODIFICAR o ELIMINAR");
                    Console.Write("Elija la opcion que desea realizar: ");
                    string opcion = Console.ReadLine().ToUpper();

                    if (opcion == "MODIFICAR")
                    {
                        Console.WriteLine("Atributos disponibles para modificar: NOMBRE, APELLIDO, SEXO, EDAD");
                        Console.Write("Elija el atributo que desee modificar: ");
                        string atributoAModificar = Console.ReadLine().ToUpper();

                        switch (atributoAModificar)
                        {
                            case "NOMBRE":
                                Console.Write("Ingrese el nuevo nombre: ");
                                estudianteAModificar.Nombres = Console.ReadLine();
                                break;
                            case "APELLIDO":
                                Console.Write("Ingrese el nuevo apellido: ");
                                estudianteAModificar.Apellidos = Console.ReadLine();
                                break;
                            case "SEXO":
                                Console.Write("Ingrese el nuevo sexo: ");
                                estudianteAModificar.Sexo = Console.ReadLine();
                                break;
                            case "EDAD":
                                Console.Write("Ingrese la nueva edad: ");
                                if (int.TryParse(Console.ReadLine(), out int nuevaEdad))
                                {
                                    estudianteAModificar.Edad = nuevaEdad;
                                }
                                else
                                {
                                    Console.WriteLine("Edad no válida.");
                                }
                                break;
                            default:
                                Console.WriteLine("Atributo no válido.");
                                break;
                        }
                        contextdb.SaveChanges();
                        Console.WriteLine("Estudiante modificado.");
                    }
                    else if (opcion == "ELIMINAR")
                    {
                        contextdb.Estudiante.Remove(estudianteAModificar);
                        contextdb.SaveChanges();
                        Console.WriteLine("Estudiante eliminado.");
                    }
                    else
                    {
                        Console.WriteLine("Acción no válida.");
                    }
                }
                else
                {
                    Console.WriteLine("Estudiante no encontrado.");
                }
            }
            else
            {
                Console.WriteLine("ID no es válido.");
            }
        }
    }
}
