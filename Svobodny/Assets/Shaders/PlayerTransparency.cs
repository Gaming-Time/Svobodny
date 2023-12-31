using UnityEngine;

public class PlayerTransparency : MonoBehaviour
{
    public Material Material;
    public LayerMask LayerMask;
    public Camera Camera;
    public static int PosId = Shader.PropertyToID("_PlayerPos");
    public static int SizeId = Shader.PropertyToID("_Size");

    public void Construct(Camera camera)
    {
        Camera = camera;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var dir = Camera.transform.position - transform.position;
        var ray = new Ray(transform.position, dir.normalized);
        if (Physics.Raycast(ray, 3000, LayerMask))
        {
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            Material = hit.collider.gameObject.GetComponent<Renderer>().material;

            if (Material != null)
                Material.SetFloat(SizeId, 1);
        }
        else
        {
            if (Material != null)
                Material.SetFloat(SizeId, 0);
        }

        var view = Camera.WorldToViewportPoint(transform.position); //Получение координат

        if (Material != null)
            Material.SetVector(PosId, view);
    }
}