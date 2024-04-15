using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Assets.TakiExtension.SoundAdjustment
{
    public class SoundAdjustment : MonoBehaviour
    {
        // オーディオミキサー関連の変数。
        [SerializeField] AudioMixer mixer;
        [SerializeField] string mixerParameter_BGMVolume;
        [SerializeField] string mixerParameter_SEVolume;

        // サウンド調整用のスライダー
        [SerializeField] Slider BGMSlider;
        [SerializeField] Slider SESlider;
        // サウンドの音量が変わった時用に再生するオーディオ
        [SerializeField] AudioSource SETestAudio;
        [SerializeField] private float sampleSoundSpan;
        private float time;

        private void Awake()
        {
            BGMSlider.onValueChanged.AddListener(BGMChanged);
            SESlider.onValueChanged.AddListener(SEChanged);

            //ここ修正ポイント、値が変わるたびに何回も何回も鳴る
            SESlider.onValueChanged.AddListener(_ =>
            {
                if (time > sampleSoundSpan)
                {
                    SETestAudio?.Play();
                    time = 0;
                }
            });
        }

        private void Update()
        {
            time += Time.deltaTime;
        }


        void BGMChanged(float slider)
        {

            mixer.SetFloat(mixerParameter_BGMVolume, Liner2dB(slider));
        }

        void SEChanged(float slider)
        {
           // mixer.SetFloat( mixerParameter_SEVolume, Liner2dB(slider));

        }


        float Liner2dB(float liner)
        {
            if (liner < 0.001f)
            {
                liner = 0.001f;
            }
            return 20 * Mathf.Log10(liner);
        }


    }

}