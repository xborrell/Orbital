using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

abstract public class Decision : ILogable
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
                Debug.Assert(value.Level == 0);
                _logItem = value;
            }
        }
    }
    LogItem _logItem = null;

    private List<Paso> pasosAEjecutar = new List<Paso>();
    private List<Paso> DefinicionDePasos = new List<Paso>();
    public Paso PasoActual { get { return pasosAEjecutar.Count > 0 ? pasosAEjecutar[0] : null; } }

    public bool DecisionFinalizada { get; protected set; }
    protected SateliteData Data { get; set; }

    abstract public bool DebeActuar();

    public Decision(SateliteData data)
    {
        Data = data;
    }

    virtual public void Inicializar()
    {
        DecisionFinalizada = false;
        pasosAEjecutar.AddRange(DefinicionDePasos);
        pasosAEjecutar.ForEach(x => x.Inicializar());

        if (pasosAEjecutar.Count > 0)
            Data.Logger.Informar(pasosAEjecutar[0]);
    }

    public string AccionEnCurso
    {
        get
        {
            int index = 0;

            while (index < pasosAEjecutar.Count)
            {
                LogItem logItem = pasosAEjecutar[index].LogItem;
                if ( logItem != null)
                    return logItem.Titulo;

                index++;
            }

            return string.Empty;
        }
    }

    protected void DefinirPaso(Paso paso)
    {
        DefinicionDePasos.Add(paso);
    }

    public void Actua(float deltaTime)
    {
        while (pasosAEjecutar[0].PasoFinalizado)
        {
            bool informarDelCambioDePaso = !(pasosAEjecutar[0] is PasoEsperar);

            pasosAEjecutar.RemoveAt(0);

            if (pasosAEjecutar.Count == 0)
            {
                DecisionFinalizada = true;
                return;
            }

            if (informarDelCambioDePaso)
                Data.Logger.Informar(pasosAEjecutar[0]);
        }

        pasosAEjecutar[0].Ejecutar(deltaTime);

        var segundosAEsperar = pasosAEjecutar[0].SegundosAEsperar;

        if (segundosAEsperar > 0)
        {
            pasosAEjecutar.Insert(0, new PasoEsperar(Data, segundosAEsperar));
            pasosAEjecutar[0].SegundosAEsperar = 0;
        }
    }

    protected void SolicitarEspera(float segundosAEsperar)
    {
        pasosAEjecutar.Insert(0, new PasoEsperar(Data, segundosAEsperar));
    }
}
