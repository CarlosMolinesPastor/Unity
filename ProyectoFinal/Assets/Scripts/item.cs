using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Karlinux -  Carlos Molines Pastor
public class Item : MonoBehaviour
{
    //Variables de Controlador de escena y sonido para recoger el item
    //en este casio las baterias
    SceneController sceneController;
    [SerializeField] AudioClip sonido;
    // Start is called before the first frame update
    void Start()
    {
        //Recogemos el controlador de escena buscandolo
        sceneController = FindObjectOfType<SceneController>();
    }

    // Funcion collision con el objeto y el player
    private void OnTriggerEnter(Collider collision)
    {
        //Si el objeto que colisiona es el player, se destruye el objeto
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            //Anotamos los puntos
            sceneController.AnotarItemRecogido();
            //Reproduce el sonido
            AudioSource.PlayClipAtPoint(sonido, transform.position);
        }

    }
}
