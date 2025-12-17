using UnityEngine;

public class MenuController : MonoBehaviour
{ 
    [SerializeField] private SettingsController _menuController;
    public void Quit()
    {
        Debug.Log("SYFM");
        Application.Quit();
    }

    public void OpenSettings()
    { 
    
    }
   
}
