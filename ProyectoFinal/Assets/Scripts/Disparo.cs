using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// Karlinux -  Carlos Molines Pastor
public class Disparo : MonoBehaviour
{
    // Referencia a la cámara
    public Camera camara;
    // Referencia al sonido
    public AudioClip sonidoDisparo;
    // Referencia al prefab de la explosion de enemigos y nave
    public GameObject explosionPrefab;
    public GameObject explosionNave;
    // Referencia al prefab de la RAFAGA de disparo, es decir de la ilumnicion al disparar

    public GameObject rafagaDisparoPrefab;
    // Referencia a la punta del arma para disparar
    public Transform puntaArma;
    // Referencia al sonido del disparo
    AudioSource audioSourceDisparo;
    // Referencia al sonido de la explosion
    public AudioClip audioExplosion;

    // Referencia al script de la camara para instanciar el movimiento
    public CameraShake1 cameraShake;

    void Start()
    {
        audioSourceDisparo = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Si pulsamos el botón de disparo
        if (Input.GetButtonDown("Fire1"))
        {
            //Hacemos que el disparo tenga retroceso al mover la camara
            StartCoroutine(cameraShake.Shake(0.07f, 0.07f));
            // Reproduce el sonido de la explosión
            audioSourceDisparo.PlayOneShot(sonidoDisparo);
            Debug.Log("Disparando...");
            // Crea una ráfaga de disparo en la punta del arma
            GameObject rafagaDisparoInstance = Instantiate(rafagaDisparoPrefab, puntaArma.position, puntaArma.rotation);
            // Destruye la RAFAGA pasada 0.1 segundos
            Destroy(rafagaDisparoInstance, 0.02f);
            // Crea un rayo en la punta del arma
            float distanciaMaxima = 30;
            // Referencia del rayo
            RaycastHit hit;
            // Booleano que indica si el rayo impacta
            bool impactado = Physics.Raycast(camara.transform.position,
            camara.transform.forward, out hit, distanciaMaxima);
            // Si el rayo impacta
            if (impactado)
            {
                Debug.Log("Disparo impactado");
                // Si el rayo impacta con un enemigo
                if (hit.collider.CompareTag("enemigo"))
                {
                   
                    Debug.Log("Enemigo acertado");
                    // Crea una explosión en la posición del enemigo
                    GameObject explosionInstance = Instantiate(explosionPrefab, hit.point, Quaternion.identity);
                    //Eliminar instancia de la explosión pasados 1 segundos
                    Destroy(explosionInstance, 1f);
                    //Referencia al enemigo
                    Enemigo enemigo = hit.collider.GetComponent<Enemigo>();
                    //Le dice  que sigue al player
                    enemigo.SetDestinationToPlayer();
                    // Reduce la vida del enemigo si es el nivel 2 nos cuesta mas matarlo ademas de llevar mas velocidad
                    // si no cuesta menos
                    if (SceneManager.GetActiveScene().name == "Nivel2")
                    {
                        enemigo.RecibirDaño(15);
                    }
                    else
                    {
                        enemigo.RecibirDaño(25);
                    }
                    // Indica que ha sido golpeado
                    enemigo.Hit();
                    //Si el enemigo se ha destruido
                    if (enemigo.life <= 0)
                    {
                        //Hacemos que la camara se mueva por la explosion del enemigo
                        StartCoroutine(cameraShake.Shake(0.4f, 0.4f));
                        //Reproducimos la explosion
                        audioSourceDisparo.PlayOneShot(audioExplosion);
                    }
                }
                // Si el rayo impacta con la nave y hemos recogido todas las baterias
                if (hit.collider.CompareTag("nave") && FindObjectOfType<SceneController>().itemsRestantes == 0)
                {
                    // Destruye la nave
                    Destroy(hit.collider.gameObject);
                    Debug.Log("Nave impactada");
                    //Hacemos que la camara se mueva por la explosion
                    StartCoroutine(cameraShake.Shake(2f, 2f));
                    // Crea una explosión en la posición de la nave
                    GameObject explosionInstance = GameObject.Instantiate(explosionNave, hit.point, Quaternion.identity);
                    //Reproducimos la explosion
                    audioSourceDisparo.PlayOneShot(audioExplosion);
                    //Eliminar instancia de la explosión pasados 1 segundos
                    Destroy(explosionInstance, 1f);
                    // Instanciamos LA corroutina de la escena que hemos ganado
                    GameObject.Find("SceneController").GetComponent<SceneController>().StartCoroutine("Win");
                }
            }
        }



    }
}
