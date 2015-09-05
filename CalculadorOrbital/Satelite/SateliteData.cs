using UnityEngine;
using System.Collections;
using System;

namespace CalculadorOrbital
{
    public class SateliteData
    {
        public Vector3 Velocidad;
        public Vector3 Posicion;
        public ActitudRotacion Actitud = ActitudRotacion.CaidaLibre;
        public ActitudRotacion ActitudSolicitada = ActitudRotacion.Ninguna;

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
                    return Posicion.magnitude - Constantes.EarthRadius;
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
                    Vector3 referencia = new Vector3(Posicion.x, 0, Posicion.z);

                    return Vector3.Angle(referencia, Posicion);
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
                    Vector3 referencia = new Vector3(0, Posicion.y, Posicion.z);

                    return Vector3.Angle(referencia, Posicion);
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