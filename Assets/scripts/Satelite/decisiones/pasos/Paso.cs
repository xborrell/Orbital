using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

abstract public class Paso
{
    public string Titulo { get; protected set; }
    public SateliteData Data { get; protected set; }
    public bool PasoFinalizado { get; protected set; }
    public float SegundosAEsperar { get; set; }

    public Paso(SateliteData data, string titulo)
    {
        Data = data;
        Titulo = titulo;
        PasoFinalizado = false;
        SegundosAEsperar = 0;
    }

    abstract public void Ejecutar(float time);
}