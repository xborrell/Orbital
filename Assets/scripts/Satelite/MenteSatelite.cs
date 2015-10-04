using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MenteSatelite
{
    List<Decision> decisiones;
    public Decision decisionEnCurso { get; protected set; }
    SateliteData data;

    public string Descripcion
    {
        get
        {
            return decisionEnCurso == null ? "Pensando" : decisionEnCurso.Descripcion;
        }
    }

    public MenteSatelite(SateliteData data)
    {
        this.data = data;

        decisiones = new List<Decision>() {
                new CalcularSentidoDeLaOrbita(data),
                new CalcularApoapsis(data),
                new CalcularPeriapsis(data),
                new CalcularInclinacion(data),
                new Circularizar(data),
                new PosicionarSateliteEnActitudOrbital(data),
                new Esperar(data, 60F),
            };
        ObtieneLaSiguienteDecision();
    }

    internal void Update(float deltaTime)
    {
        if (decisionEnCurso == null)
            ObtieneLaSiguienteDecision();

        else if (decisionEnCurso.DecisionFinalizada)
            FinalizaDecision();

        else
            decisionEnCurso.Actua(deltaTime);
    }

    void ObtieneLaSiguienteDecision()
    {
        foreach (Decision decision in decisiones)
        {
            if (decision.DebeActuar())
            {
                decision.Inicializar();
                decisionEnCurso = decision;
                return;
            }
        }
    }

    private void FinalizaDecision()
    {
        decisionEnCurso = null;

        if (data.Actitud != ActitudRotacion.CaidaLibre)
            data.ActitudSolicitada = ActitudRotacion.CaidaLibre;
    }
}

