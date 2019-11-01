using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Modules;
using Runtime;

namespace Monobehaviours
{
    public class ButtonClick : MonoBehaviour
    {

        public void beginAdventure()
        {
            Main.instance.processNextUnitContent(Main.unit);
        }

        public void next()
        {
            Main.instance.processNextUnitContent(Main.unit);
        }

        public void previous()
        {
            Main.instance.processPreviousUnitContent(Main.unit);
        }

        public Texture2D correctTexture, incorrectTexture;
        public void checkAnswer()
        {
            Renderer m_Renderer;
            m_Renderer = GetComponent<Renderer>();

            //get object ...
            if (gameObject.GetComponent<CustomObjectData>().possibleAnswer.state)
            {
                //then this answer is right 
                m_Renderer.material.SetTexture("_MainTex", correctTexture);
                gameObject.GetComponent<MeshCollider>().enabled = false;
            }
            else
            {
                //it;s wrong
                m_Renderer.material.SetTexture("_MainTex", incorrectTexture);
                gameObject.GetComponent<MeshCollider>().enabled = false;

            }

            //get data ...
        }


    }

}
