using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

abstract public class DecisionCompleja : Decision
{
    private List<Paso> pasosAEjecutar = new List<Paso>();
    private List<Paso> DefinicionDePasos = new List<Paso>();

    public DecisionCompleja(SateliteData data) : base(data) { }
    override public string AccionEnCurso
    {
        get
        {
            int index = 0;

            while (index < pasosAEjecutar.Count)
            {
                if (!string.IsNullOrEmpty(pasosAEjecutar[index].Titulo))
                    return pasosAEjecutar[index].Titulo;

                index++;
            }

            return string.Empty;
        }
    }

    override public void Inicializar()
    {
        base.Inicializar();

        pasosAEjecutar.AddRange(DefinicionDePasos);
    }

    protected void DefinirPaso(Paso paso)
    {
        DefinicionDePasos.Add(paso);
    }

    override public void Actua(float deltaTime)
    {
        while (pasosAEjecutar[0].PasoFinalizado)
        {
            pasosAEjecutar.RemoveAt(0);

            if (pasosAEjecutar.Count == 0)
            {
                DecisionFinalizada = true;
                return;
            }
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