using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class SpaceShip : MonoBehaviour
{
    //Variable para la velocidad horizontal de la nave.
    private float speed = 10f;
    //Variables booleanas para el movimiento de la nave.
    private bool inMarginMoveX = true;
    private bool inMarginMoveY = true;
    //Variable para acceder al código general
    private GeneralCode generalCode;
    //Variable para hacer desaparecer la nave cuando choca.
    private MeshRenderer meshRender;
    //Variables para instanciar la explosión.
    [SerializeField] GameObject explosion;
    [SerializeField] Transform explPos;
    [SerializeField] AudioClip explSound;
    //Variables para el misil
    [SerializeField] GameObject missile;
    [SerializeField] AudioClip missileSound;
    private Vector3 missilePos;
    private float pos;
    //Variable para el Audio Source
    private AudioSource audioSource;
    //Variable para cambiar la skin de la nave
    private MeshFilter meshFilter;
    //Variable para saber si se mueve la nave
    private bool isMovingRight = false;
    private bool isMovingLeft = false;
    private bool isMovingUp = false;
    private bool isMovingDown = false;
    //Variable para el Animator
    private Animator animator;
    private BoxCollider boxCollider;
    //Variable para saber si ha disparado ya
    private bool firstShot = false;
    //variable para asignar el lado desde el que dispara
    private float shootSide;
    //Variable para saber si está disparando
    private bool isShooting = false;
    //Variables para la explosión de la mina
    [SerializeField] GameObject explMina;
    [SerializeField] AudioClip minaSound;
    private Vector3 minaPos;
    void Start()
    {
        //Accedemos al Game Object general.
        generalCode = GameObject.Find("VarObject").GetComponent<GeneralCode>();
        //Accedemos al audio source
        audioSource = GetComponent<AudioSource>();
        //Accedemos al mesh render
        meshRender = GetComponent<MeshRenderer>();
        //Accedemos al mesh filter
        meshFilter = GetComponent<MeshFilter>();
        //Accedemos al animator
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        //Método para el movimiento de la nave.
        if (generalCode.isAlive == true)
        { 
        moveSpaceship();
        }
        //Distancia y velocidad.
        Distancia();
        //Comprobamos si ha pulsado, si tiene munición y si está vivo
        if (Input.GetButtonDown("Fire1") && generalCode.ammunition > 0 && generalCode.isAlive == true && !isShooting)
        {
            //Disparar
            Dispara();
        }
    }

    //Colisiones
    private void OnTriggerEnter(Collider other)
    {
        //Detectamos si ha colisionado con un obstáculo
        if (other.gameObject.tag == "Obstacle")
        {
            //Desactivamos el render para que la nave desaparezca
            meshRender.enabled = false;
            //Creamos la explosión con su sonido
            Instantiate(explosion, explPos);
            audioSource.PlayOneShot(explSound);
            //Cambiamos la variable de vivo a falso
            generalCode.isAlive = false;
            //Le impedimos moverse
            inMarginMoveX = false;
            inMarginMoveY = false;
            //Detectamos si el obstáculo contra el que ha chocado es una mina
            if (other.gameObject.GetComponent<MineMove>() != null)
            {
                //Creamos la posición de la mina e instanciamos la explosión y activamos el sonido
                minaPos = new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y, other.gameObject.transform.position.z);
                Instantiate(explMina, minaPos, Quaternion.identity);
                audioSource.PlayOneShot(minaSound);
                //Destruimos la mina
                Destroy(other.gameObject);
            }
        }
        //Detectamos si ha chocado con una caja de munición
        if (other.gameObject.GetComponent<AmmoBox>() != null)
        {
            //Destruimos la caja de munición
            Destroy(other.gameObject);
            //Aumentamos la munición disponible
            generalCode.gotAmmo = true;
        }
    }
    //Método para mover la nave.
    private void moveSpaceship()
    {
        //Variables para la posición de la nave
        float myPosX = transform.position.x;
        float myPosY = transform.position.y;
        //Variables para los input del jugador
        float desplX = Input.GetAxis("Horizontal");
        float desplY = Input.GetAxis("Vertical");
        //Le dejamos moverse si está en los márgenes
        if (inMarginMoveX == true)
        {
            //Hacemos que se mueva
            transform.Translate(Vector3.right * Time.deltaTime * speed * desplX, Space.World);
            //Detectamos la dirección
            if(desplX > 0)
            {
                //Cambiamos las booleanas de la dirección
                isMovingRight = true;
                isMovingLeft = false;
            }
            //Detectamos la dirección
            else if (desplX < 0)
            {
                //Cambiamos las booleanas de la dirección
                isMovingLeft = true;
                isMovingRight = false;
            }
        }
        //Creamos los márgenes y activamos o desactivamos la booleana en consecuencia
        if (myPosX < -9 && desplX < 0)
        {
            inMarginMoveX = false;
        }
        else if (myPosX > 9 && desplX > 0)
        {
            inMarginMoveX = false;
        }
        else if (myPosX < -9 && desplX > 0)
        {
            inMarginMoveX = true;
        } 
        else if (myPosX > 9 && desplX < 0)
        {
            inMarginMoveX = true;
        }
        if (inMarginMoveY == true)
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed * desplY, Space.World);
            //Detectamos la dirección
            if (desplY > 0)
            {
                //Cambiamos las booleanas de la dirección
                isMovingUp = true;
                isMovingDown = false;
            }
            //Detectamos la dirección
            else if (desplY < 0)
            {
                //Cambiamos las booleanas de la dirección
                isMovingDown = true;
                isMovingUp = false;
            }
        }
        //Creamos los márgenes y activamos o desactivamos la booleana en consecuencia
        if (myPosY < 1 && desplY < 0)
        {
            inMarginMoveY = false;
        }
        else if (myPosY > 20 && desplY > 0)
        {
            inMarginMoveY = false;
        }
        else if (myPosY < 1 && desplY > 0)
        {
            inMarginMoveY = true;
        }
        else if (myPosY > 20 && desplY < 0)
        {
            inMarginMoveY = true;
        }
        if (desplX == 0)
        {
            isMovingRight = false;
            isMovingLeft = false;
        }
        if (desplY == 0)
        {
            isMovingUp = false;
            isMovingDown = false;
        }
        //Cambiamos las animaciones según las booleanas
        animator.SetBool("MoveUp", isMovingUp);
        animator.SetBool("MoveDown", isMovingDown);
        animator.SetBool("MoveRight", isMovingRight);
        animator.SetBool("MoveLeft", isMovingLeft);
    }
    //Método para la distancia y la velocidad de la nave
    private void Distancia()
    {
        //Asignamos una fórmula para la distancia
        generalCode.distance += 1 * Time.deltaTime * generalCode.spaceSpeed;
        //Comprobamos si está vivo
        if (generalCode.isAlive == true)
        {
            //Asignamos una fórmula para la velocidad del mundo
            generalCode.spaceSpeed = 10f + 1 * generalCode.distance / 50;
        }
        else if (generalCode.isAlive == false)
        {
            //Cambiamos la velocidad a 0
            generalCode.spaceSpeed = 0f;
        }
        //Comprobamos si la velocidad está muy alta
        if (generalCode.spaceSpeed >= 50)
        {
            //Limitamos la velocidad a 50
            generalCode.spaceSpeed = 50;
        }
        //Asignamos una fórmula para la velocidad horizontal y vertical de la nave
        speed = 10 * generalCode.distance / 1000;
        //Le asignamos unos límites a la velocidad
        if (speed < 15)
        {
            speed = 15;
        }
        else if (speed > 30)
        {
            speed = 30;
        }
    }
    //Método para disparar el misil
    private void Dispara()
    {
        //Comprobamos si ha disparado alguna vez
        if (firstShot == false)
        {
            //Cambiamos la booleana
            firstShot = true;
            //Asignamos el lado del disparo
            shootSide = 1;
            pos = transform.position.x + shootSide;
            //Cambiamos el lado del disparo
            shootSide = -1;
            //Disparamos
            StartCoroutine(Missile());
        }
        else if (firstShot == true)
        {
            //Asignamos el lado del disparo
            pos = transform.position.x + shootSide;
            //Invertimos la variable para cambiar el lado del disparo
            shootSide = shootSide * -1;
            //Disparamos
            StartCoroutine(Missile());
        }
    }
    //Corrutina para el disparo
    IEnumerator Missile()
    {
        //Decimos que está disparando
        isShooting = true;
        //Asignamos la posición del misil
        missilePos = new Vector3(pos, transform.position.y, transform.position.z);
        //Creamos el misil con el audio
        Instantiate(missile, missilePos, Quaternion.identity);
        audioSource.PlayOneShot(missileSound);
        //Reducimos la munición disponible
        generalCode.ammunition -= 1;
        //Esperamos para que no pueda usar varios misiles de golpe
        yield return new WaitForSeconds(1f);
        //Decimos que no está disparando
        isShooting = false;
        //Paramos la corrutina para que no se repita 
        StopCoroutine(Missile());
    }
}
