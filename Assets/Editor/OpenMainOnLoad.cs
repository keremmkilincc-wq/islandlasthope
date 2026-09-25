using UnityEditor;
using UnityEditor.SceneManagement;
[InitializeOnLoad]
public static class OpenMainOnLoad
{
    static OpenMainOnLoad()
    {
        EditorApplication.delayCall += () =>
        {
            var scene = EditorSceneManager.GetActiveScene();
            if (scene.name != "Main")
            {
                try
                {
                    EditorSceneManager.OpenScene("Assets/Scenes/Main.unity", OpenSceneMode.Single);
                    UnityEngine.Debug.Log("✅ Main sahnesi otomatik açıldı!");
                }
                catch (System.Exception e) { UnityEngine.Debug.LogWarning("Main açılamadı: " + e.Message); }
            }
        };
    }
}
