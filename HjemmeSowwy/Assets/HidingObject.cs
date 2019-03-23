using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingObject : MonoBehaviour
{

    public int id;

    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Managers").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseEnter() {
        Debug.Log("MouseEnter");
        var matRenderer = this.GetComponent<MeshRenderer>();
        Material[] mats = new Material[2];
        mats[0] = matRenderer.material;
        mats[1] = Resources.Load<Material>("OutlineMaterial");
        matRenderer.materials = mats;
    }

    public void OnMouseExit() {
        Debug.Log("MouseExit");
        var matRenderer = this.GetComponent<MeshRenderer>();
        Material[] mats = new Material[1];
        mats[0] = matRenderer.material;
        matRenderer.materials = mats;
    }

    public void OnMouseDown() {
        gameManager.SetChosenObject(id);
    }
}
