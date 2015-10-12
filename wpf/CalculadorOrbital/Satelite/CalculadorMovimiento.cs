using System.Collections;
using System;
using System.Windows.Media.Media3D;

namespace CalculadorOrbital
{

    public class CalculadorMovimiento
    {
        SateliteData _data;
        const float incremento = 0.1F;

        public CalculadorMovimiento(SateliteData data)
        {
            _data = data;
        }

        public void CalcularNuevaPosicion(float deltaTime)
        {
            while (deltaTime > incremento)
            {
                InternalCalcularNuevaPosicion(incremento);
                deltaTime -= incremento;
            }

            if (deltaTime > 0)
            {
                InternalCalcularNuevaPosicion(deltaTime);
            }
        }

        private void InternalCalcularNuevaPosicion(float deltaTime)
        {
            Vector3D vectorGravedad = CalcularAceleracionGravitatoria(deltaTime);
            Vector3D vectorMotor = CalcularAceleracionMotor(deltaTime);

            Vector3D vectorTotal = vectorGravedad + vectorMotor;

            // AplicarAceleraciones
            _data.Velocidad = _data.Velocidad + (vectorTotal * deltaTime);

            // Calcular Nueva Posicion
            Vector3D desplazamiento = _data.Velocidad * deltaTime;
            _data.Posicion += desplazamiento;
        }

        public Vector3D CalcularAceleracionGravitatoria(float deltaTime)
        {
            Vector3D fuerzaGravitatoria = CalcularFuerzaGravitatoria();
            return fuerzaGravitatoria / Constantes.SatellitMass;
        }

        public Vector3D CalcularAceleracionMotor(float deltaTime)
        {
            var moduloAceleracion = _data.Impulso * deltaTime;

            if (moduloAceleracion == 0) return Constantes.Zero;

            Vector3D vectorDireccion = ObtenerVectorDireccion();
            if (vectorDireccion == Constantes.Zero) return Constantes.Zero;

            vectorDireccion.Normalize();
            Vector3D vectorAceleracion = vectorDireccion * moduloAceleracion;

            return vectorAceleracion;
        }

        public Vector3D ObtenerVectorDireccion()
        {
            Vector3D direccion;

            switch (_data.Actitud)
            {
                case ActitudRotacion.EnfocadoATierra:
                    direccion = _data.Posicion;
                    direccion *= -1;
                    break;

                case ActitudRotacion.EnfocadoAlExterior:
                    direccion = _data.Posicion;
                    break;

                case ActitudRotacion.Orbital:
                    direccion = _data.Velocidad;
                    direccion *= -1;
                    break;

                case ActitudRotacion.OrbitalInverso:
                    direccion = _data.Velocidad;
                    break;

                default:
                    return Constantes.Zero;
            }

            direccion.Normalize();

            return direccion;
        }

        Vector3D CalcularFuerzaGravitatoria()
        {
            double gravitationModulus = CalcularAtraccionGravitatoria(Constantes.SatellitMass, _data.Posicion.Length);
            Vector3D gravitationForce = new Vector3D(_data.Posicion.X, _data.Posicion.Y, _data.Posicion.Z);
            gravitationForce.Normalize();
            gravitationForce = gravitationForce * (float)(gravitationModulus * -1);

            return gravitationForce;
        }

        private double CalcularAtraccionGravitatoria(double masa2, double distancia)
        {
            return Constantes.Mu * masa2 / Math.Pow(distancia, 2);
        }
    }
}