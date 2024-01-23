using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class Nave : MonoBehaviour
{
    //Prefab del disparo de la nave (se asigna desde el inspector)
    [SerializeField] Transform prefabDisparo;
    [SerializeField] Transform prefabExplosion;
    // Velocidad de la nave
    [SerializeField] float velocidad = 2;
 
    [SerializeField] int vidas = 2;
    // Array de vidas de la nave
    [SerializeField] private GameObject[] vidasNave;

    //Declaramos un array de audiosources
    private AudioSource[] audios;
   
    //Añadimos el texto 
    public UnityEngine.UI.Text textoSaludo;
    // Start is called before the first frame update
    void Start()
    {
        audios = GetComponents<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        //Movimiento
        float horizontal = Input.GetAxis("Horizontal");
        //Velocidad de movimiento
        transform.Translate(horizontal * velocidad * Time.deltaTime, 0, 0);
        //Limites de la pantalla
        transform.position = new Vector3(
        Mathf.Clamp(transform.position.x, -4.5f, 4.5f),
        transform.position.y,
        transform.position.z
        );
        //Disparo
        // Si pulsan el boton de disparo y la nave esta visible se dispara
        if ((Input.GetButtonDown("Fire1")) && (GetComponent<Renderer>().enabled == true))
        {
            //Instancia el disparo y reproduce el sonido
            Instantiate(prefabDisparo, transform.position, Quaternion.identity);
            //Reproduce el sonido de diosparo dentro del array de sonidos es el primero
            audios[0].Play();
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "DisparoEnemigo")
        {
            if (vidas > 1)
            {
                vidas--;
                // Destruimos el disparo enemigo para que no se vea que atraviesa
                Destroy(collision.gameObject);
                // Destruimos la vida
                Destroy(vidasNave[vidas].gameObject);
            }
            else
            {
            audios[1].Play();
            //Destruimos la ultima vida
            Destroy(vidasNave[vidas].gameObject);
            // Instanciamos la explosión. La posición es la de la nave
            Transform explosion = Instantiate(prefabExplosion, collision.transform.position, Quaternion.identity);
            // Destruimos el disparo enemigo
            Destroy(collision.gameObject);
            // Destruimos la explosión pasados 1 segundo
            Destroy(explosion.gameObject, 1f);
            Destroy(GetComponent<BoxCollider2D>());
            GetComponent<Renderer>().enabled = false;
            textoSaludo.text = "GAME OVER";
            StartCoroutine(CambiarEscena());
            }
        }
        if (collision.tag == "Enemigo")
        {
            audios[1].Play();
            Transform explosion = Instantiate(prefabExplosion, collision.transform.position, Quaternion.identity);
            // Destruimos el disparo enemigo
            Destroy(collision.gameObject);
            // Destruimos la explosión pasados 1 segundo
            Destroy(explosion.gameObject, 1f);
            Destroy(GetComponent<BoxCollider2D>());
            GetComponent<Renderer>().enabled = false;
            textoSaludo.text = "GAME OVER";
            StartCoroutine(CambiarEscena());
        }
        
    }
    IEnumerator CambiarEscena()
    {
    
    // Esperamos 5 segundos
    yield return new WaitForSeconds(5f);
    // Cargamos la escena del menú
    SceneManager.LoadSceneAsync("Menu");
    // Destruyo aqui la nave para que no haya problemas en iniciar la corrutina porque si no al destruir el objeto
    // la corrutina se paraba antes de que el objeto se destruyese
    Destroy(gameObject);
    }

}
