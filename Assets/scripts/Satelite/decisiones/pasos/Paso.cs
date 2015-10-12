using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

abstract public class Paso : ILogable
{
    public LogItem LogItem
    {
        get
        {
            return _logItem;
        }
        protected set
        {
            if (_logItem != value)
            {
                Debug.Assert(value.Level == 1);
                _logItem = value;
            }
        }
    }
    LogItem _logItem = null;

    public SateliteData Data { get; protected set; }
    public bool PasoFinalizado { get; protected set; }
    public float SegundosAEsperar { get; set; }

    public Paso(SateliteData data)
    {
        Data = data;
        PasoFinalizado = false;
        SegundosAEsperar = 0;
    }

    virtual public void Inicializar()
    {
        PasoFinalizado = false;
    }

    abstract public void Ejecutar(float time);
}