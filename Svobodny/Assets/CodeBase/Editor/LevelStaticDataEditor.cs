using System.Linq;
using CodeBase.Infrastructure.Logic.UsableObjects;
using CodeBase.Infrastructure.Services.StaticData.Level;
using CodeBase.Infrastructure.Services.StaticData.Monster;
using CodeBase.Infrastructure.Services.StaticData.Npc;
using CodeBase.Infrastructure.Services.StaticData.UsableObjects;
using CodeBase.Logic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Editor
{
    [CustomEditor(typeof(LevelStaticData))]
    public class LevelStaticDataEditor : UnityEditor.Editor
    {
        private const string InitialDayPointTag = "InitialDayPointTag";
        private const string InitialNightPointTag = "InitialNightPointTag";

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            LevelStaticData levelData = target as LevelStaticData;

            if (GUILayout.Button("Collect"))
            {
                levelData!.EnemySpawners = FindObjectsOfType<EnemySpawnMarker>().Select(x =>
                    new EnemySpawnerData(x.GetComponent<UniqueId>().Id, x.TypeID, x.transform.rotation,
                        x.transform.position)).ToList();
                levelData.NpcSpawners = FindObjectsOfType<NPCSpawnMarker>().Select(x =>
                    new NpcSpawnerData(x.GetComponent<UniqueId>().Id, x.TypeId, x.transform.rotation,
                        x.transform.position)).ToList();
                levelData.ObjectsSpawners = FindObjectsOfType<UsableObjectSpawnMarker>().Select(x =>
                    new ObjectSpawnerData(x.GetComponent<UniqueId>().Id, x.TypeId, x.transform.rotation,
                        x.transform.position)).ToList();

                levelData.LevelKey = SceneManager.GetActiveScene().name;
                var dayPlayer = GameObject.FindWithTag(InitialDayPointTag);
                var nightPlayer = GameObject.FindWithTag(InitialNightPointTag);
                levelData.DayPlayerPosition = dayPlayer.transform.position;
                levelData.DayPlayerRotation = dayPlayer.transform.rotation;
                levelData.NightPlayerPosition = nightPlayer.transform.position;
                levelData.NightPlayerRotation = nightPlayer.transform.rotation;
            }

            EditorUtility.SetDirty(target);
        }
    }
}