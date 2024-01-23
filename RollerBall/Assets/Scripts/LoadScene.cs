using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//Karlinux -  Carlos Molines Pastor
public class LoadScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Cargamos la escena del menu. En Bootstrap
        SceneManager.LoadScene("Menu");
    }
}
