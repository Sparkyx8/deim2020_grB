using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMove : MonoBehaviour
{
    //Variable para que afecte al obstáculo.
    [SerializeField] GameObject Columna;
    //Variables para la velocidad de los obstáculos.
    public static float spaceSpeed;
    public static int isAlive = 1;
    // Start is called before the first frame update
    void Start()
    {
        //Variable para la velocidad.
        spaceSpeed = 10f;
        //Giramos el obstáculo para que forward sea hacia la nave y no al revés.
        transform.Rotate(0, 180, 0);
        //Empezamos la corrutina que destruirá los obstáculos cuando salgan del campo de visión.
        StartCoroutine("Limite");
    }

    // Update is called once per frame
    void Update()
    {
        //Movimiento de los obstáculos.
        transform.Translate(Vector3.forward * Time.deltaTime * spaceSpeed);
    }
    //Corrutina para destruir los obstáculos cuando salgan del campo de visión.
    IEnumerator Limite()
    {
        for (int n = 0; ; n++)
        {
            if(transform.position.z <= -8)
            {
                Destroy(Columna);
            }
            yield return new WaitForSeconds(2f);
        }

    }
}
