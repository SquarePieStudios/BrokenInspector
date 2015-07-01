using UnityEngine;
using System;

public class BrokenInspectorScript : MonoBehaviour {
    public BrokenInspectorObject ObjectToBreak;

    private void Start() {
        for (int i = 0; i < ObjectToBreak.CharList1.Count; i++) {
            Debug.LogFormat("ObjectToBreak's CharList1[{0}] = {1}", i, ObjectToBreak.CharList1[i]);
        }
        for (int i = 0; i < ObjectToBreak.CharList2.Count; i++) {
            Debug.LogFormat("ObjectToBreak's CharList2[{0}] = {1}", i, ObjectToBreak.CharList2[i]);
        }
    }
}