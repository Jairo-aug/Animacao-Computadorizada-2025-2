using System.Collections.Generic;
using UnityEngine;

public enum EmitterType { Point, Area, Circle }

[System.Serializable]
public class ParticleSettings
{
    public Color startColor = Color.white;
    public Color endColor = new Color(1, 1, 1, 0);
    public float startScale = 0.2f;
    public float endScale = 0.05f;
    public float lifetime = 2f;
    public float maxDistance = 50f;
    public Sprite[] sprites;
    public float minSpeed = 1f;
    public float maxSpeed = 3f;
}

public class SimpleEmitter : MonoBehaviour
{
    [Header("Pool & Prefab")]
    public GameObject particlePrefab;
    public int initialPool = 200;
    private List<GameObject> pool;

    [Header("Emitter")]
    public EmitterType emitterType = EmitterType.Point;
    public ParticleSettings settings;
    public int particlesPerBurst = 30;
    public float emitRate = 10f; // per second when continuous
    public bool continuous = false;

    [Header("Point / Area / Circle params")]
    public Vector3 jitter = Vector3.zero;          // point jitter
    public Vector2 areaSize = new Vector2(3f, 2f); // area emitter
    public float radius = 2f;                      // circle emitter
    public float speedMultiplier = 1f;
    public float angleSpread = 30f;

    private float emitTimer;

    void Awake()
    {
        // create pool parent
        var parent = new GameObject(name + "_Pool");
        parent.transform.SetParent(transform);
        pool = new List<GameObject>(initialPool);
        for (int i = 0; i < initialPool; i++)
        {
            var go = Instantiate(particlePrefab, parent.transform);
            go.SetActive(false);
            pool.Add(go);
        }
    }

    void Update()
    {
        if (!continuous) return;
        emitTimer += Time.deltaTime;
        float interval = 1f / Mathf.Max(0.0001f, emitRate);
        while (emitTimer >= interval)
        {
            Emit(1);
            emitTimer -= interval;
        }
    }

    public void EmitBurst()
    {
        Emit(particlesPerBurst);
    }

    public void Emit(int count)
    {
        for (int i = 0; i < count; i++) SpawnParticle();
    }

    private GameObject GetFromPool()
    {
        for (int i = 0; i < pool.Count; i++)
            if (!pool[i].activeInHierarchy)
            {
                pool[i].SetActive(true);
                return pool[i];
            }

        // expand pool if necessary
        var go = Instantiate(particlePrefab, transform);
        pool.Add(go);
        return go;
    }

    private void SpawnParticle()
    {
        var go = GetFromPool();
        var pb = go.GetComponent<ParticleBehavior>();
        if (pb == null) pb = go.AddComponent<ParticleBehavior>();

        Vector3 pos = transform.position;
        Vector3 dir = transform.up;
        float s = Random.Range(settings.minSpeed, settings.maxSpeed) * speedMultiplier;

        switch (emitterType)
        {
            case EmitterType.Point:
                pos += new Vector3(
                    Random.Range(-jitter.x, jitter.x),
                    Random.Range(-jitter.y, jitter.y),
                    Random.Range(-jitter.z, jitter.z)
                );
                float angle = Random.Range(-angleSpread, angleSpread);
                dir = Quaternion.Euler(0, 0, angle) * transform.up;
                break;

            case EmitterType.Area:
                pos += new Vector3(
                    Random.Range(-areaSize.x / 2f, areaSize.x / 2f),
                    Random.Range(-areaSize.y / 2f, areaSize.y / 2f),
                    0f
                );
                dir = (Vector3.up + (Vector3)Random.insideUnitCircle * 0.5f).normalized;
                break;

            case EmitterType.Circle:
                float r = Random.Range(0f, radius);
                float a = Random.Range(0f, Mathf.PI * 2f);
                pos += new Vector3(Mathf.Cos(a), Mathf.Sin(a), 0f) * r;
                dir = (pos - transform.position).normalized;
                if (dir.sqrMagnitude < 0.0001f) dir = transform.up;
                break;
        }

        Vector3 vel = dir.normalized * s;

        // init particle
        pb.Init(settings, pos, vel, ParticleController.Instance.currentBehavior, transform.position);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        if (emitterType == EmitterType.Area)
            Gizmos.DrawWireCube(transform.position, new Vector3(areaSize.x, areaSize.y, 0.1f));
        if (emitterType == EmitterType.Circle)
            Gizmos.DrawWireSphere(transform.position, radius);
    }
}
