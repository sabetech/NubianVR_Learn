using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Runtime;

namespace Modules
{
    public class WebModule : MonoBehaviour
    {

        public bool isDataReady = false;
        public string data_result = "";
        Main main;
        // Start is called before the first frame update
        void Start()
        {
            main = GetComponent<Main>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void fetchData(string url)
        {
            StartCoroutine(GetText(url));
        }

        IEnumerator GetText(string url)
        {
            UnityWebRequest www = UnityWebRequest.Get(url);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else {

                // Show results as text
                //Debug.Log(www.downloadHandler.text);
                data_result = www.downloadHandler.text;
                main.resourceRequestState = Main.ResourceRequestState.request_completed;
                
                // Or retrieve results as binary data
                //byte[] results = www.downloadHandler.data;
            }
        }
    }

}
