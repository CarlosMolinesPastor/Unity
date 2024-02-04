using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    SceneController sceneController;
    [SerializeField] AudioClip sonido;
    // Start is called before the first frame update
    void Start()
    {
        sceneController = FindObjectOfType<SceneController>();
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Si el objeto que colisiona es el player, se destruye el objeto
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            //Anotamos los puntos
            sceneController.AnotarItemRecogido();
            AudioSource.PlayClipAtPoint(sonido, transform.position);
        }
  
    }
}
