using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
   // loads next scene based on the scene ordering defined on Unity > build settings
   public void LoadNextScene()
   {
      int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
      SceneManager.LoadScene(currentSceneIndex + 1);
   }

   // always the 0 indexed scene
   public void LoadStartScene()
   {
      SceneManager.LoadScene(0);
   }

   public void Quit()
   {
      Application.Quit();
   }
}
