using CodeBase.Logic;
using UnityEditor;
using UnityEngine;

namespace CodeBase.Editor
{
    [CustomEditor(typeof(WaypointMarker))]
    public class WaypointMarkerEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(WaypointMarker marker, GizmoType gizmoType)
        {
            Gizmos.DrawWireSphere(marker.transform.position, 0.5f);
        }
    }
}