using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Modules;

namespace Runtime
{
    public class Main : MonoBehaviour
    {
        const string CONTENTPLACEHOLDER = "ContentPlaceholder";        

        //https://drive.google.com/file/d/1_y2cKneAuDZecWCwNTT_3WPvw88r2H6L/view?usp=sharing
        //https://www.filehosting.org/file/details/828341/Unit.json real json is here ...

        public enum ResourceRequestState
        {
            started,
            waiting_for_resource,
            request_completed,
            request_failed
        }

        public ResourceRequestState resourceRequestState;
        enum UnitProgressState
        {
            initialized
        }

        public static UnitModule unit;
        public static Main instance;
        UnitProgressState unit_progress_state;

        bool initialized = false;
        
        GameObject contentPlaceholder;
        PrefabManager prefabMgr;
        int GlobalProgressIndex = 0;
        int info_dialog_progress = 0, media_progress = 0, mcq_progress = 0; 
        
        string url = Application.streamingAssetsPath + "/Unit.json";
        WebModule webReqs;
        private VR_ScreenManager _VRScreenManager;


        void Awake()
        {
            instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            
            //get UNIT JSON from web server ...
            webReqs = GetComponent<WebModule>();
            _VRScreenManager = GetComponent<VR_ScreenManager>();

            resourceRequestState = ResourceRequestState.waiting_for_resource;
            webReqs.fetchData(url);
            
        }

        // Update is called once per frame
        void Update()
        {
            if (resourceRequestState == ResourceRequestState.waiting_for_resource)
            {
                return;
            }

            if (resourceRequestState == ResourceRequestState.request_completed)
            {
                if (!initialized)
                {
                    initializeModule();
                    initialized = true;
                }
                
            }
        }

        void initializeModule() {

            //get data from the resource acquired from the server to build the module ...
            //this function should be called when the data fetch is ready ...
            //this is a json result ...

            unit = JsonUtility.FromJson<UnitModule>(webReqs.data_result);
            _VRScreenManager.showTitle(unit);

        }

        public UnitModule getCurrentUnit()
        {
            return unit;
        }

        

        public void processNextUnitContent(UnitModule unit)
        {
            //it means we are done ...
            if ((unit.unitcontent.unit_progress.Length - 1) <= GlobalProgressIndex) return;

            string property = unit.unitcontent.unit_progress[ GlobalProgressIndex++ ];
            var value = unit.unitcontent.GetType().GetField(property).GetValue(unit.unitcontent);
            
            publishUnit(value, property);
            
        }

        public void processPreviousUnitContent(UnitModule unit)
        {
            if (GlobalProgressIndex < 0) return;

            string property = unit.unitcontent.unit_progress[--GlobalProgressIndex];
            var value = unit.unitcontent.GetType().GetField(property).GetValue(unit.unitcontent);

            publishUnit(value, property, -1);
        }

        void publishUnit(object value, string objectType, int direction = 1)
        {

            switch (objectType)
            {
                case "info_dialog":

                    string[] myStringDialogs = (string[])value;

                    info_dialog_progress = info_dialog_progress + direction;

                    if (info_dialog_progress > myStringDialogs.Length) info_dialog_progress--;
                    if (info_dialog_progress <= 0) info_dialog_progress++;

                    _VRScreenManager.showDialogMessage(myStringDialogs[info_dialog_progress - 1]);
                    
                    break;

                case "media_contents":
                    
                    MediaContent[] media = (MediaContent[])value;

                    media_progress = media_progress + direction;

                    if (media_progress > media.Length) media_progress--;
                    if (media_progress <= 0) media_progress++;
                    
                    _VRScreenManager.showMediaContent(media[media_progress - 1]);
                    
                break;

                case "Questions":
                    Question[] questions = (Question[])value;

                    mcq_progress = mcq_progress + direction;

                    if (mcq_progress > questions.Length) mcq_progress--;
                    if (mcq_progress <= 0) mcq_progress++;

                    _VRScreenManager.showMCQContent(questions[mcq_progress - 1]);

                    break;

            }
        }
       
    }

}
