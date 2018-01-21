using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public abstract class ISavingManager : MonoBehaviour
{
    abstract public void DoSave(BinaryWriter _stream);
    abstract public void DoLoad(int _version, BinaryReader _stream);
    virtual public char[] uniqueID { get { return new char[4] { 'I', 'S', 'M','_' }; }
    }
}
