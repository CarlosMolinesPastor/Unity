using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Karlinux -  Carlos Molines Pastor
public class Rotator : MonoBehaviour
{
    //Script que se agrega a los pickups para que roten
    //Variable publica para el giro
    public Vector3 giro;
    private int gradosx;
    private int gradosy;
    private int gradosz;
    // Start is called before the first frame update
    void Start()
    {
        //Generamos un vector de giro aleatorio
        gradosx = Random.Range(0, 100);
        gradosy = Random.Range(0, 100);
        gradosz = Random.Range(0, 100);
        giro = new Vector3(gradosx, gradosy, gradosz);
    }

    // Update is called once per frame
    void Update()
    {
        //Rotamos el objeto
        transform.Rotate(giro * Time.deltaTime);    
    }
}
