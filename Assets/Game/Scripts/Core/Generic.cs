///-----------------------------------------------------------------
///   Class:          Generics
///   Description:    Generics : All generics method for a object
///   Author:         Joachim Miens                   Date: 28/05/2017
///   E-mail :        joachim.miens@gmail.com
///   Notes:          <Notes>
///   Revision History:
///   Name:           Date & Time:        Description:
///-----------------------------------------------------------------


using UnityEngine;
using System.IO;
using System;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// Generic class for generic methods
/// </summary>
public class Generic : MonoBehaviour 
{

    ///------------------------------------------------------------------------------------------------------------
    /// Class Variables
    ///------------------------------------------------------------------------------------------------------------


    ///------------------------------------------------------------------------------------------------------------
    /// Beginning
    ///------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Check by using layer
    /// </summary>
    /// <param name="layer">the layer of the gameobject</param>
    /// <returns>the gameobject in collision</returns>
    protected bool Check(string layer)
    {
        //Debug.Log(coll.IsTouchingLayers(LayerMask.GetMask(layer)));
        Collider2D coll = gameObject.GetComponent<Collider2D>();
        return coll.IsTouchingLayers(LayerMask.GetMask(layer));
    }

    /// <summary>
    /// Check tag by using layer with the gameObject that collide with the tag
    /// </summary>
    /// <param name="layer">Name of layer</param>
    /// <param name="go">The gameObject</param>
    /// <returns>the gameobject match with the tag</returns>
    protected bool Check(string layer, GameObject go)
    {
        //Debug.Log(coll.IsTouchingLayers(LayerMask.GetMask(layer)));
        Collider2D coll = go.GetComponent<Collider2D>();
        return coll.IsTouchingLayers(LayerMask.GetMask(layer));
    }

    /// <summary>
    /// 2D Method : Check all tag that the object in child collide
    /// </summary>
    /// <param name="tag">the tag for checking collision</param>
    /// <returns>the gameobject match with the tag</returns>
    protected GameObject GetTouchingTag2D(string tag)
	{
		GameObject[] listgo = GameObject.FindGameObjectsWithTag (tag);

		GameObject res = null;

		foreach(GameObject goFound in listgo)
		{
			if (Physics2D.IsTouching(GetComponent<Collider2D>(), goFound.GetComponent<Collider2D>() )) 
			{
				res = goFound;
			}
		}

		return res;
	}

    /// <summary>
    /// 2D Method : Check tag by return the object that collide with a gameobject
    /// </summary>
    /// <param name="tag">The tag for checking collision between collider</param>
    /// <param name="go">The gameobject wich collide with the tag</param>
    /// <returns>Gameobject if tag not null</returns>
    protected GameObject GetTouchingTag2D(string tag, GameObject go)
    {
        GameObject[] listgo = GameObject.FindGameObjectsWithTag(tag);

        GameObject res = null;

        foreach (GameObject goFound in listgo)
        {
            if (Physics2D.IsTouching(go.GetComponent<Collider2D>(), goFound.GetComponent<Collider2D>()))
            {
                res = goFound;
            }
        }

        return res;
    }


