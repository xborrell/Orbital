using UnityEngine;
using System.Collections;
using System;

public class SateliteData
{
    public Vector3 Velocidad;
    public Vector3 PosicionEnModelo;
    public Quaternion Rotacion;
    public ActitudRotacion Actitud = ActitudRotacion.CaidaLibre;
    public ActitudRotacion ActitudSolicitada = ActitudRotacion.Ninguna;

    public int Apoapsis { get; set; }
    public int Periapsis { get; set; }
    public float Inclinacion { get; set; }

    public int Altura
    {
        get
        {
            if (Actitud == ActitudRotacion.EnfocadoATierra)
            {
                float alturaAbsoluta = PosicionEnModelo.magnitude;
                float alturaSobreElSuelo = alturaAbsoluta - Config.EarthRadius;

                return (int)Math.Round(alturaSobreElSuelo, 0);
            }

            return 0;
        }
    }
}
