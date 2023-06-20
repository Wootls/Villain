using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundManagement : MonoBehaviour
{
    // 오디오 믹서
    public AudioMixer audioMixer;

    // 동전소리
    public AudioSource coinSound;

    // 슬라이더
    public Slider bgmSlider;
    public Slider sfxSlider;

    // 볼륨 조절
    public void SetBgmVolume()
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(bgmSlider.value) * 20);
    }

    public void SetSfxVolume()
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(sfxSlider.value) * 20);
    }
}
