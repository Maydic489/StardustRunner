using UnityEngine;
using System.Collections;
using MoreMountains.InfiniteRunnerEngine;
using System.Collections.Generic;
using System;
using MoreMountains.Tools;

namespace MoreMountains.InfiniteRunnerEngine
{
    public class SimpleScenario : ScenarioManager
    {
        public MMMultipleObjectPooler buildingSpawner;
        private bool swap;
        private static float checkPoint = 500;
        private float timePass;

        protected override void Start()
        {
            Scenario();
            // once our scenario is planned, we start invoking its evaluation at regular intervals
            //InvokeRepeating("EvaluateScenario", EvaluationFrequency, EvaluationFrequency);
        }

        private void Update()
        {
            timePass += Time.deltaTime;
            if (timePass > 1.0f)
            {
                EvaluateScenario();
                timePass = 0;
            }
        }

        protected override void Scenario()
		{
            // Extend the ScenarioManager into your own class, and override this Scenario() method to describe your own scenario.
            // You can trigger events based on elapsed time, or the current score.
            // Here are a few examples :

            //AddTimeEvent("00:00:01:000", () => TestMethod("this event will occur after one second"));
            //AddTimeEvent("00:02:00:000", () => TestMethod("this event will occur after two minutes "));
            //AddTimeEvent("03:00:03:000", () => TestMethod("this event will occur after three hours"));

            //AddScoreEvent(10f, () => TestMethod("this event will occur when the score reaches 10 and will also trigger the 'ten' MMEvent"), "ten");
            //AddScoreEvent(150f, () => TestMethod("this event will occur when the score reaches 150"));

            AddScoreEvent(checkPoint, () => ChangeZone());
        }

        public void ChangeZone()
        {
            _scenario[0].StartScore += 500;
            swap = !swap;
            print("Change Zone");
            buildingSpawner.Pool[0].Enabled = !swap;
            buildingSpawner.Pool[1].Enabled = !swap;
            buildingSpawner.Pool[2].Enabled = swap;
            buildingSpawner.Pool[3].Enabled = swap;
            buildingSpawner.Pool[4].Enabled = swap;
        }

        protected override void EvaluateScenario()
        {
            // we get the current time and score to compare them with our event's values
            float currentTime = LevelManager.Instance.RunningTime;
            float currentScore = GameManager.Instance.Points;

            // for each item in the scenario
            foreach (var item in _scenario)
            {
                if (item.ScenarioEventType == ScenarioEvent.ScenarioEventTypes.TimeBased)
                {
                    // if it's time based, we check if we've reached the trigger time, and if the event hasn't been fired yet, we fire it.
                    if (item.StartTime <= currentTime && item.Status == true)
                    {
                        item.ScenarioEventAction();
                        item.Status = false;
                        if (item.MMEventName != "" && UseEventManager)
                        {
                            MMEventManager.TriggerEvent(new MMGameEvent(item.MMEventName));
                        }
                    }
                }

                if (item.ScenarioEventType == ScenarioEvent.ScenarioEventTypes.ScoreBased)
                {
                    Debug.Log("check point score " + item.StartScore);
                    // if it's score based, we check if we've reached the trigger score, and if the event hasn't been fired yet, we fire it.
                    if (item.StartScore <= currentScore && item.Status == true)
                    {
                        item.ScenarioEventAction();
                        //item.Status = false;
                        if (item.MMEventName != "" && UseEventManager)
                        {
                            MMEventManager.TriggerEvent(new MMGameEvent(item.MMEventName));
                        }
                    }
                }
            }
        }
    }
}
