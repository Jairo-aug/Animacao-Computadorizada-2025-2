using UnityEngine;
using System.Collections.Generic;

public class RatFollower : MonoBehaviour
{
    [Header("Curvas")]
    public CatMullRomCurve catmullRomCurve;
    public BezierCurve bezierCurve;

    [Header("Config")]
    public float speed = 2f;

    private List<Vector3> currentPath;
    private int currentIndex = 0;
    private bool onBezier = false; // controla se estamos na Bézier

    void Start()
    {
        // começa no Catmull
        catmullRomCurve.GenerateCurve();
        currentPath = catmullRomCurve.curvePoints;
        currentIndex = 0;
    }

    void Update()
    {
        if (currentPath == null || currentPath.Count == 0) return;

        Vector3 target = currentPath[currentIndex];
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        transform.LookAt(target);

        if (Vector3.Distance(transform.position, target) < 0.05f)
        {
            currentIndex++;
            if (currentIndex >= currentPath.Count)
            {
                // terminou curva atual → troca
                if (!onBezier)
                {
                    // vai pra Bézier
                    bezierCurve.GenerateCurve();
                    currentPath = bezierCurve.curvePoints;
                    onBezier = true;
                }
                else
                {
                    // volta pro Catmull
                    catmullRomCurve.GenerateCurve();
                    currentPath = catmullRomCurve.curvePoints;
                    onBezier = false;
                }

                currentIndex = 0;
            }
        }
    }
}
