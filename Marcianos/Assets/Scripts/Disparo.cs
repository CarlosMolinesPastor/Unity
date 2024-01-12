using UnityEngine;

public class Disparo : MonoBehaviour
{
    //Prefab de la explosión que se instanciará al destruir un enemigo
    [SerializeField] Transform prefabExplosion;
    // Velocidad del disparo
    private float velocidadDisparo = 2;
    // Start is called before the first frame update
    void Start()
    {
        //El disparo se mueve hacia arriba
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
    //Funcion que se ejecuta cuando el disparo colisiona con un enemigo
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Si el disparo colisiona con un enemigo
        if (other.tag == "Enemigo")
        {
            
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
            // Instanciamos la explosión. La posición es la del enemigo
            Transform explosion = Instantiate(prefabExplosion, other.transform.position, Quaternion.identity);
            // Destruimos el enemigo
            Destroy(other.gameObject);
            // Destruimos la explosión pasados 1 segundo
            Destroy(explosion.gameObject, 1f);
            // Destruimos también el propio disparo
            Destroy(gameObject, audio.clip.length);


        }
    }
}
