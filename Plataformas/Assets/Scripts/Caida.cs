using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Karlinux -  Carlos Molines Pastor
public class Caida : MonoBehaviour
{
   
//Detectamos la colision del player con el Collider del fondo
private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player"){
            //Llmamos al metodo PerderVida del GameManager
            GameManager.instance.PerderVida();
            //Llamamos al metodo Recolocar del Player
            FindObjectOfType<Player>().SendMessage("Recolocar");
        }
    }
}
