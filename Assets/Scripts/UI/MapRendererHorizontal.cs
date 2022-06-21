using EnhancedUI.EnhancedScroller;

namespace UI.LevelMap
{
    public class MapRendererHorizontal : EnhancedScrollerCellView
    {
        public static readonly int MAXItem = 5;
        public MapRenderer[] mapRenderers = new MapRenderer[MAXItem];

        public void Render(int index)
        {
            index *= MAXItem;
            MapManager mapManager = MapManager.Instance;
            int increaseValue;
            int lineIndex;
            if (index % 2 != 0) // 4 % 2  =  phần nguyên = 2, phần dư = 0
            {
                // If Row Index is Odd, => Flow is Right to Left 
                index += MAXItem - 1;
                increaseValue = -1;
                // Line Index = Start Index, Flow is Right to Left, so Line Index in Right
                lineIndex = MAXItem - 1;
            }
            else
            {
                // If Row Index is Odd, => Flow is Left to Right 
                increaseValue = 1;
                // Line Index = Start Index, Flow is Left to Right, so Line Index in Left
                lineIndex = 0;
            }

            for (int i = 0; i < MAXItem; i++)
            {
                if (index < mapManager.Maps.Count && index >= 0)
                {
                    Map map = mapManager.Maps[index];
                    mapRenderers[i].SetLevel(map.level);
                    mapRenderers[i].SetStar(map.starCount);
                    mapRenderers[i].SetLock(map.isLock);
                    // If Line is first line or last line will don't render it!
                    mapRenderers[i].SetLine(i == lineIndex && map.level != 1 && map.level != mapManager.MapCount - 1);
                }

                index += increaseValue;
            }
        }
    }
}