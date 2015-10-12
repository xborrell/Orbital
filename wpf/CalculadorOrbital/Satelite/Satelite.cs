using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace CalculadorOrbital
{
    public class Satelite
    {
        SateliteData data;
        public SateliteData Data { get { return data; } }
        CalculadorMovimiento calculadorMovimiento;
        CalculadorRotacion calculadorRotacion;
        MenteSatelite mente;
        MotorSatelite motor;
        public Vector3D Posicion { get { return data.Posicion; } }
        public Vector3D Velocidad { get { return data.Velocidad; } }
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

        public Satelite(Vector3D posicion, Vector3D velocidad)
        {
            data = new SateliteData();
            calculadorMovimiento = new CalculadorMovimiento(data);
            calculadorRotacion = new CalculadorRotacion(data);
            mente = new MenteSatelite(data);
            motor = new MotorSatelite(data);

            data.Posicion = posicion;
            data.Velocidad = velocidad;
            data.Rotacion = new Quaternion();
        }

        public void Update(float deltaTime)
        {
            mente.Update(deltaTime);

            motor.CalcularImpulso(deltaTime);
            calculadorMovimiento.CalcularNuevaPosicion(deltaTime);
            calculadorRotacion.CalcularNuevaRotacion(deltaTime);
        }
    }
}
