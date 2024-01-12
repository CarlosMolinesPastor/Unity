using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMAnagerStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Indispensable para que se carguen los datos de nuevo de las vidas y los puntos
        GameManager.instance.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
