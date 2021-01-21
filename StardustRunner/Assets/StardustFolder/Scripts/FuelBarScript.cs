using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MoreMountains.InfiniteRunnerEngine
{
    public class FuelBarScript : MonoBehaviour
    {
        public Slider fuelSlider1;

        // Update is called once per frame
        void Update()
        {
            fuelSlider1.value = GameManager.Instance.FuelPoints;
        }
    }
}
