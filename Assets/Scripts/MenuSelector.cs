using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;

public class MenuSelector : MonoBehaviour
{
    
    // configuration
    [SerializeField] public GameObject[] menuOptions;
    [Range(0, 100)][SerializeField] public int marginLeft = 100;

    // status
    private int _selectedMenuIndex = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.position = GetMenuSelectorPosition(this._selectedMenuIndex);
    }

    private Vector2 GetMenuSelectorPosition(int menuOptionIndex)
    {
        GameObject menuOption = this.menuOptions[menuOptionIndex];
        
        var selectorY = menuOption.transform.position.y;
        Vector2 selectorPosition = new Vector2(transform.position.x, selectorY);

        return selectorPosition;
    }

    // Update is called once per frame
    void Update()
    {
        var maxOptionIndex = this.menuOptions.Length - 1;

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            this._selectedMenuIndex++;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            this._selectedMenuIndex--;
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            GameObject menuOption = this.menuOptions[_selectedMenuIndex];

            if (menuOption.name == "MenuOptionStart")
            {
                FindObjectOfType<SceneLoader>().LoadNextScene();
                
            } else if (menuOption.name == "MenuOptionOptions")
            {
                FindObjectOfType<SceneLoader>().Quit();
            }
            else
            {
                FindObjectOfType<SceneLoader>().Quit();
            }
        }

        // clamping input
        this._selectedMenuIndex = Mathf.Clamp(_selectedMenuIndex, 0, maxOptionIndex);

        // updating position
        transform.position = GetMenuSelectorPosition(this._selectedMenuIndex);
    }
}
