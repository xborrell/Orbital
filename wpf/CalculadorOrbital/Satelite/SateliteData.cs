using System.Collections;
using System;
using System.Windows.Media.Media3D;

namespace CalculadorOrbital
{
    public class SateliteData
    {
        public Vector3D Velocidad;
        public Vector3D Posicion;
        public Quaternion Rotacion;
        public ActitudRotacion Actitud = ActitudRotacion.CaidaLibre;
        public ActitudRotacion ActitudSolicitada = ActitudRotacion.Ninguna;
        public float Impulso = 0;
        public float ImpulsoSolicitado = -1;

        public float Apoapsis { get; set; }
        public float Periapsis { get; set; }
        public float SemiejeMayor { get { return (Apoapsis + Periapsis) / 2; } }
        public float Inclinacion { get; set; }
        public bool? OrbitaSubiendo { get; set; }

        public float Altura
        {
            get
            {
                if (Actitud == ActitudRotacion.EnfocadoATierra)
                {
                    return (float)Posicion.Length - Constantes.EarthRadius;
                }

                return -1;
            }
        }

        public float Latitud
        {
            get
            {
                if (Actitud == ActitudRotacion.EnfocadoATierra)
                {
                    Vector3D referencia = new Vector3D(Posicion.X, 0, Posicion.Z);

                    return (float)Vector3D.AngleBetween(referencia, Posicion);
                }

                return -1;
            }
        }

        public float Longitud
        {
            get
            {
                if (Actitud == ActitudRotacion.EnfocadoATierra)
                {
                    Vector3D referencia = new Vector3D(0, Posicion.Y, Posicion.Z);

                    return (float)Vector3D.AngleBetween(referencia, Posicion);
                }

                return -1;
            }
        }

        public float VelocidadPeriapsis
        {
            get
            {
                double rp = Periapsis + Constantes.EarthRadius;
                double ra = Apoapsis + Constantes.EarthRadius;
                double h = Math.Sqrt(Constantes.Mu * 2) * Math.Sqrt((ra * rp) / (ra + rp));

                return (float)(h / rp);
            }
        }

        public SateliteData()
        {
            InvalidateOrbitalValues();
        }

        public void InvalidateOrbitalValues()
        {
            Apoapsis = -1;
            Periapsis = -1;
            Inclinacion = -1;
            OrbitaSubiendo = null;
        }
    }
}