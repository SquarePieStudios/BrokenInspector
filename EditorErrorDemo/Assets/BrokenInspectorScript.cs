using UnityEngine;
using System;
using System.Collections.Generic;

public class BrokenInspectorScript : MonoBehaviour {
    public BrokenInspectorObject ObjectToBreak;
}

[Serializable]
public class BrokenInspectorObject {
    [SerializeField]
    public List<char> CharList1 = new List<char>();
    //Comment out CharList2 and the editor script works fine.
    public List<char> CharList2 = new List<char>();
}