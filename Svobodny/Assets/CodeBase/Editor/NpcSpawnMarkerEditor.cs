using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeBase.Editor
{
    [CustomEditor(typeof(NPCSpawnMarker))]
    public class NpcSpawnMarkerEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(NPCSpawnMarker marker, GizmoType gizmo)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(marker.transform.position, 0.5f);
        }
    }
}