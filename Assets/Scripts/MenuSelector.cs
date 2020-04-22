using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;

public class MenuSelector : VerticalMenuSelector
{
    // status
    private readonly string MENU_OPTION_START = "MenuOptionStart";
    private readonly string MENU_OPTION_INSTRUCTIONS = "MenuOptionInstructions";
    private readonly string MENU_OPTION_OPTIONS = "MenuOptionOptions";

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
        var currentMenu = this.GetCurrentMenu();

        if (currentMenu.name == MENU_OPTION_START) 
            this.sceneLoader.LoadSceneByName("Level1");
        
        else if (currentMenu.name == MENU_OPTION_INSTRUCTIONS) 
            this.sceneLoader.LoadSceneByName("InstructionsMenu");
        
        else if (currentMenu.name == MENU_OPTION_OPTIONS) 
            this.sceneLoader.LoadSceneByName("OptionsMenu");

        else 
            this.sceneLoader.Quit();
    }
}
