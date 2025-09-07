using System.Collections.Generic;
using UnityEngine;

public class CatMullRomCurve : MonoBehaviour
{
    [Header("Pontos de Controle (arraste aqui)")]
    public List<Transform> controlPoints = new List<Transform>();

    [Header("Configuração da curva")]
    [Range(10, 200)]
    public int curveResolution = 20;

    [HideInInspector]
    public List<Vector3> curvePoints = new List<Vector3>();

    void OnDrawGizmos()
    {
        if (curvePoints.Count > 1)
        {
            Gizmos.color = Color.cyan;
            for (int i = 0; i < curvePoints.Count - 1; i++)
            {
                Gizmos.DrawLine(curvePoints[i], curvePoints[i + 1]);
            }
        }
    }

    [ContextMenu("Gerar Catmull-Rom")]

    public void GenerateCurve()
    {
        curvePoints.Clear();

        if (controlPoints.Count < 4)
        {
            Debug.LogWarning("Catmull-Rom requer pelo menos 4 pontos de controle.");
            return;
        }

        // Se quiser curva fechada, adiciona também o primeiro ponto no final
        List<Vector3> extended = new List<Vector3>(controlPoints.Count + 3);
        extended.Add(controlPoints[controlPoints.Count - 1].position); // conecta fim com início
        foreach (var t in controlPoints) extended.Add(t.position);
        extended.Add(controlPoints[0].position);
        extended.Add(controlPoints[1].position); // ajuda a suavizar a volta

        float step = 1f / curveResolution;

        for (int i = 0; i < extended.Count - 3; i++)
        {
            Vector3 p0 = extended[i];
            Vector3 p1 = extended[i + 1];
            Vector3 p2 = extended[i + 2];
            Vector3 p3 = extended[i + 3];

            for (int j = 0; j <= curveResolution; j++)
            {
                float t = j * step;
                Vector3 pos = GetCatmullRomPosition(t, p0, p1, p2, p3);
                curvePoints.Add(pos);
            }
        }
    }


    private Vector3 GetCatmullRomPosition(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float t2 = t * t;
        float t3 = t2 * t;

        float x = 0.5f * ((2f * p1.x) +
                          (-p0.x + p2.x) * t +
                          (2f * p0.x - 5f * p1.x + 4f * p2.x - p3.x) * t2 +
                          (-p0.x + 3f * p1.x - 3f * p2.x + p3.x) * t3);

        float y = 0.5f * ((2f * p1.y) +
                          (-p0.y + p2.y) * t +
                          (2f * p0.y - 5f * p1.y + 4f * p2.y - p3.y) * t2 +
                          (-p0.y + 3f * p1.y - 3f * p2.y + p3.y) * t3);

        float z = 0.5f * ((2f * p1.z) +
                          (-p0.z + p2.z) * t +
                          (2f * p0.z - 5f * p1.z + 4f * p2.z - p3.z) * t2 +
                          (-p0.z + 3f * p1.z - 3f * p2.z + p3.z) * t3);

        return new Vector3(x, y, z);
    }
}
