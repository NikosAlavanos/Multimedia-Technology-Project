using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class CharacterStats : MonoBehaviour
{
    private EntityFX fx;

    [SerializeField] private AudioSource audioSource; // The AudioSource component
    [SerializeField] private AudioClip[] damageSounds; // Array of damage sound effects
    [SerializeField] private AudioMixerGroup soundFxGroup; // Reference to the SoundFx group in the mixer

    
    [Header("Major stats")]
    public Stat strength;
    public Stat agility;
    public Stat intelligence;
    public Stat vitality;

    [Header("Attack stats")]
    public Stat damage;
    public Stat critChance;
    public Stat critPower;

    [Header("Defence stats")]
    public Stat maxHealth;
    public Stat armor;
    public Stat evasion;
    public Stat magicResistance;

    [Header("Magic stats")]
    public Stat fireDamage;
    public Stat iceDamage;
    public Stat lightingDamage;

    public int currentHealth;

    public System.Action onHealthChanged;

    public bool isDead { get; private set; }
    public bool isInvincible { get; private set; }

    protected virtual void Start()
    {
        critPower.SetDefaultValue(151);
        currentHealth = GetMaxHealthValue();

        fx = GetComponent<EntityFX>();
        
        // Assign the AudioMixerGroup to the AudioSource's output
        if (audioSource != null && soundFxGroup != null)
        {
            audioSource.outputAudioMixerGroup = soundFxGroup;
        }
    }

    public virtual void IncreaseStatBy(int _modifier, float _duration, Stat _statToModify)
    {
        // start corototuine for stat increase
        StartCoroutine(StatModCoroutine(_modifier, _duration, _statToModify));
    }

    private IEnumerator StatModCoroutine(int _modifier, float _duration, Stat _statToModify)
    {
        _statToModify.AddModifier(_modifier);

        yield return new WaitForSeconds(_duration);

        _statToModify.RemoveModifier(_modifier);
    }

    public virtual void DoDamage(CharacterStats _targetStats)
    {
        if (TargetCanAvoidAttack(_targetStats))
            return;

        _targetStats.GetComponent<Entity>().SetupKnockbackDir(transform);

        int totalDamage = damage.GetValue() + strength.GetValue();

        if (CanCrit())
        {
            totalDamage = CalculateCriticalDamage(totalDamage);
        }

        totalDamage = CheckTargetArmor(_targetStats, totalDamage);
        _targetStats.TakeDamage(totalDamage);
    }


    public virtual void TakeDamage(int _damage)
    {
        if(isInvincible)
            return;

        PlayRandomDamageSound();
        
        DecreaseHealthBy(_damage);

        GetComponent<Entity>().DamageImpact();
        fx.StartCoroutine("FlashFX");

        if (currentHealth < 0 && !isDead)
            Die();
    }
    
    private void PlayRandomDamageSound()
    {
        if (damageSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, damageSounds.Length);
            AudioClip randomClip = damageSounds[randomIndex];
            audioSource.PlayOneShot(randomClip);
        }
    }

    public virtual void IncreaseHealthBy(int _amount)
    {
        currentHealth += _amount;

        if (currentHealth > GetMaxHealthValue())
            currentHealth = GetMaxHealthValue();

        if (onHealthChanged != null)
            onHealthChanged();
    }

    protected virtual void DecreaseHealthBy(int _damage)
    {
        currentHealth -= _damage;

        if (onHealthChanged != null)
            onHealthChanged();
    }

    protected virtual void Die()
    {
        isDead = true;
    }

    public void MakeInvincible(bool _invincible) => isInvincible = _invincible;

    #region Calculations for Stats

    private int CheckTargetArmor(CharacterStats _targetStats, int totalDamage)
    {
        totalDamage -= _targetStats.armor.GetValue();
        totalDamage = Mathf.Clamp(totalDamage, 0, int.MaxValue);
        return totalDamage;
    }

    private bool TargetCanAvoidAttack(CharacterStats _targetStats)
    {
        int totalEvasion = _targetStats.evasion.GetValue() + _targetStats.agility.GetValue();

        if (Random.Range(0, 100) < totalEvasion)
        {
            return true;
        }
        return false;
    }

    private bool CanCrit()
    {
        int totalCriticalChance = critChance.GetValue() + agility.GetValue(); 

        if (Random.Range(0, 100) <= totalCriticalChance)
        {
            return true;
        }

        return false;
    }

    private int CalculateCriticalDamage(int _damage)
    {
        float totalCritPower = (critPower.GetValue() + strength.GetValue()) * .01f; //in order to work as a %
        float critDamage = _damage * totalCritPower;

        return Mathf.RoundToInt(critDamage);
    }

    public int GetMaxHealthValue()
    {
        return maxHealth.GetValue() + vitality.GetValue() * 5;
    }

    #endregion
}
