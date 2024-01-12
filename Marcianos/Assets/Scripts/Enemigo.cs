using System.Collections;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    // Velocidad del enemigo
    [SerializeField] float velocidadX = 2;
    // Velocidad del enemigo
    [SerializeField] float velocidadY = -1.1f;
    // Prefab del disparo del enemigo (se asigna desde el inspector)
    [SerializeField] Transform prefabDisparoEnemigo;

    // Start is called before the first frame update
    void Start()
    {
        // Disparo cada 5-10 segundos
        StartCoroutine(Disparar());
    }

    // Update is called once per frame
    void Update()
    {
        //Movimiento
        transform.Translate(velocidadX * Time.deltaTime, velocidadY * Time.deltaTime, 0);
        if ((transform.position.x < -4) || (transform.position.x > 4))
        {
            //Si llega a los limites de la pantalla, cambia de dirección
            velocidadX = -velocidadX;
        }
        //Si llega a los limites de la pantalla, cambia de dirección
        if ((transform.position.y < -2.5) || (transform.position.y > 2.5))
        {
            velocidadY = -velocidadY;
        }
    }
    //Funcion que se ejecuta cuando por el enemigo al disparar
    IEnumerator Disparar()
    {
        while(true)
        {
            //Esperamos entre 5 y 10 segundos
            float pausa = Random.Range(5.0f, 11.0f);
            //Esperamos la pausa
            yield return new WaitForSeconds(pausa);
            //Instanciamos el disparo
            Instantiate(prefabDisparoEnemigo, transform.position, Quaternion.identity);
        }
    }
}
