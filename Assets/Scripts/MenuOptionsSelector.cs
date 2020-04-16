using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class MenuOptionsSelector : VerticalMenuSelector
{
    // configuration
    [SerializeField] public TMP_Text gameModeText;
    
    // status
    private readonly string MENU_OPTION_BACK = "MenuOptionBack";
    private readonly string MENU_OPTION_GAME_MODE = "MenuOptionMode";
    
    private readonly string[] _gameModes = new string[]{"easy", "normal", "insane"};
    private int _selectedGameModeIndex = 0;
    
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
        // up and down selection is always allowed
        this.HandleUpDownArrowPresses();
        
        // left and right selection only for the 'game mode' option of the menu
        if (this.GetCurrentMenu().name == this.MENU_OPTION_GAME_MODE) 
            this.HandleLeftRightArrowPresses();
        
        // ENTER is only allowed for the 'back' option of the menu
        if (this.GetCurrentMenu().name == this.MENU_OPTION_BACK)
            this.HandleReturn();
    }
    
    /**
     * Handles ENTER from users. The only ENTER option supported is for the 'back' option which leads
     * the user back to the start menu scene.
     */
    private void HandleReturn()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            this.sceneLoader.LoadStartScene();
    }

    /**
     * Handles left, right arrows presses from users. Only applies if the current selected menu option
     * is the 'game mode' which allows the user to change how hard the game will be. 
     */
    private void HandleLeftRightArrowPresses()
    {
        var maxOptionIndex = this._gameModes.Length - 1;
        
        if (Input.GetKeyDown(KeyCode.RightArrow))
            this._selectedGameModeIndex++;
        
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            this._selectedGameModeIndex--;
        this._selectedGameModeIndex = Mathf.Clamp(_selectedGameModeIndex, 0, maxOptionIndex);
        
        gameModeText.text = this._gameModes[this._selectedGameModeIndex];
    }
}
