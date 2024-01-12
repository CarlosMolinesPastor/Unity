using System.Collections;
//using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

//Carlos Molines Pastor: karlinux

public class Enemigo : MonoBehaviour
{
    //Velocidad en el eje X
    [SerializeField] float velocidadX = -2f;
    //Posicion en el eje Y
    private float posicionY = 0.5f;
    //Prefab del disparo enemigo
    [SerializeField] private GameObject prefabDisparoEnemigo;

    void Start()
    {
        //Empezamos una coroutine para controlar el disparo enemigo
        StartCoroutine("DisparoEnemigo");
    }
    void Update()
    {
        //Log para ver la posicion del enemigo
        Debug.Log("Posicion X => " + transform.position.x + "Posicion Y => " + transform.position.y);
        // Nos movemos en el eje X
        transform.Translate(velocidadX * Time.deltaTime, 0, 0);
        //Si el enemigo se sale de la pantalla en X
        if ((transform.position.x < -8.5) || (transform.position.x > 8.5))
        {
            //Cambiamos la posicion en el eje Y es decir lo movemos hacia arriba o abajo indistintamente 
            //cada vez que llegue al final
            transform.position = new Vector3(transform.position.x, transform.position.y - posicionY, 0);
            //Cambiamos de direccion   
            velocidadX = -velocidadX;
            //Cambiamos de posicion en el eje Y
            posicionY = -posicionY;
        }
    }
    //Colisiones
    void OnDestroy()
    {
        // Disminuye el contador de enemigos en el GameManager
        GameManager.instance.enemyCount--;
        //Si el contador de enemigos es 0 y el juego esta en progreso
        if (GameManager.instance.gameInProgress && GameManager.instance.enemyCount == 0)
        {
            //Cargamos la escena del Jefe
            SceneManager.LoadScene("Final");
        }
    }
    //Coroutina para controlar el disparo enemigo
    IEnumerator DisparoEnemigo()
    {
        //Ciclo infinito
        while (true)
        {
            //Esperamos un tiempo aleatorio entre 4 y 8 segundos
            float pausa = Random.Range(4f, 8f);
            //Esperamos el tiempo aleatorio
            yield return new WaitForSeconds(pausa);
            //Instanciamos el disparo enemigo
            Instantiate(prefabDisparoEnemigo, transform.position, Quaternion.identity);
        }
    }

}
