using UnityEngine;

/// <summary>
/// 70 gün döngüsü: 1 gün = 5dk gündüz + 3dk gece = 8dk
/// Umut sistemi ile entegre
/// </summary>
public class DayNightCycle70 : MonoBehaviour
{
    public int totalDays = 70;
    public int currentDay = 1;
    public float dayDuration = 300f; // 5dk
    public float nightDuration = 180f; // 3dk
    public Light sun;

    public bool IsNight { get; private set; }
    public bool IsGameOver => currentDay > totalDays;

    private float timer;

    void Update()
    {
        if (IsGameOver) return;
        timer += Time.deltaTime;
        float cycle = dayDuration + nightDuration;
        float t = timer % cycle;
        IsNight = t >= dayDuration;

        if (sun)
        {
            float target = IsNight ? 0.06f : 1.0f;
            sun.intensity = Mathf.Lerp(sun.intensity, target, Time.deltaTime * 0.6f);
        }
        RenderSettings.fogDensity = IsNight ? 0.019f : 0.007f;

        if (timer > cycle)
        {
            timer = 0;
            currentDay++;
            Debug.Log($"🌅 Gün {currentDay}/{totalDays}");
            if (currentDay == 70) Debug.Log("🚁 SON GÜN! Feneri çalıştır ve kaç!");
            if (IsGameOver) Debug.Log("⏰ 70 gün doldu!");
        }
    }
}
