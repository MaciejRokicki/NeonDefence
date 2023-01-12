using System.Collections.Generic;
using UnityEngine;

public class EnemyEffectHandler : MonoBehaviour 
{
    private Enemy enemy;
    [SerializeField]
    private List<EnemyEffect> effects;
    public bool slowdownEffect = false;
    public bool poisonEffect = false;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        effects = new List<EnemyEffect>();
    }

    private void Update()
    {
        for (int i = 0; i < effects.Count; i++)
        {
            if (effects[i] != null)
            {
                effects[i].Update();
            }
        }
    }

    public void Reset()
    {
        effects = new List<EnemyEffect>();
        slowdownEffect = false;
        poisonEffect = false;
    }

    public void ApplyEffect(EnemyEffect enemyEffect)
    {
        if(enemyEffect.effectDuration <= 0.0f)
        {
            return;
        }

        bool isEffectDuplicated = false;

        foreach (EnemyEffect effect in effects)
        {
            if (effect.CheckDuplicates(enemyEffect))
            {
                isEffectDuplicated = true;
                effect.turret = enemyEffect.turret;
                break;
            }
        }

        if (!isEffectDuplicated)
        {
            effects.Add(enemyEffect);

            if(enemyEffect is EnemySlowdownEffect)
            {
                slowdownEffect = true;

                enemy.effectSprite.color = enemy.slowdownEffectSpriteColor;
            }

            if(enemyEffect is EnemyPoisonEffect)
            {
                poisonEffect = true;

                enemy.effectSprite.color = enemy.poisonEffectSpriteColor;
            }

            if(slowdownEffect && poisonEffect)
            {
                enemy.effectSprite.color = enemy.poisonAndSlowdownEffectSpriteColor;
            }

            enemyEffect.OnEffectStart();
        }
    }

    public void RemoveEffect(EnemyEffect enemyEffect)
    {
        slowdownEffect = false;
        poisonEffect = false;

        enemyEffect.OnEffectEnd();
        effects.Remove(enemyEffect);

        foreach (EnemyEffect effect in effects)
        {
            if (effect is EnemySlowdownEffect)
            {
                slowdownEffect = true;
            }

            if (effect is EnemyPoisonEffect)
            {
                poisonEffect = true;
            }
        }

        if (!slowdownEffect && !poisonEffect)
        {
            enemy.effectSprite.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        }
        else
        {
            if(slowdownEffect)
            {
                enemy.effectSprite.color = enemy.slowdownEffectSpriteColor;
            }

            if(poisonEffect)
            {
                enemy.effectSprite.color = enemy.poisonEffectSpriteColor;
            }
        }
    }

    public void RemoveEffects(Turret turret)
    {
        for (int i = 0; i < effects.Count; i++)
        {
            EnemyEffect enemyEffect = effects[i];

            if (enemyEffect.turret == turret)
            {
                RemoveEffect(enemyEffect);
            }
        }
    }
}
