using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    class Cliente
    {
        private int _id;
        private String _apellido;
        private String _nombre;
        private String _direccion;
        private String _telefono;

        public Cliente()
        {
        }

        public Cliente(int id, string apellido, string nombre, string direccion, string telefono)
        {
            _id = id;
            _apellido = apellido ?? throw new ArgumentNullException(nameof(apellido));
            _nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
            _direccion = direccion ?? throw new ArgumentNullException(nameof(direccion));
            _telefono = telefono ?? throw new ArgumentNullException(nameof(telefono));
        }

        public Cliente(string apellido, string nombre, string direccion, string telefono)
        {
            _apellido = apellido ?? throw new ArgumentNullException(nameof(apellido));
            _nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
            _direccion = direccion ?? throw new ArgumentNullException(nameof(direccion));
            _telefono = telefono ?? throw new ArgumentNullException(nameof(telefono));
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public String Apellido
        {
            get { return _apellido; }
            set { _apellido = value; }
        }

        public String Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public String Direccion
        {
            get { return _direccion; }
            set { _direccion = value; }
        }

        public String Telefono
        {
            get { return _telefono; }
            set { _telefono = value; }
        }

    }
}
