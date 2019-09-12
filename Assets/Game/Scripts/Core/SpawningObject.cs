using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawningList
{
    [Tooltip("The GameObject to spawn")]
    public GameObject gameObject;
    [Tooltip("Percentage of rate spawning")]
    [Range(0f, 1f)]
    public float rateSpawning = 1f;
    [Tooltip("Number of spawning objects")]
    public int numberSpawn = 1;
    [Tooltip("Time between the next Spawn")]
    public float secondBetweenSpawn = 2f;
    [Tooltip("Offset when spawn at the Spawn Point")]
    public Vector3 offset;


}

[System.Serializable]
public class SpawningObject : MonoBehaviour
{
    public SpawningManager parentManager;

    // Allow Spawning in the world 
    [Tooltip("Allow the spawning of GameObject")]
    [SerializeField]
    private bool allowSpawn = false;
    
    // Set Spawning in code
    private bool waitSpawn = false;

    //Random Gameobject in the list
    [Tooltip("Randomized you spawning list")]
    [SerializeField]
    private bool randomizedSpawning = false;

    //Delete Gameobject after using it
    [Tooltip("Delete the GameObject that is already use in the list")]
    [SerializeField]
    private bool deleteGameobjectAfterUsing = false;

    //Boolean to set if spawn is by using list or only one gameObject
    [Tooltip("Choose to spawning by using a list")]
    [SerializeField]
    private bool spawnByUsingList = false;

    //Time until the spawning gameobject is destroy
    [Tooltip("Time until the spawning GameObject is destroy")]
    [SerializeField]
    private float timeUntilDestroy = 4f;

    //Time between spawning gameobject
    private float timeBetweenSpawn =  4;

    //Start in a specific point or in the beginning
    [Tooltip("Choose when you begin to spawn in this List. Set 0 if you want to begin in the top of the list.")]
    public int beginStateList = 0;

    public int stateList = 0;

    //2 Methods to spawning
    [Tooltip("List of spawning GameObject")]
    //public List<GameObject> spawningList; -- Old Method
    public List<SpawningList> spawningList;

    /*[Tooltip("List of spawning GameObject (expert mode)")]
    public List<SpawningList> spawningList2;*/

    [Tooltip("Choose the properly GameObject to spawn")]
    public GameObject spawnGameObject = null;

    //Choose a customPosition
    [Tooltip("Choose a custom position point to spawn object")]
    [SerializeField]
    private GameObject customPosition = null;

    //set a specific parent to the gameObject
    [Tooltip("Set a specific parent to the gameObject")]
    [SerializeField]
    private GameObject setParent = null;




    public bool AllowSpawn
    {
        get
        {
            return allowSpawn;
        }

        set
        {
            allowSpawn = value;
        }
    }

    public float TimeUntilDestroy
    {
        get
        {
            return timeUntilDestroy;
        }

        set
        {
            timeUntilDestroy = value;
        }
    }


    // Use this for initialization
    void Start ()
    {
        stateList = beginStateList;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
       if(allowSpawn && !waitSpawn)
        {
            if(spawnByUsingList && spawningList.Count != 0)
            {
                if(randomizedSpawning)
                {
                    int random = (int) Random.Range(0, spawningList.Count - 1);

                    Spawn(spawningList[random]);

                    if (deleteGameobjectAfterUsing)
                    {
                        spawningList.RemoveAt(random);
                    }

                }
                else
                {
                    if(stateList >= spawningList.Count)
                    {
                        stateList = 0;
                    }


                    Spawn(spawningList[stateList]);


                    if (deleteGameobjectAfterUsing)
                    {
                        spawningList.RemoveAt(stateList);
                    }

                    stateList++;
                    
                }
            }
            else if(!spawnByUsingList)
            {

                StartCoroutine(SpawnCoroutine(spawnGameObject, Vector3.zero));
                waitSpawn = true;

            }
            
            
        }
	}


    IEnumerator SpawnCoroutine(GameObject gameObject, Vector3 offset)
    {
        //Debug.Log("Easy Spawn : Spawning one gameObject named " + gameObject.name);
        GameObject instance = gameObject;

        if (setParent != null)
        {
            instance.transform.SetParent(setParent.transform);
        }

        if (customPosition != null)
        {
            instance.transform.position = customPosition.transform.position + offset;
        }
        else
        {
            instance.transform.position = transform.position + offset;
        }

        instance.transform.rotation = transform.rotation;

        //Debug.Log(instance.name);
        SpawningManager.instance.Spawn(instance);

        /*if (customPosition != null)
        {
            instance = Instantiate(customPosition, transform.position + offset, transform.rotation) as GameObject;
        }
        else
        {
            instance = Instantiate(gameObject, transform.position + offset, transform.rotation) as GameObject;
        }*/

        yield return new WaitForSeconds(timeBetweenSpawn);
        waitSpawn = false;
    }





    public void Spawn(SpawningList spawningList)
    {

        for(int i = 1; i <= spawningList.numberSpawn; i++)
        {
            if(Random.Range(0f,1f) <= spawningList.rateSpawning)
            {
                timeBetweenSpawn = spawningList.secondBetweenSpawn;

                StartCoroutine(SpawnCoroutine(spawningList.gameObject, spawningList.offset));
                waitSpawn = true;
            }
        }
    }
}