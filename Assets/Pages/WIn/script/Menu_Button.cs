using UnityEngine;

public class Menu_Button : MonoBehaviour
{
    public void OnMenuButtonClicked(){
        // Reload this scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}