using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPotion : MonoBehaviour
{
    //Prefabs potions drop
    [SerializeField] List<GameObject> listOfPotion;
    [SerializeField] List<GameObject> blocks;
    
    [SerializeField] int choose_number;// 10 là 100%
    private bool on;

    private void Start()
    {
        blocks = new List<GameObject>(GameObject.FindGameObjectsWithTag("Breakable")); //list các block có thể breack
    }
    private void Update()
    {
         
        int random_number = Random.Range(0, 10);

        for (int i = 0; i < blocks.Count; i++)
        {
            if (blocks[i].gameObject.activeInHierarchy == false) //if set active(false)
            {
                //Debug.Log(blocks[i].transform.position);
                if (random_number < choose_number) // cách chỉnh tỉ lệ rớt item
                {                    
                    int index = Random.Range(0, 4); // ramdom potion rớt

                    switch (index)
                    {
                        case 0:
                            Instantiate(listOfPotion[index], blocks[i].transform.position, Quaternion.identity);
                            break;
                        case 1:
                            Instantiate(listOfPotion[index], blocks[i].transform.position, Quaternion.identity);
                            break;
                        case 2:
                            Instantiate(listOfPotion[index], blocks[i].transform.position, Quaternion.identity);
                            break;
                        case 3:
                            Instantiate(listOfPotion[index], blocks[i].transform.position, Quaternion.identity);
                            break;
                    }
                }
                on = true; // công tắc để thực hiện việc xoá block trong mảng đi để không bị update Potion

            }
            if (on == true)
            {
                blocks.Remove(blocks[i]);
                on = false;
            }
        }
    }

}


