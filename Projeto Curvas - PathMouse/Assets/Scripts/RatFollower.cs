using UnityEngine;
using System.Collections.Generic;

public class RatFollower : MonoBehaviour
{
    [Header("Curvas")]
    public CatMullRomCurve catmullRomCurve;
    public BezierCurve bezierCurve;

    [Header("Config")]
    public float speed = 2f;
    public KeyCode switchKey = KeyCode.Space; // tecla para trocar curva

    [Header("Câmeras")]
    public Camera catmullCamera;
    public Camera bezierCamera;

    private List<Vector3> currentPath;
    private int currentIndex = 0;
    private bool onBezier = true; // agora começa no Bézier

    void Start()
    {
        // Inicia com a curva Bézier
        bezierCurve.GenerateCurve();
        currentPath = bezierCurve.curvePoints;
        currentIndex = 0;
        onBezier = true;

        // Ativa a câmera do Bézier
        if (bezierCamera != null) bezierCamera.gameObject.SetActive(true);
        if (catmullCamera != null) catmullCamera.gameObject.SetActive(false);
    }

    void Update()
    {
        if (currentPath == null || currentPath.Count == 0) return;

        // Movimento do rato ao longo do caminho
        Vector3 target = currentPath[currentIndex];
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        Vector3 lookTarget = (currentIndex + 1 < currentPath.Count)
            ? currentPath[currentIndex + 1]
            : target;

        Quaternion rot = Quaternion.LookRotation(lookTarget - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 5f);

        // Quando atinge o ponto alvo, avança para o próximo
        if (Vector3.Distance(transform.position, target) < 0.05f)
        {
            currentIndex++;
            if (currentIndex >= currentPath.Count)
            {
                currentIndex = 0;
                transform.position = currentPath[0];
            }
        }

        // Alterna entre Bézier e Catmull-Rom ao apertar a tecla definida
        if (Input.GetKeyDown(switchKey))
        {
            if (onBezier)
            {
                // Troca para Catmull-Rom
                catmullRomCurve.GenerateCurve();
                currentPath = catmullRomCurve.curvePoints;
                onBezier = false;

                if (bezierCamera != null) bezierCamera.gameObject.SetActive(false);
                if (catmullCamera != null) catmullCamera.gameObject.SetActive(true);
            }
            else
            {
                // Troca para Bézier
                bezierCurve.GenerateCurve();
                currentPath = bezierCurve.curvePoints;
                onBezier = true;

                if (catmullCamera != null) catmullCamera.gameObject.SetActive(false);
                if (bezierCamera != null) bezierCamera.gameObject.SetActive(true);
            }

            currentIndex = 0;
            transform.position = currentPath[0];
        }
    }
}
