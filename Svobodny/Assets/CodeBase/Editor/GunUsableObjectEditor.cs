using System;
using CodeBase.Infrastructure.Helpers;
using CodeBase.Logic.UsableObjects;
using CodeBase.Modules.Inventory;
using UnityEditor;
using UnityEngine;

namespace CodeBase.Editor
{
    [CustomEditor(typeof(GunUsableObjectSpawnMarker))]
    public class GunUsableObjectEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(GunUsableObjectSpawnMarker marker, GizmoType gizmoType)
        {
            var image = marker.GunType switch
            {
                GunType.Knife => AssetPath.Gizmos.KnifeImage,
                GunType.Pistol => AssetPath.Gizmos.PistolImage,
                _ => throw new ArgumentOutOfRangeException()
            };
            
            Gizmos.DrawIcon(marker.transform.position, image, false);
        }
    }
}