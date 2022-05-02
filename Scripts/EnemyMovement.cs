using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Patrol points")]
    [SerializeField] private Transform LeftEdge;
    [SerializeField] private Transform RightEdge;

    [Header("Enemy")]
    [SerializeField] private Transform Enemy;

    [Header("Movement speed")] 
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool moveLeft;


    private void Awake()
    {
        initScale = Enemy.localScale;
    }

    private void Update()
    {
        if (moveLeft)
        {
            if (Enemy.position.x >= LeftEdge.position.x)
                movementDirection(-1);
            else
            
                moveLeft = !moveLeft;
            
        }
        else
        {
            if (Enemy.position.x <= RightEdge.position.x)
                movementDirection(1);

            else
            
                moveLeft = !moveLeft;
            
        }
    }
    private void movementDirection(int direction)
    {
        Enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * direction, initScale.y, initScale.z);

        Enemy.position = new Vector3(Enemy.position.x + Time.deltaTime * direction * speed, 
            Enemy.position.y, Enemy.position.z);
    }
}

