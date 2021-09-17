using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureBase : MonoBehaviour
{
    public delegate void CollisionHandler(CollisionEventArgs e);
    /// <summary>
    /// Столкновение с каким-то объектом, которое требует обработки вовне
    /// </summary>
    public event CollisionHandler CollisionEvent;

    /// <summary>
    /// Спрайт квадрата вокруг, который активируется при тапе на существо
    /// </summary>
    public GameObject SelectionRectangle;
    /// <summary>
    /// Спрайт, обозначающий текущее действие (TODO: возможно, убрать, когда перейдем на анимации)
    /// </summary>
    public GameObject ActionSprite;

    /// <summary>
    /// Цвет создания
    /// </summary>
    public CreatureColor Color { get; protected set; } = CreatureColor.blue;

    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public Guid Uid { get; set; } = Guid.NewGuid();
    /// <summary>
    /// Идентификатор клетки, в которой создание сейчас
    /// </summary>
    public Guid CurrentCellUid { get; set; }

    /// <summary>
    /// Движение создания
    /// </summary>
    public CreatureMoveController MoveController { get; set; }

    /// <summary>
    /// Размножение создания
    /// </summary>
    public CreatureReproductionController ReproductionController { get; set; }

    /// <summary>
    /// Статус создания
    /// </summary>
    public CreatureStateController StateController { get; set; }

    /// <summary>
    /// Показатели создания
    /// </summary>
    public CreatureStatsController StatsController { get; set; }

    /// <summary>
    /// Действия, для которых нет отдельного контроллера
    /// </summary>
    public CreatureActionsController ActionsController { get; set; }

    /// <summary>
    /// Мирный
    /// </summary>
    public bool Peaceful { get; set; } = true;

    /// <summary>
    /// Выбран
    /// </summary>
    public bool IsSelected { get { return isSelected; } set { isSelected = value; SelectionRectangle.SetActive(isSelected); } }
    private bool isSelected;

    /// <summary>
    /// Условие повышения настроения
    /// </summary>
    /// <returns></returns>
    public virtual bool MoodCondition() { return true; }

    /// <summary>
    /// Функция, выполняемая созданием
    /// </summary>
    public virtual void PerformFunction() { }

    private void Awake()
    {
        decisionController = GetComponent<CreatureDecisionController>();
        decisionController.MadeDecisionEvent += OnMadeDecisionEvent;
        MoveController = GetComponent<CreatureMoveController>();
        ReproductionController = GetComponent<CreatureReproductionController>();
        StateController = GetComponent<CreatureStateController>();
        StatsController = GetComponent<CreatureStatsController>();
        StatsController.InitializeParams(Color);
        ActionsController = GetComponent<CreatureActionsController>();
        IsSelected = false;
    }

    void Start()
    {

    }

    void Update()
    {
        if (StatsController.CanPerformFunction())
            PerformFunction();
    }

    /// <summary>
    /// Возникает при появлении создания
    /// </summary>
    public virtual void OnAwake()
    {
        Color = CreatureColor.blue;
    }

    /// <summary>
    /// Возникает при обнаружении столкновения. Нужно, чтобы создания разного цвета реагировали на столкновения по-разному, но в чем-то были похожи
    /// </summary>
    public virtual void CollisionDetect(GameObject collider)
    {
        if (collider.tag == "feeder" && StateController.CurrentState == CreatureState.feedSearching)
        {
            ActionsController.StartFeeding(collider);
        }
        else
        {
            var e = new CollisionEventArgs(this.gameObject, collider);
            CollisionEvent?.Invoke(e);
        }
    }

    /// <summary>
    /// Возникает при принятии решения
    /// </summary>
    /// <param name="decision"></param>
    private void OnMadeDecisionEvent(CreatureDecision decision)
    {
        if (decision != null)
        {
            switch (decision.Type)
            {
                case CreatureDecisionType.move:
                case CreatureDecisionType.feedSearch:
                    StateController.CurrentState = decision.State;
                    MoveController.MoveDirection = decision.Direction;
                    break;
                case CreatureDecisionType.dancingAlone:
                    ActionsController.StartDancing();
                    break;
                case CreatureDecisionType.playingAlone:
                    ActionsController.StartPlaying();
                    break;
                case CreatureDecisionType.singingAlone:
                    ActionsController.StartSinging();
                    break;
                case CreatureDecisionType.sleep:
                    ActionsController.StartSleeping();
                    break;
            }
        }
    }

    /// <summary>
    /// Столкновение с другим объектом
    /// </summary>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collider = collision.gameObject;
        CollisionDetect(collider);
    }

    protected CreatureDecisionController decisionController;
}
