//State Pattern odpowiedzialny za stan Landera, odpowiedzialny głównie za animacje.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LanderState {

    protected RocketScript _rocket;  // odnośnik do Landera który jest obsługiwany

    public abstract void Update();   // metoda uruchamiana co klatkę

    public virtual void OnStateEnter() { } 
    public virtual void OnStateExit() { }

    public LanderState(RocketScript rocket)    // przekazuje obiekt landera wykorzystywany do uaktualniania stanu
    {
        _rocket = rocket;

        OnStateEnter();
    }



}

public class LanderLeft : LanderState
{

    public override void Update()
    {
        //zmiana animacji 
        if (_rocket.mainEngine.IsRunning()) _rocket.animator.Play("LanderLeft");  // IsRunning zwraca true jesli silniki są uruchomione
        else _rocket.animator.Play("LanderOff");
        // zmiana stanu rakiety w zależności od tego jak się porusza
        if (_rocket.GetComponent<Rigidbody2D>().velocity.x > 0) _rocket.SetState(new LanderRight(_rocket));
        if (_rocket.GetComponent<Rigidbody2D>().velocity.x == 0) _rocket.SetState(new LanderIdle(_rocket));


    }

    public LanderLeft(RocketScript rocket) : base(rocket)
    {
    }


}

public class LanderRight : LanderState
{
    public override void Update()
    {
        // zmiana aniamacji
        if(_rocket.mainEngine.IsRunning()) _rocket.animator.Play("LanderRight");
        else _rocket.animator.Play("LanderOff");
        // zmiana stanu rakiety w zaleznosci od tego jak się porusza
        if (_rocket.GetComponent<Rigidbody2D>().velocity.x < 0) _rocket.SetState(new LanderLeft(_rocket));
        if (_rocket.GetComponent<Rigidbody2D>().velocity.x == 0) _rocket.SetState(new LanderIdle(_rocket));
    }

    public LanderRight(RocketScript rocket) : base(rocket)
    {
    }
}

public class LanderIdle : LanderState
{
    public override void Update()
    {
        // zmiana animacji
        if (_rocket.mainEngine.IsRunning())
            _rocket.animator.Play("Lander1");
        else _rocket.animator.Play("LanderOff");
        // zmiana stanu w zależnosic od ruchu landera
        if (_rocket.GetComponent<Rigidbody2D>().velocity.x < 0) _rocket.SetState(new LanderLeft(_rocket));
        if (_rocket.GetComponent<Rigidbody2D>().velocity.x > 0) _rocket.SetState(new LanderRight(_rocket));
    }

    public LanderIdle(RocketScript rocket) : base(rocket)
    { }

}

public class LanderBoom : LanderState
{
    public override void Update()
    {
        _rocket.inputEnable = false;    //wyłączenie sterowania
        _rocket.animator.Play("Boom");  // zmiana animajci
    }
    public LanderBoom(RocketScript rocket) : base(rocket)
    { }
}

public class LanderWaterboom : LanderState
{
    public override void Update()
    {
        _rocket.inputEnable = false; //wyłączenia sterowania
        _rocket.animator.Play("WaterBoom");  //zmiana animacji
    }
    public LanderWaterboom(RocketScript rocket) : base(rocket)
    { }
}

public class LanderLanded : LanderState
{
    public override void Update()
    {
        _rocket.inputEnable = false;  // wyłączenie sterowania
        _rocket.animator.Play("Lander1");  // zmiana animacji
        _rocket.GameReset(true); // zresetowanie gry, true oznacza ze naliczane są punkty
    }

    public LanderLanded(RocketScript rocket) : base(rocket)
    { }
}

