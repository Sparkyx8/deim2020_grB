using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GeneralCode : MonoBehaviour
{
    //Variables para la velocidad de los obstáculos.
    [HideInInspector] public float spaceSpeed;
    //Variable para la distancia.
    [HideInInspector] public float distance;
    //Variable para saber si está vivo.
    [HideInInspector] public bool isAlive = true;
    //Variables de la UI.
    [SerializeField] Text distanceText;
    [SerializeField] Text speedText;
    [SerializeField] Text distanceText2;
    [SerializeField] Text ammoText;
    [SerializeField] Text pauseText;
    [SerializeField] GameObject menuImage;
    [SerializeField] GameObject titleImage;
    //Accedemos al audio source
    private AudioSource audioSource;
    //Booleana para saber si ha elegido skin
    [HideInInspector] public static bool hasSkin = false;
    //Variable para la cantidad de munición
    [HideInInspector] public int ammunition = 0;
    //Variable para saber si consiguio la munición
    [HideInInspector] public bool gotAmmo;
    //Variable para saber si está pausado el juego
    [HideInInspector] public bool paused;
    void Start()
    {
        //Accedemos a la UI
        Text distanceText = GetComponent<Text>();
        Text speedText = GetComponent<Text>();
        Text distanceText2 = GetComponent<Text>();
        Text ammoText = GetComponent<Text>();
        Text pauseText = GetComponent<Text>();
        GameObject menuImage = GetComponent<GameObject>();
        GameObject titleImage = GetComponent<GameObject>();
        //Accedemos al Audio Source y empezamos la música
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }
    void Update()
    {
        //Cambiamos los textos con la información nueva
        int myDistance = (int)distance;
        distanceText.text = "Distance: " + myDistance;
        distanceText2.text = "Distance: " + myDistance;
        float spaceShipSpeed = spaceSpeed * 10;
        speedText.text = "Speed: " + spaceShipSpeed.ToString("F2");
        ammoText.text = "X" + ammunition;
        //Si está muerto empieza la corrutina game over
        if (!isAlive)
        {
            StartCoroutine("GameOver");
        }
        //Desactivamos el menú si está vivo
        else if (isAlive)
        {
            menuImage.SetActive(false);
        }
        //Para que no se coja la munición más de una vez
        if (gotAmmo == true)
        {
            gotAmmo = false;
            ammunition += 2;
        }
        //Pausar el juego
        if (Input.GetButtonDown("Pause") && !paused)
        {
            pauseText.enabled = true;
            paused = true;
            Time.timeScale = 0;
            audioSource.Pause();
        }
        else if (Input.GetButtonDown("Pause") && paused == true)
        {
            pauseText.enabled = false;
            paused = false;
            Time.timeScale = 1;
            audioSource.Play();
        }
        if (!paused)
        {
            pauseText.enabled = false;
        }
    }
    //Corrutina para el game over
    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(3f);
        distanceText.enabled = false;
        speedText.enabled = false;
        titleImage.SetActive(false);
        menuImage.SetActive(true);
    }
    //Métodos para los botones
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("InitGame", LoadSceneMode.Single);
        hasSkin = true;
    }
    public void Exit()
    {
        Application.Quit();
    }
}
