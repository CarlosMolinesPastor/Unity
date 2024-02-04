using System.Collections;
using UnityEngine;
//Karlinux -  Carlos Molines Pastor
public class EnemigoTierra : MonoBehaviour
{
    public float speed = 3f; 
    //Objeto para detectar si hay contacto con el suelo
    public Transform controladorSuelo;
    //Variable para calcular la distancia del suelo
    public float groundCheckDistance = 0.5f;
    private Rigidbody2D rb;
    //Variable para controlar el incremento de velocidad
    private float incrementoVelocidad = 1.15215f;
    //Variable el sentido de direccion
    private float direccion = 1; // 1 para la derecha, -1 para la izquierda
    //variable para la velocidad base del enemigo
    private float velocidadBase; // La velocidad base del enemigo


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        velocidadBase = speed; // Guarda la velocidad base
        StartCoroutine(AumentarVelocidadEnemigos());
    }

    // Funcion para que el enemigo se mueva hacia la izquierda o derecha, lanzamos un rayop con el controlador del suelo
    // y si la informacion que le llega es false, no hay contacto con el suelo y se llama a la funcion Girar, el controlador
    // de suelo es hijo del enemigo, es un objeto vacio y esta adelantado al enemigo para que no caiga
    private void FixedUpdate()
    {
        RaycastHit2D informacionSuelo = Physics2D.Raycast(controladorSuelo.position, Vector2.down, groundCheckDistance);
        rb.velocity = new Vector2(speed * direccion, rb.velocity.y); // Usa la dirección para determinar la dirección de la velocidad
        if (informacionSuelo.collider == false)
        {
            Girar();
            Debug.Log("Informacion de suelo" + informacionSuelo.collider);
        }
    }
    //Funcion para girar el enemigo
    private void Girar()
    {
        direccion *= -1; // Cambia la dirección
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y - 180, 0);
    }
    //Funcion para aumentar la velocidad del enemigo conforme el tiempo transcurre, lanzamos la courrutina desd
    // el start y le damos velocidad hasta 5 que es la velocidad del player
   private IEnumerator AumentarVelocidadEnemigos()
    {
        while (velocidadBase < 5f)
        {
            yield return new WaitForSeconds(20f);
            velocidadBase *= incrementoVelocidad; // Aumenta la velocidad base
            speed = Mathf.Abs(velocidadBase); // Asegura que la velocidad siempre sea positiva
        }
    }

}
