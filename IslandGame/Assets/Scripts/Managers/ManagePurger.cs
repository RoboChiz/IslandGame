using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagePurger : MonoBehaviour
{
    private static ManagePurger alpha = null;

	// Use this for initialization
	void Awake ()
    {
        if(alpha == null)
        {
            alpha = this;
        }

        if(this != alpha)
        {
            Debug.Log("I'm less than perfect! Must kill self!");
            DestroyImmediate(gameObject);
        }
	}
	
}
