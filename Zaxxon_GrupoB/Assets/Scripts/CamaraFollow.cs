using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraFollow : MonoBehaviour
{
    //Variables para que la cámara siga a la nave.
    [SerializeField] Transform playerPosition;
    private float smoothTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //La cámara sigue a la nave de manera suave(Requiere mejora).
        transform.LookAt(playerPosition);
        Vector3 targetPosition = new Vector3(playerPosition.position.x, 2.2f, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        /*if (SpaceShip.distance >= 1000 && SpaceShip.distance < 1500)
        {
            smoothTime = 0.10f;
        }
        else if (SpaceShip.distance >= 1500 && SpaceShip.distance < 2000)
        {
            smoothTime = 0.05f;
        }
        else if (SpaceShip.distance >= 2000)
        {
            smoothTime = 0.01f;
        }*/
        smoothTime = 75 / SpaceShip.distance;
        if (smoothTime >= 0.15f)
        {
            smoothTime = 0.15f;
        }
        print(smoothTime);
    }
}
