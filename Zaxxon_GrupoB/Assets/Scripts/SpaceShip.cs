using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class SpaceShip : MonoBehaviour
{
    //Variables para incluir el texto en el UI.
    [SerializeField] Text distanceText;
    [SerializeField] Text speedText;
    //Variable para la velocidad horizontal de la nave.
    float speed = 10f;
    //Variable para la distancia.
    public static float distance;
    //Variables booleanas para el movimiento de la nave.
    public bool inMarginMoveX = true;
    public bool inMarginMoveY = true;
    //Variables para acceder a otros componentes de los obstáculos.
    public GameObject movimientoObstaculo;
    private ObstacleMove obstacleMove;
    //Variable para hacer desaparecer la nave cuando choca.
    [SerializeField] MeshRenderer myMeshRender;
    // Start is called before the first frame update
    void Start()
    {
        //Accedemos a los componentes, variables, métodos, etc de los obstáculos.
        movimientoObstaculo = GameObject.Find("Obstacle");
        //obstacleMove = movimientoObstaculo.GetComponent<ObstacleMove>();
        //Accedemos a los textos.
        Text distanceText = GetComponent<Text>();
        Text speedText = GetComponent<Text>();
        //StartCoroutine("Distancia");
    }

    // Update is called once per frame
    void Update()
    {
        //Método para el movimiento de la nave.
        moveSpaceship();
        //Distancia y velocidad.
        //Pendiente de cambiar para que los obstáculos cojan la variable de la nada y no al revés.
        distance = distance + 1 * Time.deltaTime * ObstacleMove.spaceSpeed;
        ObstacleMove.spaceSpeed = ObstacleMove.isAlive * 10f + 1 * distance / 50;
        if (ObstacleMove.spaceSpeed >= 50)
        {
            ObstacleMove.spaceSpeed = 50;
        }
        speed = 10 * distance / 1000;
        if (speed < 10)
        {
            speed = 10;
        }
        else if (speed > 30)
        {
            speed = 30;
        }
        int myDistance = (int)distance;
        distanceText.text = "Distance: " + myDistance;
        float spaceShipSpeed = ObstacleMove.spaceSpeed * 4;
        speedText.text = "Speed: " + spaceShipSpeed.ToString("F2");
        //print(ObstacleMove.spaceSpeed);
    }

    //Colisiones con los obstáculos.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            print("GAME OVER");
            myMeshRender.enabled = false;
            ObstacleMove.isAlive = 0;
        }
    }
    /*IEnumerator Distancia()
    {
        for (int n = 0; ; n++)
        {
            distanceText.text = "Distancia: " + n;
            yield return new WaitForSeconds(1f / ObstacleMove.spaceSpeed);
        }

    }*/
    //Método para mover la nave.
    public void moveSpaceship()
    {
        float myPosX = transform.position.x;
        float desplX = Input.GetAxis("Horizontal");
        //Makes the spaceship move vertically.
        /*transform.Translate(Vector3.forward * Time.deltaTime * speed * Input.GetAxis("Vertical"));
        if (transform.position.y >= 10)
        {
            transform.position = new Vector3(transform.position.x, 10, transform.position.z);
        }
        else if (transform.position.y <= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }*/

        //Makes the spaceship move horizontally.
        /*transform.Translate(Vector3.right * Time.deltaTime * speed * desplX);
        if (myPosX >= 10)
        {
            transform.position = new Vector3(10, transform.position.y, transform.position.z);
        }
        else if (myPosX <= -10)
        {
            transform.position = new Vector3(-10, transform.position.y, transform.position.z);
        }*/

        //Makes the spaceship rotate.
        /*float rotation = 30f * Time.deltaTime * speed * desplX);
        transform.Rotate(0f, 0f, rotation);
        print(rotation);
        if (transform.rotation.z >= 30)
        {
            transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, 30f, transform.rotation.w);
        }
        else if (transform.rotation.z <= -30)
        {
            transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, -30f, transform.rotation.w);
        }*/
        if (inMarginMoveX == true)
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed * desplX);
        }
        if (myPosX < -10 && desplX < 0)
        {
            inMarginMoveX = false;
        }
        else if (myPosX > 10 && desplX > 0)
        {
            inMarginMoveX = false;
        }
        else if (myPosX < -10 && desplX > 0)
        {
            inMarginMoveX = true;
        } 
        else if (myPosX > 10 && desplX < 0)
        {
            inMarginMoveX = true;
        }
    }
}
