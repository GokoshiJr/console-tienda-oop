using System;
using System.Collections;

namespace Tienda
{
    class Program
    {

        static void Main(string[] args)
        {
            ArrayList personal = new ArrayList();
            string line;
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                System.IO.StreamReader sr = new System.IO.StreamReader("../../../data.csv");
                //Read the first line of text
                string separador = ",";
                line = sr.ReadLine();
                Console.WriteLine("-----------------------------------");
                Console.WriteLine(" Departamento de administracion (Subway)");
                Console.WriteLine("-----------------------------------");
                //Continue to read until you reach end of file
                while ((line = sr.ReadLine()) != null)
                {
                    string[] fila = line.Split(separador);
                    
                    double tipoEmpleado = Convert.ToDouble(fila[0]);
                    string nombre = fila[1];
                    string cedula = fila[2];
                    double horas = Convert.ToDouble(fila[3]);

                    if (tipoEmpleado == 0)
                    {
                        // gerente
                        personal.Add(new Gerente(nombre, cedula, horas, 10));

                    } else
                    {
                        // empleado
                        personal.Add(new Empleado(nombre, cedula, horas));
                    }
                }                
                foreach (Persona persona in personal)
                {
                    persona.mostrarDatos();
                    Console.WriteLine("-----------------------------------");
                }
                //close the file
                sr.Close();
                Console.WriteLine(" Total pago a gerentes: {0} bs", calcularPagoGerentes(personal));                
                Console.WriteLine(" Total pago a empleados: {0} bs", calcularPagoEmpleados(personal));                
                Console.WriteLine(" Total nomina: {0} bs", calcularPagoEmpleados(personal) + calcularPagoGerentes(personal));
                Console.WriteLine("-----------------------------------");
                Console.Write(" Presione Enter para continuar");
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("-----------------------------------");
                Console.WriteLine(" Exception: " + e);
            }
            finally
            {
                Console.WriteLine("-----------------------------------");
                Console.WriteLine(" Fin del programa...");
            }
            
        }

        public static double calcularPagoGerentes(ArrayList personal)
        {
            double total = 0;
            foreach (Persona persona in personal)
            {
                Type a = persona.GetType();
                if (a == typeof(Gerente))
                {
                    Gerente gerente = (Gerente)persona;
                    total += gerente.calcularSueldo();
                }
            }
            
            return total;
        }

        public static double calcularPagoEmpleados(ArrayList personal)
        {
            double total = 0;
            foreach (Persona persona in personal)
            {
                Type a = persona.GetType(); 
                if (a == typeof(Empleado))
                {
                    Empleado gerente = (Empleado)persona;
                    total += gerente.calcularSueldo();
                }
            }

            return total;
        }

    }
        
    class Persona
    {
        protected string nombre;
        protected string cedula;
        
        public Persona(string nombre, string cedula)
        {
            this.nombre = nombre;
            this.cedula = cedula;
        }

        public virtual void mostrarDatos()
        {
            Console.WriteLine(" Nombre: {0}, Cedula: {1}", this.nombre, this.cedula);
        }
    }

    class Empleado : Persona
    {
        protected double horas;
        private int sueldoHora = 5;
        public Empleado(string nombre, string cedula, double horas)
            : base(nombre, cedula)
        {
            this.horas = horas;
        }

        public override void mostrarDatos()
        {
            Console.WriteLine(" - Empleado");
            base.mostrarDatos();
            Console.WriteLine(" Sueldo: {0} bs", this.calcularSueldo());
        }

        public double calcularSueldo()
        {   
            return this.horas * this.sueldoHora;
        }
    }

    class Gerente : Persona
    {
        private double regalias, horas;
        private int sueldoHora = 20;
        public Gerente(string nombre, string cedula, double horas, double regalias)
            : base(nombre, cedula)
        {
            this.regalias = regalias;
            this.horas = horas;
        }
        public string getNombre()
        {
            return this.nombre;
        }
        public override void mostrarDatos()
        {
            Console.WriteLine(" - Gerente");
            base.mostrarDatos();
            Console.WriteLine(" Sueldo: {0} bs", this.calcularSueldo());
        }
        public double calcularSueldo()
        {
            return (this.horas * this.sueldoHora) + this.regalias;
        }
    }

}