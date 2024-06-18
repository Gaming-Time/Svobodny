using UnityEngine;

namespace CodeBase.Data.StaticData.Cursor
{
    [CreateAssetMenu(fileName = "CursorData", menuName = "Static Data/Cursor", order = 0)]
    public class CursorData : ScriptableObject
    {
        public CursorType CursorType;
        public Texture2D Texture;
        public Vector2 HotSpot;
    }

    public enum CursorType
    {
        Menu,
        Game,
    }
}