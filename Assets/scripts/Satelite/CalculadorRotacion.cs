using UnityEngine;
using System.Collections;
using System;

public class CalculadorRotacion
{
    ManiobraRotacion maniobra = null;

    protected SateliteData _data;

    public CalculadorRotacion(SateliteData data)
    {
        _data = data;
    }

    public void CalcularNuevaRotacion(float deltaTime)
    {
        if (_data.ActitudSolicitada != ActitudRotacion.Ninguna)
        {
            GestionarCambioDeRotacion();
        }

        if ( (maniobra != null) && (maniobra.ManiobraCompletada) )
        {
            _data.Actitud = maniobra.SiguienteActitud;
            maniobra = null;
        }

        switch (_data.Actitud)
        {
            case ActitudRotacion.CaidaLibre: break;
            case ActitudRotacion.Maniobrando: _data.Rotacion = maniobra.CalcularNuevaRotacion(deltaTime); break;
            case ActitudRotacion.EnfocadoATierra: _data.Rotacion = CalcularRotacionATierra(); break;
            case ActitudRotacion.Orbital: _data.Rotacion = CalcularRotacionOrbital(); break;
            default: throw new ArgumentException("Actitud no implementada en CalculadorRotacion2");
        }
    }

    private void GestionarCambioDeRotacion()
    {
        if (_data.ActitudSolicitada != _data.Actitud)
        {
            switch (_data.ActitudSolicitada)
            {
                case ActitudRotacion.CaidaLibre: RotacionLibre(); break;
                case ActitudRotacion.EnfocadoATierra: RotacionATierra(); break;
                case ActitudRotacion.Orbital: RotacionOrbital(); break;
            }
        }
        _data.ActitudSolicitada = ActitudRotacion.Ninguna;
    }

    private void RotacionLibre()
    {
        _data.Actitud = ActitudRotacion.CaidaLibre;
    }

    private void RotacionOrbital()
    {
        _data.Actitud = ActitudRotacion.Maniobrando;
        Quaternion rotacion = CalcularRotacionOrbital();

        maniobra = new ManiobraRotacion(ActitudRotacion.Orbital, _data, rotacion);
    }

    private void RotacionATierra()
    {
        _data.Actitud = ActitudRotacion.Maniobrando;
        Quaternion rotacion = CalcularRotacionATierra();

        maniobra = new ManiobraRotacion(ActitudRotacion.EnfocadoATierra, _data, rotacion);
    }

    Quaternion CalcularRotacionATierra()
    {
        Vector3 forward = _data.Posicion;
        forward.Normalize();
        forward *= -1;

        Vector3 upward = _data.Velocidad;
        upward.Normalize();

        return Quaternion.LookRotation(forward, upward);
    }

    Quaternion CalcularRotacionOrbital()
    {

        Vector3 forward = _data.Velocidad;
        forward.Normalize();

        Vector3 upward = _data.Posicion;
        upward.Normalize();

        return Quaternion.LookRotation(forward, upward);
    }
}
