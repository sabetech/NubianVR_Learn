using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSetup;
using Monobehaviours;


namespace Modules
{
    public class QuestionEngine : MonoBehaviour
    {
        public GameObject questionDialog;
        public GameObject answerQuad;
        public GameObject idontknow;
        public GameObject skipButton;

        public static QuestionEngine instance;
        GameObject questionHolder;

        void Awake()
        {
            instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }


        public GameObject createQuestionPrefab(Question question)
        {

            questionDialog = Instantiate(questionDialog) as GameObject;

            questionDialog.GetComponentInChildren<TextMesh>().text = CreatePanelText(question.question_text);
            questionDialog.transform.SetParent( VR_ScreenManager.instance.VR_Screen.transform );

            questionDialog.transform.Translate(Vector3.up * 1.8f);//this is for some reason moving the answers up ..
            //answer panels should be based o the question type ...
            //create question Answers
            CreateAnswerPanels(questionDialog, question);
            
            //create multipe choice answers
            return questionDialog;
        }

     
        public void CreateAnswerPanels(GameObject questionDialog, Question question )
        {
            switch (question.question_type)
            {
                case "mcq":
                    createMCQ_answers(question, questionDialog);
                    break;
                case "sata":
                    //createMCQ_sata(question);
                    break;
                case "mcq_mtf":
                    //createMCQ_mtf(question);
                    break;
            }
        }

        void createMCQ_answers(Question question, GameObject questionDialog)
        {
            PossibleAnswer[] possible_answers = question.possible_answers;
            shufflePossibleAnswers(possible_answers);

            float y_offset = -2f;
            float x_offset = -2f;
            GameObject[] answerPanels = new GameObject[4];
            
            for(int i = 0; i < 4; i++)
            {
                answerPanels[i] = Instantiate(answerQuad, 
                                new Vector3(questionDialog.transform.position.x + x_offset,
                                questionDialog.transform.position.y + y_offset,
                                VR_ScreenManager.instance.VR_Screen.transform.position.z),
                                Quaternion.identity,
                                VR_ScreenManager.instance.VR_Screen.transform) 
                                as GameObject;

                //set answer texts ...
                SetAnswerText(answerPanels[i], possible_answers[i]);
                AddClickEventLogic(answerPanels[i], possible_answers[i]);

                y_offset += 0.6f;
            }

            GameObject idk = Instantiate(idontknow,
                new Vector3(answerPanels[0].transform.position.x + -x_offset + 1.5f,
                            answerPanels[0].transform.position.y,
                            VR_ScreenManager.instance.VR_Screen.transform.position.z),
                            Quaternion.identity,
                            VR_ScreenManager.instance.VR_Screen.transform)
                            as GameObject;

            GameObject skp = Instantiate(skipButton,
                new Vector3(idk.transform.position.x + -x_offset - 0.5f,
                            idk.transform.position.y,
                            VR_ScreenManager.instance.VR_Screen.transform.position.z),
                            Quaternion.identity,
                            VR_ScreenManager.instance.VR_Screen.transform)
                            as GameObject;

        }

        void addIdontKnowSkipButtons()
        {

        }

        void SetAnswerText(GameObject answerPanel, PossibleAnswer answer)
        {
            answerPanel.GetComponentInChildren<TextMesh>().text = CreatePanelText(answer.answer);
        }

        void AddClickEventLogic(GameObject answerPanel, PossibleAnswer answer)
        {
            //add custom data to the button ...
            answerPanel.AddComponent<CustomObjectData>().possibleAnswer = answer;

        }

        void shufflePossibleAnswers(PossibleAnswer[] possibleAnswers)
        {
            
            for (int t = 0; t < possibleAnswers.Length; t++)
            {
                PossibleAnswer tmp = possibleAnswers[t];
                int r = Random.Range(t, possibleAnswers.Length);
                possibleAnswers[t] = possibleAnswers[r];
                possibleAnswers[r] = tmp;
            }
        }

        //create questino dialog
        //split question text into blocks of 5 words
        public string CreatePanelText(string questionText)
        {
            //put \n after every 5 words
            string[] questionArray = questionText.Split(' ');
            string newString = "";
            for (int i = 0; i < questionArray.Length; i++)
            {
                //if it gets to the last letter just add the letter without a space at the end
                if (i == (questionArray.Length - 1))
                {
                    newString += questionArray[i];
                    continue;
                }

                newString += questionArray[i] + " ";
                if (i % 5 == 0) newString += "\n";

            }
            return newString;
        }


    }

}
