using UnityEngine;

public class EnemySlowdownEffect : EnemyEffect
{
    private float effectTimer = 0.0f;
    private float effectDuration;
    private float slowdownEffectiveness;

    public EnemySlowdownEffect(Turret turret, Enemy enemy, float slowdownEffectDuration, float slowdownEffectiveness) : base(turret, enemy)
    {
        effectDuration = slowdownEffectDuration;
        this.slowdownEffectiveness = enemy.movementSpeed * slowdownEffectiveness;
    }

    public override void ApplyEffect()
    {
        enemy.SetMovementSpeed(enemy.movementSpeed - slowdownEffectiveness);
    }

    public override void RemoveEffect()
    {
        enemy.SetMovementSpeed(enemy.movementSpeed + slowdownEffectiveness);
    }

    public override bool CheckDuplicates(EnemyEffect enemyEffect)
    {
        EnemySlowdownEffect enemySlowdownEffect = enemyEffect as EnemySlowdownEffect;

        if(enemySlowdownEffect != null)
        {
            if(enemySlowdownEffect.turret.data == turret.data)
            {
                effectTimer = 0.0f;

                return true;
            }
        }

        return false;
    }

    public override void EffectUpdate()
    {
        effectTimer += Time.deltaTime;

        if(effectTimer > effectDuration)
        {
            enemy.RemoveEffect(this);
        }
    }
}