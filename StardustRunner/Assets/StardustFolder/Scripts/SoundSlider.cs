using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MoreMountains.InfiniteRunnerEngine
{
    public class SoundSlider : MonoBehaviour
    {
        private float valueInSlider;
        public Slider VolumeLevelSlider;
        public enum WhatSoundType { BackgroundMusic, SoundEffect}

        [Header("SoundType")]
        public WhatSoundType soundType;
        SoundManager sM;

        public void Start()
        {
            sM = SoundManager.Instance;
            switch(soundType)
            {
                case WhatSoundType.BackgroundMusic:
                    valueInSlider = sM.MusicVolume;
                    break;
                case WhatSoundType.SoundEffect:
                    valueInSlider = sM.SfxVolume;
                    break;
            }

            VolumeLevelSlider.value = valueInSlider;
            VolumeLevelSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        }

        public void ValueChangeCheck()
        {
            valueInSlider = VolumeLevelSlider.value;
            sM.ChangeVolumeLevel(valueInSlider, soundType.ToString());
        }
    }
}
