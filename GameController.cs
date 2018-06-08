using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    // zmienne Landera
    public GameObject rocket;
    RocketScript rocketScript;
    // zmienne platformy
    public GameObject platform;
    public GameObject instrocket;
    // zmienne odpowiadające za obsługę UI
    public Text fueltext;
    public Text monoproptext;
    public Text attempts;
    public GameObject startingpanel;
    
    // zmienne minitorujace pracę Landera
    private bool _engineRunning;
    private bool _isleft;
    private bool _isright;

    //Stan rozgrywki wygrane/orób
    public int atempts;
    public int wins;

    // Use this for initialization
    void Start () {
        rocketScript = rocket.GetComponent<RocketScript>();
        Time.timeScale = 0;   // gra startuje w trybie pausy
        //zerowanie statystyl
        atempts = 0;
        wins = 0;
    }
	
	// Update is called once per frame
	void Update () {
        //warunek sterowania rakiety
        if (rocketScript.inputEnable)
        {
            if (_engineRunning) RunEngine();
            if (_isright) TurnRight();
            if (_isleft) TurnLeft();

        }

        // zmiana UI
        fueltext.text = "Fuel: " + rocketScript.fuel;
        monoproptext.text = "Monoprop: " + rocketScript.monoprop;
        attempts.text = wins + "/" + atempts;
	}

   private void RunEngine()  // uruchamia skrypt obsługi silnika
    {
        rocketScript.mainEngine.RunThruster(rocketScript);
    }
    public void EngineSet() // włącza/wyłącza silnik
    {
        _engineRunning = !_engineRunning;
        if (!_engineRunning) rocketScript.mainEngine.StopThruster();
    }

    private void TurnLeft()
    {
        rocketScript.TLeft();
    }

    public void SetLeft()
    {
        _isleft = !_isleft;
    }


    private void TurnRight()
    {
        rocketScript.TRight();
    }

    public void SetRight()
    {
        _isright = !_isright;
    }


    public void GameReset()  //resetuje stan gry
    {
        // reset Landera
        Destroy(rocket);
        rocket = Instantiate(instrocket);
        rocketScript = rocket.GetComponent<RocketScript>();
        var rand = Random.Range(-2f, 2f);
        rocket.transform.position = new Vector3(rand,2,0);
        //ustawienie stanu poczatkowego rakiety
        rocketScript.SetState(new LanderIdle(rocketScript));
        rocketScript.inputEnable = true;
        // zresetowanie pozycji platformy
        rand = Random.Range(-1.5f, 1.5f);
        platform.transform.position = new Vector3(rand,-2.05f, 0);
        //zresetowanie paliwa i monopropylanu
        rocketScript.monoprop = rocketScript.startingmonoprop;
        rocketScript.fuel = rocketScript.startingfuel;
        //zwiekszenie ilosci podjętych prób
        atempts++;
    }

    public void PressToStart()  //skrypt wyłączający początkowy ekran
    {
        Time.timeScale = 1;
        startingpanel.SetActive(false);
        GameReset();
    }
}
