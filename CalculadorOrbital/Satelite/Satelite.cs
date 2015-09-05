using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CalculadorOrbital
{
    public class Satelite
    {
        SateliteData data;
        public SateliteData Data { get { return data; } }
        CalculadorMovimiento calculadorMovimiento;
        CalculadorRotacion calculadorRotacion;
        MenteSatelite mente;
        public Vector3 Posicion { get { return data.Posicion; } }
        public Vector3 Velocidad { get { return data.Velocidad; } }
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

        public Satelite(Vector3 posicion, Vector3 velocidad)
        {
            data = new SateliteData();
            calculadorMovimiento = new CalculadorMovimiento(data);
            calculadorRotacion = new CalculadorRotacion(data);
            mente = new MenteSatelite(data);

            data.Posicion = posicion;
            data.Velocidad = velocidad;
        }

        public void Update(float deltaTime)
        {
            mente.Update(deltaTime);

            calculadorMovimiento.CalcularNuevaPosicion(deltaTime);
            calculadorRotacion.CalcularNuevaRotacion(deltaTime);
        }
    }
}
