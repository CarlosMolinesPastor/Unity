using System.Collections;
using UnityEngine;

//Carlos Molines Pastor: karlinux

public class Asteroide : MonoBehaviour
{
    //Variable que almacena el prefab del asteroide
    [SerializeField] GameObject asteroidePrefab;
    // Start is called before the first frame update
    void Start()
    {
        //Empezamos la coroutina de crear asteroides
        StartCoroutine(CreateAsteroides());
    }
    IEnumerator CreateAsteroides()
    {
        //Ciclo infinito para crear asteroides
        while (true)
        {
            //Genera un nuevo asteroide cada un segundo en una posicion aleatoria dentro del ancho(-9,9)
            // y la altura de 5 es decir la camara, instancia el asteroidePrefab
            float ancho = Random.Range(-9, 9);
            Vector3 spawnPosition = new Vector3(ancho, 5, 0);
            //Instanciamos el asteroide
            Instantiate(asteroidePrefab, spawnPosition, Quaternion.identity);
            //Esperamos un segundo
            yield return new WaitForSeconds(1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
    
    }
}
