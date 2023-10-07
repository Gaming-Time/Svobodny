using UnityEngine;

namespace Assets.CodeBase.Logic
{
    public static class DataExtensions
    {
        public static T ToDeserialized<T>(this string json) => JsonUtility.FromJson<T>(json);
    }
}
