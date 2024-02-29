using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
//Karlinux -  Carlos Molines Pastor
public class Enemigo : MonoBehaviour
{
    //Variables para el patrullaje
    [SerializeField] Transform[] wayPoints;
    [SerializeField] Transform player;
    //Variable para la animación
    new Animation animation;
    //Variable siguiente posicion
    int numeroSiguientePosicion;
    //Variable navegación
    NavMeshAgent agente;
    //Variable para saber si el enemigo ha sido golpeado y asi desde el player
    //llamar a que venga a por el jugador
    private bool isHit;
    //Variable para saber si el enemigo corre
    private bool isRun;

    //Prefab de explosion
    [SerializeField] GameObject explosionPrefab;
    //Vida del enemigo
    public int life = 100;
    //Variable para saber si esta muerto o no
    private bool isDeath = false;
    //Angulo de vision del enemigo
    private float angle;
    private float angulo;
    //Deteccion de la distancia al jugador
    public float distanceDetection = 10f;
    //Agrupamos el sonido en el inspector
    [Header("Audio")]
    //Sonidos
    public AudioClip audioWalking;
    public AudioClip audioRunning;
    //Variable de sonido
    private AudioSource audioSource;


    void Start()
    {
        //Recoger componentes
        //el navmeshagent
        agente = GetComponent<NavMeshAgent>();
        //Control de waypoints
        numeroSiguientePosicion = 0;
        //Iniciamos patrullaje de enemigos
        SetNextDestination();
        // Inicializa la animación
        animation = GetComponent<Animation>(); // Inicializa la referencia al componente Animation
        //Booleanas a falso corre y no ha sido golpeado 
        isHit = false;
        isRun = false;
        //Recogemos el audio
        audioSource = gameObject.AddComponent<AudioSource>();

    }

    void Update()
    {
        //Vector de direccion al jugador
        Vector3 directionToPlayer = player.position - transform.position;
        //Angulo de vision
        angle = Vector3.Angle(transform.forward, directionToPlayer);
        //Diferenciamos si estamos en el nivel 1 o 2 para calculo del angulo, si estamos en nivel 1 el 
        //angulo es menor para que sea mas facil que en nivel 2 que nos localiza en todas las direcciones
        //Ademas en nivel 2 la distancia de deteccion es mayor con lo que iran a por nosotros desde mas lejos
        if (SceneManager.GetActiveScene().name == "Nivel1")
        {
            angulo = 75;
        }
        //Distancia al jugador
        if (SceneManager.GetActiveScene().name == "Nivel2")
        {
            distanceDetection = 65f;
            //Detecta en todas las direcciones
            angulo = 360;
        }
        //Distancia al jugador
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);
        //Si el enemigo ha sido golpeado o la distancia es mayor a 10 y el angulo es menor a 75(nivel1) o 360(nivel2)
        if (isHit || (distanceToPlayer < distanceDetection && angle < angulo))
        {
            //Audio de correr si no esta sonando
            if (!audioSource.isPlaying)
            {
                audioSource.clip = audioRunning;
                audioSource.Play();
            }
            if (distanceToPlayer > 0.5f) // Si la distancia al jugador es mayor a 0.5, sigue al jugador
            {

                //Le indicamos que se dirija al jugador
                SetDestinationToPlayer();
            }
            else // Si la distancia al jugador es 0.5 o menos,  ajusta su destino a un punto a 0.5 metros del jugador
            //Es para evitar que lo sobrepase
            {
                //Vector de direccion al jugador para ajustar su destino
                Vector3 playerDirection = (transform.position - player.position).normalized;
                //Ajustamos el punto
                Vector3 newDestination = player.position + playerDirection * 0.5f;
                //Ajustamos la velocidad 
                agente.speed = 3.5f;
                //Ajustamos el destino a un nuevo punto
                agente.SetDestination(newDestination);
            }
        }
        //Si el enemigo no ha sido golpeado o la distancia es menor a 10 y el angulo es mayor a 75
        else if (!agente.pathPending && agente.remainingDistance < 0.5f)
        {
            //Reproduce el audio de andar
            if (!audioSource.isPlaying)
            {
                audioSource.clip = audioWalking;
                audioSource.Play();
            }
            //Y buscamos un nuevo punto
            SetNextDestination();
        }
    }

    //Funcion de busqueda de nuevo punto
    void SetNextDestination()
    {
        //Play de audio andar ya que si esta corriendo pasamos a andar
        if (isRun)
        {
            animation.Play("Walk"); // Reproduce la animación 
        }
        //Si hemos recorrido todos los waypoints volvemos al primero
        if (wayPoints.Length == 0)
            return;
        agente.speed = 2f; //Establecemos la velocidad

        agente.destination = wayPoints[numeroSiguientePosicion].position;
        numeroSiguientePosicion = (numeroSiguientePosicion + 1) % wayPoints.Length;
    }

    //Funcion destino al player
    public void SetDestinationToPlayer()
    {
        //Play de audio correr ya que si esta andando pasamos a correr
        animation.Play("Run");
        isRun = true; // Reproduce la animación "Run" y señala que corre
        agente.destination = player.position; //Le decimos que sigue al player

        // Calcula la distancia entre el enemigo y el jugador
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        //Si la escena es el nivel 2 aumentamos la velocidad si no la dejamos en nivel1
        if (SceneManager.GetActiveScene().name == "Nivel2")
        {
            // Si el enemigo está a menos de 10  unidades del jugador, reduce la velocidad.
            // Lo que i tento es que el enmemigo salga corriendo a gran velocidad desde mas lejos pero que cuando este cerca que
            //reduzca la velocidad para que no lo atraviese.
            if (distanceToPlayer <= 10f)
            {
                agente.speed = 6f;
            }
            else
            {
                agente.speed = 9f;
            }
        }
        else
        {
            agente.speed = 6f;
        }
        //Si la vida del enemigo es menor o igual a 0 y no esta muerto entonces muere
        if (life <= 0 && isDeath == false)
        {
            DeathEnemy();
        }

    }
    //Funcion golpeo enemigo
    public void Hit()
    {
        isHit = true;
    }
    //Metodo para que el enemigo reciba daño
    public void RecibirDaño(int daño)
    {
        life -= daño;

    }
    private void DeathEnemy()
    {
        Debug.Log("Enemigo muerto");
        GameManager.instance.puntos += 200;
        // Desactiva el NavMeshAgent para que el enemigo deje de moverse
        agente.isStopped = true;
        agente.enabled = false;
        //Se para la reproduccion de sonido
        audioSource.Stop();
        // Desactiva los componentes visuales del objeto, ya que si no no se reproducia bien la explosion
        foreach (var component in gameObject.GetComponentsInChildren<Renderer>())
        {
            component.enabled = false;
        }
        foreach (var component in gameObject.GetComponentsInChildren<Collider>())
        {
            component.enabled = false;
        }
        //Instancia la explosion
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        //Destroy(explosionSoundObject, audioExplosion.length);
        Destroy(explosion, 1.0f);
        Destroy(gameObject);

    }
}
