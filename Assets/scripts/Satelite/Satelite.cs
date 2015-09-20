using UnityEngine;
using System.Collections;
using System;

public class Satelite : MonoBehaviour
{
    public string Nombre;
    public Vector3 VelocidadInicial;

    GameManager gameManager;
    Transform childModel;
    
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

    void Awake()
    {
        data = new SateliteData();
        calculadorMovimiento = new CalculadorMovimiento(data);
        calculadorRotacion = new CalculadorRotacion(data);
        mente = new MenteSatelite(data);
        motor = new MotorSatelite(data);
    }

    // Use this for initialization
    void Start()
    {
        var model = GameObject.Find("Model");
        Debug.Assert(model != null, "No se ha encontrado GameManager en Satelite.");

        gameManager = (GameManager)model.GetComponent(typeof(GameManager));

        foreach (Transform child in transform)
        {
            if (child.name.StartsWith("model"))
            {
                childModel = child;
            }
        }

        data.Posicion = transform.position;
        data.Velocidad = VelocidadInicial;
        data.Rotacion = childModel.transform.rotation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mente.Update(Time.deltaTime);
        motor.CalcularImpulso(Time.deltaTime);

        transform.position = calculadorMovimiento.CalcularNuevaPosicion(Time.deltaTime);
        childModel.transform.rotation = calculadorRotacion.CalcularNuevaRotacion(Time.deltaTime);
    }
}
