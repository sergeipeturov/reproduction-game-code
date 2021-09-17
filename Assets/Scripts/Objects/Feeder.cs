using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Кормушка
/// </summary>
public class Feeder : MonoBehaviour
{
    public Sprite FullOfPeacefulFeedSprite;
    public Sprite FullOfWildFeedSprite;
    public Sprite EmptySprite;

    /// <summary>
    /// Полная (true) или пустая (false)
    /// </summary>
    public bool IsFull
    {
        get
        {
            return CurrentFeedCount > 0;
        }
    }

    /// <summary>
    /// Какой едой наполнена сейчас: мирной (true) или дикой (false)
    /// </summary>
    public bool IsPeacefull { get; set; } = true;

    private float currentFeedCount;
    /// <summary>
    /// Сколько единиц еды сейчас в кормушке
    /// </summary>
    public float CurrentFeedCount { get { return currentFeedCount; } set { OnCurrentFeedCountChanging(currentFeedCount, value); currentFeedCount = value; } }

    /// <summary>
    /// Максимальное кол-во еды для кормушки
    /// </summary>
    public float MaxFeedCount { get; set; } = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("feed: " + CurrentFeedCount);
    }

    /// <summary>
    /// Добавить еды до максимального кол-ва
    /// </summary>
    public void AddMaxFeed()
    {
        CurrentFeedCount = MaxFeedCount;
    }

    private void OnCurrentFeedCountChanging(float oldValue, float newValue)
    {
        if (oldValue == 0 && newValue > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = IsPeacefull ? FullOfPeacefulFeedSprite : FullOfWildFeedSprite;
        }
        if (oldValue < 0 && newValue == 0)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = EmptySprite;
        }
    }
}
