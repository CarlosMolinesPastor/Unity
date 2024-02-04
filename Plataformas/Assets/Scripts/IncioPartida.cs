using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//Karlinux -  Carlos Molines Pastor
public class IncioPartida : MonoBehaviour
{
    //Se llama al pulsar el boton de jugar
    public void LanzarPartida() {
    //Cargamos la escena 1
     SceneManager.LoadScene("Nivel1");
     //Iniciamos el juego
     GameManager.instance.Start();
     Debug.Log("Inicio de partida");   
    }
}
