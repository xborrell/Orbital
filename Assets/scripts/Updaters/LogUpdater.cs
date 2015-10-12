using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Text;

public class LogUpdater : MonoBehaviour
{
    protected Text campo;
    protected GameManager gameManager;

    void Start()
    {
        var model = GameObject.Find("Model");
        Debug.Assert(model != null, "No se ha encontrado el modelo.");

        gameManager = (GameManager)model.GetComponent(typeof(GameManager));

        campo = gameObject.GetComponent<Text>();
    }

    void Update()
    {
        Satelite satelite = gameManager.SateliteSeleccionado;

        Decision ultimaDecision = satelite.Mente.DecisionEnCurso;

        StringBuilder sb = new StringBuilder();

        if (ultimaDecision != null)
        {
            sb.AppendLine(ultimaDecision.LogItem.Descripcion);
        }

        foreach (LogItem logItem in satelite.Data.Logger.Mensajes)
        {
            switch (logItem.Level)
            {
                case 0:
                    sb.Append(logItem.Descripcion);
                    break;
                case 1:
                    sb.AppendFormat("    -{0}", logItem.Descripcion);
                    break;
                default:
                    throw new ArgumentException(string.Format("Nivell de log no previst: {0}", logItem.Level));
            }

            sb.AppendLine();
        }

        campo.text = sb.ToString();
    }
}
