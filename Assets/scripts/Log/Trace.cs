using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Pathfinding.Serialization.JsonFx;
using UnityEngine;

public sealed class Trace : IDisposable
{
    public static Trace Instance { get; set; }
    JsonWriter writer;

    public Trace()
    {
        writer = new JsonWriter(@"C:\fonts\Orbital\wpf\OrbitalTrace.txt");
    }

    ~Trace()
    {
        Dispose(false);
    }

    #region IDisposable Members
    private bool disposed = false;

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                writer = null;
            }
        }
        disposed = true;
    }
    #endregion

    internal void Save(float timeInSeconds, SateliteData sateliteData)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(timeInSeconds);

        var data = new SateliteTrace()
        {
            TimeSpan = timeSpan,
            Velocidad = sateliteData.Velocidad,
            Posicion = sateliteData.Posicion,
        };

        writer.Write(data);
    }
}

public class SateliteTrace
{
    public TimeSpan TimeSpan;
    public Vector3 Velocidad;
    public Vector3 Posicion;
    //public Vector3 Orientacion;
    //public Vector3 Camara;
    //public ActitudRotacion Actitud;
    //public ActitudRotacion ActitudSolicitada;
    //public float Impulso;
    //public float ImpulsoSolicitado;
    //public float AlturaDeReferencia;
    //public float Apoapsis;
    //public float Periapsis;
    //public float Inclinacion;
    //public float SemiejeMayor;
    //public bool? OrbitaSubiendo;
    //public float Altura;
    //public float Latitud;
    //public float Longitud;
    //public float VelocidadPeriapsis;
}
