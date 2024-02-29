using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Karlinux -  Carlos Molines
public class VidaPlayer : MonoBehaviour
{

    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        //Buscamos el gameManager
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }



    private void OnTriggerStay(Collider other)
    {
        //Si el objeto colisiona con el enemigo restamos vida
        if (other.gameObject.CompareTag("enemigo"))
        {
            gameManager.restarVida();

        }
    }
}
