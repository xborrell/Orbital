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
    Quaternion rotacionInicial;
    Quaternion rotacionFinal;

    public ManiobraRotacion( ActitudRotacion actitudDestino, SateliteData data, Quaternion rotacion)
    {
        SiguienteActitud = actitudDestino;

        rotacionInicial = data.Rotacion;
        rotacionFinal = rotacion;

        var anguloEnGrados = Quaternion.Angle(rotacionInicial, rotacionFinal);

        tiempoParaFinalizarEnSegundos = anguloEnGrados / velocidadAngularEnGradosPorSegundo;
    }

    public Quaternion CalcularNuevaRotacion(float deltaTime)
    {
        Quaternion rotacionCalculada;

        if (!ManiobraCompletada)
        {
            tiempoTranscurridoEnSegundos += deltaTime;

            var porcentajeDeRotacion = Math.Min(tiempoTranscurridoEnSegundos / tiempoParaFinalizarEnSegundos, 1);

            rotacionCalculada = Quaternion.Lerp(rotacionInicial, rotacionFinal, porcentajeDeRotacion);

            ManiobraCompletada = porcentajeDeRotacion == 1;
        }
        else
        {
            rotacionCalculada = rotacionFinal;
        }

        return rotacionCalculada;
    }
}
