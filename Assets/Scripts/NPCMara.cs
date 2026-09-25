using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Dr. Mara NPC - köy kulübesinde, görev verir, umut artırır
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class NPCMara : MonoBehaviour
{
    public Transform player;
    public HopeSystem hopeSystem;
    public float interactDistance = 3f;
    private NavMeshAgent agent;

    void Awake() { agent = GetComponent<NavMeshAgent>(); }

    void Update()
    {
        if (!player) return;
        float d = Vector3.Distance(transform.position, player.position);
        if (d < interactDistance && Input.GetKeyDown(KeyCode.E))
        {
            Talk();
        }
        // NavMesh yoksa hareketsiz dur (bembeyaz hata vermesin)
        if (agent == null || !agent.isOnNavMesh) return;
        try
        {
            if (!agent.hasPath || agent.remainingDistance < 1f)
            {
                Vector3 rnd = transform.position + Random.insideUnitSphere * 8f;
                if (NavMesh.SamplePosition(rnd, out var hit, 10f, NavMesh.AllAreas))
                    agent.SetDestination(hit.position);
            }
        }
        catch { /* NavMesh bake edilene kadar bekle */ }
    }

    void Talk()
    {
        Debug.Log("Mara: Bugün 70 güne bir gün daha yaklaştık, umudunu kaybetme!");
        if (hopeSystem) hopeSystem.AddHope(8f);
    }
}
