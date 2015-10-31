using UnityEngine;
using System.Collections;
using System;

public class ManiobraRotacion
{
    public bool ManiobraCompletada { get; protected set; }
    public ActitudRotacion SiguienteActitud { get; protected set; }

    private const float velocidadAngularEnGradosPorSegundo = 10.0F;
    private float tiempoParaFinalizarEnSegundos;
    private float tiempoTranscurridoEnSegundos;
    Vector3 orientacionInicial;
    Vector3 orientacionFinal;

    public ManiobraRotacion(ActitudRotacion actitudDestino, SateliteData data, Vector3 orientacionSolicitada)
    {
        SiguienteActitud = actitudDestino;

        orientacionInicial = data.Orientacion;
        orientacionFinal = orientacionSolicitada;

        var anguloEnGrados = Vector3.Angle( orientacionInicial, orientacionFinal);

        tiempoParaFinalizarEnSegundos = anguloEnGrados / velocidadAngularEnGradosPorSegundo;
    }

    public Vector3 CalcularNuevaOrientacion()
    {
        Vector3 orientacionCalculada;

        if (!ManiobraCompletada)
        {
            tiempoTranscurridoEnSegundos += Time.fixedDeltaTime;

            var porcentajeDeRotacion = Math.Min(tiempoTranscurridoEnSegundos / tiempoParaFinalizarEnSegundos, 1);

            orientacionCalculada = Vector3.Lerp(orientacionInicial, orientacionFinal, porcentajeDeRotacion);

            ManiobraCompletada = porcentajeDeRotacion == 1;
        }
        else
        {
            orientacionCalculada = orientacionFinal;
        }

        return orientacionCalculada;
    }
}
