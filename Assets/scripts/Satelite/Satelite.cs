using UnityEngine;
using System.Collections;
using System;

public class Satelite
{
    public string Nombre;
    public Vector3 PosicionInicial;
    public Vector3 VelocidadInicial;

    SateliteData data;
    CalculadorMovimiento calculadorMovimiento;
    CalculadorRotacion calculadorRotacion;
    MenteSatelite mente;
    MotorSatelite motor;

    public float Altura { get { return data.Altura; } }
    public float Apoapsis { get { return data.Apoapsis; } }
    public float Periapsis { get { return data.Periapsis; } }
    public float Inclinacion { get { return data.Inclinacion; } }
    public SateliteData Data { get { return data; } }
    public MenteSatelite Mente { get { return mente; } }

    public string Accion
    {
        get
        {
            return Mente.DecisionEnCurso == null ? "Pensando" : Mente.DecisionEnCurso.LogItem.Titulo;
        }
    }

    public string Actitud
    {
        get
        {
            switch (data.Actitud)
            {
                case ActitudRotacion.CaidaLibre: return "Caida Libre";
                case ActitudRotacion.EnfocadoATierra: return "A Tierra";
                case ActitudRotacion.Orbital: return "Orbital";
                case ActitudRotacion.Maniobrando: return "Maniobrando";
                default: throw new ArgumentException("Actitud de rotacion desconocida");
            }
        }
    }

    public Satelite(Vector3 posicionInicial, Vector3 velocidadInicial, GameManager manager)
    {
        data = new SateliteData(posicionInicial, velocidadInicial);
        calculadorMovimiento = new CalculadorMovimiento(data);
        calculadorRotacion = new CalculadorRotacion(data);
        mente = new MenteSatelite(data, manager);
        motor = new MotorSatelite(data);
    }

    public void FixedUpdate()
    {
        mente.Update(Time.deltaTime);
        motor.CalcularImpulso(Time.deltaTime);

        calculadorMovimiento.CalcularNuevaPosicion(Time.deltaTime);
        calculadorRotacion.CalcularNuevaRotacion(Time.deltaTime);
    }
}
