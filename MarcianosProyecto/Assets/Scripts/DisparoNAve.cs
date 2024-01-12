//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;

public class DisparoNAve : MonoBehaviour
{
    // Vrlocidad Disparo
    private float velocidadDisparo = 3;
    // Puntos por destruir
    private int puntosAsteroide = 20;
    private int puntosNaveEnemiga = 10;
    private int puntosJefe = 300;
    // Prefab de la explosion
    [SerializeField] private GameObject prefabExplosion;
    // Prefab de la explosion del jefe
    [SerializeField] private GameObject prefabExplosionJefe;
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
        Debug.Log("Posicion: " + transform.position);
        Destroy(gameObject);
    }
    //Colisiones
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Si colisiona con un asteroide o con una nave enemiga
        if (other.tag == "Asteroide" || other.tag == "NaveEnemiga")
        {
            //EXplosion. Instanciamos a la explosion
            Transform explosion = Instantiate(prefabExplosion, other.transform.position, Quaternion.identity).transform;
            //Destruimos la explosion pasado un segundo
            Destroy(explosion.gameObject, 1f);
            //Destruimos el objeto con el que colisiona
            Destroy(other.gameObject);
            //Destruimos el disparo
            Destroy(gameObject);
            if (other.tag == "NaveEnemiga")
            {
                //Añadimos los puntos
                ScoreManager.instance.AddScore(puntosNaveEnemiga);
            }
            if (other.tag == "Asteroide")
            {
                //Añadimos los puntos
                ScoreManager.instance.AddScore(puntosAsteroide);
            }
        }
        if (other.tag == "Jefe")
        {
            //Añádimos los puntos del jefe al marcador
            ScoreManager.instance.AddScore(puntosJefe);
            //EXplosion. Instanciamos a la explosion
            Transform explosion = Instantiate(prefabExplosion, other.transform.position, Quaternion.identity).transform;
            //Destruimos la explosion pasado un segundo
            Destroy(explosion.gameObject, 1f);
            //Destruimos el objeto con el que colisiona ya que el Jefe lo controlamos en su propio script
            Destroy(gameObject);
        }
    }

}
