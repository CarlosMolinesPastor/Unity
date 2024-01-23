using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Karlinux -  Carlos Molines Pastor

public class Enemigo : MonoBehaviour
{
    //Creamos un array de waypoints
    [SerializeField] Transform[] wayPoints;
    //Creamos un entero para el punto aleatorio
    private int puntoAleatorio;
    //Creamos un vector3 para el siguiente punto
    Vector3 siguientePunto;
    //Creamos una variable para la velocidad
    private float velocidad = 2f;
    //Creamos una variable para la distancia de cambio de punto
    private float distanciaCambio = 0.2f;
    //Creamos un gameobject para la explosion 
    [SerializeField] private GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
        //Definimos el siguiente punto con un valor aleatorio de posicion entre los waypoints,
       //  si tuvieramos mas waypoints, deberiamos cambiar el 1 por el numero de waypoints
        puntoAleatorio = Random.Range(0, 1);
        //El siguiente punto es el punto aleatorio
        siguientePunto = wayPoints[puntoAleatorio].position;
        
    }

    // Update is called once per frame
    void Update()
    {
        //Indicamos el siguiente punt y Nos movemos hacia la siguiente posición
        transform.position = Vector3.MoveTowards(transform.position,siguientePunto, velocidad * Time.deltaTime);
        //Si la distancia entre el enemigo y el siguiente punto es menor que la distancia de cambio
        if (Vector3.Distance(transform.position, siguientePunto) < distanciaCambio)
        {
            //Cambiamos el siguiente punto
                puntoAleatorio++;
                //Si el punto aleatorio es mayor que la longitud del array de waypoints
                if (puntoAleatorio >= wayPoints.Length)
                //El punto aleatorio es 0
                    puntoAleatorio = 0;
                    //El siguiente punto es el punto aleatorio
                siguientePunto = wayPoints[puntoAleatorio].position;
        }
        Debug.Log("Siguiente punto: " + siguientePunto);
    }
    //Cuando colisiona
    private void OnCollisionEnter(Collision other) {
        //Sie es con el jugador, instanciamos la explosion 
        if (other.gameObject.tag == "Player")
        {
            //Instanciamos la explosion
            GameObject instanciaExplosion = Instantiate(explosion, transform.position, Quaternion.identity);
            //Destruimos la explosion pasado 0.5 segundo
            Destroy(instanciaExplosion, 0.5f);
            Debug.Log("Colision con el jugador");
        }
    }
}
