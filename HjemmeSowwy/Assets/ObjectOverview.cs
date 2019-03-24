using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ObjectOverview : MonoBehaviour
{
    
    //List<HidingObject> hidingObjects;
    public GameObject[] objs;
    public int objIndex = 0;
    GameObject currentShownObject;
    public float objectScale = 200f;

    private GameManager gameManager;
    public int chosenObjectID;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Managers").GetComponent<GameManager>();
        objs = Resources.LoadAll<GameObject>("HidingObjects");
        currentShownObject = Instantiate(objs[objIndex], this.transform.position, this.transform.rotation) as GameObject;
        currentShownObject.transform.parent = this.transform;
        currentShownObject.transform.localScale = new Vector3(currentShownObject.transform.localScale.x * objectScale, currentShownObject.transform.localScale.y * objectScale, currentShownObject.transform.localScale.z * objectScale);

    }

    void Update()
    {
        currentShownObject.transform.eulerAngles = new Vector3 (-90f , currentShownObject.transform.eulerAngles.y+Mathf.Sin(currentShownObject.transform.eulerAngles.y)*10, currentShownObject.transform.eulerAngles.z);
    }

    public void NextHidingObject() {
        Destroy(currentShownObject);
        if (objIndex < objs.Length-1) {
            objIndex++;
        } else {
            objIndex = 0;
        }
        currentShownObject = Instantiate(objs[objIndex], this.transform.position, this.transform.rotation) as GameObject;
        currentShownObject.transform.parent = this.transform;
        currentShownObject.transform.localScale = new Vector3(currentShownObject.transform.localScale.x * objectScale, currentShownObject.transform.localScale.y * objectScale, currentShownObject.transform.localScale.z * objectScale);
    }

    public void PreviousHidingObject() {
        Destroy(currentShownObject);
        if (objIndex <= 0) {
            objIndex = objs.Length-1;
        } else {
            objIndex--;
        }
        currentShownObject = Instantiate(objs[objIndex], this.transform.position, this.transform.rotation) as GameObject;
        currentShownObject.transform.parent = this.transform;
        currentShownObject.transform.localScale = new Vector3(currentShownObject.transform.localScale.x * objectScale, currentShownObject.transform.localScale.y * objectScale, currentShownObject.transform.localScale.z * objectScale);
    }

    public void SetHidingObject() {
        chosenObjectID = objs[objIndex].GetComponent<HidingObject>().id;
        gameManager.SetHidingObject(chosenObjectID);
    }

}
