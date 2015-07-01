using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class BrokenInspectorObject {
    [SerializeField]
    public List<char> CharList1 = new List<char>();
    [SerializeField]
    public List<char> CharList2 = new List<char>();
}