using UnityEngine;

public class ThemeManager : MonoBehaviour
{
    public Light directionLight;
    public void SetLightingAccordingToLevel(int level)
    {
        if (level == 1)
        {
            ChangeLight(true);
        }
        else if (level == 2)
        {
            ChangeLight(false);
        }
    }
    public void ChangeLight(bool isDark)
    {
        if (isDark)
        {
            RenderSettings.ambientIntensity = 0;
            RenderSettings.reflectionIntensity = 0;
            directionLight.intensity = 0;
        }
        else
        {
            RenderSettings.ambientIntensity = 1;
            RenderSettings.reflectionIntensity = 1;
            directionLight.intensity = 1;
        }
    }
}
