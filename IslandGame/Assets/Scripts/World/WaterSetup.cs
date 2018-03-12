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

        foreach (Transform child in volumeGameobject.transform)
        {
            Destroy(child.gameObject);
        }


        //Setup Copy
        volumeGameobject.layer = 2;
        volumeGameobject.AddComponent<WaterBody>();

        BoxCollider copyBox = volumeGameobject.GetComponent<BoxCollider>();
        copyBox.isTrigger = true;

        //Clear Copy
        DestroyImmediate(volumeGameobject.GetComponent<WaterMeshGen>());
        DestroyImmediate(volumeGameobject.GetComponent<WaterSetup>());
        DestroyImmediate(volumeGameobject.GetComponent<MeshCollider>());
        DestroyImmediate(volumeGameobject.GetComponent<MeshRenderer>());
        DestroyImmediate(volumeGameobject.GetComponent<MeshFilter>());

        //Setup This
        gameObject.layer = 4;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
