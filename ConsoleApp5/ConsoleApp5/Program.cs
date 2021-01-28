using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    class Program
    {

        static void Main(string[] args)
        {
            menuPrincipal();
        }

        private static void menuPrincipal()
        {
            Cliente[] vectorCliente;
            Cliente cliente = new Cliente();
            String ruta = @"C:\Users\Lemmar Dell\source\repos\ConsoleApp5\bd\db.dat";
            ConsoleKeyInfo consoleKeyInfo;
            int limite = 0;
            char opcion = ' ';
            do
            {
                Console.Clear();
                Console.WriteLine("1.- Registrar Cliente");
                Console.WriteLine("2.- Modificar Cliente");
                Console.WriteLine("3.- Eliminar Cliente");
                Console.WriteLine("4.- Listar Cliente");
                Console.WriteLine("5.- Salir");
                opcion = Console.ReadKey().KeyChar;
                switch (opcion)
                {
                    case '1':
                        do
                        {
                            Console.Clear();
                            cliente.Id = RALCliente(ruta) + 1;
                            cliente.Apellido = Valida_Cadena(1, 6, "Apellido Cliente: ");
                            cliente.Nombre = Valida_Cadena(1, 8, "Nombre Cliente: ");
                            cliente.Direccion = Valida_Cadena(1, 10, "Dirección Cliente: ");
                            cliente.Telefono = Valida_Cadena(1, 12, "Telefono Cliente: ", 0);
                            Console.SetCursorPosition(1, 14); Console.Write("¿Desea almacenar los datos? S/N");
                            consoleKeyInfo = Console.ReadKey(true);
                            if (consoleKeyInfo.Key == ConsoleKey.S)
                            {
                                GAClientes(ruta, cliente);
                                Console.SetCursorPosition(1, 16); Console.Write("Datos almacenados de forma éxitosa...");
                            }
                            Console.SetCursorPosition(1, 18); Console.Write("¿Desea salir? ESP");
                            consoleKeyInfo = Console.ReadKey(true);
                        } while (consoleKeyInfo.Key != ConsoleKey.Escape);
                        break;

                    case '2':
                        do
                        {
                            Console.Clear();
                            limite = RALCliente(ruta);
                            vectorCliente = new Cliente[limite];
                            LAClientes(ruta, ref vectorCliente);
                            for (int i = 0; i < vectorCliente.Length; i++)
                            {
                                Console.WriteLine("id: {0} || Apellido: {1} || Nombre: {2}", vectorCliente[i].Id, vectorCliente[i].Apellido, vectorCliente[i].Nombre);
                            }
                            Console.ReadKey();
                            Console.Clear();
                            cliente.Id = Valida_Numero(1, 2, "Cliente a modificar: ");
                            cliente.Apellido = Valida_Cadena(1, 4, "Apellido Cliente: ");
                            cliente.Nombre = Valida_Cadena(1, 6, "Nombre Cliente: ");
                            cliente.Direccion = Valida_Cadena(1, 8, "Dirección Cliente: ");
                            cliente.Telefono = Valida_Cadena(1, 10, "Telefono Cliente: ", 0);
                            for (int j = 0; j < vectorCliente.Length; j++)
                            {
                                if (vectorCliente[j].Id == cliente.Id)
                                {
                                    vectorCliente[j].Apellido = cliente.Apellido;
                                    vectorCliente[j].Nombre = cliente.Nombre;
                                    vectorCliente[j].Direccion = cliente.Direccion;
                                    vectorCliente[j].Telefono = cliente.Telefono;
                                    Console.SetCursorPosition(1, 14); Console.Write("¿Desea almacenar los datos? S/N");
                                    consoleKeyInfo = Console.ReadKey(true);
                                    if (consoleKeyInfo.Key == ConsoleKey.S)
                                    {
                                        EACliente(ruta, vectorCliente);
                                        Console.SetCursorPosition(1, 16); Console.Write("Datos almacenados de forma éxitosa...");
                                    }
                                }
                            }
                            Console.SetCursorPosition(1, 18); Console.Write("¿Desea salir? ESP");
                            consoleKeyInfo = Console.ReadKey(true);
                        } while (consoleKeyInfo.Key != ConsoleKey.Escape);
                        break;

                    case '3':

                        break;

                    case '4':
                        Console.Clear();
                        limite = RALCliente(ruta);
                        vectorCliente = new Cliente[limite];
                        LAClientes(ruta, ref vectorCliente);
                        for (int i = 0; i < vectorCliente.Length; i++)
                        {
                            Console.Clear();
                            Console.SetCursorPosition(2, 2); Console.Write("ID: {0}", vectorCliente[i].Id);
                            Console.SetCursorPosition(2, 4); Console.Write("Apellido: {0}", vectorCliente[i].Apellido);
                            Console.SetCursorPosition(2, 6); Console.Write("Nombre: {0}", vectorCliente[i].Nombre);
                            Console.SetCursorPosition(2, 8); Console.Write("Dirección: {0}", vectorCliente[i].Direccion);
                            Console.SetCursorPosition(2, 10); Console.Write("Telefono: {0}", vectorCliente[i].Telefono);
                            Console.SetCursorPosition(2, 14); Console.Write("{0}/{1}", i + 1, vectorCliente.Length);
                            Console.ReadKey();
                        }
                        break;

                    case '5':

                        break;

                    default:

                        break;
                }
            } while (opcion != '5');
        }

        #region Manejo Archivo
        private static void GAClientes(String ruta, Cliente cliente)
        {
            FileStream ArchivoBinario = new FileStream(ruta, FileMode.Append, FileAccess.Write);
            BinaryWriter Escribir = new BinaryWriter(ArchivoBinario);
            Escribir.Write(cliente.Id);
            Escribir.Write(cliente.Apellido);
            Escribir.Write(cliente.Nombre);
            Escribir.Write(cliente.Direccion);
            Escribir.Write(cliente.Telefono);
            Escribir.Close();
            ArchivoBinario.Close();
        }

        private static void LAClientes(String ruta, ref Cliente[] clientes)
        {
            int conta = 0;
            FileStream ArchivoBinario = new FileStream(ruta, FileMode.OpenOrCreate, FileAccess.Read);
            BinaryReader Leer = new BinaryReader(ArchivoBinario);
            while (Leer.PeekChar() != -1)
            {
                Cliente cliente = new Cliente();
                cliente.Id = Leer.ReadInt32();
                cliente.Apellido = Leer.ReadString();
                cliente.Nombre = Leer.ReadString();
                cliente.Direccion = Leer.ReadString();
                cliente.Telefono = Leer.ReadString();
                clientes[conta] = cliente;
                conta++;
            }
            Leer.Close();
            ArchivoBinario.Close();
        }

        private static void EACliente(String ruta, Cliente[] clientes)
        {
            FileStream ArchivoBinario = new FileStream(ruta, FileMode.Truncate);
            ArchivoBinario.Close();
            FileStream Arch = new FileStream(ruta, FileMode.Append, FileAccess.Write);
            BinaryWriter Escribir = new BinaryWriter(Arch);
            for (int i = 0; i < clientes.Length; i++)
            {
                Escribir.Write(clientes[i].Id);
                Escribir.Write(clientes[i].Apellido);
                Escribir.Write(clientes[i].Nombre);
                Escribir.Write(clientes[i].Direccion);
                Escribir.Write(clientes[i].Telefono);
            }
            Escribir.Close();
            Arch.Close();
        }

        private static int RALCliente(String ruta)
        {
            int limite = 0;
            FileStream ArchivoBinario = new FileStream(ruta, FileMode.OpenOrCreate, FileAccess.Read);
            BinaryReader Leer = new BinaryReader(ArchivoBinario);
            while (Leer.PeekChar() != -1)
            {
                Leer.ReadInt32();
                Leer.ReadString();
                Leer.ReadString();
                Leer.ReadString();
                Leer.ReadString();
                limite++;
            }
            Leer.Close();
            ArchivoBinario.Close();
            return limite;
        }
        #endregion

        #region Validadores

        static string Valida_Cadena(int x, int y, string mensaje, int op)
        {
            string cadena = "";
            int n = 0;
            char letra = ' ';
            bool ban = true;
            do
            {
                ban = true;
                Console.SetCursorPosition(x + mensaje.Length + 1, y); Console.Write("                                                            ");
                Console.SetCursorPosition(x, y); Console.Write(mensaje);
                Console.SetCursorPosition(x + mensaje.Length + 1, y); cadena = Console.ReadLine();
                for (int i = 0; i < cadena.Length && ban == true; i++)
                {
                    letra = cadena[i];
                    n = (int)letra;
                    if (i == 0)
                    {
                        if (n == 32)
                        {
                            MensajeError("Error: Cadena inválida");
                            ban = false;
                        }
                    }
                    if ((n >= 65 && n <= 90) || (n >= 97 && n <= 122) || (n == 241) || (n == 209) || (n == 225) || (n == 233) || (n == 237) || (n == 243) || (n == 250) || (n == 32) || (n == 193) || (n == 201) || (n == 205) || (n == 211) || (n == 218))
                    {
                        break;
                    }
                }
            } while (ban == false);
            return cadena;
        }
        static string Valida_Cadena(int x, int y, string mensaje)
        {
            string cadena = "";
            int n = 0;
            char letra = ' ';
            bool ban = true;
            do
            {
                ban = true;
                Console.SetCursorPosition(x + mensaje.Length + 1, y); Console.Write("                                                            ");
                Console.SetCursorPosition(x, y); Console.Write(mensaje);
                Console.SetCursorPosition(x + mensaje.Length + 1, y); cadena = Console.ReadLine();
                for (int i = 0; i < cadena.Length && ban == true; i++)
                {
                    letra = cadena[i];
                    n = (int)letra;
                    if (i == 0)
                    {
                        if (n == 32)
                        {
                            MensajeError("Error: Cadena inválida");
                            ban = false;
                        }
                    }
                    if ((n >= 65 && n <= 90) || (n >= 97 && n <= 122) || (n == 241) || (n == 209) || (n == 225) || (n == 233) || (n == 237) || (n == 243) || (n == 250) || (n == 32) || (n == 193) || (n == 201) || (n == 205) || (n == 211) || (n == 218))
                    {
                        break;
                    }
                    else
                    {
                        ban = false;
                        MensajeError("Error: Cadena inválida");
                    }
                }
            } while (ban == false);
            return cadena;
        }

        static int Valida_Numero(int x, int y, string mensaje)
        {
            int calificacion = 0;
            bool ban;
            do
            {
                do
                {
                    Console.SetCursorPosition(x + mensaje.Length + 1, y); Console.Write("                            ");
                    Console.SetCursorPosition(x, y); Console.Write(mensaje);
                    Console.SetCursorPosition(x + mensaje.Length + 1, y);
                    ban = int.TryParse(Console.ReadLine(), out calificacion);
                    if (!ban)
                        MensajeError("Error: Ingrese números");
                } while (!ban);
                if (calificacion < 0)
                    MensajeError("Error: Ingrese números positivos");
            } while (calificacion < 0);
            return calificacion;
        }
        #endregion

        #region Mensajes Errores
        static void MensajeError(string Mess)
        {
            Console.SetCursorPosition(2, 23); Console.Write(Mess);
            Console.SetCursorPosition(2, 24); Console.Write("Continuar...");
            Console.ReadKey();
            Console.SetCursorPosition(2, 23); Console.Write("                                         ");
            Console.SetCursorPosition(2, 24); Console.Write("                                         ");
        }
        #endregion

    }
}
