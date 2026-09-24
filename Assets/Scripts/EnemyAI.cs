using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// NavMesh yamyam/gardiyan AI - ateşe yaklaşmama, fener görünürlüğü
/// SON IŞIK'taki tryMoveEntity mantığının NavMesh hali
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    public enum Type { Cannibal, Guardian }
    public Type enemyType = Type.Cannibal;

    [Header("Stats")]
    public float walkSpeed = 3.2f;
    public float nightSpeed = 4.0f;
    public float damage = 15f;
    public float attackCooldown = 1.2f;

    [Header("Sense")]
    public float seeDistance = 18f;
    public float hearDistance = 12f;
    public float flashlightSeeDistance = 30f;

    [Header("Refs")]
    public Transform player;
    public Transform campfire;
    public DayNightCycle dayNight;

    private NavMeshAgent agent;
    private PlayerController playerCtrl;
    private float lastAttack;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        if (player) playerCtrl = player.GetComponent<PlayerController>();
    }

    void Update()
    {
        if (!player) return;
        bool isNight = dayNight ? dayNight.IsNight : false;
        agent.speed = isNight ? nightSpeed : walkSpeed;

        if (enemyType == Type.Guardian && !isNight)
        {
            agent.isStopped = true;
            return;
        }
        agent.isStopped = false;

        if (enemyType == Type.Cannibal && campfire && Vector3.Distance(transform.position, campfire.position) < 18f
            && Vector3.Distance(player.position, campfire.position) < 18f)
        {
            Vector3 away = (transform.position - campfire.position).normalized * 22f + campfire.position;
            agent.SetDestination(away);
            return;
        }

        float dist = Vector3.Distance(transform.position, player.position);
        bool canSee = CanSeePlayer(dist, isNight);

        if (canSee)
        {
            agent.SetDestination(player.position);
            if (dist < 2.0f && Time.time - lastAttack > attackCooldown) Attack();
        }
        else
        {
            if (!agent.hasPath || agent.remainingDistance < 1f)
            {
                Vector3 rnd = transform.position + Random.insideUnitSphere * 25f;
                rnd.y = transform.position.y;
                if (NavMesh.SamplePosition(rnd, out var hit, 30f, NavMesh.AllAreas))
                    agent.SetDestination(hit.position);
            }
        }
    }

    bool CanSeePlayer(float dist, bool isNight)
    {
        float see = isNight ? (playerCtrl && playerCtrl.IsFlashOn ? flashlightSeeDistance : 12f) : seeDistance;
        if (dist > see) return false;
        return true;
    }

    void Attack()
    {
        lastAttack = Time.time;
        var surv = player.GetComponent<SurvivalManager>();
        if (surv) surv.health -= damage;
        Debug.Log($"{enemyType} vurdu! -{damage} can");
    }
}
