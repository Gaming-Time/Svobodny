using CodeBase.Infrastructure.Logic.UsableObjects;
using CodeBase.Logic;
using UnityEditor;
using UnityEngine;

namespace CodeBase.Editor
{
    [CustomEditor(typeof(UsableObjectSpawnMarker))]
    public class UsableObjectSpawnMarkerEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(UsableObjectSpawnMarker marker, GizmoType gizmo)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(marker.transform.position, 0.5f);
        }
    }
}