using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;

namespace MoreMountains.InfiniteRunnerEngine
{
    public class TutorialTouch : MMSingleton<TutorialTouch>
    {
        [SerializeField]
        private string tutorialType;
        protected Vector2 firstPressPos;
        protected Vector2 secondPressPos;
        protected Vector2 currentSwipe;
        [SerializeField]
        protected bool touchDown;
        [SerializeField]
        protected bool triggerInput_1;
        protected bool triggerInput_2;

        void Update()
        {
            if (GameManager.Instance.isPopUp)
                MouseSwipe();
        }
        protected virtual void MouseSwipe()
        {
            if (Input.GetMouseButtonDown(0))
            {
                touchDown = true;
                //save began touch 2d point
                firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            }
            if (touchDown)
            {
                //save ended touch 2d point
                secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

                //create vector from the two points
                currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                //normalize the 2d vector
                currentSwipe.Normalize();

                if (Vector2.Distance(firstPressPos, secondPressPos) > 30 && !triggerInput_1)
                {
                    //swipe upwards
                    if (currentSwipe.y > 0 && currentSwipe.x > -0.4f && currentSwipe.x < 0.4f && (tutorialType == "up" || tutorialType == "tap"))
                    {
                        if(tutorialType != "tap")
                            InputManager.Instance.UpButtonDown();
                        triggerInput_1 = true;

                        if (tutorialType != "")
                        {
                            tutorialType = "";
                            GameManager.Instance.PauseGeneric();
                        }
                    }
                    //swipe down
                    if (currentSwipe.y < 0 && currentSwipe.x > -0.4f && currentSwipe.x < 0.4f && (tutorialType == "down"))
                    {
                        InputManager.Instance.DownButtonDown();
                        triggerInput_1 = true;

                        if (tutorialType != "")
                        {
                            tutorialType = "";
                            GameManager.Instance.PauseGeneric();
                        }
                    }
                    //swipe left
                    if (currentSwipe.x < 0 && currentSwipe.y > -0.85f && currentSwipe.y < 0.85f && (tutorialType == "turn" || tutorialType == "left"))
                    {
                        InputManager.Instance.LeftButtonDown();
                        triggerInput_1 = true;

                        if (tutorialType != "")
                        {
                            tutorialType = "";
                            GameManager.Instance.PauseGeneric();
                        }
                    }
                    //swipe right
                    if (currentSwipe.x > 0 && currentSwipe.y > -0.85f && currentSwipe.y < 0.85f && (tutorialType == "turn" || tutorialType == "right"))
                    {
                        InputManager.Instance.RightButtonDown();
                        triggerInput_1 = true;

                        if (tutorialType != "")
                        {
                            tutorialType = "";
                            GameManager.Instance.PauseGeneric();
                        }
                    }
                }

                //long swipe for two lane 
                //if (Vector2.Distance(firstPressPos, secondPressPos) > 200 && triggerInput_1 && !triggerInput_2)
                //{
                //    //swipe left
                //    if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                //    {
                //        InputManager.Instance.LeftButtonDown();
                //        triggerInput_2 = true;
                //    }
                //    //swipe right
                //    if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                //    {
                //        InputManager.Instance.RightButtonDown();
                //        triggerInput_2 = true;
                //    }
                //}
            }
            if (Input.GetMouseButtonUp(0))
            {
                touchDown = false;
                triggerInput_1 = false;
                triggerInput_2 = false;
                //save ended touch 2d point
                secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

                //create vector from the two points
                currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                //normalize the 2d vector
                //currentSwipe.Normalize();

                //swipe upwards
                if (Vector2.Distance(firstPressPos, secondPressPos) < 10 && (tutorialType == "up" || tutorialType == "tap"))
                {
                    if(tutorialType != "tap")
                        InputManager.Instance.UpButtonDown();
                    //triggerInput = true;

                    if (tutorialType != "")
                    {
                        tutorialType = "";
                        GameManager.Instance.PauseGeneric();
                    }
                }
                //    //swipe down
                //    if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                //    {
                //        Debug.Log("down swipe");
                //    }
                //    //swipe left
                //    if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                //    {
                //        Debug.Log("left swipe");
                //    }
                //    //swipe right
                //    if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                //    {
                //        Debug.Log("right swipe");
                //    }
            }
        }

        public virtual void ChangeTutorialType(string type)
        {
            tutorialType = type;
        }

    }
}
