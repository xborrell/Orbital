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

    public void CalcularNuevaRotacion()
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
            case ActitudRotacion.Maniobrando: _data.Orientacion = maniobra.CalcularNuevaOrientacion(); break;
            case ActitudRotacion.EnfocadoATierra: _data.Orientacion = CalcularOrientacionATierra(); break;
            case ActitudRotacion.Orbital: _data.Orientacion = CalcularOrientacionOrbital(); break;
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
                case ActitudRotacion.EnfocadoATierra: OrientacionATierra(); break;
                case ActitudRotacion.Orbital: OrientacionOrbital(); break;
            }
        }
        _data.ActitudSolicitada = ActitudRotacion.Ninguna;
    }

    private void RotacionLibre()
    {
        _data.Actitud = ActitudRotacion.CaidaLibre;
    }

    private void OrientacionOrbital()
    {
        _data.Actitud = ActitudRotacion.Maniobrando;
        Vector3 orientacion = CalcularOrientacionOrbital();

        maniobra = new ManiobraRotacion(ActitudRotacion.Orbital, _data, orientacion);
    }

    private void OrientacionATierra()
    {
        _data.Actitud = ActitudRotacion.Maniobrando;
        Vector3 orientacion = CalcularOrientacionATierra();

        maniobra = new ManiobraRotacion(ActitudRotacion.EnfocadoATierra, _data, orientacion);
    }

    Vector3 CalcularOrientacionATierra()
    {
        Vector3 forward = _data.Posicion;
        forward.Normalize();
        forward *= -1;

        return forward;
    }

    Vector3 CalcularOrientacionOrbital()
    {

        Vector3 forward = _data.Velocidad;
        forward.Normalize();

        return forward;
    }
}
