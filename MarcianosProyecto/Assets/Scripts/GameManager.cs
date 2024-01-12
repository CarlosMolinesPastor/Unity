//using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

//Carlos Molines Pastor 2024: karlinux

public class GameManager : MonoBehaviour
{
    // Instancia del GameManager
    public static GameManager instance;
    // Contador de enemigos
    public int enemyCount;
    // Vidas del jefe
    public int jefeLifes;
    // Vidas de la nave
    public int naveLifes;
    // Juego en progreso booleano
    public bool gameInProgress;

    // Singleton del GameManager para que solo haya una instancia del mismo en todo el juego
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //Imopotante para que no se destruya el objeto al cambiar de escena
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Inicializa los contadores
    public void Start()
    {
        // Inicializa el contador 
        enemyCount = 11;
        jefeLifes = 6;
        naveLifes = 3;
        gameInProgress = true;
        ScoreManager.instance.score = 0;
    }
    // Finaliza el juego
    public void EndGame()
    {
        gameInProgress = false;
    }
}
