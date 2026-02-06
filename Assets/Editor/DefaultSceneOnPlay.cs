using Constants;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Editor;

[InitializeOnLoad]
public class DefaultSceneOnPlay
{
    static DefaultSceneOnPlay()
    {
        var pathOfFirstScene = EditorBuildSettings.scenes.First(scene => scene.path.EndsWith($"{SceneNames.Boot}.unity")).path;
        var sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(pathOfFirstScene);
        EditorSceneManager.playModeStartScene = sceneAsset;
        Debug.Log($"Set {SceneNames.Boot} scene as a default scene on play");
    }
}