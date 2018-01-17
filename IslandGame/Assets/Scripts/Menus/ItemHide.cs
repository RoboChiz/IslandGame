using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Hide a Button depending on the number of items actually in the game (DEBUG FEATURE)
/// </summary>
public class ItemHide : MonoBehaviour
{
    public int uniqueID;

    // Use this for initialization
    public void UpdateAvailablilty()
    {
         BuildingPartDatabaseManager buildingPartDatabaseManager = FindObjectOfType<BuildingPartDatabaseManager>();

        if(buildingPartDatabaseManager != null)
        {
            if(uniqueID > buildingPartDatabaseManager.GetBuildingPartCount())
            {
                gameObject.SetActive(false);
                Debug.Log(uniqueID + " doesn't exist!");
            }
            else
            {
                transform.GetChild(0).GetComponent<Image>().sprite = buildingPartDatabaseManager.GetBuildingPart(uniqueID).icon;
            }
        }
	}

}
