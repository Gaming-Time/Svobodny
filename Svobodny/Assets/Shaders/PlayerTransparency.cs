using UnityEngine;
using UnityEngine.Serialization;

public class PlayerTransparency : MonoBehaviour
{
    [FormerlySerializedAs("LayerMask")] public LayerMask WallLayerMask;
    [SerializeField] private LayerMask characterLayerMask;
    public Camera Camera;
    public static int PosId = Shader.PropertyToID("_PlayerPos");
    public static int SizeId = Shader.PropertyToID("_Size");

    private Material[] _wallMaterials;

    public void Construct(Camera camera)
    {
        Camera = camera;
    }

    // Update is called once per frame
    void Update()
    {
        var dir = Camera.transform.position - transform.position;
        var ray = new Ray(transform.position, dir.normalized);
        if (Physics.Raycast(ray, 3000, WallLayerMask))
        {
            RaycastHit hit;
            Physics.Raycast(ray, out hit, 3000, WallLayerMask);
            _wallMaterials = hit.collider.gameObject.GetComponent<Renderer>()?.materials;

            if (_wallMaterials != null)
            {
                foreach (var wallMaterial in _wallMaterials)
                {
                    wallMaterial.SetFloat(SizeId, 1);
                }
            }
        }
        else
        {
            if (_wallMaterials != null)
            {
                foreach (var wallMaterial in _wallMaterials)
                {
                    wallMaterial.SetFloat(SizeId, 0);
                }
            }
        }

        var view = Camera.WorldToViewportPoint(transform.position); //Получение координат

        if (_wallMaterials != null)
        {
            foreach (var wallMaterial in _wallMaterials)
            {
                wallMaterial.SetVector(PosId, view);
            }
        }
    }
}