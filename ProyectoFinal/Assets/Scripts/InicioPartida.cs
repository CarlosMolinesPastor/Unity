using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//Karlinux -  Carlos Molines Pastor
public class IncioPartida : MonoBehaviour
{
    //Se llama al pulsar el boton de jugar
    public void LanzarPartida()
    {
        //Cargamos la escena de texto de inicio 
        SceneManager.LoadScene("TextoInicio");
        //Iniciamos el juego instanciando el controlador de escena por si volvemos al menu
        //despues de haber perdido
        GameManager.instance.Start();
        Debug.Log("Inicio de partida");
    }
}
