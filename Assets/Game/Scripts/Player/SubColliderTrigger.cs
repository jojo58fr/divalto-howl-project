using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubColliderTrigger : MonoBehaviour {

    private IGenericEntity parent;

    void Start()
    {
        parent = transform.parent.GetComponent<IGenericEntity>();
        //Debug.Log(transform.parent.name);
    }

    void OnTriggerEnter2D(Collider2D aCol)
    {
        //Debug.Log(aCol.name);
        parent.OnChildTriggerEnter2D(GetComponent<Collider2D>(), aCol); // pass the own collider and the one we've hit
    }

    void OnTriggerStay2D(Collider2D aCol)
    {
        //Debug.Log(aCol.name);
        parent.OnChildTriggerEnter2D(GetComponent<Collider2D>(), aCol); // pass the own collider and the one we've hit
    }
}
