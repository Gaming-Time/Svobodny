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

        private static GameObject LoadPrefab(string path) =>
            Resources.Load<GameObject>(path);
    }
}