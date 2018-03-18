using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffHelper : MonoBehaviour
{
    public List<GameObject> turnOn, turnOff;

	// Use this for initialization
	void Awake ()
    {
		foreach(GameObject gameObject in turnOn)
        {
            gameObject.SetActive(true);
        }

        foreach (GameObject gameObject in turnOff)
        {
            gameObject.SetActive(false);
        }
    }
}
