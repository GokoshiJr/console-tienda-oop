using System;
using System.Collections;

namespace Tienda
{

    class Product
    {

        private string name;
        private int value;
        private int count;

        public Product(string _name, int _value, int _count)
        {
            this.name = _name;
            this.value = _value;
            this.count = _count;
        }

        public override string ToString()
        {
            return String.Format(" {0,-15} {1,-15} {2,-15} {3,-15}", this.name, this.value, this.count, this.getTotal());
        }

        // getters
        public string getName()
        {
            return this.name;
        }

        public int getValue()
        {
            return this.value;

        }
        
        public int getCount()
        {
            return this.count;
        }

        public int getTotal()
        {
            return this.count * this.value;
        }

        // setters
        public void setName(string _name)
        {
            this.name = _name;
        }

        public void setValue(int _value)
        {
            this.value = _value;

        }
        
        public void setCount(int _count)
        {
            this.count = _count;
        }

    }
    
    class User
    {

        private string name;
        private string id;
        private string phone;

        public User(string _name, string _id, string _phone)
        {
            this.name = _name;
            this.id = _id;
            this.phone = _phone;
        }

        // getters
        public string getName()
        {
            return this.name;
        }

        public string getID()
        {
            return this.id;
        }

        public string getPhone()
        {
            return this.phone;
        }

        // setters
        public void setName(string _name)
        {
            this.name = _name;
        }

        public void setID(string _id)
        {
            this.id = _id;
        }

        public void setPhone(string _phone)
        {
            this.phone = _phone;
        }

        public override string ToString()
        {
            return String.Format(" Comprador: {0}\n Cedula: {1}\n Telefono: {2}", this.name, this.id, this.phone);
        }
    
    }
    
    class Program
    {

        private static string linea = "-------------------------------------------------------------";
        
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
                    MostrarTitulo("Tienda 1");
                    User user = RegisterUser();
                    ArrayList products = RegisterProducts();
                    Factura(products, user);
                    Console.Write(" ¿Desea registrar otro cliente? Si(1)/No(0): ");
                    input = Console.ReadLine();
                    if (input == "0")
                    {
                        salir = true;
                    }
                }
                Console.WriteLine(linea);
                Console.WriteLine(" Fin el programa...");
            }
            
        }

        // Calcula monto final a pagar con el iva a 20%
        static double[] IVA(ArrayList _products, int _IVA)
        {
            double[] result = {0,0};
            double acum = 0;
            foreach (Product product in _products)
            {
                acum += product.getTotal();
            }
            result[0] = (acum * _IVA) / 100;
            result[1] = acum + (acum * _IVA) / 100;
            return result;
        }

        // Mostrar titulo
        static void MostrarTitulo(string _titulo)
        {
            Console.Clear();
            Console.WriteLine(linea);
            Console.WriteLine("{0,32}", _titulo);
            Console.WriteLine(linea);
        }

        // Muestra los datos de los productos
        static void Factura(ArrayList _products, User _user)
        {
            MostrarTitulo("Factura");
            Console.WriteLine(_user);
            Console.WriteLine(linea);
            Console.WriteLine(" {0,-15} {1,-15} {2,-15} {3,-15}", "Producto", "Precio bs", "Cantidad", "Total bs");
            Console.WriteLine(linea);
            foreach (Product product in _products)
            {
                Console.WriteLine(product);
            }
            Console.WriteLine(linea);            
            double[] results = IVA(_products, 20);
            Console.WriteLine(" IVA (20%): {0} bs", results[0]);
            Console.WriteLine(" Monto total a pagar: {0} bs", results[1]);
            Console.WriteLine(linea);
        }

        // Registra productos
        static ArrayList RegisterProducts()
        {

            bool salir = false;
            string name = "";
            int value = 0;
            int count = 0;
            string input = "";

            ArrayList products =  new ArrayList();
            MostrarTitulo("Tienda 1");
            Console.WriteLine(" Ingrese los productos:");

            while (!salir)
            {
                Console.WriteLine(linea);
                Console.Write(" Nombre: ");
                name = Console.ReadLine();
                Console.Write(" Coste: ");
                value = Int32.Parse(Console.ReadLine());
                Console.Write(" Cantidad: ");
                count = Int32.Parse(Console.ReadLine());
                
                if (value < 2)
                {
                    Console.WriteLine(linea);
                    Console.WriteLine(" Coste menor a 2 bs ingrese el producto otra vez");
                    continue;
                }
                
                products.Add(new Product(name, value, count));
                Console.WriteLine(linea);
                Console.Write(" Registrar otro producto(1)/Mostrar la factura(0): ");
                input = Console.ReadLine();

                if (input == "0")
                {
                    salir = true;
                }
            }
            
            return products;
        }

        // Registra los datos del comprador
        static User RegisterUser()
        {
            string name = "";
            string id = "";
            string phone = "";

            Console.WriteLine(" Ingrese los datos del comprador");
            Console.WriteLine(linea);

            Console.Write(" Nombre: ");
            name = Console.ReadLine();
            Console.Write(" Cedula: ");
            id = Console.ReadLine();
            Console.Write(" Telefono: ");
            phone = Console.ReadLine();

            return new User(name, id, phone);
        }

        // Comprueba la clave de logeo
        static bool Login(string _clave)
        {
            string input = "";
            int intentos = 0;
            bool salir = false;

            Console.WriteLine(linea);
            Console.WriteLine("{0,32}", "Tienda 1");
            
            while(!salir)
            {
                intentos++;
                Console.WriteLine(linea);
                Console.Write(" Ingrese la clave para iniciar: ");
                input = Console.ReadLine();

                if (input == _clave)
                {
                    return true;
                } else
                {
                    Console.WriteLine(linea);
                    Console.WriteLine(" Clave incorrecta.");
                    if (intentos > 3)
                    {
                        Console.WriteLine(linea);
                        Console.WriteLine(" Acceso denegado...");
                        salir = true;
                    }
                }
            }

            return false;

        }
    
    }

}
