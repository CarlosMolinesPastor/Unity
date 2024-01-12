using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Carlos Molines Pastor: karlinux

public class JEfe : MonoBehaviour
{
    //Verlocidad en X
    [SerializeField] float velocidadX = -2f;
    //Velocidad en Y
    [SerializeField] float velocidadY = 2f;
    //Prefab del disparo Jefe
    [SerializeField] private GameObject prefabDisparoJefe;
    //Prefab de la explosion del jefe
    [SerializeField] private GameObject prefabExplosionJEfe;
    //Texto de GAnar
    [SerializeField] Text textYouWin;
    //Texto de Vidas Jefe
    [SerializeField] Text textJefeLifes;
    //Factor de incremento de velocidad
    [SerializeField] float factorIncremento = 1.15f;
    void Start()
    {
        //Empezamos una coroutine para controlar el disparo enemigo
        StartCoroutine("DisparoEnemigo");
    }
    void Update()
    {
        //Log de la posicion
        Debug.Log("Posicion X => " + transform.position.x + "Posicion Y => " + transform.position.y);
        //Movemos el enemigo
        transform.Translate(velocidadX * Time.deltaTime, velocidadY * Time.deltaTime, 0);
        //Si el enemigo se sale de la pantalla
        if ((transform.position.x < -8.5) || (transform.position.x > 8.5))
        {
            //Si la velocidad es mayor que 4 o menor que -4
            if (velocidadX > 4f || velocidadX < -4f)
            {
                //Invertimos la velocidad
                velocidadX = -velocidadX;
            }
            //Si no
            else
            {
                //Invertimos la velocidad y la multiplicamos por el factor de incremento
                velocidadX = -velocidadX * factorIncremento;
            }
        }
        //Si el enemigo se sale de la pantalla
        if (transform.position.y < -5f || transform.position.y > 5f)
        {
            if (velocidadY > 4f || velocidadY < -4f)
            {
                velocidadY = -velocidadY;
            }
            else
            {
                velocidadY = -velocidadY * factorIncremento;
            }
        }
        //Actualizamos el texto de las vidas del jefe
        textJefeLifes.text = "Vidas Jefe: " + GameManager.instance.jefeLifes;
    }
    //Colisiones
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Si colisiona con un disparo de la nave
        if (other.tag == "DisparoNAve")
        {
            //Restamos una vida controlado por el GameManager
            GameManager.instance.jefeLifes--;
            //Si la vida es 0
            if (GameManager.instance.jefeLifes == 0)
            {
                //Paramos la coroutine del disparo
                StopCoroutine("DisparoEnemigo");
                //Instanciamos la explosion del jefe
                Transform explosionJefe = Instantiate(prefabExplosionJEfe, transform.position, Quaternion.identity).transform;
                //Destruimos la explosion pasado un segundo
                Destroy(explosionJefe.gameObject, 1f);
                //Analizamos la puntuacion de 1000 puntos
                ScoreManager.instance.AddScore(1000);
                //Coroutina para destruir al jefe y volver al menu
                StartCoroutine(MainMenu());
            }
        }
    }
    //Coroutine para el disparo del enemigo
    IEnumerator DisparoEnemigo()
    {
        //Bucle infinito
        while (true)
        {
            //Pausa aleatoria entre 1 y 2.5 segundos
            float pausa = Random.Range(1f, 2.5f);
            //Esperamos la pausa
            yield return new WaitForSeconds(pausa);
            //Instanciamos el disparo enemigo
            Instantiate(prefabDisparoJefe, transform.position, Quaternion.identity);
        }
    }
    //Coroutine para volver al menu
    IEnumerator MainMenu()
    {
        //Hacemos que el jefe no sea visible y no tenga colisiones hasta que lo eliminemos pues si lo eliminamos
        //antes de que termine la explosion no se ve y no va a la escena de menu
        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        //Instanciamos la funcion EndGame para que gameInProgress sea false
        GameManager.instance.EndGame();
        //Texto de You Win
        textYouWin.text = "You Win \n" + "Puntuacion: " + ScoreManager.instance.score;
        //Esperamos 6 segundos
        yield return new WaitForSeconds(6f);
        //Destruimos el jefe
        Destroy(gameObject);
        //Cargamos la escena de menu
        SceneManager.LoadScene("Menu");
    }
}
