using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ObstacleCreator : MonoBehaviour
{
    [SerializeField] GameObject Columna;
    [SerializeField] Transform InitPos;
    private float randomNumber;
    private Vector3 RandomPos;
    private int a;
    private float limit = 1;
    public GameObject creacionObstaculo;
    // Start is called before the first frame update
    void Start()
    {
        creacionObstaculo = GameObject.Find("Obstacle");
        InvokeRepeating("Creation", 1f, 10^999);
        for(a=1; a<=27; a++)
        {
            randomNumber = Random.Range(-10.0f, 10.0f);
            RandomPos = new Vector3(randomNumber, 0, -a*10);
            Vector3 FinalPos = InitPos.position + RandomPos;
            Instantiate(Columna, FinalPos, Quaternion.identity);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        //Límite para que no aparezcan demasiadas columnas.
       if (SpaceShip.distance >= 2000)
        {
            limit = 0.8f;
        }
       else if (SpaceShip.distance >= 5000)
        {
            limit = 0.65f;
        }
    }
    // Crea obstáculos según la velocidad de la nave y la distancia recorrida.
    IEnumerator CrearObstaculo()
    {
        for (int n = 0; ; n++)
        {
            //Creamos y aplicamos un número aleatorio para que los obstáculos se generen en una posición aleatoria del eje X.
            randomNumber = Random.Range(-10.0f, 10.0f);
            RandomPos = new Vector3(randomNumber, 0, 0);
            Vector3 FinalPos = InitPos.position + RandomPos;
            //Instanciamos el prefab del obstáculo.
            Instantiate(Columna, FinalPos, Quaternion.identity);
            //Creamos la fórmula para que la corrutina varíe según la velocidad de la nave.
            float interval = 1f / (0.1f * ObstacleMove.spaceSpeed * limit);
            //print(interval);
            //Usamos el intervalo para definir el tiempo entre columnas y cuando parar la corrutina.
            yield return new WaitForSeconds(interval);
            if (interval > 10)
            {
                StopCoroutine("CrearObstaculo");
            }
        }

    }
    //Función para empezar la corrutina.
    void Creation()
    {
        StartCoroutine("CrearObstaculo");
    }

    // Creates obstacles depending on speed and time
    /*void CrearColumna()
    {
        randomNumber = Random.Range(-10.0f, 10.0f);
        RandomPos = new Vector3(randomNumber, 0, 0);
        Vector3 FinalPos = InitPos.position + RandomPos;
        Instantiate(Columna, FinalPos, Quaternion.identity);
    }*/

}
