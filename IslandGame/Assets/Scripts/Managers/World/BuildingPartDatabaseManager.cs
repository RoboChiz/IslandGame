using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class BuildingPartDatabaseManager : MonoBehaviour
{
    private List<BuildingPart> buildingPartDatabase;

    private void Start()
    {
        LoadDatabase("ItemDatabase");
    }

    /// <summary>
    /// Load the Building Part Database from an XML File
    /// </summary>
    public void LoadDatabase(string fileName)
    {
        buildingPartDatabase = new List<BuildingPart>();

        //Get the Text File
        TextAsset textAsset = Resources.Load<TextAsset>(fileName);
        if (textAsset == null)
        {
            throw new Exception("Dialogue file \"" + fileName + "\" dosen't exist.");
        }

        XmlDocument xmldoc = new XmlDocument();
        xmldoc.LoadXml(textAsset.text);

        foreach (XmlNode xmlNode in xmldoc.DocumentElement.ChildNodes)
        {
            Debug.Log("Loading Group: " + xmlNode.Name);

            if(xmlNode.Name == "BuildingPart")
            {
                int id = int.Parse(xmlNode.Attributes["id"].Value);
                if (id != buildingPartDatabase.Count)
                {
                    throw new Exception("Dialogue ID's are an incorrect Order");
                }

                //Make the Class
                BuildingPart buildingPart = new BuildingPart(id);

                buildingPart.partName = xmlNode.Attributes["partname"].Value;
                buildingPart.gridSize = int.Parse(xmlNode.Attributes["gridsize"].Value);

                buildingPart.prefabResourceName = xmlNode.Attributes["resource"].Value;
                buildingPart.prefab = Resources.Load<GameObject>(buildingPart.prefabResourceName);

                buildingPartDatabase.Add(buildingPart);

                Debug.Log("Loaded #" + id + ": Name:" + buildingPart.partName + " " + (buildingPart.prefab != null ? buildingPart.prefabResourceName : "{NO PREFAB}"));
            }
        }
    }
}

public class BuildingPart
{
    public int uniqueID { get; private set; }
    public int gridSize;
    public string partName;

    //Prefab Data
    public string prefabResourceName;
    public GameObject prefab;

    public BuildingPart(int _uniqueID)
    {
        uniqueID = _uniqueID;
    }

}
