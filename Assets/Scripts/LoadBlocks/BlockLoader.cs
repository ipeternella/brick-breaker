using UnityEngine;
using UnityEngine.AddressableAssets.Pooling;
using System.Collections;
using System.Collections.Generic;// phải có thư viện này mới public list

public class BlockLoader : MonoBehaviour
{ }
//    //private static BlockLoader _instance;
//    //public static BlockLoader Instance => _instance;

//    [SerializeField] private AddressableGameObjectSpawner spawner;

//    //list bắt đầu từ 0
//    //khi băt đầu scene levelmap thì 
//    public int _typeBlock;
//    public bool delete;

//    public async void StartLoadBlock()
//    {

//        await spawner.InitializeAsync();// né thằng này ra // nó lưu lại các dòng phía dưới 
//        //foreach (var NumberCSV in blocks)
//        for (int i = 0; i < blocks.Count; i++)
//        {
//            var _blocks = blocks[i];
//                //if (blocks[i].typeBlock == 0)
//                //{
//                //    blocks.Remove(blocks[i]);
//                //}
//                //if (blocks[i].typeBlock == -1) _typeBlock = 0;
//                //if (blocks[i].typeBlock == 1) _typeBlock = 1;
//                //if (blocks[i].typeBlock == 2) _typeBlock = 2;
//                if (blocks[i].typeBlock == -1)
//                {
//                var block = await spawner.GetAsync("Type1");// + _typeBlock.ToString());//

//                    block.transform.position = new Vector2(blocks[i].positionX, blocks[i].positionY);
//                    //blocks.Remove(blocks[i]);//bị xoá trước khi kịp nhân bản

//            }



//                Debug.Log(blocks[i].typeBlock);
//            }
//    }

//    public void Return(GameObject deleteBlock)// truyền Block để ẩn đi ở đây
//    {
//        spawner.Return(deleteBlock);
//    }
//}
