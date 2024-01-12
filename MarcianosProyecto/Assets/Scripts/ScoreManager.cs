using UnityEngine;
//using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // Creamos una instancia de esta clase para poder acceder a ella desde otras clases
    public static ScoreManager instance;
    // Creamos una variable para mostrar el marcador la hacemos publica para poder acceder a ella desde el inspector
    public int score = 0;

    // Empezamos el juego, creamos una instancia de esta clase
    void Awake()
    {
        // Si ya existe una instancia de esta clase, destruimos esta
        if (instance == null)
        {
            instance = this;
            // Indicamos que no se destruya al cargar una nueva escena
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Creamos una funcion para añadir puntos al marcador y mostrarlos por pantalla
    // Se le pasan los puntos y se añaden a la variable score
    public void AddScore(int points)
    {
        score += points;
    }
}
