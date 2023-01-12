using UnityEngine;

public class EnemySlowdownEffect : EnemyEffect
{
    private float slowdownEffectiveness;

    public EnemySlowdownEffect(Turret turret, Enemy enemy, float effectDuration, float slowdownEffectiveness) : base(turret, enemy, effectDuration)
    {
        this.slowdownEffectiveness = enemy.movementSpeed * slowdownEffectiveness;
    }

    public override void OnEffectStart()
    {
        enemy.SetMovementSpeed(enemy.movementSpeed - slowdownEffectiveness);
        enemy.enemyEffectHandler.slowdownEffect = true;
    }

    public override void OnEffectEnd()
    {
        enemy.SetMovementSpeed(enemy.movementSpeed + slowdownEffectiveness);
    }

    public override void Update()
    {
        effectTimer += Time.deltaTime;

        if (effectTimer > effectDuration)
        {
            enemy.enemyEffectHandler.RemoveEffect(this);
        }
    }
}