    /// <summary>
    /// Disable all gameObject that using a specific tag
    /// </summary>
    /// <param name="tag">the specific tag of the gameObject</param>
    protected void DisableTag(string tag)
    {
        GameObject[] gameobjects = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject go in gameobjects)
        {
            go.SetActive(false);
        }
    }

    /// <summary>
    /// Enable all object in this list
    /// </summary>
    /// <param name="objects">Objects to enable in the unity hierarchy</param>
    protected void EnableObjects(GameObject[] objects)
    {
        foreach (GameObject go in objects)
        {
            go.SetActive(true);
        }
    }

    /// <summary>
    /// Resume time in Unity
    /// </summary>
    protected void TimeContinue()
    {
        Time.timeScale = 1f;
    }

    /// <summary>
    /// Pausing time in Unity
    /// </summary>
    protected void TimePause()
    {
        Time.timeScale = 0f;
    }


    /// <summary>
    /// Find object in hierarchy
    /// </summary>
    /// <param name="name">name of the object</param>
    /// <returns>the needed object</returns>
    protected GameObject FindObject(string name)
    {
        Transform[] trs = gameObject.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in trs)
        {
            if (t.name == name)
            {
                return t.gameObject;
            }
        }
        return null;
    }


    /// <summary>
    /// Save using different methods of writings
    /// </summary>
    /// <typeparam name="T">Type of Object to write</typeparam>
    /// <param name="save">Object of write</param>
    /// <param name="typesave">Type of writing method</param>
    /// <param name="path">path of the writing</param>
    /// <param name="option">option of differents methods : 
    ///     Binary/XML : Append if overwrinting
    ///     Json : Pretty Writing in the file
    /// </param>
    protected void Save<T>(T save, TypeSave typesave, string path, bool option = false)
    {
        switch(typesave)
        {
            case TypeSave.Binary:
                DataWriteRead_Unity.WriteToBinaryFile(Application.persistentDataPath + "/" + path, save, option);
                break;
            case TypeSave.Json:
                Debug.Log("Sauvegarde en cours. " + Application.persistentDataPath + "/" + path);
                Debug.Log(Directory.GetCurrentDirectory());
                DataWriteRead_Unity.WriteToJsonFile(Application.persistentDataPath + "/" + path, save, option);
                break;
            case TypeSave.XML:
                DataWriteRead_Unity.WriteToXmlFile(Application.persistentDataPath + "/" + path, save, option);
                break;
        }
    }

    /// <summary>
    /// Load using different methods of loading
    /// </summary>
    /// <typeparam name="T">Type of object to load</typeparam>
    /// <param name="path">path of the loading</param>
    /// <param name="typesave">Type of loading method</param>
    /// <returns></returns>
    protected T Load<T>(string path, TypeSave typesave)
    {
        T res = default(T);

        switch (typesave)
        {
            case TypeSave.Binary:
                res = DataWriteRead_Unity.ReadFromBinaryFile<T>(Application.persistentDataPath + "/" + path);
                break;
            case TypeSave.Json:
                res = DataWriteRead_Unity.ReadFromJsonFile<T>(Application.persistentDataPath + "/" + path);
                break;
            case TypeSave.XML:
                res = DataWriteRead_Unity.ReadFromXmlFile<T>(Application.persistentDataPath + "/" + path);
                break;
        }

        return res;
    }

    /// <summary>
    /// Check if saving exist
    /// </summary>
    /// <param name="path">Path of saving</param>
    /// <returns>boolean : if the file exist or not</returns>
    protected bool SaveExist(string path)
    {
        return File.Exists(Application.persistentDataPath + "/" + path);
    }

    protected string PathGameFile()
    {
        return Application.persistentDataPath + "/";
    }

    /// <summary>
    /// Set secury by hashing and using cryptography
    /// </summary>
    /// <param name="save">The save to hash</param>
    /// <returns>the string of the hash</returns>
    public string SetHashSecurity(SecureData save)
    {
        //Save content in JSon as a string
        string saveStateString = JsonUtility.ToJson(save, true);

        //Setup SHA
        SHA256Managed crypt = new SHA256Managed();
        string hash = String.Empty;

        //Compute Hash
        byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(saveStateString), 0, Encoding.UTF8.GetByteCount(saveStateString));

        //Convert to Hex
        foreach (byte bit in crypto)
        {
            hash += bit.ToString("x2");
        }

        return hash;
    }

}

/// <summary>
/// Generic class of player object for writing
/// </summary>
[Serializable]
public class SecureData
{
    public string PlayerName;
    public string VersionString;
    public string HashOfContents;
}


/// <summary>
/// Differents type of saving (XML, Binary, Json)
/// </summary>
public enum TypeSave
{
    XML,
    Binary,
    Json
}