using System.Collections;
using UnityEngine;

namespace GeneralTemplate
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Singleton;
        private void Awake()
        { 
            if (Singleton == null)
            {
                Singleton = this;
            }
            else
            {
                Destroy(this);
            }
        }
        
        [SerializeField]
        private UIControllerBase userInterface;

        [SerializeField]
        private SoundController soundController;

        [SerializeField]
        private VibrationController vibrationController;

        private void Start()
        {
            InitializeSettings();
        }

        private void InitializeSettings()
        {
            hasSound = PlayerPrefs.GetInt("Sound") == 0;
            isHaptic = PlayerPrefs.GetInt("Haptic") == 0;

            soundController.ShouldProduceSound = hasSound;
            userInterface.SetSoundPresentation(hasSound);

            vibrationController.IsAllowedToVibrate = isHaptic;
            userInterface.SetVibrationPresentation(isHaptic);
        }

        #region SettingsAlternations

        private bool isHaptic;
        public void AlternateHaptic()
        {
            isHaptic = !isHaptic;
            PlayerPrefs.SetInt("Haptic", isHaptic ? 0 : 1);
            vibrationController.IsAllowedToVibrate = isHaptic;
            vibrationController.Vibrate();
        }

        private bool hasSound;
        public void AlternateSound()
        {
            hasSound = !hasSound;
            PlayerPrefs.SetInt("Sound", hasSound ? 0 : 1);
            soundController.ShouldProduceSound = hasSound;
            if (hasSound)
            {
                soundController.PlaySoundTurnOn();
            }
        }

        #endregion
    }
}


