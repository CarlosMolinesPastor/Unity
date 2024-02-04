using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Karlinux -  Carlos Molines Pastor
public class EnemigoPerseguidor : MonoBehaviour
{
    //Controlamos el enemigo que se mueve hacia el player
    //Variable velocidad
    public float speed = 1.3f;
    //Variable de distancia de ataque para regularlo
    public float ataqueDistancia = 3.5f;
    //Variable para asignar el objeto que sera perseguido, en este caso el player
    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        //Asignamos el player
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        //Si la distancia entre el enemigo y el player es menor que la distancia de ataque
        if (Vector2.Distance(transform.position, player.transform.position) < ataqueDistancia)
        {
            //Movemos al enemigo hacia el player
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

            //Determinamos la dirección en la que el enemigo debe mirar
            Vector3 direction = player.transform.position - transform.position;
            //Si la dirección es positiva y el enemigo está mirando a la derecha
            if (direction.x > 0 && transform.localScale.x > 0)
            {
                //hacemos que el enemigo mire a la izquierda
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
            else if (direction.x < 0 && transform.localScale.x < 0)
            {
                //Si el jugador está a la izquierda del enemigo y el enemigo está mirando a la izquierda, 
                //hacemos que el enemigo mire a la derecha
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }
    }
}
