using System.Collections.Generic;
using UnityEngine;
// Karlinux -  Carlos Molines Pastor
public class SalirJuego : MonoBehaviour
{
        // Start is called before the first frame update

        public void Salir()
        {
                // Lo he buscado por internet que el símbolo # se utiliza para las
                // directivas de preprocesador. Es decir son instrucciones que se ejecutan antes 
                // de compilar el código. En este caso, si estamos en el editor de Unity,
                // detenemos la reproducción, y si estamos en una versión compilada, salimos de la aplicación.
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }
}
