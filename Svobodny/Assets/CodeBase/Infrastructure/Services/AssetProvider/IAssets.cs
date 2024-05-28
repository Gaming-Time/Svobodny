using UnityEngine;

namespace CodeBase.Infrastructure.Services.AssetProvider
{
    public interface IAssets : IService
    {
        GameObject Instantiate(string path, Vector3 at);
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Transform parent);
        GameObject Instantiate(string path, Vector3 at, Quaternion rotation);
        T Instantiate<T>(string path) where T : Object;
        T Instantiate<T>(string path, Vector3 at) where T : Object;
        T Instantiate<T>(string path, Vector3 at, Quaternion rotation) where T : Object;
        T Instantiate<T>(string path, Transform parent) where T : Object;
    }
}