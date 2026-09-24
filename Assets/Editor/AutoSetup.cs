using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.AI;

public class AutoSetup
{
    [MenuItem("Island/Setup Scene (70 Gün)")]
    public static void Setup()
    {
        var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        // Directional Light
        var lightGO = new GameObject("Directional Light");
        var light = lightGO.AddComponent<Light>();
        light.type = LightType.Directional;
        light.intensity = 1f;
        lightGO.transform.rotation = Quaternion.Euler(50, -30, 0);
        lightGO.AddComponent<DayNightCycle70>().sun = light;

        // Swamp Island
        var swampPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Models/Environment/SwampIsland/model.obj");
        GameObject swamp = null;
        if (swampPrefab != null)
        {
            swamp = (GameObject)PrefabUtility.InstantiatePrefab(swampPrefab);
            swamp.name = "SwampIsland";
            var col = swamp.GetComponent<MeshCollider>();
            if (!col) col = swamp.AddComponent<MeshCollider>();
            // Add BobEffect WindSway
            var bob = swamp.GetComponent<BobEffect>();
            if (!bob) bob = swamp.AddComponent<BobEffect>();
            bob.mode = BobEffect.Mode.WindSway;
            bob.speed = 0.4f;
            // Static for NavMesh
            GameObjectUtility.SetStaticEditorFlags(swamp, StaticEditorFlags.NavigationStatic);
        }

        // Player - SEN (Adventurer)
        var playerGO = new GameObject("Player");
        playerGO.tag = "Player";
        var cc = playerGO.AddComponent<CharacterController>();
        cc.height = 1.8f; cc.center = new Vector3(0, 0.9f, 0);
        playerGO.AddComponent<PlayerController70>();
        var hope = playerGO.AddComponent<HopeSystem>();
        var weapon = playerGO.AddComponent<WeaponController>();
        playerGO.transform.position = new Vector3(0, 2, 0);
        // Adventurer - SENİN karakterin (animasyonlu, BobEffect YOK)
        var advPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Models/Characters/Adventurer/Adventurer.fbx");
        if (advPrefab != null)
        {
            var adv = (GameObject)PrefabUtility.InstantiatePrefab(advPrefab);
            adv.name = "Adventurer_Player";
            adv.transform.SetParent(playerGO.transform);
            adv.transform.localPosition = new Vector3(0, -0.9f, 0);
            adv.transform.localRotation = Quaternion.identity;
            adv.transform.localScale = Vector3.one * 0.7f;
            // NO BobEffect - zaten animasyonlu
        }

        var camRoot = new GameObject("CameraRoot");
        camRoot.transform.SetParent(playerGO.transform);
        camRoot.transform.localPosition = new Vector3(0, 1.6f, 0);
        var pc70 = playerGO.GetComponent<PlayerController70>();
        pc70.cameraRoot = camRoot.transform;

        var cam = Camera.main;
        if (!cam) cam = new GameObject("Main Camera").AddComponent<Camera>();
        cam.transform.SetParent(camRoot.transform);
        cam.transform.localPosition = Vector3.zero;
        cam.transform.localRotation = Quaternion.identity;
        cam.tag = "MainCamera";
        var fLight = cam.gameObject.AddComponent<Light>();
        fLight.type = LightType.Spot; fLight.spotAngle = 40; fLight.range = 25; fLight.intensity = 2.5f;
        fLight.enabled = true;
        pc70.flashlight = fLight;

        // Weapons as children of CameraRoot (FPS view)
        SetupWeapon("Axe", "Assets/Models/Weapons/Axe/Axe.fbx", camRoot.transform, weapon, ref weapon.axe, BobEffect.Mode.Swing);
        SetupWeapon("Pickaxe", "Assets/Models/Weapons/Pickaxe/Pickaxe.fbx", camRoot.transform, weapon, ref weapon.pickaxe, BobEffect.Mode.Swing);
        SetupWeapon("Dagger", "Assets/Models/Weapons/Dagger/Dagger.fbx", camRoot.transform, weapon, ref weapon.dagger, BobEffect.Mode.BobRotate);
        SetupWeapon("Tools", "Assets/Models/Tools/Tools.fbx", camRoot.transform, weapon, ref weapon.tools, BobEffect.Mode.Float);
        SetupWeaponPistol("Pistol", "Assets/Models/Weapons/Pistol/Pistol.fbx", camRoot.transform, weapon, ref weapon.pistol);

        // HopeSystem refs
        // Mara
        var maraGO = new GameObject("Mara");
        maraGO.transform.position = new Vector3(10, 0, 10);
        var agent = maraGO.AddComponent<NavMeshAgent>();
        agent.speed = 2.5f;
        var mara = maraGO.AddComponent<NPCMara>();
        mara.player = playerGO.transform;
        mara.hopeSystem = hope;
        // Simple visual for Mara (capsule)
        var cap = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        cap.transform.SetParent(maraGO.transform);
        cap.transform.localPosition = Vector3.zero;
        hope.maraTransform = maraGO.transform;
        hope.dayNight = lightGO.GetComponent<DayNightCycle70>();

        // Ground for NavMesh
        var ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
        ground.name = "Ground_NavMesh";
        ground.transform.position = new Vector3(0, 0, 0);
        ground.transform.localScale = new Vector3(15, 1, 15);
        ground.isStatic = true;
        GameObjectUtility.SetStaticEditorFlags(ground, StaticEditorFlags.NavigationStatic);

        EditorSceneManager.MarkSceneDirty(scene);
        EditorSceneManager.SaveScene(scene, "Assets/Scenes/Main.unity");
        Debug.Log("✅ Island Last Hope sahnesi hazır! Play'e basabilirsin. 70 gün, Mara, silahlar bağlandı.");
    }

    static void SetupWeapon(string name, string path, Transform parent, WeaponController wc, ref GameObject field, BobEffect.Mode mode)
    {
        var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
        if (!prefab) { Debug.LogWarning("Bulunamadı: " + path); return; }
        var go = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
        go.name = name;
        go.transform.SetParent(parent);
        go.transform.localPosition = new Vector3(0.3f, -0.2f, 0.6f);
        go.transform.localRotation = Quaternion.identity;
        go.transform.localScale = Vector3.one * 0.8f;
        var bob = go.AddComponent<BobEffect>();
        bob.mode = mode;
        bob.amplitude = 0.08f;
        bob.speed = 1.1f;
        go.SetActive(false);
        field = go;
    }

    static void SetupWeaponPistol(string name, string path, Transform parent, WeaponController wc, ref GameObject field)
    {
        var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
        if (!prefab) { Debug.LogWarning("Bulunamadı: " + path); return; }
        var go = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
        go.name = name;
        go.transform.SetParent(parent);
        go.transform.localPosition = new Vector3(0.3f, -0.2f, 0.6f);
        go.transform.localRotation = Quaternion.identity;
        go.transform.localScale = Vector3.one * 0.5f;
        // NO BobEffect for pistol - zaten animasyonlu
        go.SetActive(false);
        field = go;
    }
}
