using System.Collections.Generic;
using UnityEngine;

public class EnemyEffectHandler : MonoBehaviour 
{
    [SerializeField]
    private List<EnemyEffect> effects;
    private Enemy enemy;

    private void Awake()
    {
        effects = new List<EnemyEffect>();
    }

    private void Update()
    {
        for (int i = 0; i < effects.Count; i++)
        {
            if (effects[i] != null)
            {
                effects[i].EffectUpdate();
            }
        }
    }

    public EnemyEffectHandler SetEnemy(Enemy enemy)
    {
        this.enemy = enemy;

        return this;
    }

    public void ApplyEffect(EnemyEffect enemyEffect)
    {
        bool isEffectDuplicated = false;

        foreach (EnemyEffect enemy in effects)
        {
            if (enemy.CheckDuplicates(enemyEffect))
            {
                isEffectDuplicated = true;
            }
        }

        if (!isEffectDuplicated)
        {
            effects.Add(enemyEffect);
            enemyEffect.ApplyEffect();
        }
    }

    public void RemoveEffect(EnemyEffect enemyEffect)
    {
        enemyEffect.RemoveEffect();
        effects.Remove(enemyEffect);
    }

    public void RemoveEffects(Turret turret)
    {
        foreach (EnemyEffect enemy in effects)
        {
            if (enemy.turret == turret)
            {
                enemy.RemoveEffect();
            }
        }
    }
}
