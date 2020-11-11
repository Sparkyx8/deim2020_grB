using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraFollow : MonoBehaviour
{
    //Variables para que la cámara siga a la nave.
    [SerializeField] Transform playerPosition;
    [SerializeField] float smoothTime;
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
    }
}
