using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Menus
{
    public class Instructions : MonoBehaviour
    {

        public Text esc;
        public Text enter;
        public Text arrow;

        void Start()
        {
            esc.text = GameText.Instance.Get(GameText.Text.INSTRUCT_ESC);
            enter.text = GameText.Instance.Get(GameText.Text.INSTRUCT_ENTER);
            arrow.text = GameText.Instance.Get(GameText.Text.INSTRUCT_ARROWS);
        }

        public void update()
        {
            esc.text = GameText.Instance.Get(GameText.Text.INSTRUCT_ESC);
            enter.text = GameText.Instance.Get(GameText.Text.INSTRUCT_ENTER);
            arrow.text = GameText.Instance.Get(GameText.Text.INSTRUCT_ARROWS);
        }
    }

}