using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    public Sprite targetGraphic;
    public Sprite backgroundGraphic;

    public float sizeWidthImage;
    public float sizeHeightImage;

    [SerializeField]
    private int minValue = 0;
    public int MinValue
    {
        get
        {
            return minValue;
        }

        set
        {
            this.minValue = value;
            CreateHealthBar();
        }
    }
    [SerializeField]
    private int maxValue = 1;
    public int MaxValue
    {
        get
        {
            return maxValue;
        }

        set
        {
            this.maxValue = value;
            CreateHealthBar();
        }
    }
    [SerializeField]
    private int value;

    public int Value
    {
        get
        {
            CheckHealthStatus();
            return value;
        }

        set
        {
            this.value = value;
            CheckHealthStatus();
        }
    }

    private void CheckHealthStatus()
    {
        Image[] listgo = GetComponentsInChildren<Image>();

        for (int i = 0; i < listgo.Length; i++)
        {
            if (i >= value)
            {
                listgo[i].sprite = backgroundGraphic;
            }
            else
            {
                listgo[i].sprite = targetGraphic;
            }
        }
    }

    // Use this for initialization
    private void Awake () {


		
	}

    private void CreateHealthBar()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        GameObject heartObject = new GameObject("HeartImage");
        Image heartImage = heartObject.AddComponent<Image>();
        heartImage.sprite = targetGraphic;

        if (sizeWidthImage != 0 & sizeHeightImage != 0)
            heartImage.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeWidthImage, sizeHeightImage);

        for (int i = minValue; i < maxValue; i++)
        {
            GameObject go = Instantiate(heartObject, transform.position, Quaternion.identity, this.transform);
        }
    }

}
