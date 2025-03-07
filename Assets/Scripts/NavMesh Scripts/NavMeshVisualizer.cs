using UnityEngine;
using UnityEngine.AI;

public class NavMeshVisualizer : MonoBehaviour
{
    public bool ShowVisualization = true;

    [SerializeField]
    private Material VisualizationMaterial;
    private Vector3 GeneratedMeshOffset = new(0, 0.05f, 0);

    MeshRenderer Mrenderer;
    MeshFilter filter;
    private GameObject MeshVisualization;
    Mesh navMesh;

    private void Start()
    {
        MeshVisualization = new("NavMesh Visualization");
        Mrenderer = MeshVisualization.AddComponent<MeshRenderer>();
        filter = MeshVisualization.AddComponent<MeshFilter>();
        navMesh = new Mesh();
    }

    private void Update()
    {
        NavMeshTriangulation triangulation = NavMesh.CalculateTriangulation();

        navMesh.SetVertices(triangulation.vertices);
        navMesh.SetIndices(triangulation.indices, MeshTopology.Triangles, 0);

        Mrenderer.sharedMaterial = VisualizationMaterial;
        filter.mesh = navMesh;

        MeshVisualization.SetActive(ShowVisualization);
        MeshVisualization.transform.position = GeneratedMeshOffset;
    }
}