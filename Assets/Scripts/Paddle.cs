using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField]
    public float minRelativePosX = 15f;  // assumes paddle size of 1 relative unit
    
    [SerializeField]
    public float maxRelativePosX = 1f;  // assumes paddle size of 1 relative unit
    
    [SerializeField]
    public float fixedRelativePosY = .62f;  // paddle does not move on the Y directiob
    
    // Unity units of the WIDTH of the screen (e.g. 16)
    [SerializeField]
    public float screenWidthUnits = 16;

    // Start is called before the first frame update
    void Start()
    {
        float startPosX = ConvertPixelToRelativePosition(screenWidthUnits / 2, Screen.width);
        transform.position = GetUpdatedPaddlePosition(startPosX);
    } 

    // Update is called once per frame
    void Update()
    {
        var relativePosX = ConvertPixelToRelativePosition(pixelPosition: Input.mousePosition.x, Screen.width);
        transform.position = GetUpdatedPaddlePosition(relativePosX);
    }

    public Vector2 GetUpdatedPaddlePosition(float relativePosX)
    {
        Vector2 newPaddlePosition = new Vector2(relativePosX, PADDLE_FIXED_RELATIVE_POS_Y);
        return newPaddlePosition;
    }
    
    public float ConvertPixelToRelativePosition(float pixelPosition, int screenWidth)
    { 
        var relativePosition = pixelPosition/screenWidth * screenWidthUnits;
        return relativePosition;
    }

}