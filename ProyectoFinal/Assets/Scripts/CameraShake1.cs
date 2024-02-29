using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake1 : MonoBehaviour
{
    public float duracion = 1.5f;
    public float intensidad = 1.5f;

    public CharacterController playerController;



    //Creamoas una corutina para la camara
    //public IEnumerator Shake(float duracion, float intensidad)
    //{
    //    //Primero obtenemos la posicion inicial de la camara para que vuelva a su posicion original
    //    Vector3 originalPosCamera = transform.localPosition;
    //    //Crreamos una variable para el tiempo para saber cuando tiempo9 ha pasado 
    //    //desde que hemos empezado el tempblor
    //    float tiempo = 0.0f;
    //    // Bucle para  que mientras el tiempo  sea menor que la duracion que hemos establecido 
    //    // se mantenga la vibracion
    //    while (tiempo < duracion)
    //    {
    //        //Crreamos dos randoms para los ejes x e y para que se mueva aleatoriamente
    //        float randomX = Random.Range(-0.5f, 0.5f) * intensidad;
    //        float randomY = Random.Range(-0.2f, 0.2f) * intensidad;
    //        //Modificamos la posicion de la camara con el random menos la z que mantenemos la
    //        // de la camra original
    //        transform.localPosition = originalPosCamera + new Vector3(randomX, randomY, originalPosCamera.z);
    //        //tenemos que sabere el tiempo que ha transcurrido
    //        tiempo += Time.deltaTime;
    //        //Esperamos un tiempo para volver a la posicion original
    //        yield return null;
    //    }
    //    //Volvemos a la posicion original
    //    transform.localPosition = originalPosCamera;
    //}
public IEnumerator Shake(float duracion, float intensidad)
{
    Vector3 originalPosCamera = transform.localPosition;
    Vector3 originalPosPlayer = playerController.transform.position; // Almacenar la posición original del jugador

    float tiempo = 0.0f;

    while (tiempo < duracion)
    {
        float randomX = Random.Range(-0.5f, 0.5f) * intensidad;
        float randomY = Random.Range(-0.2f, 0.2f) * intensidad;
        transform.localPosition = originalPosCamera + new Vector3(randomX, randomY, originalPosCamera.z);
        tiempo += Time.deltaTime;
        yield return null;
    }

    transform.localPosition = originalPosCamera;
    playerController.transform.position = originalPosPlayer; // Mover al jugador de vuelta a su posición original
}
}

