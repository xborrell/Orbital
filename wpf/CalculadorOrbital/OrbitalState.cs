using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace CalculadorOrbital
{
    public class OrbitalState : IEquatable<OrbitalState>
    {
        public readonly Vector3D Posicion;
        public readonly Vector3D Velocidad;

        public OrbitalState(Vector3D posicion, Vector3D velocidad)
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
