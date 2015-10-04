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
    List<string> mensajes;
    List<string> pasos;
    Decision ultimaDecision;

    void Start()
    {
        var model = GameObject.Find("Model");
        Debug.Assert(model != null, "No se ha encontrado el modelo.");

        gameManager = (GameManager)model.GetComponent(typeof(GameManager));

        campo = gameObject.GetComponent<Text>();

        mensajes = new List<string>();
        pasos = new List<string>();

        ultimaDecision = gameManager.SateliteSeleccionado.Mente.decisionEnCurso;
    }

    void Update()
    {
        StringBuilder sb = new StringBuilder();

        if (ultimaDecision != null)
        {
            sb.AppendLine(ultimaDecision.Descripcion);

            if (!string.IsNullOrEmpty(ultimaDecision.AccionEnCurso))
            {
                if ((pasos.Count > 0) && (ultimaDecision.AccionEnCurso != pasos[0]))
                {
                    pasos.Add(string.Format("    {0}", ultimaDecision.AccionEnCurso));
                }
            }

            if (ultimaDecision != gameManager.SateliteSeleccionado.Mente.decisionEnCurso)
            {
                mensajes.Insert(0, ultimaDecision.Descripcion);
                ultimaDecision = gameManager.SateliteSeleccionado.Mente.decisionEnCurso;
                pasos.Clear();
            }

            foreach (string s in pasos)
                sb.AppendLine(s);
        }
        else
        {
            ultimaDecision = gameManager.SateliteSeleccionado.Mente.decisionEnCurso;
        }

        foreach (string s in mensajes)
            sb.AppendLine(s);

        campo.text = sb.ToString();
    }
}
