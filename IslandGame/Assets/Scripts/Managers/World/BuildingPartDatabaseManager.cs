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

    public BuildingPart GetBuildingPart(int _id)
    {
        return buildingPartDatabase[_id - 1];
    }

    public int GetBuildingPartCount()
    {
        if (buildingPartDatabase != null)
        {
            return buildingPartDatabase.Count;
        }
        else
        {
            return 0;
        }
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
                if (id != buildingPartDatabase.Count + 1)
                {
                    throw new Exception("Dialogue ID's are an incorrect Order");
                }

                //Make the Class
                BuildingPart buildingPart = new BuildingPart(id);

                buildingPart.partName = xmlNode.Attributes["partname"].Value;
                buildingPart.gridSize = int.Parse(xmlNode.Attributes["gridsize"].Value);

                buildingPart.prefabResourceName = xmlNode.Attributes["resource"].Value;
                buildingPart.prefab = Resources.Load<GameObject>(buildingPart.prefabResourceName);

                buildingPart.iconResourceName = xmlNode.Attributes["icon"].Value;
                buildingPart.icon = Resources.Load<Sprite>(buildingPart.iconResourceName);

                buildingPart.partType = BuildingPart.PartType.Max;

                if(xmlNode.Attributes["type"] != null)
                {
                    switch(xmlNode.Attributes["type"].Value)
                    {
                        case "base": buildingPart.partType = BuildingPart.PartType.Base; break;
                    }
                }

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

    public enum PartType { Base, Max};
    public PartType partType;

    //Prefab Data
    public string prefabResourceName;
    public GameObject prefab;

    public string iconResourceName;
    public Sprite icon;

    public BuildingPart(int _uniqueID)
    {
        uniqueID = _uniqueID;
    }

}
