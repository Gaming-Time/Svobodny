using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerTransparency : MonoBehaviour
{
    [FormerlySerializedAs("LayerMask")] public LayerMask WallLayerMask;
    [SerializeField] private LayerMask characterLayerMask;
    public Camera Camera;
    public static int PosId = Shader.PropertyToID("_PlayerPos");
    public static int SizeId = Shader.PropertyToID("_Size");

    private List<Material> _wallMaterials;
    private RaycastHit _previousHit;

    public void Construct(Camera camera)
    {
        Camera = camera;
    }
    
    void Update()
    {
        var dir = Camera.transform.position - transform.position;
        var ray = new Ray(transform.position, dir.normalized);
        if (Physics.Raycast(ray, out var hit, 3000, WallLayerMask))
        {
            if(!Equals(hit, _previousHit))
                _wallMaterials?.ForEach(material => material.SetFloat(SizeId, 0));
            
            _wallMaterials = hit.collider.gameObject.GetComponent<Renderer>()?.materials.ToList();

            _wallMaterials?.ForEach(material => material.SetFloat(SizeId, 1));
            _previousHit = hit;
        }
        else
        {
            _wallMaterials?.ForEach(material => material.SetFloat(SizeId, 0));
        }

        var view = Camera.WorldToViewportPoint(transform.position); //Получение координат

        _wallMaterials?.ForEach(material => material.SetVector(PosId, view));
    }
}