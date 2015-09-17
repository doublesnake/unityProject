using UnityEngine;
using System.Collections;

namespace Menus
{
    public class Input
    {
        private static Input instance;

        public static Input Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Input();
                }
                return instance;
            }
        }

        public KeyCode left;
        public KeyCode right;
        public KeyCode up;
        public KeyCode down;
        public KeyCode enter;
        public KeyCode cancel;

        private Input()
        {
            this.up = KeyCode.UpArrow;
            this.down = KeyCode.DownArrow;
            this.left = KeyCode.LeftArrow;
            this.right = KeyCode.RightArrow;
            this.enter = KeyCode.Return;
            this.cancel = KeyCode.Escape;
        }

        public bool isKeyLeft()
        {
            return UnityEngine.Input.GetKeyDown(left);
        }
        public bool isKeyRight()
        {
            return UnityEngine.Input.GetKeyDown(right);
        }
        public bool isKeyUp()
        {
            return UnityEngine.Input.GetKeyDown(up);
        }
        public bool isKeyDown()
        {
            return UnityEngine.Input.GetKeyDown(down);
        }
        public bool isKeyEnter()
        {
            return UnityEngine.Input.GetKeyDown(enter);
        }
        public bool isKeyCancel()
        {
            return UnityEngine.Input.GetKeyDown(cancel);
        }
    }

}