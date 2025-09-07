using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public static ParticleController Instance;
    public SimpleEmitter[] emitters;
    public BehaviorType currentBehavior = BehaviorType.Straight;
    private int activeEmitter = 0;

    void Awake() { Instance = this; }

    void Start()
    {
        if (emitters == null || emitters.Length == 0) return;
        ActivateEmitter(0);
    }

    void Update()
    {
        // keys: 1/2/3 switch emitters (if present)
        if (Input.GetKeyDown(KeyCode.Alpha1)) ActivateEmitter(0);
        if (Input.GetKeyDown(KeyCode.Alpha2) && emitters.Length > 1) ActivateEmitter(1);
        if (Input.GetKeyDown(KeyCode.Alpha3) && emitters.Length > 2) ActivateEmitter(2);

        // cycle behaviors
        if (Input.GetKeyDown(KeyCode.B))
        {
            int next = ((int)currentBehavior + 1) % System.Enum.GetValues(typeof(BehaviorType)).Length;
            currentBehavior = (BehaviorType)next;
            Debug.Log("Behavior: " + currentBehavior);
        }

        // burst
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (emitters.Length > activeEmitter) emitters[activeEmitter].EmitBurst();
        }

        // toggle continuous
        if (Input.GetKeyDown(KeyCode.C))
        {
            var e = emitters[activeEmitter];
            e.continuous = !e.continuous;
            Debug.Log("Emitter continuous: " + e.continuous);
        }
    }

    public void ActivateEmitter(int index)
    {
        if (emitters == null || index < 0 || index >= emitters.Length) return;
        for (int i = 0; i < emitters.Length; i++)
            emitters[i].gameObject.SetActive(i == index);
        activeEmitter = index;
        Debug.Log("Activated emitter " + index + " (" + emitters[index].emitterType + ")");
    }
}

