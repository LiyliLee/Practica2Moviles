using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Vector2 touchPos_;

    public enum MoveType { DRAG, NONE};
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            // guarda posicion del touch
            touchPos_ = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            GameManager.GetInstance().ProcessInput(MoveType.DRAG, touchPos_);
            Debug.Log("TOUCH");
        }

        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("TOUCH END");
            // fin del movimiento
            GameManager.GetInstance().ProcessInput(MoveType.NONE, touchPos_);
        }
#else
        if (Input.touchCount > 0)
        {
            // guarda posicion del touch
            Touch touch = Input.touches[0];

            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
            {
                touchPos_ = touch.position;
                GameManager.GetInstance().ProcessInput(MoveType.DRAG, touchPos_);
            }

            else if (touch.phase == TouchPhase.Ended)
            {
                // fin del movimiento
                 GameManager.GetInstance().ProcessInput(MoveType.NONE, touchPos_);
            }
        }

#endif
    }
}
