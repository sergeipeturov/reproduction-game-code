using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

/// <summary>
/// Отлов событий тапа на разные объекты
/// </summary>
public class InputManager : MonoBehaviour
{
    public GameObject CurrentSelectedObject { get; set; } = null;

    public UIManager UIManager { get; set; }

    private void Awake()
    {
        EnhancedTouchSupport.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Input.simulateMouseWithTouches = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (UnityEngine.InputSystem.EnhancedTouch.Touch.activeFingers.Count == 1)
        {
            //получаем тач
            UnityEngine.InputSystem.EnhancedTouch.Touch activeTouch = UnityEngine.InputSystem.EnhancedTouch.Touch.activeFingers[0].currentTouch;

            //получаем hit и коллайдер объекта, по которому тапнули
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(activeTouch.startScreenPosition), Vector2.zero);
            var collider = hit.collider;

            //если коллайдер есть, разбираемся, по какому объекту тапнули
            if (collider != null)
            {
                //если уже есть выбранный объект и тапнутый сейчас это не он, убираем выделение с выбранного, прежде чем выделить тапнутый
                if (CurrentSelectedObject != null)
                {
                    if (CurrentSelectedObject != collider.gameObject)
                        TakeOffSelection();
                    else //если тапнули по уже выбранному, ничего не делаем вообще
                        return;
                }

                //разбираемся, по чему тапнули и делам соответствующие действия
                if (collider.gameObject.tag.Contains("creature_"))
                {
                    var creatureScript = collider.gameObject.GetComponent<CreatureBase>();
                    creatureScript.IsSelected = true;
                    CurrentSelectedObject = collider.gameObject;
                    UIManager.SetCreatureToStatsPanel(creatureScript);
                }
                else
                {
                    //TODO: тап по другим объектам
                }
            }
            else
            {
                TakeOffSelection();
            }
        }
    }

    private void TakeOffSelection()
    {
        if (CurrentSelectedObject != null)
        {
            if (CurrentSelectedObject.tag.Contains("creature_"))
            {
                CurrentSelectedObject.GetComponent<CreatureBase>().IsSelected = false;
                CurrentSelectedObject = null;
            }
        }
        UIManager.CloseAllPanels();
    }
}
