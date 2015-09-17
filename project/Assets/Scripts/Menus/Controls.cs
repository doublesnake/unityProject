using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class Controls
{
    public class InputSet
    {
        public enum Button { UP, DOWN, LEFT, RIGHT, LIGHT, MEDIUM, STRONG, SPECIAL, START };

        public Dictionary<Button,KeyCode> inputs = new Dictionary<Button,KeyCode>();
        public InputSet()
        {

        }

        public InputSet(KeyCode up, KeyCode down, KeyCode left, KeyCode right, KeyCode light, KeyCode medium, KeyCode strong, KeyCode special, KeyCode start)
        {
            configure(up, down, left, right, light, medium, strong, special, start);
        }
        public InputSet(InputSet input)
        {
            copy(input);
        }
        public void copy(InputSet input)
        {
            configure(input.inputs[Button.UP], input.inputs[Button.DOWN], input.inputs[Button.LEFT], input.inputs[Button.RIGHT],
                input.inputs[Button.LIGHT], input.inputs[Button.MEDIUM], input.inputs[Button.STRONG], input.inputs[Button.SPECIAL], input.inputs[Button.START]);
        }
        public void configure(KeyCode up, KeyCode down, KeyCode left, KeyCode right, KeyCode light, KeyCode medium, KeyCode strong, KeyCode special, KeyCode start)
        {
            this.inputs[Button.UP] = up;
            this.inputs[Button.LEFT] = left;
            this.inputs[Button.RIGHT] = right;
            this.inputs[Button.DOWN] = down;
            this.inputs[Button.LIGHT] = light;
            this.inputs[Button.MEDIUM] = medium;
            this.inputs[Button.STRONG] = strong;
            this.inputs[Button.SPECIAL] = special;
            this.inputs[Button.START] = start;
        }
    }
    public enum Layout { A, B };

    public Layout layout;
    InputSet defaultA;
    InputSet defaultB;
    InputSet typeA;
    InputSet typeB;

    public InputSet current;

    public Controls(Layout type)
    {
        init();
        if(type == Layout.A)
            setLayout(Layout.A);
        else
            setLayout(Layout.B);
    }

    public Controls()
    {
        init();
    }

    void init()
    {
        defaultA = new InputSet(KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D, KeyCode.H, KeyCode.U, KeyCode.I, KeyCode.J, KeyCode.Return);
        defaultB = new InputSet(KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.Keypad5, KeyCode.Keypad6, KeyCode.Keypad3, KeyCode.Keypad2, KeyCode.KeypadEnter);
        typeA = new InputSet(defaultA);
        typeB = new InputSet(defaultB);
        current = new InputSet();
    }

    public void setInputs(InputSet input)
    {
        current.copy(input);
    }

    public void resetLayout()
    {
        if (layout == Layout.A)
        {
            typeA.copy(defaultA);
            current.copy(typeA);
        }
        else
        {
            typeB.copy(defaultB);
            current.copy(typeB);
        }
    }
    public void setLayout(Layout type)
    {
        layout = type;
        if (type == Layout.A)
            current.copy(typeA);
        else
            current.copy(typeB);
    }

}
