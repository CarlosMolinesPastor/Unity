using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
// Karlinux -  Carlos Molines
public class TextoEscribe : MonoBehaviour
{
    AudioSource audioSource;
    string nivel1 = "La tierra ha sufrido una invasion extraterrestre de zombies. \n Ha sido un gran desastre para la humanidad. \n Ahora, el futuro de la tierra esta en tus manos. \n Debes recoger una serie de baterias para poder acabar con la nave nodriza y asi solucionar esta invasion. \n Toda la humanidad esta en tus manos \n Suerte en tu aventura.";
    public Text texto;

    void Start()
    {
        StartCoroutine(Tiempo());
        audioSource = Camera.main.GetComponent<AudioSource>();
    }

    IEnumerator Tiempo()
    {
        foreach (char letra in nivel1)
        {
            texto.text += letra;
            yield return new WaitForSeconds(0.1f);
        }

        if (audioSource != null)
        {
            audioSource.Stop();
        }

        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Nivel1");
    }
}