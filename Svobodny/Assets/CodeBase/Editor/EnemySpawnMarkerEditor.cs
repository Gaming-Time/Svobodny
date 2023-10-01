using CodeBase.Logic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeBase.Editor
{
    [CustomEditor(typeof(EnemySpawnMarker))]
    public class EnemySpawnMarkerEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(EnemySpawnMarker marker, GizmoType gizmo)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(marker.transform.position, 0.5f);
        }
    }
}