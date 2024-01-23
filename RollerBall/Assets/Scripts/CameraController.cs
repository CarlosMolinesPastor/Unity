using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  //Variable publica para el jugador
  public GameObject player;
  //Variable privada para la distancia entre la camara y el jugador
  private Vector3 offset;
    // Start is called before the first frame update
  void Start()
    {
      //Calculamos la distancia entre el jugador y la camara
      offset = transform.position - player.transform.position;
    }
    // Se ejecuta después de hacer los Update y FixedUpdate. Ya tendríamos
    // los movimientos calculados del resto de elementos
  void LateUpdate()
    {
      // Hacemos que la posición de la cámara sea la posición
      // del jugador más la distancia calculada al principio.
      transform.position = player.transform.position + offset;
    }
    // Update is called once per frame
  void Update()
    {
        
    }
}
