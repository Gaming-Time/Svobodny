using System;
using System.Linq;
using CodeBase.Logic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeBase.Editor
{
    [CustomEditor(typeof(UniqueId))]
    public class UniqueIdEditor : UnityEditor.Editor
    {
        private void OnEnable()
        {
            var uniqueId = (UniqueId)target;

            if (string.IsNullOrEmpty(uniqueId.Id))
                Generate(uniqueId);
            else
            {
                var uniqueIds = FindObjectsOfType<UniqueId>();

                if (uniqueIds.Any(other => other != uniqueId && other.Id == uniqueId.Id))
                    Generate(uniqueId);
            }
        }

        private void Generate(UniqueId uniqueId)
        {
            if (uniqueId.gameObject.scene.name == null)
                return;
            
            uniqueId.Id = $"{uniqueId.gameObject.scene.name}_{Guid.NewGuid().ToString()}";

            if (Application.isPlaying)
                return;

            EditorUtility.SetDirty(uniqueId);
            EditorSceneManager.MarkSceneDirty(uniqueId.gameObject.scene);
        }
    }
}