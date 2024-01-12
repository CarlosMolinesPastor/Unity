using UnityEngine;

public class DisparoEnemigo : MonoBehaviour
{

    // Iniciamos el disparo enemigo con una velocidad de -3
    //Este disparo es el de los enemigos y esta diferenciado del del /jefe
    [SerializeField] float velocidadDisparo = -3;
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
    private void OnBecameInvisible() {
        Destroy(gameObject);
    }

}
