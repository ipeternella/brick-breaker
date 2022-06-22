using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadMap : MonoBehaviour
{
    List<BlockPosition> blockPosition = new List<BlockPosition>();

    public GameObject[] prefap;
    public int y;

    void Start()
    {
        TextAsset questdata = Resources.Load<TextAsset>("Level_1"); //lấy dữ liệu từ Level_1 csv. Level_1 csv phải chứa trong Resources để truyền vào
 
        string[] data = questdata.text.Split(new char[] { '\n'}); //tạo mảng data chứa 13 dòng 
        Debug.Log(data.Length);// =13 số dòng. dòng cuối rỗng
        for (int i =1; i <data.Length -1; i++) // với mỗi dòng i
        {
            string[] row = data[i].Split(new char[] { ',' }); //tạo mảng row chứa 16 ô lấy từ dòng i.
            for (int j =0; j <row.Length; j++)
            {
                BlockPosition _Block = new BlockPosition();
                int.TryParse(row[j], out _Block.typeBlock); //kiểu số, loại Block
                _Block.positionY = 11.5f - i;
                _Block.positionX = j + 0.5f;

                blockPosition.Add(_Block);
            }
       // Debug.Log(row.Length);      
        }
       

       foreach (var x in blockPosition)
       {
            if (x.typeBlock == -1) y = 0;
            if (x.typeBlock == 0) y = 3;
            if (x.typeBlock == 1) y = 1;
            if(x.typeBlock == 2) y = 2;
            //Debug.Log(x.positionY.ToString());

            GameObject blog = Instantiate(prefap[y], new Vector2(x.positionX, x.positionY), Quaternion.identity);
            
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
