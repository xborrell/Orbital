using System;
using System.Windows.Media.Media3D;

namespace CalculadorOrbital
{
    static public class Constantes
    {
        public const float Mu = 398600F;                                    // Km3 / segundo2
        public const float RadianToDegreeCoeficient = 180 / (float)Math.PI;
        public const float DegreeToRadianCoeficient = (float)Math.PI / 180;
        public const float Pi2 = (float)(2 * Math.PI);
        public const float ConstanteGravitacionUniversal = 6.674E-11F; //6,6722464010713090056913290927352e-20
        public const float EarthRadius = 6378.280F; // KM
        public const float EarthMass = 5.974E24F; // KG
        public const float SatellitMass = 750F; // KG
        static public Vector3D Forward;
        static public Vector3D Zero;

        static Constantes()
        {
            Forward = new Vector3D(0, 0, 1);
            Zero = new Vector3D(0, 0, 0);
        }
    }
}
