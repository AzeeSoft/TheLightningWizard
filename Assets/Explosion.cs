using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    public SphereCollider col;
    public float cutOff;
    public int damage;

    void Start()
    {
        col = GetComponent<SphereCollider>();
    }
    void Update()
    {
        col.radius = col.radius + .025f;

      if(col.radius >= cutOff)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider _col)
    {
        if(_col.gameObject.tag == "Player")
        {
            _col.GetComponent<PlayerHealthandSave>().TakeDamage(damage);
        }
    }
    
}
