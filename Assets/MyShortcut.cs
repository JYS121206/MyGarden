using UnityEngine;
using UnityEditor;

public class MyShortcut : Editor
{
    public static GameObject go;

    [MenuItem("GameObject/Reset Position _`")]
    public static void ResetPosition()
    {
        go = Selection.activeGameObject;
        if (go == null)
            return;

        Undo.RecordObject(go.transform, "Reset Position");
        go.transform.position = Vector3.zero;
    }
    [MenuItem("GameObject/Reset Transform #`")]
    public static void ResetTransform()
    {
        go = Selection.activeGameObject;
        if (go == null)
            return;

        Undo.RecordObject(go.transform, "Reset Transform");
        go.transform.position = Vector3.zero;
        go.transform.rotation = Quaternion.identity;
        go.transform.localScale = Vector3.one;
    }
}
