using UnityEngine;
using System.Collections;



public class Options
{
    public enum Player { P1, P2 };
    public enum Lang { EN, FR };
    public enum Time { TIME_30, TIME_60, TIME_99, TIME_INF };
    public enum Round { ROUND_1, ROUND_2, ROUND_3};

    public enum Level { EASY, MEDIUM, HARD, EXTREME };

    public Level level;
    public Time time;
    public Round rounds;
    public Lang language;
    public Controls controlsP1;
    public Controls controlsP2;
    public Audio audio;


    private static Options instance;

    public static Options Instance
    {
        get 
        {
            if (instance == null)
            {
                instance = new Options();
            }
            return instance;
        }
    }

    private Options()
    {
        this.level = Level.MEDIUM;
        this.time = Time.TIME_60;
        this.rounds = Round.ROUND_2;
        this.language = Lang.EN;
        controlsP1 = new Controls(Controls.Layout.A);
        controlsP2 = new Controls(Controls.Layout.B);
        audio = new Audio();
    }


    /*
    public void setOptions(Level level, Time time, Round round, Lang language, Controls controls, Audio audio)
    {
        this.level = level;
        this.time = time;
        this.language = language;
        this.rounds = round;
        this.controls = controls;
        this.audio = audio;
    }
    public void setOptions(Options options)
    {
        this.level = options.level;
        this.time = options.time;
        this.language = options.language;
        this.rounds = options.rounds;
        this.controls = options.controls;
        this.audio = options.audio;
    }
    */
}
