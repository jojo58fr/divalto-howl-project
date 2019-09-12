///-----------------------------------------------------------------
///   Class:          SpawningManager.cs
///   Description:    Set your description here
///   Author:         Joachim Miens                   Date: XX/XX/XXXX
///   E-mail :        joachim.miens@gmail.com
///   Website :		  http://www.joachim-miens.com/
///   Notes:          <Notes>
///   Revision History:
///   Name:           Date & Time:        Description:
///-----------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpawningManager : MonoBehaviour 
{
    #region structure list

    struct Data
    {
        public GameObject gameObject;
        public float timeUntilDestroy;

        public Data(GameObject gameObject, float timeUntilDestroy)
        {
            this.gameObject = gameObject;
            this.timeUntilDestroy = timeUntilDestroy;
        }
    }

    #endregion



    #region Variables

    private List<Data> listGameObject = new List<Data>();

    private static bool boolBetweenObjects = false;

    public float delayBetweenObjects;

    public static SpawningManager instance;


    public bool WaitHandlingIsActive = false;

    #endregion


    #region Unity Methods

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        boolBetweenObjects = false;
    }

    void Start () 
	{
		
	}
	

	void Update () 
	{
		if(!boolBetweenObjects && listGameObject.Count > 0)
        {
            GameObject instance = Instantiate(listGameObject[0].gameObject);

            if (listGameObject[0].timeUntilDestroy > 0)
            {
                DestroyObject(instance, listGameObject[0].timeUntilDestroy);        
            }

            boolBetweenObjects = true;
            listGameObject.RemoveAt(0);
        }

        /*if(boolBetweenObjects)
        {
            foreach(Data go in listGameObject)
            {
                Debug.Log(go.gameObject.name);
            }
        }*/
	}

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("Debug : reset spawn hello world" + collision.name + " " + collision.tag);
        if ( (collision.tag == "Obstacle" || collision.tag == "Object") && GameObject.FindGameObjectsWithTag("Pattern") == null || collision.transform.tag == "Pattern")
        {
            Debug.Log(collision.name + " va �tre reset apr�s cette objet.");
            if (!WaitHandlingIsActive)
            {
                StopCoroutine(WaitHandling());
                StartCoroutine(WaitHandling());
            }

        }

    }

    #endregion


    public void Spawn(GameObject spawnObject, bool instantSpawn = false)
    {
        //Debug.Log(spawnObject.name);
        //Debug.Log(spawnObject.transform.parent.name);

        if (instantSpawn)
        {
            Instantiate(spawnObject);
        }
        else
        {
            //Debug.Log(boolBetweenObjects + " /  is adding in a list" );
            listGameObject.Add(new Data(spawnObject,0f));
        }
    }

    public void Spawn(GameObject spawnObject, float timeUntilDestroy, bool instantSpawn = false)
    {
        Debug.Log(spawnObject.name);
        //Debug.Log(spawnObject.transform.parent.name);

        if (instantSpawn)
        {
            GameObject instance = Instantiate(spawnObject);
            DestroyObject(instance, timeUntilDestroy);
        }
        else
        {
            listGameObject.Add(new Data(spawnObject, timeUntilDestroy));
        }
    }

    IEnumerator DestroyObject(GameObject ObjectDestroy, float timeUntilDestroy)
    {
        yield return new WaitForSeconds(timeUntilDestroy);
        //Debug.Log("Easy Spawn : Destroying one gameObject named " + gameObject.name);
        Destroy(ObjectDestroy);
    }

    IEnumerator WaitHandling()
    {
        WaitHandlingIsActive = true;
        yield return new WaitForSeconds(delayBetweenObjects);
        //Debug.Log("Easy Spawn : Destroying one gameObject named " + gameObject.name);
        boolBetweenObjects = false;
        WaitHandlingIsActive = false;
    }
}
