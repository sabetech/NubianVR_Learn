using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Runtime;

namespace Monobehaviours
{
    public class EditorTestRun : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
            if (Input.GetKeyDown("space"))
            {
                Main.instance.processNextUnitContent(Main.unit);
            }

        }
    }

}
