using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strike : MonoBehaviour
{
    public float speed;
    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);    
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<mob>().GetHit(damage);
        }
    }
}
