using UnityEngine;
using System.Collections;
using System;

public class SateliteData
{
    public Vector3 Velocidad;
    public Vector3 Posicion;
    public Vector3 Orientacion;
    public Vector3 Camara;
    public ActitudRotacion Actitud = ActitudRotacion.CaidaLibre;
    public ActitudRotacion ActitudSolicitada = ActitudRotacion.Ninguna;
    public float Impulso = 0;
    public float ImpulsoSolicitado = -1;
    public float AlturaDeReferencia;

    public float Apoapsis { get; set; }
    public float Periapsis { get; set; }
    public float Inclinacion { get; set; }
    public float SemiejeMayor { get { return (Apoapsis + Periapsis) / 2; } }
    public bool? OrbitaSubiendo { get; set; }

    public float Altura
    {
        get
        {
            if (Actitud == ActitudRotacion.EnfocadoATierra)
            {
                float alturaAbsoluta = Posicion.magnitude;
                float alturaSobreElSuelo = alturaAbsoluta - Config.EarthRadius;

                return alturaSobreElSuelo;
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

                return (float)Vector3.Angle(referencia, Posicion);
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

                return (float)Vector3.Angle(referencia, Posicion);
            }

            return -1;
        }
    }

    public float VelocidadPeriapsis
    {
        get
        {
            double rp = Periapsis + Config.EarthRadius;
            double ra = Apoapsis + Config.EarthRadius;
            double h = Math.Sqrt(Config.Mu * 2) * Math.Sqrt((ra * rp) / (ra + rp));

            return (float)(h / rp);
        }
    }

    public SateliteData(Vector3 posicion, Vector3 velocidad)
    {
        Velocidad = velocidad;
        Posicion = posicion;
        
        Orientacion = new Vector3(Config.GetRandomValue(1, 10), Config.GetRandomValue(1, 10), Config.GetRandomValue(1, 10));
        Orientacion.Normalize();
        
        Camara = new Vector3(posicion.x, posicion.y, posicion.z);
        Camara.Normalize();
        Camara = Camara * 10;

        InvalidateOrbitalValues();

        //Apoapsis = 408.384766F;
        //Periapsis = 397.3208F;
    }

    public void InvalidateOrbitalValues()
    {
        Apoapsis = -1;
        Periapsis = -1;
        Inclinacion = -1;
        OrbitaSubiendo = null;
    }
}
