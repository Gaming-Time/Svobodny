using System;
using CodeBase.Infrastructure.Helpers;
using CodeBase.Logic.UsableObjects.Essentials;
using UnityEditor;
using UnityEngine;

namespace CodeBase.Editor
{
    [CustomEditor(typeof(EssentialUsableObjectSpawnMarker))]
    public class EssentialUsableObjectEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(EssentialUsableObjectSpawnMarker marker, GizmoType gizmoType)
        {
            var image = marker.EssentialType switch
            {
                EssentialType.Bullet => AssetPath.Gizmos.BulletImage,
                EssentialType.Medicine => AssetPath.Gizmos.MedicineImage,
                _ => throw new ArgumentOutOfRangeException()
            };
            
            Gizmos.DrawIcon(marker.transform.position, image, false);
        }
    }
}