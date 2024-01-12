using UnityEngine;

public class DisparoEnemigo : MonoBehaviour
{
    //Prefab de la explosión que se instanciará al destruir un enemigo
    //[SerializeField] Transform prefabExplosion;
    // Velocidad del disparo
    private float velocidadDisparo = -2;

    // Start is called before the first frame update
    void Start()
    {
        //El disparo se mueve hacia abajo
        GetComponent<Rigidbody2D>().velocity = new Vector3(0, velocidadDisparo, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnBecameInvisible()
    {
        // Cuando deja de ser visible, se destruye
        Debug.Log("Posicion: " + transform.position);
        Destroy(gameObject);
    }
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "Nave")
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
            // Instanciamos la explosión. La posición es la de la nave
            Transform explosion = Instantiate(prefabExplosion, collision.transform.position, Quaternion.identity);
            // Destruimos la nave
            Destroy(collision.gameObject);
            // Destruimos la explosión pasados 1 segundo
            Destroy(explosion.gameObject, 1f);
            // Destruimos también el propio disparo pasado el tiempo de duracion del audio
            Destroy(gameObject, audio.clip.length);
        }
    }*/


}
