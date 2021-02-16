﻿using UnityEngine;
using System.Collections;
using MoreMountains.InfiniteRunnerEngine;
using System.Collections.Generic;
using System;
using MoreMountains.Tools;

namespace MoreMountains.InfiniteRunnerEngine
{
    public class SimpleScenario : ScenarioManager
    {
        public LaneManager _laneManager;
        public MMMultipleObjectPooler buildingSpawner;
        public List<DistanceSpawner> Set1;
        public List<DistanceSpawner> Set2;
        private bool onBreak;
        private bool swap1;
        private bool swap2;
        private static float sideRoadPoint = 500;
        private static float obstaclePoint = 1000;
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

            AddScoreEvent(sideRoadPoint, () => ChangeZone());
            AddScoreEvent(obstaclePoint, () => ChangeObstacle());
        }

        public void ChangeZone()
        {
            _scenario[0].StartScore += 500;
            swap1 = !swap1;
            print("Change Zone");
            buildingSpawner.Pool[0].Enabled = !swap1;
            buildingSpawner.Pool[1].Enabled = !swap1;
            buildingSpawner.Pool[2].Enabled = swap1;
            buildingSpawner.Pool[3].Enabled = swap1;
            buildingSpawner.Pool[4].Enabled = swap1;
        }

        public void ChangeObstacle()
        {
            if (!onBreak)
            {
                onBreak = true;
                foreach (DistanceSpawner obs in Set1)
                {
                    obs.Spawning = false;
                }
                foreach (DistanceSpawner obs in Set2)
                {
                    obs.Spawning = false;
                }
                StartCoroutine(OnABreak());
                _scenario[1].Status = false;
            }
            else
            {
                print("change obstacle");
                onBreak = false;
                _scenario[1].StartScore += 1000;
                if (swap2) _laneManager.maxInLane = 2;
                else _laneManager.maxInLane = 1;

                swap2 = !swap2;
                foreach (DistanceSpawner obs in Set1)
                {
                    obs.Spawning = !swap2;
                }
                foreach (DistanceSpawner obs in Set2)
                {
                    obs.Spawning = swap2;
                }
            }
        }

        IEnumerator OnABreak()
        {
            if(!swap2) yield return new WaitForSeconds(3);
            else yield return new WaitForSeconds(0);

            _scenario[1].Status = true;
            _scenario[1].StartScore = GameManager.Instance.Points + 20;
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