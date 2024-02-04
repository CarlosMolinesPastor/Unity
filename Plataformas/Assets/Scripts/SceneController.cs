using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
// Karlinux -  Carlos Molines Pastor
public class SceneController : MonoBehaviour
{

    //Variables del SceneController, textos , la llave y el audioclip de muerte
    private int itemsRestantes;
    public Text vidasText;
    public Text puntosText;
    public Text nivelText;
    public Text textoGameOver;
    public GameObject llave;
    [SerializeField] private AudioClip sonidoMuerte;

    // Start is called before the first frame update
    void Start()
    {
        //Cargamos la escena, contando los objetos items que recoger. Escondemos la llave
        // y lanzamos la courutina de mensaje de inicio que lo controlo todo en el mensaje gameOver
        itemsRestantes = FindObjectsOfType<Item>().Length;
        llave.SetActive(false);
        StartCoroutine(Mensaje());
        //Dejamos claro el nivel actual
        if (SceneManager.GetActiveScene().name == "Nivel1")
        {
            GameManager.instance.nivelActual = 1;
        }
        if (SceneManager.GetActiveScene().name == "Nivel2")
        {
            GameManager.instance.nivelActual = 2;
        }

    }
    //Funcion de anotar los items recogidos y mostrarlos por pantalla
    //Ademas controlamos que si el nivel es el uno y no quedan items
    //Lanzamos la courutina de mensaje de inicio que idica que busquemos la 
    // llave
    public void AnotarItemRecogido()
    {
        GameManager.instance.puntos += 100;
        Debug.Log("Puntos: " + GameManager.instance.puntos);
        itemsRestantes--;
        Debug.Log("Items: " + itemsRestantes);
        if (itemsRestantes <= 0)
        {
            if (GameManager.instance.nivelActual == 1)
            {
                llave.SetActive(true);
                StartCoroutine(Mensaje());
            }
            else{
                ControlPartida();
            }
        }
    }

    //Funcion de control de la partida con los textos finales de gameOver, es decir si
    //pasamos de nivel, ganamos o si perdemos la partida
    //Exceptuando si pasamos de nivel llamamos a la courrutina de volver al menu
    public void ControlPartida()
    {
        //Vemos que estemos jugando
        if (GameManager.instance.gameInProgress == true){
            if (GameManager.instance.nivelActual == 1){
                GameManager.instance.nivelActual++;
                textoGameOver.enabled = true;
                textoGameOver.text = "HAS PASADO AL NIVEL " + GameManager.instance.nivelActual;
                SceneManager.LoadScene("Nivel" + GameManager.instance.nivelActual);
            }else{
                textoGameOver.enabled = true;
                textoGameOver.text = "HAS GANADO \n PUNTUACIÓN: " + GameManager.instance.puntos + "\n VIDAS RESTANTES: " + GameManager.instance.vidas;
                StartCoroutine(VolverAlMenuPrincipal());

            }
        }else{
            textoGameOver.enabled = true;
            textoGameOver.text = "HAS PERDIDO";
            StartCoroutine(VolverAlMenuPrincipal());
        }
    }
    //Corrutina para volver al menu
    private IEnumerator VolverAlMenuPrincipal()
    {
        //Reduce el tiempo real a 1/10
        Time.timeScale = 0.1f;
        // Tres segundos de tiempo real
        yield return new WaitForSeconds(0.3f);
        // Volvemos a poner el tiempo normal
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    //Funcion de actualizar los textos de la partida
    
    public void Update()
    {
        vidasText.text = "Vidas: " + GameManager.instance.vidas;
        puntosText.text = "Puntos: " + GameManager.instance.puntos;
        nivelText.text = "Nivel: " + GameManager.instance.nivelActual;
        //Si no estamos jugando llamamos al metodo ControlPartida
        if (GameManager.instance.gameInProgress == false)
        {
            ControlPartida();
        }
    }

    //Corrutina del mensaje dentro del juego pero que no es fin
    private IEnumerator Mensaje()
    {
        if (itemsRestantes <= 0)
        {
            //Mensaje para buscar la llave
            textoGameOver.enabled = true;
            textoGameOver.text = "BUSCA LA LLAVE";
            yield return new WaitForSeconds(0.5f);
            textoGameOver.enabled = false;
        }else{
            //Mensaje de incio para que vayamos a jugar
            yield return new WaitForSeconds(0.5f);
            textoGameOver.enabled = true;
            textoGameOver.text = "GO >>>";
            yield return new WaitForSeconds(0.5f);
            textoGameOver.enabled = false;
        }
        
    }

}