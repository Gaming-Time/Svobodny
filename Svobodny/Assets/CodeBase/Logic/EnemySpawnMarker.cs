using System.Collections.Generic;
using CodeBase.Data.StaticData.Monster;
using UnityEngine;

namespace CodeBase.Logic
{
    public class EnemySpawnMarker : MonoBehaviour
    {
        public MonsterTypeID TypeID;
        public List<WaypointMarker> Waypoints;
    }
}