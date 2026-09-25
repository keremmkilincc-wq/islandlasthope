using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
public class FixScene
{
    [MenuItem("Island/Fix - Ortada Cube Ekle")]
    public static void Fix()
    {
        var scene = EditorSceneManager.GetActiveScene();
        if (!scene.IsValid()) scene = EditorSceneManager.OpenScene("Assets/Scenes/Main.unity");
        // Add a red cube in front of player for test
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.name = "TEST_CUBE";
        cube.transform.position = new Vector3(0, 1, 5);
        cube.transform.localScale = new Vector3(1, 1, 1);
        var rend = cube.GetComponent<Renderer>();
        rend.material.color = Color.red;
        // Add a visible plane under player
        var plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        plane.name = "TEST_PLANE";
        plane.transform.position = new Vector3(0, 0, 0);
        plane.transform.localScale = new Vector3(5, 1, 5);
        // Move SwampIsland up a bit if exists
        var swamp = GameObject.Find("SwampIsland");
        if (swamp != null) swamp.transform.position = new Vector3(0, -0.5f, 0);
        // Ensure Main Camera is active
        var cam = Camera.main;
        if (cam != null) cam.backgroundColor = new Color(0.5f, 0.7f, 0.9f);
        EditorSceneManager.MarkSceneDirty(scene);
        EditorSceneManager.SaveScene(scene);
        Debug.Log("✅ Fix: TEST_CUBE eklendi (0,1,5) kırmızı, kamera önüne bak. Play'e bas, cube görünecek.");
    }
}
