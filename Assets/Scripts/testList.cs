using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class testList : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform parent;

     void Update()
    {
        this.x();
    }

    protected virtual void x()
    {    

        foreach (Transform block in parent.transform)
        {
            if (block.gameObject.activeInHierarchy == true) Debug.Log(block.name);
        }
    }
            

}


