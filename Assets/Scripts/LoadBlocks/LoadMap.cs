using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets.Pooling;

public class LoadMap : MonoBehaviour
{
    public int type;
    public int map;
    [SerializeField] private AddressableGameObjectSpawner spawner;
   // List<GameObject> Blocks = new List<GameObject>();
    public Transform BlockParent;

    void Start()
    {
        spawner.InitializeAsync();
    }
    public void ReturnAll()
    {
         foreach (Transform block in BlockParent.transform)
        {
            if (block.gameObject.activeInHierarchy == true) spawner.Return(block.gameObject);
        }
    }
    public void nextMap()
    {
        map++;
        TextAsset questdata = Resources.Load<TextAsset>("Level_" + map.ToString()); //lấy dữ liệu từ Level_ csv. Level_1 csv phải chứa trong Resources để truyền vào
        loadMap(questdata);
        FindObjectOfType<Ball>().ballInitialPosition();// ball về vị trí ban đầu
        Debug.Log("nextMap");
    }
    public void LoadMapByName(int level)
    {
        map = level;
        TextAsset questdata = Resources.Load<TextAsset>("Level_" + level.ToString());
        loadMap(questdata);
        GameSession.Instance.StartGameSession();//ball về vị trí ban đầu
        Debug.Log("LoadNameMap");
    }
    public async void loadMap(TextAsset questdata)
    {        
        string[] data = questdata.text.Split(new char[] { '\n' }); //tạo mảng data chứa 13 dòng 
        //Debug.Log(data.Length);// =13 số dòng. dòng cuối rỗng
        for (int i = 1; i < data.Length - 1; i++) // với mỗi dòng i
        {
            string[] row = data[i].Split(new char[] { ',' }); //tạo mảng row chứa 16 ô lấy từ dòng i.
            for (int j = 0; j < row.Length; j++)
            {
                int _Block; //gọi block  a
                int.TryParse(row[j], out _Block); //kiểu số, loại Block// cho block = 1 số

                if (_Block == -1) type = 0;

                if (_Block == 1)
                {
                    FindObjectOfType<LevelController>().IncrementBlocksCounter();
                    type = 1;
                }
                if (_Block == 2)
                {
                    type = 2;
                    FindObjectOfType<LevelController>().IncrementBlocksCounter();
                }
                //Debug.Log(_Block);
                if ((_Block  != 0))// gọi block
                {
                    var block = await spawner.GetAsync(type.ToString());// 
                    block.transform.localPosition = new Vector2(j + 0.5f, 11.5f - i);
                }
            }
        }
    }
    public void Return(GameObject deleteBlock)// truyền Block để ẩn đi ở đây
    {
            spawner.Return(deleteBlock);
    }
}









// Cắt phần dưới ra vì mình đã có danh sách và suất Block ra theo poop ko dùng Instantiate nữa
//foreach (var NumberCSV in blockPosition)
//{
//    if (NumberCSV.typeBlock == -1) _typeBlock = 0;
//    if (NumberCSV.typeBlock == 1) _typeBlock = 1;
//    if (NumberCSV.typeBlock == 2) _typeBlock = 2;
//    if (NumberCSV.typeBlock != 0)
//    {
//        GameObject block = Instantiate(prefap[_typeBlock], new Vector2(NumberCSV.positionX, NumberCSV.positionY), Quaternion.identity);
//    }
//} Danh sach nó sẽ đầy lên
//        StartCoroutine(loadblocks());
//    }


//    private IEnumerator loadblocks() //load level khi bấm bất kỳ
//    {
//        yield return new WaitForSeconds(0.5f);
//        FindObjectOfType<BlockLoader>().StartLoadBlock();
//    }
//}
