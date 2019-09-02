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
            print("Hit " + hits[0].gameObject.name);
            hits[0].gameObject.GetComponent<Health>().ApplyDamage(damage);
            gameObject.SetActive(false);
        }
    }
}
