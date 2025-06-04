using UnityEditor;
using UnityEngine;

namespace Eloi.ThreePoints { 
[CustomEditor(typeof(ThreePointsMono_MeshTriangleIndexTransform))]
public class ThreePointsMono_MeshTriangleIndexTransformEditor : Editor { 

    public override void OnInspectorGUI()
    {
        ThreePointsMono_MeshTriangleIndexTransform myScript = (ThreePointsMono_MeshTriangleIndexTransform)target;
        
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Next"))
        {
            myScript.Next();
        }
        if (GUILayout.Button("Previous"))
        {
            myScript.Previous();
        }
        GUILayout.EndHorizontal();
        base.OnInspectorGUI();

    }

}

}