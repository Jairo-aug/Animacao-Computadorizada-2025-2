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
    private bool onBezier = false;

    void Start()
    {
        catmullRomCurve.GenerateCurve();
        currentPath = catmullRomCurve.curvePoints;
        currentIndex = 0;
    }

    void Update()
    {
        if (currentPath == null || currentPath.Count == 0) return;

        Vector3 target = currentPath[currentIndex];
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        Vector3 lookTarget = (currentIndex + 1 < currentPath.Count)
            ? currentPath[currentIndex + 1]
            : target;

        transform.LookAt(lookTarget);

        if (Vector3.Distance(transform.position, target) < 0.05f)
        {
            currentIndex++;
            if (currentIndex >= currentPath.Count)
            {

                if (!onBezier)
                {
                    bezierCurve.GenerateCurve();
                    currentPath = bezierCurve.curvePoints;
                    onBezier = true;
                }
                else
                {
                    catmullRomCurve.GenerateCurve();
                    currentPath = catmullRomCurve.curvePoints;
                    onBezier = false;
                }

                currentIndex = 0;
            }
        }
    }
}

