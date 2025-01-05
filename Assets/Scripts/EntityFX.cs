using Cinemachine;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class EntityFX : MonoBehaviour
{
    protected Hero hero;
    protected SpriteRenderer sr;

    
    [Header("Flash FX")]
    [SerializeField] private float flashDuration = .2f;
    [SerializeField] private Material hitMaterial;
    private Material originalMaterial;

    //[Header("Hit FX")]
    //[SerializeField] private GameObject hitFx;
    //[SerializeField] private GameObject criticalHitFx;

    private GameObject myHealthBar;

    protected virtual void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        hero = HeroManager.instance.hero;

        originalMaterial = sr.material;

        myHealthBar = GetComponentInChildren<UI_HealthBar>(true).gameObject;
    }


    public void MakeTransprent(bool _transprent)
    {
        if (_transprent)
        {
            myHealthBar.SetActive(false);
            sr.color = Color.clear;
        }
        else
        {
            myHealthBar.SetActive(true);
            sr.color = Color.white;
        }
    }

    private IEnumerator FlashFX()
    {
        sr.material = hitMaterial;

        yield return new WaitForSeconds(flashDuration);

        sr.material = originalMaterial;
    }

    private void RedColorBlink()
    {
        if (sr.color != Color.white)
            sr.color = Color.white;
        else
            sr.color = Color.red;
    }

    private void CancelRedBlink()
    {
        CancelInvoke();
        sr.color = Color.white;
    }
}
