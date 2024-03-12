using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveLevelManager : MonoBehaviour
{
    public static WaveLevelManager main;

    public Transform startPoint;
    public Transform[] path;

    public void Awake() {
        main = this;
    }
}
