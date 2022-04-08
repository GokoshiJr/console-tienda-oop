using System;
using System.Collections;

namespace Tienda
{
    class Program
    {
        public static string a = "------------------------------------------------------------------------------------------------------------------------------------------";
        static void Main(string[] args)
        {   
            try
            {
                ArrayList gerentes = leoArchivo();
                mostrarDatos(gerentes);
                promediosMensuales(gerentes);
                Console.WriteLine(a);
                
                Console.Write(" Presione Enter para continuar");
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(a);
                Console.WriteLine(" Exception: " + e);
            }
            finally
            {
                Console.WriteLine(a);
                Console.WriteLine(" Fin del programa...");
            }
            
        }

        public static ArrayList leoArchivo()
        {
            ArrayList gerentes = new ArrayList();
            string linea;
            
            System.IO.StreamReader sr = new System.IO.StreamReader("../../../data.csv");
            
            string separador = ",";
            linea = sr.ReadLine();

            Console.WriteLine(a);
            Console.WriteLine(" {0,80}", "Departamento de ventas (KFC)");
            Console.WriteLine(a);
            Console.WriteLine(" {0,-15} {1,-20} {2,-12} {3,-12} {4,-12} {5,-12} {6,-12} {7,-12}  {8,-12}", "Gerente", "Sucursal", "Nov", "Dic", "Ene", "Feb", "Mar", "Abr", "Promedio de Sucursal");
            Console.WriteLine(a);

            
            while ((linea = sr.ReadLine()) != null)
            {                
                string[] fila = linea.Split(separador);
                double[] ventas = new double[fila.Length-2];
                string sucursal = fila[0];
                string nombre = fila[1];
                
                for (int i=2; i<(fila.Length); i++)
                {
                    ventas[i-2] = Convert.ToDouble(fila[i]);                    
                }

                gerentes.Add(new Gerente(nombre, sucursal, ventas));
                
            }
            
            sr.Close();
            return gerentes;
        }

        public static void mostrarDatos(ArrayList gerentes)
        {
            foreach (Gerente gerente in gerentes)
            {
                gerente.mostrarDatos();
            }
        }

        public static void promediosMensuales(ArrayList gerentes)
        {
            double promedio = 0;
            Console.WriteLine(a);
            Gerente aux = (Gerente)gerentes[0];
            Console.Write(" {0,32}    ", "Promedio mensual");
            for (int i=0; i<aux.ventas.Length; i++)
            {
                foreach (Gerente gerente in gerentes)
                {
                    promedio += gerente.ventas[i];
                }
                Console.Write(" {0,-12:N2}", promedio / gerentes.Count);
            }
            Console.WriteLine("");
        }
    }
        
    class Persona
    {
        protected string nombre;        
        
        public Persona(string nombre)
        {
            this.nombre = nombre;
            
        }

        public virtual void mostrarDatos()
        {
            Console.Write(" {0,-15}", this.nombre);
        }
    }

    class Gerente : Persona
    {        
        private string sucursal;
        public double[] ventas;

        public Gerente(string nombre, string sucursal, double[] ventas)
            : base(nombre)
        {            
            this.sucursal = sucursal;
            this.ventas = ventas;
        }

        public override void mostrarDatos()
        {   
            base.mostrarDatos();
            Console.Write(" {0,-20} ", this.sucursal);
            double promedio = 0;
            foreach (double venta in ventas)
            {
                Console.Write("{0,-12:N2} ", venta);
                promedio += venta;
            }
            promedio = promedio / ventas.Length;
            Console.WriteLine(" {0:N2} bs", promedio);            
        }        
    }

}