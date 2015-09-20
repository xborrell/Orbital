using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace CalculadorOrbital
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            ConversorOrbital conversor = new ConversorOrbital();

            var posicionInicial = new Vector3D(-822.79774, -4438.63582, 5049.31502);
            var velocidadInicial = new Vector3D(7.418175658, .709253354, 1.828703177);

            var satelite = new Satelite(posicionInicial, velocidadInicial );

            OrbitalElements elementosIniciales = conversor.Convertir(satelite.Posicion, satelite.Velocidad);
            double remainTimeInSeconds = elementosIniciales.Period.TotalSeconds;
            double deltaTimeInSeconds = 0.1;

            while (remainTimeInSeconds > deltaTimeInSeconds)
            {
                satelite.Update((float)deltaTimeInSeconds);

                OrbitalElements elementosBucle = conversor.Convertir(satelite.Posicion, satelite.Velocidad);

                remainTimeInSeconds -= deltaTimeInSeconds;
            }

            Console.WriteLine("Periapsis           {0} Km.", satelite.Periapsis);
            Console.WriteLine("Apoapsis            {0} Km.", satelite.Apoapsis);
            Console.WriteLine("Semieje Mayor       {0} Km.", satelite.Data.SemiejeMayor);
            Console.WriteLine("Velocidad Periapsis {0} Km/s", satelite.Data.VelocidadPeriapsis);
        }
    }
}
