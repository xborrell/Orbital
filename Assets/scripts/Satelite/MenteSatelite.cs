using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MenteSatelite
{
    List<Decision> decisiones;
    public Decision DecisionEnCurso { get; protected set; }
    SateliteData data;
    GameManager manager;

    public MenteSatelite(SateliteData data, GameManager manager)
    {
        this.manager = manager;
        this.data = data;

        decisiones = new List<Decision>() {
                new CalcularSentidoDeLaOrbita(data),
                new CalcularApoapsis(data),
                new CalcularPeriapsis(data),
                new CalcularInclinacion(data),
                new Circularizar(data),
                new Esperar(data, 60F),
            };
        ObtieneLaSiguienteDecision();
    }

    internal void Update()
    {
        if (DecisionEnCurso == null)
            ObtieneLaSiguienteDecision();

        else if (DecisionEnCurso.DecisionFinalizada)
            FinalizaDecision();

        else
            DecisionEnCurso.Actua();
    }

    void ObtieneLaSiguienteDecision()
    {
        foreach (Decision decision in decisiones)
        {
            if (decision.DebeActuar())
            {
                decision.Inicializar();
                DecisionEnCurso = decision;
                manager.Pausa();
                return;
            }
        }
    }

    private void FinalizaDecision()
    {
        data.Logger.Informar( DecisionEnCurso);
        DecisionEnCurso = null;

        if (data.Actitud != ActitudRotacion.CaidaLibre)
            data.ActitudSolicitada = ActitudRotacion.CaidaLibre;
    }
}

