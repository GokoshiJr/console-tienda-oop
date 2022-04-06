using System;
using System.Collections;

namespace Tienda
{
    class Program
    {

        static void Main(string[] args)
        {

            bool salir = false;
            bool userLog = false;
            string input = "";
            string clave = "1234";

            userLog = Login(clave);

            if (userLog) // usuario logeado exitosamente
            {
                while (!salir)
                {
                    User user = RegisterUser();
                    ArrayList products = RegisterProducts();
                    Factura(products, user);
                    Console.Write(" ¿Registrar otro cliente? Si(1)/No(0): ");
                    input = Console.ReadLine();
                    if (input == "0")
                    {
                        salir = true;
                    }
                }
                Console.WriteLine("");
            }

        }

        static double[] IVA(ArrayList _products, int _IVA)
        {
            double[] result = { 0, 0 };
            double acum = 0;
            foreach (Producto product in _products)
            {
                acum += product.getTotal();
            }
            result[0] = (acum * _IVA) / 100;
            result[1] = acum + (acum * _IVA) / 100;
            return result;
        }

        static void Factura(ArrayList _products, User _user)
        {

            Console.WriteLine(_user);
            Console.WriteLine("");
            Console.WriteLine(" {0,-15} {1,-15} {2,-15} {3,-15}", "Producto", "Precio bs", "Cantidad", "Total bs");
            Console.WriteLine("");
            foreach (Producto product in _products)
            {
                Console.WriteLine(product);
            }
            Console.WriteLine("");
            double[] results = IVA(_products, 20);
            Console.WriteLine(" IVA (20%): {0} bs", results[0]);
            Console.WriteLine(" Total a pagar: {0} bs", results[1]);
            Console.WriteLine("");
        }

        static ArrayList RegisterProducts()
        {

            bool salir = false;
            string name = "";
            int value = 0;
            int count = 0;
            string input = "";

            ArrayList products = new ArrayList();
            Console.WriteLine(" Ingrese los productos:");

            while (!salir)
            {
                Console.WriteLine("");
                Console.Write(" Nombre: ");
                name = Console.ReadLine();
                Console.Write(" Coste: ");
                value = Int32.Parse(Console.ReadLine());
                Console.Write(" Cantidad: ");
                count = Int32.Parse(Console.ReadLine());

                if (value < 2)
                {
                    Console.WriteLine("");
                    Console.WriteLine(" Coste menor a 2 bs ingrese el producto otra vez");
                    continue;
                }

                products.Add(new Producto(name, value, count));
                Console.WriteLine("");
                Console.Write(" Registrar otro producto(1)/Mostrar la factura(0): ");
                input = Console.ReadLine();

                if (input == "0")
                {
                    salir = true;
                }
            }

            return products;
        }

        static User RegisterUser()
        {
            string name = "";
            string id = "";

            Console.WriteLine(" Ingrese los datos del comprador");
            Console.WriteLine("");

            Console.Write(" Nombre: ");
            name = Console.ReadLine();
            Console.Write(" Cedula: ");
            id = Console.ReadLine();


            return new User(name, id);
        }

        static bool Login(string _clave)
        {
            string input = "";
            int intentos = 0;
            bool salir = false;

            while (!salir)
            {
                intentos++;
                Console.WriteLine("");
                Console.Write(" Ingrese la clave para iniciar: ");
                input = Console.ReadLine();

                if (input == _clave)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine(" Clave incorrecta.");
                    if (intentos > 3)
                    {
                        Console.WriteLine("");
                        Console.WriteLine(" Acceso denegado...");
                        salir = true;
                    }
                }
            }

            return false;

        }

    }

    class Producto
    {

        private string nombre;
        private int valor;
        private int cantidad;

        public Producto(string _nombre, int _valor, int _cantidad)
        {
            this.nombre = _nombre;
            this.valor = _valor;
            this.cantidad = _cantidad;
        }

        public override string ToString()
        {
            return String.Format(" {0,-15} {1,-15} {2,-15} {3,-15}", this.nombre, this.valor, this.cantidad, this.getTotal());
        }

        public int getTotal()
        {
            return this.cantidad * this.valor;
        }

    }
    
    class User
    {

        private string nombre;
        private string id;

        public User(string _nombre, string _id)
        {
            this.nombre = _nombre;
            this.id = _id;
        }

        public override string ToString()
        {
            return String.Format(" Comprador: {0}\n Cedula: {1}", this.nombre, this.id);
        }
    
    }

}