using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CalculadorOrbital
{
    public class OrbitalState : IEquatable<OrbitalState>
    {
        public readonly Vector3 Posicion;
        public readonly Vector3 Velocidad;

        public OrbitalState(Vector3 posicion, Vector3 velocidad)
        {
            Posicion = posicion;
            Velocidad = velocidad;
        }

        public override string ToString()
        {
            return string.Format("Posicion {0}, Velocidad {1}", Posicion, Velocidad);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as OrbitalState);
        }

        public bool Equals(OrbitalState other)
        {
            return Posicion == other.Posicion && Velocidad == other.Velocidad;
        }
    }
}
