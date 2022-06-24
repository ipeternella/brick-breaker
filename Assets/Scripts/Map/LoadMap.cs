using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadMap : MonoBehaviour
{
    List<BlockPosition> blockPosition = new List<BlockPosition>();  
    public GameObject[] prefap;
    public int _typeBlock;
    public int map;

    public void nextMap() 
    {
        map++;
        TextAsset questdata = Resources.Load<TextAsset>("Level_" + map.ToString()); //lấy dữ liệu từ Level_1 csv. Level_1 csv phải chứa trong Resources để truyền vào
        loadMap(questdata);
    }
    public void LoadMapByName(int level)
    {
        map = level;
        TextAsset questdata = Resources.Load<TextAsset>("Level_"+ level.ToString());
        loadMap(questdata);
        GameSession.Instance.StartGameSession();
    }
    private void loadMap(TextAsset questdata)
    {  
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
        }       
        // Cắt phần dưới ra vì mình đã có danh sách và suất Block ra theo poop ko dùng Instantiate nữa
       foreach (var NumberCSV in blockPosition)
       {
            if (NumberCSV.typeBlock == -1) _typeBlock = 0;           
            if (NumberCSV.typeBlock == 1) _typeBlock = 1;
            if (NumberCSV.typeBlock == 2) _typeBlock = 2;
            if (NumberCSV.typeBlock != 0)
            {
                GameObject blog = Instantiate(prefap[_typeBlock], new Vector2(NumberCSV.positionX, NumberCSV.positionY), Quaternion.identity);
            }
        }
    }
}
