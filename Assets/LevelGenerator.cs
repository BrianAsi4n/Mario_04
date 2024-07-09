using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private const float GroundSpacing = 6.7f; 

    public static LevelGenerator Instance { get; private set; }

    [SerializeField] private List<Transform> grounds;
    
   [HideInInspector] public float ShiftDistance = 0;
    private Queue<Transform> groundQueue = new Queue<Transform>();
    private float nextGroundPosition = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
        for (int i = 0; i < grounds.Count; i++)
        {
            Transform t = grounds[i];
            groundQueue.Enqueue(t);
        }
        nextGroundPosition = grounds[grounds.Count - 1].position.x + GroundSpacing;
    }

    private void Update()
    {
        ShiftGround();
    }
    public void ShiftGround()
    {
        if (ShiftDistance > GroundSpacing)
        {
            ShiftDistance = ShiftDistance - GroundSpacing;
            Transform ground = groundQueue.Dequeue();
            ground.position = new Vector3(nextGroundPosition, ground.position.y);
            nextGroundPosition += GroundSpacing;
            groundQueue.Enqueue(ground);
        }
    }


}
