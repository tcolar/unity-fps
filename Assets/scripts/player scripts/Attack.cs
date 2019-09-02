using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float damage = 2;
    public float radius = 1;
    public LayerMask layerMask;

    // Update is called once per frame
    void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, layerMask);
        if( hits.Length > 0 )
        {
            print("Touched" + hits[0].gameObject.tag);
            gameObject.SetActive(false);
        }
    }
}
