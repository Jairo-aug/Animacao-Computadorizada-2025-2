using UnityEngine;

public enum BehaviorType { Straight, Spiral, RandomWalk, Explosion, Sine }

public class ParticleBehavior : MonoBehaviour
{
    // --- Runtime ---
    private float age;
    private Vector3 velocity;
    private Vector3 origin;

    // --- References ---
    private SpriteRenderer sr;      // Para partículas 2D
    private Renderer meshRenderer;  // Para partículas 3D (Quad, Cubo, etc.)

    // --- Parameters ---
    private float lifetime = 2f;
    private Color startColor = Color.white;
    private Color endColor = new Color(1, 1, 1, 0);
    private float startScale = 0.2f;
    private float endScale = 0.05f;
    private float spiralStrength = 3f;
    private float sineAmplitude = 0.5f;
    private float sineFrequency = 3f;
    private float randomForce = 1f;
    private float rotationSpeed = 0f;
    private float maxDistance = 50f;
    private BehaviorType behavior = BehaviorType.Straight;

    void Awake()
    {
        // Detecta automaticamente se é SpriteRenderer ou MeshRenderer
        sr = GetComponent<SpriteRenderer>();
        if (sr == null)
            meshRenderer = GetComponent<Renderer>();
    }

    void OnEnable()
    {
        age = 0f;
    }

    public void Init(ParticleSettings s, Vector3 spawnPos, Vector3 initialVel, BehaviorType b, Vector3 originPos)
    {
        transform.position = spawnPos;
        origin = originPos;
        velocity = initialVel;
        behavior = b;

        // Copia as configurações do ParticleSettings
        startColor = s.startColor;
        endColor = s.endColor;
        startScale = s.startScale;
        endScale = s.endScale;
        lifetime = s.lifetime;
        maxDistance = s.maxDistance;

        // Randomização interna para deixar o movimento mais natural
        spiralStrength = Random.Range(1.5f, 4f);
        sineAmplitude = Random.Range(0.2f, 1f);
        sineFrequency = Random.Range(1.5f, 4f);
        randomForce = Random.Range(0.5f, 2f);
        rotationSpeed = Random.Range(-120f, 120f);

        // Configura visual inicial
        if (sr != null)
        {
            sr.color = startColor;
        }
        else if (meshRenderer != null)
        {
            var mat = meshRenderer.material;

            // Garante que o material aceite transparência
            mat.SetFloat("_Mode", 2); // Fade
            mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            mat.SetInt("_ZWrite", 0);
            mat.DisableKeyword("_ALPHATEST_ON");
            mat.EnableKeyword("_ALPHABLEND_ON");
            mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            mat.renderQueue = 3000;

            // Define a cor inicial
            mat.color = startColor;
        }

        transform.localScale = Vector3.one * startScale;
        age = 0f;

        // Debug opcional para confirmar que as cores estão sendo recebidas
        Debug.Log("Init Partícula - Start: " + startColor + " | End: " + endColor);
    }

    void Update()
    {
        float dt = Time.deltaTime;
        age += dt;
        float t = Mathf.Clamp01(age / lifetime);

        // --- Movimento por comportamento ---
        switch (behavior)
        {
            case BehaviorType.Straight:
                // Apenas segue a velocidade inicial
                break;

            case BehaviorType.Spiral:
                if (velocity.sqrMagnitude > 0.0001f)
                {
                    Vector3 perp = Vector3.Cross(velocity.normalized, Vector3.forward);
                    velocity += perp * spiralStrength * dt;
                    velocity *= 0.995f; // resistência do ar
                }
                break;

            case BehaviorType.RandomWalk:
                velocity += Random.insideUnitSphere * randomForce * dt;
                break;

            case BehaviorType.Explosion:
                velocity *= 0.995f; // desaceleração gradual
                break;

            case BehaviorType.Sine:
                // Movimento adicional feito abaixo
                break;
        }

        // Aplica movimento básico
        transform.position += velocity * dt;

        // Movimento em onda senoidal
        if (behavior == BehaviorType.Sine)
        {
            float lateral = Mathf.Sin(age * sineFrequency) * sineAmplitude;
            transform.position += transform.right * lateral * dt;
        }

        // --- Interpolação visual (cor e escala) ---
        if (sr != null)
        {
            sr.color = Color.Lerp(startColor, endColor, t);
        }
        else if (meshRenderer != null)
        {
            meshRenderer.material.color = Color.Lerp(startColor, endColor, t);
        }

        transform.localScale = Vector3.one * Mathf.Lerp(startScale, endScale, t);
        transform.Rotate(Vector3.up, rotationSpeed * dt);

        // --- Critérios de morte ---
        if (age >= lifetime)
        {
            gameObject.SetActive(false);
            return;
        }

        if (Vector3.Distance(transform.position, origin) > maxDistance)
        {
            gameObject.SetActive(false);
            return;
        }
    }
}
