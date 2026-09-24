using UnityEngine;

/// <summary>
/// Alet/silah yönetimi. Animasyonlu 2 dosya (Adventurer, Pistol) hazır Animator kullanır,
/// diğer 5 statik için BobEffect + WeaponController tetikler.
/// 1: Axe, 2: Pickaxe, 3: Dagger, 4: Tools, 5: Pistol
/// </summary>
public class WeaponController : MonoBehaviour
{
    [Header("Refs - Inspector'da bağla")]
    public GameObject axe;      // Axe.fbx - BobEffect Swing
    public GameObject pickaxe;  // Pickaxe.fbx - BobEffect Swing
    public GameObject dagger;   // Dagger.fbx - BobEffect BobRotate
    public GameObject tools;    // Tools.fbx - BobEffect Float
    public GameObject pistol;   // Pistol.fbx - ZATEN ANIMASYONLU (Animator)

    private GameObject current;

    void Start()
    {
        // Başlangıçta balta
        Equip(axe);
        // Not: Adventurer (karakter) ve Pistol zaten animasyonlu, onlara BobEffect EKLEME
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) Equip(axe);
        if (Input.GetKeyDown(KeyCode.Alpha2)) Equip(pickaxe);
        if (Input.GetKeyDown(KeyCode.Alpha3)) Equip(dagger);
        if (Input.GetKeyDown(KeyCode.Alpha4)) Equip(tools);
        if (Input.GetKeyDown(KeyCode.Alpha5)) Equip(pistol);

        if (Input.GetMouseButtonDown(0) && current)
        {
            // Statiklerde basit scale punch, Pistol'de Animator trigger
            if (current == pistol)
            {
                var anim = current.GetComponent<UnityEngine.Animator>();
                if (anim) anim.SetTrigger("Fire"); // Pistol.fbx içindeki Fire clip
            }
            else
            {
                // BobEffect olanlarda hızlı swing simülasyonu
                current.transform.localScale = Vector3.one * 1.15f;
                Invoke(nameof(ResetScale), 0.12f);
            }
        }
    }

    void Equip(GameObject go)
    {
        if (axe) axe.SetActive(false);
        if (pickaxe) pickaxe.SetActive(false);
        if (dagger) dagger.SetActive(false);
        if (tools) tools.SetActive(false);
        if (pistol) pistol.SetActive(false);
        current = go;
        if (current) current.SetActive(true);
    }

    void ResetScale()
    {
        if (current) current.transform.localScale = Vector3.one;
    }
}
