using UnityEngine;
using System.Collections;
using System;

public class RotacionTerrestre : MonoBehaviour {
	void Update () {
        TimeSpan tiempoTranscurrido = TimeSpan.FromSeconds(Time.time);
        TimeSpan borradoDeDias = TimeSpan.FromDays(tiempoTranscurrido.Days);
        tiempoTranscurrido = tiempoTranscurrido.Subtract(borradoDeDias);

        double segundosDeUnDia = 24 * 60 * 60;

        float gradosARotar = (float)(tiempoTranscurrido.TotalSeconds * 360 / segundosDeUnDia);

        transform.rotation = Quaternion.AngleAxis(-gradosARotar, Vector3.up);
    }
}
