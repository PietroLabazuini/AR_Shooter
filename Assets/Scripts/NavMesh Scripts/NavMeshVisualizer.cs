using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class NavMeshVisualizer : MonoBehaviour
{
    public bool ShowVisualization = true;

    [SerializeField]
    private Material VisualizationMaterial;
    [SerializeField]
    private Vector3 GeneratedMeshOffset = new(0, 0.05f, 0);
    [SerializeField]
    private GameObject plane;
    [SerializeField]
    Text buttonText;

    private GameObject MeshVisualization;

    Mesh navMesh;
    MeshRenderer _renderer;
    MeshFilter filter;

    private void Start()
    {
        MeshVisualization = new("NavMesh Visualization");
        MeshVisualization.transform.parent = plane.transform;
        _renderer = MeshVisualization.AddComponent<MeshRenderer>();
        filter = MeshVisualization.AddComponent<MeshFilter>();
        navMesh = new Mesh();

        RecalculateNavMesh();

        MeshVisualization.transform.localPosition = GeneratedMeshOffset - plane.transform.position;
    }

    public void ToggleVisualization()
    {
        ShowVisualization = !ShowVisualization;
        MeshVisualization.SetActive(ShowVisualization);
        if (ShowVisualization)
        {
            buttonText.text = "Disable Mesh";
        }
        else
        {
            buttonText.text = "Enable Mesh";
        }
        RecalculateNavMesh();
    }
    
    private void RecalculateNavMesh()
    {
        NavMeshTriangulation triangulation = NavMesh.CalculateTriangulation();

        navMesh.SetVertices(triangulation.vertices);
        navMesh.SetIndices(triangulation.indices, MeshTopology.Triangles, 0);

        _renderer.sharedMaterial = VisualizationMaterial;
        filter.mesh = navMesh;
    }
}