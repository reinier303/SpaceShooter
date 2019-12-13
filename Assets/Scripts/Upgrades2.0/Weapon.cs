using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private List<Module> modules;

    public virtual void Fire()
    {
        //This method is meant to be overriden
    }
}

public struct Module
{
    string stat;
    bool unlocked;
    int cap;
}
