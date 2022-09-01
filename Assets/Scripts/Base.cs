using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField]
    private float health = 100.0f;

    private void Start()
    {
        
    }


    private void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if(health < 0)
        {
            //TODO: GameOver
        }
    }
}
