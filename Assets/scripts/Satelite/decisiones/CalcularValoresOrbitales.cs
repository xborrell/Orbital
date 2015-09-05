using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CalcularValoresOrbitales : IDecision
{
    enum Paso
    {
        Inicio,
        SolicitarEnfoqueATierra,
        ComprobarEnfoqueCorrecto,
        TomarAltura,
        CalcularInclinacion,
        ComprobarSiLaOrbitaSubeOBaja,
        CalcularApoapsis,
        CalcularPeriapsis,
        Final
    }

    Stack<Action<float>> pasosAEjecutar = new Stack<Action<float>>();

    public bool DecisionFinalizada { get; protected set; }
    SateliteData _data;

    int alturaAnterior;
    float marcaDeTiempo = -1;

    public CalcularValoresOrbitales(SateliteData data)
    {
        _data = data;

        pasosAEjecutar.Push(Inicio);
    }

    public void Actua(float deltaTime)
    {
        pasosAEjecutar.Peek()(deltaTime);
    }

    void CambiarPaso(Action<float> paso)
    {
        pasosAEjecutar.Pop();
        pasosAEjecutar.Push(paso);
    }

    void SolicitarEspera(float segundosAEsperar)
    {
        marcaDeTiempo = segundosAEsperar;
        pasosAEjecutar.Push(Esperar);
    }

    void Inicio(float deltaTime)
    {
        CambiarPaso(SolicitarEnfoqueATierra);
    }

    void Final(float deltaTime)
    {
        DecisionFinalizada = true;
    }

    void SolicitarEnfoqueATierra(float deltaTime)
    {
        _data.ActitudSolicitada = ActitudRotacion.EnfocadoATierra;
        CambiarPaso(ComprobarEnfoqueCorrecto);
    }

    void ComprobarEnfoqueCorrecto(float deltaTime)
    {
        if (_data.Actitud == ActitudRotacion.EnfocadoATierra)
        {
            CambiarPaso(TomarAltura);
        }
    }

    void TomarAltura(float deltaTime)
    {
        alturaAnterior = _data.Altura;
        CambiarPaso(ComprobarSiLaOrbitaSubeOBaja);
        SolicitarEspera(5);
    }

    void Esperar(float deltaTime)
    {
        marcaDeTiempo -= deltaTime;

        if (marcaDeTiempo < 0)
        {
            pasosAEjecutar.Pop();
        }
    }

    void ComprobarSiLaOrbitaSubeOBaja(float deltaTime)
    {
        if (alturaAnterior > _data.Altura)
        {
            CambiarPaso(CalcularPeriapsis);
        }
        else if (alturaAnterior < _data.Altura)
        {
            CambiarPaso(CalcularApoapsis);
        }
    }

    void CalcularPeriapsis(float deltaTime)
    {
        if (alturaAnterior < _data.Altura)
        {
            _data.Periapsis = alturaAnterior;

            if (_data.Apoapsis == 0)
            {
                CambiarPaso(CalcularApoapsis);
            }
            else
            {
                CambiarPaso(Final);
            }
        }
        else
        {
            alturaAnterior = _data.Altura;
            SolicitarEspera(30);
        }
    }

    void CalcularApoapsis(float deltaTime)
    {
        if (alturaAnterior > _data.Altura)
        {
            _data.Apoapsis = alturaAnterior;

            if (_data.Periapsis == 0)
            {
                CambiarPaso(CalcularPeriapsis);
            }
            else
            {
                CambiarPaso(Final);
            }
        }
        else
        {
            alturaAnterior = _data.Altura;
            SolicitarEspera(30);
        }
    }
}
