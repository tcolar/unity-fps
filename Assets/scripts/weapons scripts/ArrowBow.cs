using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBow : MonoBehaviour
{
    private Rigidbody myBody;
    public float speed = 30;
    public float deactivateTimer = 3;
    public float damage = 15;


    public void Awake()
    {
        myBody = GetComponent<Rigidbody>();
    }


    // Start is called before the first frame update
    void Start()
    {
        Invoke("DeactivateGameObject", deactivateTimer);
    }

    public void Launch(Camera mainCamera)
    {
        myBody.velocity = mainCamera.transform.forward * speed;
        transform.LookAt(transform.position + myBody.velocity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DeactivateGameObject()
    {
        if (gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == Tags.ENEMY_TAG)
        {
            other.GetComponent<Health>().ApplyDamage(damage);
            gameObject.SetActive(false);
        }
    }
}
