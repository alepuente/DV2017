using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMPlayer {

	private int[,] _fsm;
    private States _state1;
    private States _state2;
    private int rows = 6;
    private int columns = 5;

    public enum States
    {
        Iddle,
        LeftCannon,
        RightCannon,
        FrontCannon,
        Steering,
        OnNest
    }
    public enum Events
    {
        ToLeftCannon,
        ToRightCannon,
        ToRudder,
        ToFrontCannon,
        ToNest
    }

    // Use this for initialization
    public SMPlayer()
    {
        _fsm = new int[rows, columns];
        resetFSM();
        
        SetRelative(States.Iddle, Events.ToFrontCannon,States.FrontCannon);
        SetRelative(States.Iddle, Events.ToLeftCannon, States.LeftCannon);
        SetRelative(States.Iddle, Events.ToRightCannon, States.RightCannon);
        SetRelative(States.Iddle, Events.ToRudder, States.Steering);
        SetRelative(States.Iddle, Events.ToNest, States.OnNest);

        SetRelative(States.FrontCannon, Events.ToLeftCannon, States.LeftCannon);
        SetRelative(States.FrontCannon, Events.ToRightCannon, States.RightCannon);
        SetRelative(States.FrontCannon, Events.ToRudder, States.Steering);
        SetRelative(States.FrontCannon, Events.ToNest, States.OnNest);

        SetRelative(States.LeftCannon, Events.ToFrontCannon, States.FrontCannon);
        SetRelative(States.LeftCannon, Events.ToRightCannon, States.RightCannon);
        SetRelative(States.LeftCannon, Events.ToRudder, States.Steering);
        SetRelative(States.LeftCannon, Events.ToNest, States.OnNest);

        SetRelative(States.RightCannon, Events.ToFrontCannon, States.FrontCannon);
        SetRelative(States.RightCannon, Events.ToLeftCannon, States.LeftCannon);
        SetRelative(States.RightCannon, Events.ToRudder, States.Steering);
        SetRelative(States.RightCannon, Events.ToNest, States.OnNest);

        SetRelative(States.Steering, Events.ToFrontCannon, States.FrontCannon);
        SetRelative(States.Steering, Events.ToLeftCannon, States.LeftCannon);
        SetRelative(States.Steering, Events.ToRightCannon, States.RightCannon);
        SetRelative(States.Steering, Events.ToNest, States.OnNest);

        SetRelative(States.OnNest, Events.ToFrontCannon, States.FrontCannon);
        SetRelative(States.OnNest, Events.ToLeftCannon, States.LeftCannon);
        SetRelative(States.OnNest, Events.ToRightCannon, States.RightCannon);
        SetRelative(States.OnNest, Events.ToRudder, States.Steering);
       
        _state1 = States.Iddle;
        _state2 = States.Iddle;
    }

    private void resetFSM()
    {
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                _fsm[r, c] = -1;
            }
        }
    }

    public void resetStates()
    {
        _state1 = States.Iddle;
        _state2 = States.Iddle;
    }

    private void SetRelative(States origin, Events evnt, States dest)
    {
        _fsm[(int)origin, (int)evnt] = (int)dest;
    }    

    public void SetEvent1(Events ev)
    {
        if (_fsm[(int)_state1, (int)ev] != -1 && _state2 != (States)_fsm[(int)_state1, (int)ev])
        {
            _state1 = (States)_fsm[(int)_state1, (int)ev];
        }
        //Debug.Log("Evento para 1 "+ ev);
    }
    public void SetEvent2(Events ev)
    {
        if (_fsm[(int)_state2, (int)ev] != -1 && _state1 != (States)_fsm[(int)_state2, (int)ev])
        {
            _state2 = (States)_fsm[(int)_state2, (int)ev];
        }
        //Debug.Log("Evento para 2 " + ev);
    }


    public States getActualState1()
    {
        return _state1;
    }

        public States getActualState2()
    {
        return _state2;
    }
}
