using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenuSelector : VerticalMenuSelector
{
    private readonly string MENU_OPTION_GO_AGAIN = "MenuOptionGoAgain";

    /**
     * Before first frame update. 
     */
    void Start()
    {
        transform.position = GetMenuSelectorPosition();
    }

    /**
     * Update per frame.
     */
    void Update()
    {
        // invokes base class up/down arrows handling
        this.HandleUpDownArrowPresses();
        
        // enter case handling
        if (Input.GetKeyDown(KeyCode.Return)) HandleReturn();
    }
    
    /**
     * Handles ENTER pressing that allows the user to choose an option.
     */
    private void HandleReturn()
    {
        GameObject currentMenu = this.GetCurrentMenu();

        if (currentMenu.name == this.MENU_OPTION_GO_AGAIN) 
            this.sceneLoader.LoadStartScene();
        else 
            this.sceneLoader.Quit();
    }
}
