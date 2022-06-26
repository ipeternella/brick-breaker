using UnityEngine;
using UnityEngine.UI;

namespace UI.LevelMap
{
    public class StarRenderer : MonoBehaviour
    {
        [SerializeField] private Image[] stars;

        private void Start()
        {
            // ResetStar();
        }

        private void ResetStar()
        {
            foreach (var star in stars)
            {
                star.enabled = false;
            }
        }

        public void Render(int starCount = 0)
        {
            starCount = Mathf.Clamp(starCount, 0, stars.Length);
            ResetStar();
            for (int i = 0; i < starCount; i++)
            {
                stars[i].enabled = true;
            }
        }
    }
}