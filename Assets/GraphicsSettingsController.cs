using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class GraphicsSettingsController : MonoBehaviour
{
    public Toggle vsyncToggle;
    public Toggle fullscreenToggle;

    void Start()
    {
        vsyncToggle.isOn = QualitySettings.vSyncCount > 0;
        fullscreenToggle.isOn = Screen.fullScreen;

        vsyncToggle.onValueChanged.AddListener(SetVSync);
        fullscreenToggle.onValueChanged.AddListener(ToggleFullscreen);
    }



    public void SetVSync(bool isEnabled)
    {
        QualitySettings.vSyncCount = isEnabled ? 1 : 0;
    }

    public void ToggleFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
