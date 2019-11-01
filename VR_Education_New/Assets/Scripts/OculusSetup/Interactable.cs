using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monobehaviours;
using Constants;

namespace OculusSetup {
    public class Interactable : MonoBehaviour
    {
        public Texture2D regularTexture;
        public Texture2D hoveredTexture;
        public Texture2D clickedTexture;
        Renderer m_Renderer;

        private bool hover_state = false;
        private bool button_clicked = false;

        public void Pressed()
        {
            if (!clickedTexture) return;

            m_Renderer = GetComponent<Renderer>();
            m_Renderer.material.SetTexture("_MainTex", clickedTexture);
            ButtonClick btnClick = GetComponent<ButtonClick>();

            switch (gameObject.name)
            {
                case Clickables.BEGIN_ADVENTURE:
                    btnClick.beginAdventure();
                break;

                case Clickables.NEXT:
                    btnClick.next();
                    break;

                case Clickables.PREVIOUS:
                    btnClick.previous();
                    break;

                case Clickables.ANSWER_BTN:
                    btnClick.checkAnswer();
                    break;

            }
            

        }

        public void Hovered()
        {
            if (hover_state) return;

            if (!hoveredTexture) return;

            m_Renderer = GetComponent<Renderer>();
            m_Renderer.material.SetTexture("_MainTex", hoveredTexture);

            hover_state = true;

        }

        public void HoveredOff()
        {

            if (!regularTexture) return;

            if (button_clicked) return;

            m_Renderer = GetComponent<Renderer>();
            m_Renderer.material.SetTexture("_MainTex", regularTexture);

            hover_state = false;

        }

        public void Released()
        {
            
            if (!regularTexture) return;

            m_Renderer = GetComponent<Renderer>();
            m_Renderer.material.SetTexture("_MainTex", regularTexture);

            hover_state = false;

        }
    }

}
