using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Karlinux -  Carlos Molines Pastor
public class EnemigoVolando : MonoBehaviour
{
    //Creamos una lista de puntos para el patrullaje
    [SerializeField] List<Transform> wayPoints;
    //Velocidad del enemigo
    [SerializeField] float velocidad = 3f;
    //Variable de la siguiente posicion
    private byte siguientePosicion;
    //Variable para poder defincir la distancia de cambio de posicion
    [SerializeField] float distanciaCambio = 0.2f;
    //Variable para el controlador de escena
    private SceneController sceneController;

    // Start is called before the first frame update
    void Start()
    {
        //Recogemos el controlador de escena
        sceneController = FindObjectOfType<SceneController>();
        //Indicamos que la siguiente posicion es la 0
        siguientePosicion = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Movemos el enemigo hacia el siguiente punto
        transform.position = Vector3.MoveTowards(transform.position,
        wayPoints[siguientePosicion].transform.position,
        velocidad * Time.deltaTime);
        // Determinamos la posición en la que el enemigo debe mirar, si el waypoitn esta a la dereecha
        if (wayPoints[siguientePosicion].transform.position.x > transform.position.x)
        {
            //Movemos al enmemigo a derecha
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (wayPoints[siguientePosicion].transform.position.x < transform.position.x)
        {
            //Si el waypoint esta a la izquierda movemos al enemigo a la izquierda
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        //Si la distancia entre el enemigo y el waypoint es menor que la distancia de cambio de waypoint vamos a la siguiente
        //Si es mayor que los puntos waypoints vamos a la posicion 
        if (Vector3.Distance(transform.position, wayPoints[siguientePosicion].transform.position) < distanciaCambio)
        {
            siguientePosicion++;
            if (siguientePosicion >= wayPoints.Count) siguientePosicion = 0;
        }
    }
}

