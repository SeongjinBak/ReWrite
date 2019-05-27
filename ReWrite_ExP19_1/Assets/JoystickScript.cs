using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


namespace SubProject14
{
    public class JoystickScript : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {

        /// 선언할 전역변수는 3개이다. 
        /// BackgroundImage 게임오브젝트를 할당할 bgImg, 
        /// JoystickImg 게임오브젝트를 할당할 joystickImg, 
        /// 이동 벡터값을 저장할 inputVector 이다. 
        private Image bgImg;
        private Image joystickImg;
        private Vector3 inputVector;

        /// 이 스크립트는 BackgroundImage 게임오브젝트에 추가할 것이므로
        ///  Start() 함수에서 아래와 같이 bgImg와 joystickImg를 할당한다.
        // Use this for initialization
        void Start()
        {
            bgImg = GetComponent<Image>();
            joystickImg = transform.GetChild(0).GetComponent<Image>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        /// 터치패드를 누르고 있을 때 실행할 onDrag(PointerEventData ped) 함수를 구현한다. 
        /// bgImg영역에 터치가 발생했을 때 
        /// (RectTransformUtility.ScreenPointToLocalPointInRectangle(
        /// bgImg.rectTransform, ped.position, ped.pressEventCamera, out pos)가 true일 때), 
        /// 터치된 로컬 좌표값을 pos에 할당하고 bgImg 직사각형의 sizeDelta값으로 나누어 pos.x는 0~-1, pos.y는 0~1사이의 값으로 만든다.

        /// joystickImg를 기준으로 좌우로 움직였을 때 pos.x는 -1~1 사이의 값으로, 
        /// 상하로 움직였을 때 pos.y는 -1~1의 값으로 변환하기 위해 pos.x*2 +1, pos.y*2-1 처리를 한다. 
        /// 이 값을 inputVector에 대입하고 단위벡터로 만든다. 
        /// 마지막으로 joystickImg를 터치한 좌표값으로 이동시킨다.


        public virtual void OnDrag(PointerEventData ped)
        {
            //Debug.Log("Joystick >>>> OnDrag()");

            Vector2 pos;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImg.rectTransform, ped.position, ped.pressEventCamera, out pos))
            {
                pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
                pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);

                inputVector = new Vector3(pos.x * 2 + 1, pos.y * 2 - 1, 0);
                //inputVector = new Vector3 (pos.x * 2, pos.y * 2, 0);
                inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

                // move joystick img
                joystickImg.rectTransform.anchoredPosition = new Vector3(inputVector.x * (bgImg.rectTransform.sizeDelta.x / 3), inputVector.y * (bgImg.rectTransform.sizeDelta.y / 3), 0);
            }
        }

        /// 터치를 하고 있을 때 발생하는 OnPointerDown(PointerEventData ped) 함수에서 
        /// OnDrag(ped)가 실행되도록 한다. 
        public virtual void OnPointerDown(PointerEventData ped)
        {
            OnDrag(ped);
        }

        /// 터치를 중지했을 때 발생하는 OnPointerUp(PointerEventData ped)에서는 
        /// inputVector와 joystickImg의 위치를 초기화한다.
        public virtual void OnPointerUp(PointerEventData ped)
        {
            inputVector = Vector3.zero;
            joystickImg.rectTransform.anchoredPosition = Vector3.zero;
        }

        /// inputVector값을 PlayerController스크립트에 넘겨 주기 위해 사용할 
        /// GetHorizontalValue()와 GetVerticalValue()를 구현한다.
        public float GetHorizontalValue()
        {
            return inputVector.x;
        }

        public float GetVerticalValue()
        {
            return inputVector.y;
        }
    }
}