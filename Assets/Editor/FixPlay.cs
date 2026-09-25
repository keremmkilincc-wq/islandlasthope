using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
public class FixPlay
{
    [MenuItem("Island/Fix - Play Görünümü Düzelt")]
    public static void Fix()
    {
        var scene = EditorSceneManager.OpenScene("Assets/Scenes/Main.unity", OpenSceneMode.Single);
        // 1. Kamera arka planını gökyüzü mavisi yap (bembeyaz olmasın)
        var cam = Camera.main;
        if (cam != null)
        {
            cam.clearFlags = CameraClearFlags.SolidColor;
            cam.backgroundColor = new Color(0.53f, 0.81f, 0.92f);
            cam.farClipPlane = 500;
        }
        RenderSettings.ambientIntensity = 0.6f;
        RenderSettings.fog = true;
        RenderSettings.fogColor = new Color(0.7f, 0.85f, 0.9f);
        RenderSettings.fogDensity = 0.004f;
        // 2. Player'ı ağaçların dışına güvenli noktaya al
        var player = GameObject.Find("Player");
        if (player != null) player.transform.position = new Vector3(0, 3, -12);
        // 3. Mara'yı Player yakınına koy (E ile konuşma testi)
        var mara = GameObject.Find("Mara");
        if (mara != null && player != null) mara.transform.position = player.transform.position + new Vector3(2, -2, 3);
        // 4. Güneş ışığını yumuşat (aşırı beyaz patlamasın)
        var sun = GameObject.Find("Directional Light");
        if (sun != null)
        {
            var l = sun.GetComponent<Light>();
            if (l != null) l.intensity = 0.9f;
        }
        EditorSceneManager.MarkSceneDirty(scene);
        EditorSceneManager.SaveScene(scene);
        Debug.Log("✅ FixPlay: kamera mavisi, Player güvenli noktada, Mara yanında. Play'e bas!");
    }
}
