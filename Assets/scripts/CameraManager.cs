using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour
{
    public float scrollSpeed = 10.0f;
    public int zoomMin = 5;
    public int zoomMax = 20000;
    float xSpeed = 5.0F;
    float ySpeed = 5.0F;

    SateliteSelector sateliteSelector;

    private bool isActivated;
    float dosPi = Mathf.PI * 2;
    Transform target;

    // Use this for initialization
    void Start()
    {
        var model = GameObject.Find("Model");
        Debug.Assert(model != null, "No se ha encontrado el modelo.");

        var gameManager = (GameManager)model.GetComponent(typeof(GameManager));

        GestionarZoom();

        sateliteSelector = (SateliteSelector)model.GetComponent(typeof(SateliteSelector));
    }

    void LateUpdate()
    {
        target = sateliteSelector.satelite.gameObject.transform;

        CheckActivation();

        if (isActivated)
        {
            GestionarXRotation();
            GestionarYRotation();
        }

        GestionarZoom();

        Camera.main.farClipPlane = Vector3.Distance(transform.position, Vector3.zero) + 50;
    }

    void GestionarZoom()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            var movimiento = Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
            var nuevaDistancia = ZoomLimit(transform.localPosition.magnitude - movimiento);

            Vector3 offset = transform.localPosition;
            offset.Normalize();
            transform.localPosition = offset * nuevaDistancia;
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

    void GestionarXRotation()
    {
        var axis = Input.GetAxis("Mouse X");
        var angulo = axis * xSpeed;

        transform.RotateAround(target.position, transform.up, angulo);
    }

    void GestionarYRotation()
    {
        var axis = Input.GetAxis("Mouse Y");
        var angulo = axis * ySpeed;

        transform.RotateAround(target.position, transform.right, angulo);
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
