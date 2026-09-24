using UnityEngine;

/// <summary>
/// 20dk döngü: 12dk gündüz + 8dk gece. Directional light + fog lerp
/// </summary>
public class DayNightCycle : MonoBehaviour
{
    [Header("Cycle")]
    public float dayDuration = 720f;
    public float nightDuration = 480f;
    public Light sun;
    public float dayIntensity = 1.0f;
    public float nightIntensity = 0.05f;

    [Header("Fog")]
    public Color dayFog = new Color(0.6f, 0.75f, 0.85f);
    public Color nightFog = new Color(0.02f, 0.03f, 0.08f);

    private float timer;
    public int dayCount = 1;
    public bool IsNight { get; private set; }

    void Update()
    {
        timer += Time.deltaTime;
        float cycle = dayDuration + nightDuration;
        float t = timer % cycle;
        IsNight = t >= dayDuration;

        float targetIntensity = IsNight ? nightIntensity : dayIntensity;
        if (sun) sun.intensity = Mathf.Lerp(sun.intensity, targetIntensity, Time.deltaTime * 0.5f);

        RenderSettings.fogColor = IsNight ? Color.Lerp(dayFog, nightFog, 0.85f) : dayFog;
        RenderSettings.fogDensity = IsNight ? 0.018f : 0.006f;

        if (timer > cycle)
        {
            timer = 0;
            dayCount++;
            Debug.Log($"🌅 Gün {dayCount} başladı");
        }
    }
}
