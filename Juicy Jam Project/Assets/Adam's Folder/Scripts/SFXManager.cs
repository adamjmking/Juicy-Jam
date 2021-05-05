using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    //Audiosource
    public AudioSource sfxSource;

    //List of audioclips
    [SerializeField] public AudioClip[] sfxList;
    [SerializeField] public AudioClip[] attackSfxList;
    [SerializeField] public AudioClip[] incapacitatedSfxList;
    [SerializeField] public AudioClip[] damageTakenSfxList;
    [SerializeField] public AudioClip[] slotmachineDamageTakenSfxList;
    [SerializeField] public AudioClip[] chipDeathSfxList;
    [SerializeField] public AudioClip[] chipHurtSfxList;
    [SerializeField] public AudioClip[] cardDeathSfxList;
    [SerializeField] public AudioClip[] cardHurtSfxList;
    [SerializeField] public AudioClip[] babyDiceDeathSfxList;
    [SerializeField] public AudioClip[] babyDiceHurtSfxList;
    [SerializeField] public AudioClip[] diceDeathSfxList;
    [SerializeField] public AudioClip[] diceHurtSfxList;
    [SerializeField] public AudioClip[] babyDiceAttackSfxList;
    [SerializeField] public AudioClip[] footstepsSfxList;
    [SerializeField] public AudioClip[] roundwonSfxList;

    public void PlaySound(int index)
    {
        sfxSource.PlayOneShot(sfxList[index]);
    }

    public void PlayAttackSound()
    {
        int randomIndex = (int)(Random.Range(0, attackSfxList.Length)-0.1f);
        sfxSource.PlayOneShot(attackSfxList[randomIndex]);
    }
    public void PlayIncapactitatedSound()
    {
        int randomIndex = (int)(Random.Range(0, incapacitatedSfxList.Length) - 0.1f);
        sfxSource.PlayOneShot(incapacitatedSfxList[randomIndex]);
    }
    public void PlayDamageTakenSound()
    {
        int randomIndex = (int)(Random.Range(0, damageTakenSfxList.Length) - 0.1f);
        sfxSource.PlayOneShot(damageTakenSfxList[randomIndex]);
    }
    public void PlaySlotMachineDamageTakenSound()
    {
        int randomIndex = (int)(Random.Range(0, slotmachineDamageTakenSfxList.Length) - 0.1f);
        sfxSource.PlayOneShot(slotmachineDamageTakenSfxList[randomIndex]);
    }
    public void PlayChipDeathSound()
    {
        int randomIndex = (int)(Random.Range(0, chipDeathSfxList.Length) - 0.1f);
        sfxSource.PlayOneShot(chipDeathSfxList[randomIndex]);
    }
    public void PlayChipHurtSound()
    {
        int randomIndex = (int)(Random.Range(0, chipHurtSfxList.Length) - 0.1f);
        sfxSource.PlayOneShot(chipHurtSfxList[randomIndex]);
    }
    public void PlayCardDeathSound()
    {
        int randomIndex = (int)(Random.Range(0, cardDeathSfxList.Length) - 0.1f);
        sfxSource.PlayOneShot(cardDeathSfxList[randomIndex]);
    }
    public void PlayCardHurtSound()
    {
        int randomIndex = (int)(Random.Range(0, cardHurtSfxList.Length) - 0.1f);
        sfxSource.PlayOneShot(cardHurtSfxList[randomIndex]);
    }
    public void PlayDiceDeathSound()
    {
        int randomIndex = (int)(Random.Range(0, diceDeathSfxList.Length) - 0.1f);
        sfxSource.PlayOneShot(diceDeathSfxList[randomIndex]);
    }
    public void PlayDiceHurtSound()
    {
        int randomIndex = (int)(Random.Range(0, diceHurtSfxList.Length) - 0.1f);
        sfxSource.PlayOneShot(diceHurtSfxList[randomIndex]);
    }
    public void PlayBabyDiceDeathSound()
    {
        int randomIndex = (int)(Random.Range(0, babyDiceDeathSfxList.Length) - 0.1f);
        sfxSource.PlayOneShot(babyDiceDeathSfxList[randomIndex]);
    }
    public void PlayBabyDiceHurtSound()
    {
        int randomIndex = (int)(Random.Range(0, babyDiceHurtSfxList.Length) - 0.1f);
        sfxSource.PlayOneShot(babyDiceHurtSfxList[randomIndex]);
    }

    public void BabyDiceAttackSound()
    {
        int randomIndex = (int)(Random.Range(0, babyDiceAttackSfxList.Length) - 0.1f);
        sfxSource.PlayOneShot(babyDiceAttackSfxList[randomIndex]);
    }
    public void FootStepsSound()
    {
        int randomIndex = (int)(Random.Range(0, footstepsSfxList.Length) - 0.1f);
        sfxSource.PlayOneShot(footstepsSfxList[randomIndex]);
    }
    public void PlayRoundWonSound()
    {
        int randomIndex = (int)(Random.Range(0, roundwonSfxList.Length) - 0.1f);
        sfxSource.PlayOneShot(roundwonSfxList[randomIndex]);
    }
}
