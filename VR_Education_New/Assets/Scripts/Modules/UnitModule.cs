using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Modules
{
    [System.Serializable]
    public class UnitModule
    {
        public string name;
        public string description;
        public int unit_number;
        public string unit_name;
        public UnitContent unitcontent;
        
    }

    [System.Serializable]
    public class UnitContent
    {
        public TitleInfo title_info;
        public string[] info_dialog;
        public MediaContent[] media_contents;
        public Question[] Questions;
        public string[] unit_progress;
    }

    [System.Serializable]
    public class TitleInfo
    {

        public string title;
        public string unit_info;
        public string unit_desc;
        public string unit_graphic;

    }

    [System.Serializable]
    public class MediaContent
    {
        public string file_source;
        public string media_description;
        public string title;
        public string type;

    }

    [System.Serializable]
    public class Question
    {
        public string question_text;
        public PossibleAnswer[] possible_answers;
        public string question_type;
        public int[] correct_answers;
        public int points;
        public string achievement;
    }

    [System.Serializable]
    public class PossibleAnswer
    {
        public string answer;
        public bool state;
        public string reason;				
    }
}

/*
{
  "name": "Basic Electronic1",
  "unit_number": 1,
  "description": "some description of the unit is here",
  "UnitContent": {
    "title_info": {
      "title": "Basic Electronics",
      "unit_info": "DIODES",
      "unit_desc": "Using Diodes to force Electric Current to flow in a particular direction",
      "unit_graphic":  "file_location_img.png"
    },
    "info_dialog": [
      "Begin Experience\nReady, Go",
      "Lesson Preface 1/2\nBuilding upon what we learnt in lesson 1 & 2, in this lesson we will learn how to control the direction of current in circuits using Diodes",
      "Lesson Preface 2/2\nAfter this lesson you’ll be able to:\n<Objective 1>\n<Objective 2>\n<Objective 3>",
      "Circuit Building\nNow you will build a circuit that forces current to move in  one direction."
    ],
    "media_contents": [
      {
        "file_source": "http://file_name_is_here.mp4",
        "media_description": "Desciption goes here",
        "title": "Introduction to Diodes",
        "type": "video2D"
      },
      {
        "file_source": "http://file_name_is_here.mp4",
        "media_description": "Desciption goes here",
        "title": "Introduction to Diodes",
        "type": "video2D"
      }
    ],
    "tmp_questioning_opportunity": {
    }
  }
}
*/
