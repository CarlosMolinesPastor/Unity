using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Creamos una instancia de la clase GameManager, publica y estatica 
    //para poder acceder desde cualquier sitio
    public static GameManager instance;
    //Creamos una variable publica para el numero de pickups
    public int pickupsCount;
    //Creamos una variable publica para las vidas
    public int playerLives;
    //Creamos una variable publica para saber si el juego esta en progreso
    public bool gameInProgress;

    // Muy importante para que no se destruya el objeto al cambiar de escena
     void Awake()
    {
        //Si la instancia es nula
        //La instancia es esta
        if (instance == null)
        {
            instance = this;
            //Imopotante para que no se destruya el objeto al cambiar de escena
            DontDestroyOnLoad(gameObject);
        }
        //Si no destruimos el objeto
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    public void Start()
    {
        //Iniciamos el juego, con los pickups a 0, las vidas a 3 y el juego en progreso
        //He puesto es scoremanager pero no me ha dado tiempo a implementarlo
        pickupsCount = 0;
        playerLives = 3;
        gameInProgress = true;
        ScoreManager.instance.score = 0;
    }

    // Creamos un metodo para acabar el juego
    public void EndGame()
    {
        gameInProgress = false;
    }

}
