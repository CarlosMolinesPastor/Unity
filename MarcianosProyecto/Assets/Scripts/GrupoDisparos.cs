using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrupoDisparos : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
     void Update()
    {
        // Si no quedan hijos, destruimos el objeto padre del grupo
        if (transform.childCount == 0)
        {
            Destroy(gameObject);
        }
    }
}
