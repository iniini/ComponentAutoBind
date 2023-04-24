using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UIManage
{
    public class UIFormLogic : MonoBehaviour
    {

        public Transform Content;
        public void Awake()
        {
            Content = transform.Find("Content");
        }
        protected virtual void AutoBind()
        {

        }
    }
}

