using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScrolling : MonoBehaviour
{
    //speed of scrolling
    public float Speed { get; set; }

    private float initPos;

    void Start()
    {
        initPos = transform.localPosition.x;
    }

    void FixedUpdate()
    {

        //translate sprite according to target velocity
        this.transform.Translate(new Vector3(-Speed, 0, 0) * Time.deltaTime);
        //set sprite is moving out of screen shift it to put clone in its place
        float width = getWidth();
        //shift right if player is moving right
        if (initPos - this.transform.localPosition.x > width)
        {
            this.transform.Translate(new Vector3(width, 0, 0));
        }
    }

    float getWidth()
    {
        //Get sprite width
        return this.GetComponentInChildren<SpriteRenderer>().bounds.size.x;
    }
}