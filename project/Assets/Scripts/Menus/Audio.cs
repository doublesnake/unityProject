using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class Audio
{
    public class Volume
    {
        int min = 0;
        int max = 100;
        int increment = 10;
        int defaultValue = 50;
        public int value;

        public Volume()
        {
            value = 50;
        }

        public Volume(int volume)
        {
            setVolume(volume);
        }
        
        public void setVolume(int volume)
        {
            if (volume > max) this.value = max;
            else if (volume < min) this.value = min;
            else this.value = volume;
        }
        public void increase()
        {
            setVolume(value + increment);
        }
        public void decrease()
        {
            setVolume(value - increment);
        }
        public bool isMax()
        {
            return value == max;
        }
        public bool isMin()
        {
            return value == min;
        }
        public void reset()
        {
            value = defaultValue;
        }
    }

    public Volume music;
    public Volume effects;
    public Volume game;

    public Audio()
    {
        music = new Volume(50);
        effects = new Volume(50);
        game = new Volume(50);
    }


}
