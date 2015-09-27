using UnityEngine;
using System.Collections;

public class CameraMaster : MonoBehaviour
{
    float scrollSpeed = 100f;
    int zoomMin = 5;
    int zoomMax = 2000;
    float xSpeed = 5.0F;
    float ySpeed = 5.0F;

    private bool isActivated;

    GameManager gameManager;
    Conversor conversor = new Conversor();

    void Start()
    {
        var model = GameObject.Find("Model");
        Debug.Assert(model != null, "No se ha encontrado el modelo.");

        gameManager = (GameManager)model.GetComponent(typeof(GameManager));
    }

    void LateUpdate()
    {
        var data = gameManager.SateliteSeleccionado.Data;
        transform.position = conversor.FromModelToRender( data.Posicion + data.Camara );

        transform.rotation = Quaternion.LookRotation(conversor.FromModelToRender( data.Camara * -1 ), Vector3.up);

        CheckActivation();

        if (isActivated)
        {
            var xAngulo = Input.GetAxis("Mouse X") * xSpeed;
            var yAngulo = Input.GetAxis("Mouse Y") * xSpeed;

            Vector3 xAxis = -data.Posicion;
            xAxis.Normalize();
            Vector3 zAxis = Vector3.forward;
            Vector3 yAxis = Vector3.Cross( xAxis, zAxis );


            var q1 = Quaternion.AngleAxis(xAngulo, Vector3.forward);
            var q2 = Quaternion.AngleAxis(yAngulo, yAxis);
            var q3 = q1 * q2;

            data.Camara = q3 * data.Camara;
        }

        GestionarZoom(data);
    }

    void GestionarZoom(SateliteData data)
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            var movimiento = Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
            var nuevaDistancia = ZoomLimit(data.Camara.magnitude - movimiento);

            data.Camara.Normalize();
            data.Camara = data.Camara * nuevaDistancia;
        }
    }

    float ZoomLimit(float dist)
    {
        if (dist < zoomMin)
            dist = zoomMin;

        if (dist > zoomMax)
            dist = zoomMax;

        return dist;
    }

    void CheckActivation()
    {
        if (Input.GetMouseButtonDown(1))        // only update if the mousebutton is held down
        {
            isActivated = true;
        }

        if (Input.GetMouseButtonUp(1))          // if mouse button is let UP then stop rotating camera
        {
            isActivated = false;
        }
    }
}
