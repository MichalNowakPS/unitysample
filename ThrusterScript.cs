// Skrypt obsługujacy silnik
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterScript  {
    private float fuelConsumption;   //tempo pobierania paliwa
    private float _power;            //moc silnika
    private bool _running = false;   //czy silnik działa

    public ThrusterScript(float fuelcons, float thrusterpower) 
    {
        _power = thrusterpower;
        fuelConsumption = fuelcons;        
    }

    public void RunThruster(RocketScript rocket)  //dzialanie sinika
    {
        if (rocket.fuel > 0)
        {
            rocket.GetComponent<Rigidbody2D>().AddForce(rocket.transform.up * _power * Time.fixedDeltaTime);
            _running = true;
            rocket.fuel -= fuelConsumption * Time.fixedDeltaTime;
        }
    }

    public void StopThruster()
    {
        _running = false;
    }
    public bool IsRunning()
    {
        return _running;
    }
}
