//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Carlos Molines Pastor: karlinux

public class Inicio : MonoBehaviour
{
    // Nombre de la siguiente escena a cargar
    public string nextSceneName = "Menu";

    // Use this for initialization
    void Start()
    {
        // Script para cargar la escena siguiente desde bootstrap
        // Carga la siguiente escena
        SceneManager.LoadScene(nextSceneName);
    }
}
