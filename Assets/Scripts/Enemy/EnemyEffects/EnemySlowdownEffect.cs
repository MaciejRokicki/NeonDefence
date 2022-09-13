using UnityEngine;

public class EnemySlowdownEffect : EnemyEffect
{
    private float effectTimer = 0.0f;
    private float effectDuration;
    private float slowdownEffectiveness;

    public EnemySlowdownEffect(Enemy enemy, float slowdownEffectDuration, float slowdownEffectiveness) : base(enemy)
    {
        effectDuration = slowdownEffectDuration;
        this.slowdownEffectiveness = slowdownEffectiveness;

        ApplyEffect();
    }

    protected override void ApplyEffect()
    {
        enemy.SetMovementSpeed(enemy.data.movementSpeed - enemy.data.movementSpeed * slowdownEffectiveness);
    }

    protected override void RemoveEffect()
    {
        enemy.SetMovementSpeed(enemy.data.movementSpeed);
    }

    public override bool CheckDuplicates(EnemyEffect enemyEffect)
    {
        EnemySlowdownEffect enemySlowdownEffect = enemyEffect as EnemySlowdownEffect;

        if(enemySlowdownEffect != null)
        {
            return true;
        }

        return false;
    }

    public override void EffectUpdate()
    {
        effectTimer += Time.deltaTime;

        if(effectTimer > effectDuration)
        {
            RemoveEffect();
            enemy.RemoveEffect(this);
        }
    }
}