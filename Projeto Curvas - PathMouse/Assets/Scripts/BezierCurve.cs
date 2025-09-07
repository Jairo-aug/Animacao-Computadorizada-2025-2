using System.Collections.Generic;
using UnityEngine;

public class BezierCurve : MonoBehaviour
{
    [Header("Pontos de Controle (arraste aqui 6 pontos)")]
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

            // Desenha uma linha do último ponto para o primeiro para visualizar o loop
            Gizmos.DrawLine(curvePoints[curvePoints.Count - 1], curvePoints[0]);
        }
    }

    [ContextMenu("Gerar Bézier")]
    public void GenerateCurve()
    {
        curvePoints.Clear();

        if (controlPoints.Count != 6)
        {
            Debug.LogWarning("Bezier de 6 pontos requer exatamente 6 pontos de controle.");
            return;
        }

        float step = 1f / resolution;

        // Gera a curva principal (do primeiro ao último ponto)
        for (int i = 0; i <= resolution; i++)
        {
            float t = i * step;
            curvePoints.Add(GetBezierPoint(t,
                controlPoints[0].position,
                controlPoints[1].position,
                controlPoints[2].position,
                controlPoints[3].position,
                controlPoints[4].position,
                controlPoints[5].position
            ));
        }

        // Gera a conexão suave do último ponto de volta ao primeiro
        // usando os mesmos pontos, mas reorganizados para criar um ciclo contínuo
        for (int i = 0; i <= resolution; i++)
        {
            float t = i * step;
            curvePoints.Add(GetBezierPoint(t,
                controlPoints[5].position,  // começa do último
                controlPoints[0].position,  // vai para o primeiro
                controlPoints[1].position,
                controlPoints[2].position,
                controlPoints[3].position,
                controlPoints[4].position
            ));
        }
    }

    private Vector3 GetBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4, Vector3 p5)
    {
        float u = 1 - t;
        float t2 = t * t;
        float t3 = t2 * t;
        float t4 = t3 * t;
        float t5 = t4 * t;

        float u2 = u * u;
        float u3 = u2 * u;
        float u4 = u3 * u;
        float u5 = u4 * u;

        Vector3 p = u5 * p0;
        p += 5 * u4 * t * p1;
        p += 10 * u3 * t2 * p2;
        p += 10 * u2 * t3 * p3;
        p += 5 * u * t4 * p4;
        p += t5 * p5;

        return p;
    }
}
