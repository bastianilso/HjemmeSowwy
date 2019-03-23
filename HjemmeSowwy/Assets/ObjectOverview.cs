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
    public float objectScale = 1f;

    private GameManager gameManager;
    public string chosenObjectID;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Managers").GetComponent<GameManager>();
        objs = Resources.LoadAll<GameObject>("HidingObjects");
        currentShownObject = Instantiate(objs[objIndex], this.transform.position, this.transform.rotation) as GameObject;
        currentShownObject.transform.parent = this.transform;
        currentShownObject.transform.localScale = new Vector3(objectScale, objectScale, objectScale);
    }

    void Update()
    {
        currentShownObject.transform.eulerAngles = new Vector3 (currentShownObject.transform.eulerAngles.x, currentShownObject.transform.eulerAngles.y+0.3f, currentShownObject.transform.eulerAngles.z);
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
        currentShownObject.transform.localScale = new Vector3(objectScale, objectScale, objectScale);
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
        currentShownObject.transform.localScale = new Vector3(objectScale, objectScale, objectScale);
    }

    public void SetHidingObject() {
        chosenObjectID = objs[objIndex].GetComponent<HidingObject>().id;
        gameManager.SetHidingObject(chosenObjectID);
    }

}
