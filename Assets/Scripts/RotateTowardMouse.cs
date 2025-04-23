using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardMouse : MonoBehaviour
{
    Camera cam;
    [Range(0.1f, 5), Tooltip("Depth at which the position of the mouse is projected at")] public float depth = 1;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //R�cup�re la position de la souris
        Vector3 screenPos = Input.mousePosition;
        //La profondeur du point par rapport � la camera est param�tr� en Z
        screenPos.z = depth;
        //Calcul le point de la souris � la profondeur donn�e par rapport � la cam�ra.
        Vector3 targetPosition = cam.ScreenToWorldPoint(screenPos);

        //Regarde dans la direction du point calcul�.
        transform.LookAt(targetPosition);
    }
}
