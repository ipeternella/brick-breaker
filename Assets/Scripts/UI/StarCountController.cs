using UnityEngine;
using UnityEngine.UI;

namespace UI.LevelMap
{
    public class StarCountController : MonoBehaviour
    {
        [SerializeField] private Text txtStarCount;
        private float StartCount => MapManager.Instance.StarCount;

        public void Render()
        {
            txtStarCount.text = StartCount.ToString();
        }
    }
}
