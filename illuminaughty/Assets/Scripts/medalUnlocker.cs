using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class medalUnlocker : MonoBehaviour
{
    public io.newgrounds.core ngio_core;
    public int medIco, medVal;
    public string medName;

    // call this method whenever you want to unlock a medal.
   public void unlockMedal(int medal_id)
    {
        // create the component
        io.newgrounds.components.Medal.unlock medal_unlock = new io.newgrounds.components.Medal.unlock();

        // set required parameters
        medal_unlock.id = medal_id;

        // call the component on the server, and tell it to fire onMedalUnlocked() when it's done.
        medal_unlock.callWith(ngio_core, onMedalUnlocked);
    }

    // this will get called whenever a medal gets unlocked via unlockMedal()
    void onMedalUnlocked(io.newgrounds.results.Medal.unlock result)
    {
        io.newgrounds.objects.medal medal = result.medal;
        Debug.Log("Medal Unlocked: " + medal.name + " (" + medal.value + " points)");

        UIcontroller.instance.ShowMedal(medName, medVal, medIco);
    }
}
