using UnityEngine;
using UnityEditor;
using UnityEditor.Rendering.Universal;

public class URPMaterialConverter : EditorWindow
{
    [MenuItem("Tools/Bulk Convert Materials to URP")]
    static void ConvertMaterialsToURP()
    {
        string[] guids = AssetDatabase.FindAssets("t:Material");
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Material mat = AssetDatabase.LoadAssetAtPath<Material>(path);
            if (mat.shader.name == "Standard")
            {
                Shader urpShader = Shader.Find("Universal Render Pipeline/Lit");
                mat.shader = urpShader;
                EditorUtility.SetDirty(mat);
                Debug.Log($"Converted: {path}");
            }
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
