using UnityEngine;

public interface IGenericEntity
{
    Animator Anim { get; set; }
    Collider2D Coll { get; set; }
    Rigidbody2D Rb { get; set; }

    void OnChildTriggerEnter2D(Collider2D collider2D, Collider2D other);
    void TakeDamage();
}