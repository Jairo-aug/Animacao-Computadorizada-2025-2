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
    public Camera catmullCamera; // arraste a Main Camera aqui
    public Camera bezierCamera;  // arraste a BezierCamera aqui

    private List<Vector3> currentPath;
    private int currentIndex = 0;
    private bool onBezier = false;

    void Start()
    {
        // começa no Catmull
        catmullRomCurve.GenerateCurve();
        currentPath = catmullRomCurve.curvePoints;
        currentIndex = 0;

        // ativa a câmera inicial
        if (catmullCamera != null) catmullCamera.gameObject.SetActive(true);
        if (bezierCamera != null) bezierCamera.gameObject.SetActive(false);
    }

    void Update()
    {
        if (currentPath == null || currentPath.Count == 0) return;

        // --- Movimento do rato ---
        Vector3 target = currentPath[currentIndex];
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // Olhar sempre para a próxima bolinha
        Vector3 lookTarget = (currentIndex + 1 < currentPath.Count)
            ? currentPath[currentIndex + 1]
            : target;

        Quaternion rot = Quaternion.LookRotation(lookTarget - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 5f);

        // Chegou no ponto atual → vai para o próximo
        if (Vector3.Distance(transform.position, target) < 0.05f)
        {
            currentIndex++;
            if (currentIndex >= currentPath.Count)
            {
                // volta para o início e continua em loop
                currentIndex = 0;
                transform.position = currentPath[0];
            }
        }

        // --- Troca de curva ao apertar tecla ---
        if (Input.GetKeyDown(switchKey))
        {
            if (!onBezier)
            {
                bezierCurve.GenerateCurve();
                currentPath = bezierCurve.curvePoints;
                onBezier = true;

                // troca para câmera da Bézier
                if (catmullCamera != null) catmullCamera.gameObject.SetActive(false);
                if (bezierCamera != null) bezierCamera.gameObject.SetActive(true);
            }
            else
            {
                catmullRomCurve.GenerateCurve();
                currentPath = catmullRomCurve.curvePoints;
                onBezier = false;

                // troca para câmera da Catmull
                if (bezierCamera != null) bezierCamera.gameObject.SetActive(false);
                if (catmullCamera != null) catmullCamera.gameObject.SetActive(true);
            }

            currentIndex = 0;
            transform.position = currentPath[0];
        }
    }
}
