using System.Collections.Generic;
using UnityEngine;

public class BezierCurve : MonoBehaviour
{
    [Header("Pontos de Controle (arraste aqui 4 pontos)")]
    public List<Transform> controlPoints = new List<Transform>();

    [Header("Configuração")]
    [Range(10, 200)]
    public int resolution = 50;

    [HideInInspector]
    public List<Vector3> curvePoints = new List<Vector3>();

    void OnDrawGizmos()
    {
        if (curvePoints.Count > 1)
        {
            Gizmos.color = Color.magenta;
            for (int i = 0; i < curvePoints.Count - 1; i++)
            {
                Gizmos.DrawLine(curvePoints[i], curvePoints[i + 1]);
            }
        }
    }

    [ContextMenu("Gerar Bézier")]
    public void GenerateCurve()
    {
        curvePoints.Clear();
        if (controlPoints.Count < 4)
        {
            Debug.LogWarning("Bezier requer exatamente 4 pontos de controle.");
            return;
        }

        float step = 1f / resolution;
        for (int i = 0; i <= resolution; i++)
        {
            float t = i * step;
            curvePoints.Add(GetBezierPoint(t));
        }
    }

    private Vector3 GetBezierPoint(float t)
    {
        Vector3 p0 = controlPoints[0].position;
        Vector3 p1 = controlPoints[1].position;
        Vector3 p2 = controlPoints[2].position;
        Vector3 p3 = controlPoints[3].position;

        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 p = uuu * p0;
        p += 3 * uu * t * p1;
        p += 3 * u * tt * p2;
        p += ttt * p3;

        return p;
    }
}
