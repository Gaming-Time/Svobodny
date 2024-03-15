using UnityEngine;

namespace CodeBase.Infrastructure.Services.AssetProvider
{
    public class AssetProvider : IAssets
    {
        public GameObject Instantiate(string path, Vector3 at) =>
            Object.Instantiate(LoadPrefab(path), at, Quaternion.identity);

        public GameObject Instantiate(string path) =>
            Object.Instantiate(LoadPrefab(path));

        public GameObject Instantiate(string path, Transform parent) =>
            Object.Instantiate(LoadPrefab(path), parent);

        public GameObject Instantiate(string path, Vector3 at, Quaternion rotation) =>
            Object.Instantiate(LoadPrefab(path), at, rotation);

        public T Instantiate<T>(string path) where T : Object =>
            Object.Instantiate(LoadComponentOfPrefab<T>(path));

        public T Instantiate<T>(string path, Vector3 at) where T : Object =>
            Object.Instantiate(LoadComponentOfPrefab<T>(path), at, Quaternion.identity);

        public T Instantiate<T>(string path, Vector3 at, Quaternion rotation) where T : Object =>
            Object.Instantiate(LoadComponentOfPrefab<T>(path), at, rotation);

        public T Instantiate<T>(string path, Transform parent) where T : Object =>
            Object.Instantiate(LoadComponentOfPrefab<T>(path), parent);

        private static GameObject LoadPrefab(string path) =>
            Resources.Load<GameObject>(path);

        private static T LoadComponentOfPrefab<T>(string path) where T : Object =>
            Resources.Load<T>(path);
    }
}