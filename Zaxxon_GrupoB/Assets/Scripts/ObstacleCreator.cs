using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEditor;

public class ObstacleCreator : MonoBehaviour
{
    //Gameobjects para los obstáculos y la munición
    [SerializeField] GameObject Columna;
    [SerializeField] GameObject Mina;
    [SerializeField] GameObject Municion;
    //Posición del instanciador
    [SerializeField] Transform InitPos;
    //Numeros aleatorios para instanciar los obstáculos
    private float randomNumberHorizontal;
    private float randomNumberHorizontalMina;
    private float randomNumberVertical;
    //Posiciones de los prefabs
    private Vector3 RandomPosColumna;
    private Vector3 RandomPosMina;
    private Vector3 MunicionPos;
    //Variables para la creación
    private int a;
    private int height = 12;
    //Accedemos al código general
    private GeneralCode generalCode;
    //Creamos la fórmula para que la corrutina varíe según la velocidad de la nave.
    private float interval = 1.5f;
    //Variable para saber si ha pasado de nivel
    private bool level2 = false;
    //Booleana para crear la municion y distancia para que se cree
    private bool ammo = false;
    private int ammoDistance = 1000;
    void Start()
    {
        //Accedemos al código general
        generalCode = GameObject.Find("VarObject").GetComponent<GeneralCode>();
        //Empezamos la corrutina para crear obstáculos
        StartCoroutine("CrearObstaculo");
        //Bucle para los obstáculo iniciales
        for (a=1; a<=14; a++)
        {
            randomNumberHorizontal = Random.Range(-10.0f, 10.0f);
            RandomPosColumna = new Vector3(randomNumberHorizontal, height, -a*20);
            Vector3 FinalPosColumna = InitPos.position + RandomPosColumna;
            Instantiate(Columna, FinalPosColumna, Quaternion.identity);
        }
    }
    void Update()
    {
        //Límite para que no aparezcan demasiadas columnas.
        if (generalCode.distance < 2000 && generalCode.distance >= 100)
        {
            interval = 1.5f - (generalCode.distance / 5000);
        }
       else if (generalCode.distance >= 2000 && generalCode.distance < 3000)
        {
            interval = 2 - (generalCode.distance / 2500);
        }
        //Empezamos a crear más obstáculos para hacer el juego más difícil
       else if (generalCode.distance >= 3000 && !level2)
        {
            level2 = true;
            interval = 0.8f;
            StartCoroutine("CrearObstaculo");
        }
        //Reseteamos el intervalo
       if (!generalCode.isAlive)
        {
            interval = 1.5f;
        }
       //Comprobamos si hay que crear munición
       if (generalCode.distance >= ammoDistance)
        {
            ammo = true;
            ammoDistance += 1000;
        }
    }
    // Crea obstáculos según la velocidad de la nave y la distancia recorrida.
    IEnumerator CrearObstaculo()
    {
        for (int n = 0; ; n++)
        {
            //Creamos y aplicamos un número aleatorio para que los obstáculos se generen en una posición aleatoria del eje X.
            randomNumberHorizontal = Random.Range(-10.0f, 10.0f);
            RandomPosColumna = new Vector3(randomNumberHorizontal, height, 0);
            Vector3 FinalPosColumna = InitPos.position + RandomPosColumna;
            randomNumberHorizontalMina = Random.Range(-10.0f, 10.0f);
            if (randomNumberHorizontal == randomNumberHorizontalMina && randomNumberHorizontalMina >= 0)
            {
                randomNumberHorizontalMina += -3.0f;
            }
            if (randomNumberHorizontal == randomNumberHorizontalMina && randomNumberHorizontalMina < 0)
            {
                randomNumberHorizontalMina += 3.0f;
            }
            randomNumberVertical = Random.Range(2.0f, 17.0f);
            RandomPosMina = new Vector3(randomNumberHorizontalMina, randomNumberVertical, 0);
            Vector3 FinalPosMina = InitPos.position + RandomPosMina;
            MunicionPos = new Vector3(InitPos.position.x, InitPos.position.y + 5, InitPos.position.z);
            //Instanciamos el prefab de la columna.
            if (!ammo)
            {
                Instantiate(Columna, FinalPosColumna, Quaternion.identity);
                if (generalCode.distance > 1500 && generalCode.distance < 3000)
                {
                    Instantiate(Mina, FinalPosMina, Quaternion.identity);
                }
            }
            else if (ammo == true)
            {
                Instantiate(Municion, MunicionPos, Quaternion.identity);
                ammo = false;
            }
            //Usamos el intervalo para definir el tiempo entre columnas y cuando parar la corrutina.
            yield return new WaitForSeconds(interval);
            if (!generalCode.isAlive)
            {
                StopCoroutine("CrearObstaculo");
            }
        }

    }

}
