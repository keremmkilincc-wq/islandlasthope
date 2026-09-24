using UnityEngine;

/// <summary>
/// Statik modellere basit animasyon ekler.
/// Adventurer ve Pistol HARİÇ 5 asset için kullanılır:
/// Axe, Pickaxe, Dagger, Tools, SwampIsland (hafif rüzgar)
/// </summary>
public class BobEffect : MonoBehaviour
{
    public enum Mode { BobRotate, Swing, Float, WindSway }
    public Mode mode = Mode.BobRotate;
    public float amplitude = 0.15f;
    public float speed = 1.2f;

    private Vector3 startPos;
    private Vector3 startRot;

    void Start()
    {
        startPos = transform.localPosition;
        startRot = transform.localEulerAngles;
    }

    void Update()
    {
        float t = Time.time * speed;
        switch (mode)
        {
            case Mode.BobRotate:
                transform.localRotation = Quaternion.Euler(startRot + new Vector3(0, Mathf.Sin(t) * 8f, 0));
                transform.localPosition = startPos + new Vector3(0, Mathf.Sin(t) * amplitude, 0);
                break;
            case Mode.Swing:
                // balta/kazma sallanma simülasyonu (idle)
                transform.localRotation = Quaternion.Euler(startRot + new Vector3(Mathf.Sin(t) * 5f, 0, 0));
                break;
            case Mode.Float:
                transform.localPosition = startPos + new Vector3(0, Mathf.Sin(t) * amplitude, Mathf.Cos(t * 0.5f) * amplitude * 0.5f);
                break;
            case Mode.WindSway:
                // SwampIsland için çok hafif sallanma
                transform.localRotation = Quaternion.Euler(startRot + new Vector3(Mathf.Sin(t * 0.3f) * 0.8f, Mathf.Cos(t * 0.2f) * 0.8f, 0));
                break;
        }
    }
}
