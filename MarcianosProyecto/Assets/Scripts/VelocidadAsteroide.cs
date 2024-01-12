using UnityEngine;

public class VelocidadAsteroide : MonoBehaviour
{
    //Velocidad de movimiento del asteroide
    [SerializeField] float velocidad = 2f;
    //Velocidad de rotacion del asteroide
    [SerializeField] float rotacion = 50f; //grados por segundo
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Mueve el asteroide hacia abajo a la velocidad en el tiempo correspondiente en el mundo
        transform.Translate(Vector3.down * velocidad * Time.deltaTime, Space.World);
        //Rota el asteroide en el eje Z a la velocidad en el tiempo correspondiente sobre si mismo
        transform.Rotate(Vector3.forward, rotacion * Time.deltaTime, Space.Self);

        //Elimina el asteroide si se sale de la pantalla
        if (transform.position.y < -6)
        {
            Destroy(gameObject);
        }
    }
}
