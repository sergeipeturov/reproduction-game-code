using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//TODO: сделать, чтобы при изменении ActiveState выезжало и заезжало за границу экрана
public class CreatureStatsPanelScript : MonoBehaviour
{
    public CreatureBase CurrentCreature { get; private set; }

    public TextMeshProUGUI NameText;
    public TextMeshProUGUI StatusText;
    public TextMeshProUGUI DiseaseText;
    public Slider PowerSlider;
    public Slider MoodSlider;
    public Slider ComfortSlider;
    public Slider HealthSlider;
    public Slider AgeSlider;

    private bool isNowActive { get { return gameObject.activeSelf; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isNowActive && CurrentCreature != null)
        {
            //текстовая инфа
            StatusText.text = CreatureStateString.Get(CurrentCreature.StateController.CurrentState);
            DiseaseText.text = "";

            //полоски состояния
            PowerSlider.value = CurrentCreature.StatsController.CurrentPower;
            MoodSlider.value = CurrentCreature.StatsController.CurrentMood;
            ComfortSlider.value = CurrentCreature.StatsController.CurrentComfort;
            HealthSlider.value = CurrentCreature.StatsController.CurrentHealth;
            AgeSlider.value = CurrentCreature.StatsController.CurrentAge;
        }
    }

    /// <summary>
    /// Установить существо для отображения информации о нем в панели состояния существа. Передать null, чтобы убрать панель с экрана
    /// </summary>
    public void SetCurrentCreature(CreatureBase creature)
    {
        if (creature == null && gameObject.activeSelf)
            gameObject.SetActive(false);

        if (CurrentCreature != creature)
        {
            CurrentCreature = creature;
            if (creature != null)
            {
                NameText.text = "Имярек";
                SetMaxValuesToSliders();
                gameObject.SetActive(true);
            }
            else
                gameObject.SetActive(false);
        }
    }

    private void SetMaxValuesToSliders()
    {
        PowerSlider.maxValue = CurrentCreature.StatsController.GetMaxPower();
        MoodSlider.maxValue = CurrentCreature.StatsController.GetMaxMood();
        ComfortSlider.maxValue = CurrentCreature.StatsController.GetMaxComfort();
        HealthSlider.maxValue = CurrentCreature.StatsController.GetMaxHealth();
        AgeSlider.maxValue = CurrentCreature.StatsController.GetMaxAge();
    }
}
