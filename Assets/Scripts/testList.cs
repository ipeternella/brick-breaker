using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class testList : MonoBehaviour
{
    // Start is called before the first frame update
    public List<string> abc = new List<string>();
    
    



    void Start()
    {
        abc.Add("1");
        abc.Add("2");
        abc.Add("3");
        abc.Add("4");
        abc.Add("5");
        abc.Add("6");
        abc.Add("7");
        abc.Add("8");

        StartCoroutine(loadLevel());
    }
    private IEnumerator loadLevel() //load level khi bấm bất kỳ
    {
        yield return new WaitForSeconds(1f);
        //abc.RemoveAt(2); còn 0
        //abc.Remove(abc[2]); còn 0
        abc.RemoveRange(0,7); // vi trí thứ 0 số lượng 40
    }
}


