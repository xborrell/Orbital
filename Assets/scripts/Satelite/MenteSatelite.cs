using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MenteSatelite
{
    enum state
    {
        inicio,
        calculoValores,
        espera,
    }

    protected SateliteData _data;
    IDecision decision;
    state estado = state.inicio;

    public string Descripcion
    {
        get
        {
            switch (estado)
            {
                case state.inicio: return "Inicializando";
                case state.calculoValores: return "Calculando orbita";
                case state.espera: return "En espera";
                default: throw new ArgumentException("Estado no implementado");
            }
        }
    }

    public MenteSatelite(SateliteData data)
    {
        _data = data;
    }

    internal void Update(float deltaTime)
    {
        if (decision == null)
            GeneraSiguienteDecision();

        else if (decision.DecisionFinalizada)
            FinalizaDecision();

        else
            decision.Actua(deltaTime);
    }

    void GeneraSiguienteDecision()
    {
        switch (estado)
        {
            case state.inicio: BeginInicio(); break;
            case state.calculoValores: BeginCalculo(); break;
            case state.espera: BeginEspera(); break;
            default: throw new ArgumentException("Estado no implementado");
        }
    }

    private void FinalizaDecision()
    {
        switch (estado)
        {
            case state.inicio: EndInicio(); break;
            case state.calculoValores: EndCalculo(); break;
            case state.espera: EndEspera(); break;
            default: throw new ArgumentException("Estado no implementado");
        }

        decision = null;
    }

    private void BeginInicio()
    {
        decision = new IniciarSatelite(_data);
    }

    private void EndInicio()
    {
        estado = state.calculoValores;
    }

    private void BeginEspera()
    {
        decision = new Esperar(_data);
    }

    private void EndEspera()
    {
        estado = state.espera;
    }

    private void BeginCalculo()
    {
        decision = new CalcularValoresOrbitales(_data);
    }

    private void EndCalculo()
    {
        estado = state.espera;
    }
}
