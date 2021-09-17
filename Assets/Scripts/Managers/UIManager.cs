using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject CreatureStatsPanel;

    public TextMeshProUGUI CreatureCountText;
    public TextMeshProUGUI TemperatureText;

    // Start is called before the first frame update
    void Start()
    {
        CloseAllPanels();
    }

    // Update is called once per frame
    void Update()
    {
        CreatureCountText.text = $"Кол-во созданий: {CellManagerStatic.CreatureCountInAllCells}";
        TemperatureText.text = $"Температура: {CellManagerStatic.Cells.FirstOrDefault().CurrentTemperature.ToString("N2")}"; //TODO: перенести в панель состояния клетки, когда та будет готова
    }

    /// <summary>
    /// Установить существо для отображения информации о нем в панели состояния существа. Передать null, чтобы убрать панель с экрана
    /// </summary>
    /// <param name="creautre"></param>
    public void SetCreatureToStatsPanel(CreatureBase creature)
    {
        if (creature != null)
        {
            CreatureStatsPanel.GetComponent<CreatureStatsPanelScript>().SetCurrentCreature(creature);
        }
        else
        {
            CreatureStatsPanel.GetComponent<CreatureStatsPanelScript>().SetCurrentCreature(null);
        }
    }

    /// <summary>
    /// Убрать с экрана все панели
    /// </summary>
    public void CloseAllPanels()
    {
        //панель показателей создания
        CreatureStatsPanel.GetComponent<CreatureStatsPanelScript>().SetCurrentCreature(null);
    }
}
