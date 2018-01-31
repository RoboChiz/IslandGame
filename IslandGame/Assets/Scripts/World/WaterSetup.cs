using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSetup : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        //Make Copy
        GameObject volumeGameobject = Instantiate(gameObject, transform.position, transform.rotation, transform.parent);
        volumeGameobject.name = "Water_Volume";
        DestroyImmediate(volumeGameobject.GetComponent<WaterSetup>());
        DestroyImmediate(volumeGameobject.GetComponent<MeshCollider>());
        volumeGameobject.AddComponent<WaterBody>();

        //Setup This
        Destroy(GetComponent<MeshRenderer>());
        Destroy(GetComponent<MeshFilter>());
        gameObject.layer = 4;

        //Setup Copy
        volumeGameobject.layer = 2;
        BoxCollider copyBox = volumeGameobject.GetComponent<BoxCollider>();
        copyBox.isTrigger = true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
