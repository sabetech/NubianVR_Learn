using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace Modules
{
    public class VR_ScreenManager : MonoBehaviour
    {
        public GameObject VR_Screen; //this is the content place holder game object
        PrefabManager prefabMgr;

        public static VR_ScreenManager instance;

        void Awake()
        {
            instance = this;
        }
        void Start()
        {
            prefabMgr = GetComponent<PrefabManager>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public static VR_ScreenManager getInstance()
        {
            return instance;
        }

        public void showTitle(UnitModule unit)
        {

            GameObject titlePage = prefabMgr.TitlePage;

            titlePage.transform.GetChild(0).GetComponent<TextMesh>().text = unit.unitcontent.title_info.title;

            titlePage.transform.GetChild(2).GetComponent<TextMesh>().text = "Unit " + unit.unit_number + ": " + unit.unitcontent.title_info.unit_info;

            titlePage.transform.GetChild(3).GetComponent<TextMesh>().text = unit.unitcontent.title_info.unit_desc;

            Instantiate(titlePage, VR_Screen.transform.position, Quaternion.identity, VR_Screen.transform);

        }

        public void showDialogMessage(string message)
        {
            removeContent();
            
            //this function must take care of the formatting and things ...
            GameObject infoDialog = prefabMgr.InfoBox;
            TextMesh infoText = infoDialog.transform.GetChild(0).GetComponent<TextMesh>();
            infoText.text = message;

            Instantiate(infoDialog, VR_Screen.transform.position, Quaternion.identity, VR_Screen.transform
                );
        }

        VideoPlayer vd;
        public void showMediaContent(MediaContent media)
        {
            //play content here ...
            removeContent();
            
            string fileLocation = Application.streamingAssetsPath + "/" + media.file_source;

            GameObject videoBox = Instantiate(prefabMgr.VideoBox, VR_Screen.transform.position, Quaternion.identity, VR_Screen.transform) as GameObject;

            GameObject vdgameObject = GameObject.Find("VideoPlayer");
            vd = vdgameObject.GetComponent<VideoPlayer>();
            vd.url = fileLocation;
            vd.Play();

        }

        GameObject questionHolder;
        public void showMCQContent(Question question)
        {
            //show MCQ type on this page ...
            removeContent();

            

            //I will need a question creation engine ...
            GameObject questionPrefab = QuestionEngine.instance.createQuestionPrefab(question);

            questionPrefab.transform.position = VR_Screen.transform.position;
            questionPrefab.transform.SetParent(VR_Screen.transform);

            //GameObject questionInstance = Instantiate(questionPrefab, VR_Screen.transform.position, Quaternion.identity, VR_Screen.transform) as GameObject;

            //questionInstance.transform.SetParent(questionHolder.transform);

            questionPrefab.transform.Rotate(0f, -90f, 0f);

            GameObject questionBox = GameObject.Find("mc_question(Clone)");
            questionBox.transform.Translate(Vector3.up * 1f);


        }

        public void removeContent()
        {   
            //check if there's a video playing... stop the video ...
            if (vd != null)
            {
                vd.Stop();
            }

            foreach(Transform child in VR_Screen.transform)
            {
                Destroy(child.gameObject);
            }
        }

        public void publishContent(GameObject content)
        {

        }


    }

}
