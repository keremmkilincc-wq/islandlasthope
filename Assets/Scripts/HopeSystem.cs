using UnityEngine;

/// <summary>
/// Umut (moral) sistemi: 0-100. Mara ile etkileşim + umut, yalnız gece - umut
/// </summary>
public class HopeSystem : MonoBehaviour
{
    [Range(0, 100)] public float hope = 70f;
    public DayNightCycle70 dayNight;
    public Transform maraTransform;
    public float nearMaraDistance = 12f;

    void Update()
    {
        bool isNight = dayNight ? dayNight.IsNight : false;
        bool nearMara = maraTransform && Vector3.Distance(transform.position, maraTransform.position) < nearMaraDistance;

        if (isNight && !nearMara) hope = Mathf.Max(0, hope - 0.05f * Time.deltaTime * 10f);
        else if (nearMara) hope = Mathf.Min(100, hope + 0.08f * Time.deltaTime * 10f);

        if (hope <= 0) Debug.Log("💔 Umut bitti - ekran gri");
    }

    public void AddHope(float v) { hope = Mathf.Clamp(hope + v, 0, 100); Debug.Log($"✨ Umut +{v} → {hope}"); }
}
