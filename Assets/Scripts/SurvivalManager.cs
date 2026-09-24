using UnityEngine;

/// <summary>
/// Açlık/susuzluk/sıcaklık tick - her 0.5s güncellenir
/// SON IŞIK'taki pil/stamina mantığının ada versiyonu
/// </summary>
public class SurvivalManager : MonoBehaviour
{
    [Header("Stats 0-100")]
    [Range(0, 100)] public float health = 100f;
    [Range(0, 100)] public float hunger = 100f;
    [Range(0, 100)] public float thirst = 100f;
    [Range(0, 100)] public float temperature = 80f;

    [Header("Rates /s")]
    public float hungerRate = 0.08f;
    public float thirstRate = 0.12f;
    public float coldDamage = 0.5f;

    [Header("Refs")]
    public Transform campfire;
    public DayNightCycle dayNight;

    private float tickTimer;

    void Update()
    {
        tickTimer += Time.deltaTime;
        if (tickTimer < 0.5f) return;
        tickTimer = 0;

        bool isNight = dayNight ? dayNight.IsNight : false;
        float nearFire = IsNearFire() ? 1f : 0f;

        float hMul = (isNight && nearFire > 0) ? 0.5f : 1f;
        hunger = Mathf.Max(0, hunger - hungerRate * 0.5f * hMul);
        thirst = Mathf.Max(0, thirst - thirstRate * 0.5f * hMul);

        if (isNight)
        {
            if (nearFire > 0) temperature = Mathf.Min(100, temperature + 1f * 0.5f);
            else temperature = Mathf.Max(0, temperature - 0.5f * 0.5f);
        }
        else
        {
            temperature = Mathf.Min(100, temperature + 0.2f * 0.5f);
        }

        if (hunger <= 0) health -= 1f * 0.5f;
        if (thirst <= 0) health -= 1.5f * 0.5f;
        if (temperature < 20) health -= coldDamage * 0.5f;

        health = Mathf.Clamp(health, 0, 100);
        if (health <= 0) OnDeath();
    }

    bool IsNearFire()
    {
        if (!campfire) return false;
        return Vector3.Distance(transform.position, campfire.position) < 18f;
    }

    void OnDeath()
    {
        Debug.Log("☠️ ÖLDÜN - Island Last Hope");
        enabled = false;
    }

    public void Eat(float amount) { hunger = Mathf.Min(100, hunger + amount); }
    public void Drink(float amount) { thirst = Mathf.Min(100, thirst + amount); }
    public void Heal(float amount) { health = Mathf.Min(100, health + amount); }
}
