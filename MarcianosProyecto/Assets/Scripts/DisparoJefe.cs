//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class DisparoJefe : MonoBehaviour
{

    // Disparo inicial del jefe con velocidad superior a la de los enemigos
    [SerializeField] float velocidadDisparo = -3.5f;
    // Start is called before the first frame update
    void Start()
    {
        // Inicia el disparo con la velocidady marcada
        GetComponent<Rigidbody2D>().velocity = new Vector3(0, velocidadDisparo, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Destruye el objeto cuando desaparece
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
