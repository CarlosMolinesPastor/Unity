using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//Karlinux -  Carlos Molines Pastor
public class Pickups : MonoBehaviour
{
    //Redered del objeto para cambiar el color
    private Renderer rend;
    [SerializeField] private GameObject explosion;
    private Color colorAleatorio;
    // Start is called before the first frame update
    void Start()
    {
        //recogemos el renderer
        rend = GetComponent<Renderer>();
        //Creamos una coroutine para cambiar el color
        StartCoroutine("CambiarColor");
    }

    // Update is called once per frame
    void Update()
    {

    }
    //Cuando el objeto desaparece setActive instanciamos el prefab de explosion
    void OnDisable()
    {
        if (GameManager.instance.gameInProgress)
        {
            GameObject explosionInstancia = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(explosionInstancia, 0.5f);
        }

    }
    //Corrutina para cambiar el color
    IEnumerator CambiarColor()
    {
        while (true) // Loop infinito
        {
            yield return new WaitForSeconds(2f); // Esperamos 2s

            // Generamos un color aleatorio
            colorAleatorio = new Color(
                Random.Range(0f, 1f), // Rojo
                Random.Range(0f, 1f), // Verde
                Random.Range(0f, 1f)  // Azul
            );
            // Cambiamos el color del material
            rend.material.color = colorAleatorio; 
            Debug.Log("Color: " + colorAleatorio);
        }
    }
}
