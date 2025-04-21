using System.Collections.Generic;
using UnityEngine;

public class CommonButtonLogic : MonoBehaviour
{
    [SerializeField] public List<AudioClip> sfxList;
    public int sfxCount;

    public void produceSFX()
    {
        sfxCount = Random.Range(0, sfxList.Count-1);
        //AudioManager.instance.PlaySFXOnce(sfxList[sfxCount]);
    }

    public void OnOptionsPressed()
    {
        produceSFX();
        MenuManager.instance.ChangeToOptions();
    }

    public void OnQuitPressed()
    {
        produceSFX();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
            // Quit the application
            Application.Quit();
        #endif
    }
}
