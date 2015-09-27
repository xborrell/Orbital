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
    public string Accion { get { return mente.Descripcion; } }
    public SateliteData Data { get { return data; } }

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

    public Satelite( Vector3 posicionInicial, Vector3 velocidadInicial)
    {
        data = new SateliteData(posicionInicial, velocidadInicial);
        calculadorMovimiento = new CalculadorMovimiento(data);
        calculadorRotacion = new CalculadorRotacion(data);
        mente = new MenteSatelite(data);
        motor = new MotorSatelite(data);
    }

    public void FixedUpdate()
    {
        //mente.Update(Time.deltaTime);
        //motor.CalcularImpulso(Time.deltaTime);

        //calculadorMovimiento.CalcularNuevaPosicion(Time.deltaTime);
        //calculadorRotacion.CalcularNuevaRotacion(Time.deltaTime);
    }
}
