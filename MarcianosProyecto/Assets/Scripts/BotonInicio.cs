using UnityEngine;
using UnityEngine.SceneManagement;

//Carlos Molines Pastor: karlinux

public class BotonInicio : MonoBehaviour
{
    //Funcion para el boton de Inicio que lanza la Escena Primera
    public void LanzarJuego()
    {
        SceneManager.LoadScene("Primera");
    }
}
