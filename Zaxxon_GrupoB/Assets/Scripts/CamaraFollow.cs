using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraFollow : MonoBehaviour
{
    //Variables para que la cámara siga a la nave.
    private Transform playerPosition;
    private float smoothTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    //Accedemos al código general
    private GeneralCode generalCode;
    void Start()
    {
        //Accedemos al código general
        generalCode = GameObject.Find("VarObject").GetComponent<GeneralCode>();
        //Accedemos al instanciador de la nave que es lo que vamos a seguir
        playerPosition = GameObject.Find("SpaceShipCreator").GetComponent<Transform>();
    }
    void Update()
    {
        //La cámara sigue a la nave de manera suave.
        transform.LookAt(playerPosition);
        Vector3 targetPosition = new Vector3(playerPosition.position.x, playerPosition.position.y + 2.2f, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        if (generalCode.distance >= 1000 && generalCode.distance < 1500)
        {
            smoothTime = 0.10f;
        }
        else if (generalCode.distance >= 1500)
        {
            smoothTime = 0.05f;
        }
    }
}
