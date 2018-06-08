//Główny skryp obsługi landera
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketScript : MonoBehaviour {
    //zmienne rakiety
    public float startingfuel = 15;      //początkowa wartość paliwa
    public float startingmonoprop = 5;  //początkowa wartość monopropylanu
    public float fuel;                  //aktualna wartosc paliwa
    public float fuelrate = 1;          // predkosc konsumpcji paliwa
    public float monoprop;              // aktualna wartość monopropylanu
    public float monoproprate = 1;      //predkosc konsumpcji monoproplanu
    public float enginepower = 300;     // mmoc silnika
    public float turningSpeed = 7;      //predkosc obrotu/siła ciagu monopropylanu
    public bool inputEnable = true;     // czy sterowanie jest aktywne
    // zmienne odpowiedzialne za animacjie monopropylanu
    public GameObject rightprop;
    public GameObject leftprop;
    public Sprite rightpropsprite;
    public Sprite nullpropsprite;
    // zmmienne głównego silnika
    public ThrusterScript mainEngine;
    // zmienne kontorlera obslugujacego landera
    private GameController gamecontrol;
    // aktywny animator
    public Animator animator;
    // aktywny stan rakiety
    private LanderState landerState;


	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        //ustawienie podstawowego stanu
        landerState = new LanderIdle(this);
        mainEngine = new ThrusterScript(fuelrate,enginepower);   //inicjacja silnika
        gamecontrol = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>(); 
        //inicjacja paliw
        fuel = startingfuel;
        monoprop = startingmonoprop;

		
	}

	// Update is called once per frame
	void FixedUpdate () {
        landerState.Update();   // metoda aktywna stanów

 
    }

    // metoda zmieniajaca stan landera
    public void SetState(LanderState state)
    {
        landerState = state;
    }
     // skręt w lewo
    public void TLeft()
    {
        if (monoprop > 0)
        {
            transform.Rotate(0f, 0f, Time.fixedDeltaTime * turningSpeed);
            monoprop -= monoproprate * Time.deltaTime;
        }
    }
    // skręt w prawo
    public void TRight()
    {
        if (monoprop > 0)
        {
            transform.Rotate(0f, 0f, Time.fixedDeltaTime * -turningSpeed);
            monoprop -= monoproprate * Time.deltaTime;

        }
    }
    // zwykły reset
    public void GameReset()
    {
        gamecontrol.GameReset();
    }
    // reset z wygraną
    public void GameReset(bool win)
    {
        if(win)gamecontrol.wins++;
        gamecontrol.GameReset();
    }
}
