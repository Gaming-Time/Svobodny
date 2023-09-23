using UnityEngine;

namespace CodeBase.Infrastructure.Services.AssetProvider
{
    public interface IAssets : IService
    {
        GameObject Instantiate(string path, Vector3 at);
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Transform parent);
        GameObject Instantiate(string path, Vector3 at, Quaternion rotation);
    }
}