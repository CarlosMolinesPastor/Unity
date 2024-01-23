using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//Karlinux -  Carlos Molines Pastor
public class Texto : MonoBehaviour
{
    //######## Aqui en el texto he implementado la funcionalidad de que cuando ganas o pierdes
    //######## se muestre un texto en pantalla y se reinicie el juego.
    //######## Quizas hubiera estado mejor hacerlo en la clase GameManager pero no me ha dado tiempo


    //Creamos un gameobject para el jugador
    [SerializeField] GameObject player;
    //Creamos un texto para el texto de ganar
    public Text winText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Si el contador de pickups es 12 ganamos
       if (GameManager.instance.pickupsCount == 12){
            winText.text = "Pasas de nivel";
            if (SceneManager.GetActiveScene().name == "Final")
            {
                winText.text = "Has Ganado";
            }
           Debug.Log("Ganar");
           //Llamamos a la corrutina
           StartCoroutine("Sigue");
       } 
       //Si las vidas son 0 perdemos
       if (GameManager.instance.playerLives == 0){
            winText.text = "Has perdido";
           Debug.Log("Perder");
           //Llamamos a la corrutina
              StartCoroutine("Perder");
       }
    }
    //Corrutina para seguir
    IEnumerator Sigue(){
        //Esperamos 4 segundos
        yield return new WaitForSeconds(4);
        //Reiniciamos el juego
        GameManager.instance.pickupsCount = 0;
        GameManager.instance.gameInProgress = true;
        winText.text = "";
        //Si la escena es la final cargamos la escena del menu
        if (SceneManager.GetActiveScene().name == "Final")
        {
            SceneManager.LoadScene("Menu");
        }
        //Si no cargamos la escena final
        else {
            SceneManager.LoadScene("Final");
        }
    }
    //Corrutina para perder
    IEnumerator Perder(){
        //Esperamos 4 segundos
        yield return new WaitForSeconds(4);
        // Reiniciamos el juego
        SceneManager.LoadScene("Menu");
    }
}
