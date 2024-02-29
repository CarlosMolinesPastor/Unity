
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
// Karlinux -  Carlos Molines
public class SceneController : MonoBehaviour
{

    //Variable gameManager
    GameObject gameManager;
    // Variable enemigo
    GameObject enemigo;
    //Variable al player
    GameObject player;
    //Baterias restantes
    public int itemsRestantes;
    //Textos
    public TextMeshProUGUI textoPuntos;
    public TextMeshProUGUI textoItemsRecogidos;
    public TextMeshProUGUI textoFinal;
    //Barra de vida
    public Image barraVida;


    // Start is called before the first frame update
    void Start()
    {
        //Instanciamos al player lo buscamos por tag
        player = GameObject.FindWithTag("Player");
        //Instanciamos al enemigo lo buscamos por tag
        enemigo = GameObject.FindWithTag("enemigo");
        //Vemos los items que tenemos 
        itemsRestantes = FindObjectsOfType<Item>().Length;
    }
    void Update()
    {
        //Ponemos los puntos
        textoPuntos.text = "Puntos: " + GameManager.instance.puntos.ToString();
        //Ponemos las baterias
        textoItemsRecogidos.text = "Baterias: " + itemsRestantes.ToString();
        //Ponemos la vida en la barra
        barraVida.fillAmount = GameManager.instance.vidaPlayer / 100f;
        // Indicamos que si la vida es menor o igual a 0 que acabe el juego
        if (GameManager.instance.vidaPlayer <= 0)
        {
            //Acaba el juego
            GameManager.instance.EndGame();
            //Reiniciamos el juego
            StartCoroutine(Reiniciar());

        }

    }
    //Funcion para anotar los puntos
    public void AnotarItemRecogido()
    {
        //Instanciamos al gameManager para anotar los puntos
        GameManager.instance.puntos += 100;
        Debug.Log("Puntos: " + GameManager.instance.puntos);
        //Restamos el item recogido
        itemsRestantes--;
        Debug.Log("Items: " + itemsRestantes);
        //Comprobamos si hemos recogido todos los items
        //Depende de la escena que estemos iniciamos un nuevo nivel
        //o si ya hemos recogido todos los items, le indicamos que destruya la nave
        if (itemsRestantes == 0)
        {
            if (SceneManager.GetActiveScene().name == "Nivel1")
            {
                StartCoroutine(Nivel2());
            }
            else if (SceneManager.GetActiveScene().name == "Nivel2")
            {
                StartCoroutine(EliminarNAve());
            }
        }
    }
    //Corutina para reiniciar el juego
    IEnumerator Reiniciar()
    {
        //Indicamos que el juego ha terminado
        // sacamos el texto que hemos perdido esperamos 3 segundos y volvemos al menu
        textoFinal.text = "Has perdido";
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Menu");

    }
    //Corutina para el segundo nivel desde el primero
    IEnumerator Nivel2()
    {
        //Indicamos que el nivel ha terminado y pasamos al segundo
        textoFinal.text = "Vas al segundo nivel, 'Las Islas'";
        //Esperamos 3 segundos
        yield return new WaitForSeconds(3f);
        //Cargamos la escena Nivel2
        SceneManager.LoadScene("Nivel2");
    }
    //Corutina para eliminar la nave
    IEnumerator EliminarNAve()
    {
        //Indicamos que dispare a nave
        textoFinal.text = "Dispara a la nave nodriza para acabar con el apocalipsis zombie";
        //Esperamos 2 segundos
        yield return new WaitForSeconds(2f);
        //Pasamos el texro a vacio
        textoFinal.text = "";
    }
    //Corutina para ganar que la llamamos desde el Disparo.cs 
    public IEnumerator Win()
    {
        //Indicamos que el juego ha terminado
        textoFinal.text = "Has ganado. Has conseguido salvar a la tierra del apocalipsis zombie";
        //Esperamos 4 segundos
        yield return new WaitForSeconds(4f);
        //Cargamos la escena Menu
        SceneManager.LoadScene("Menu");
    }


}
