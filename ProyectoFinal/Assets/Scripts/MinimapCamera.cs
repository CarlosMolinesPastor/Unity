using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Karlinux -  Carlos Molines Pastor
//Creamos un minimapa para las escenas de combate
public class MinimapCamera : MonoBehaviour
{
    //Instanciamos al player
    public Transform player;


    //Utilizamos LateUpdate para que la camara se actualice antes de que el player se actualice
    //Es lo que hace que la camara se actualice antes de que el player se actualice
    void LateUpdate()
    {
        //Centramos la camara en el player CONM UN VECTOR3
        Vector3 position = player.position;
        // Y le damos la altura del player
        position.y = transform.position.y;
        transform.position = position;
    }
}
