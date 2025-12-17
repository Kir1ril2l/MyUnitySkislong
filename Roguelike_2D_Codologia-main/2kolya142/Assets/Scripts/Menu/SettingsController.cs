using UnityEngine;

public class SettingsController : MonoBehaviour
{
    [SerializeField] private GameObject _settingsPanel;

    public void ChangeSettingState(bool state)
    { 
        _settingsPanel.SetActive(state);
    }


}
