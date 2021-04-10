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
        public GameObject _laneManager;
        public MMMultipleObjectPooler buildingSpawner;
        public List<Spawner> Set1;
        public List<Spawner> Set2;
        public List<Spawner> Set3;
        public List<Spawner> Set4;
        private bool onBreak;
        private bool swap1;
        private bool swap2;
        private static float sideRoadPoint = 500;
        private static float obstaclePoint = 2500;
        private static float difficultPoint = 600;
        private float timePass;
        public int randomSet = 1;
        public int oldRandomSet = 1;

        protected override void Start()
        {
            Scenario();
            // once our scenario is planned, we start invoking its evaluation at regular intervals
            //InvokeRepeating("EvaluateScenario", EvaluationFrequency, EvaluationFrequency);
            EvaluateScenario();
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
            AddScoreEvent(difficultPoint, () => ChangeDifficult());
        }

        public void ChangeZone()
        {
            _scenario[0].StartScore += 500;
            swap1 = !swap1;
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
                Debug.Log("break");
                onBreak = true;
                foreach (DistanceSpawner obs in Set1)
                {
                    obs.Spawning = false;
                }
                foreach (DistanceSpawner obs in Set2)
                {
                    obs.Spawning = false;
                }
                foreach (DistanceSpawner obs in Set3)
                {
                    obs.Spawning = false;
                }
                foreach (DistanceSpawner obs in Set4)
                {
                    obs.Spawning = false;
                }
                StartCoroutine(OnABreak());
                _scenario[1].Status = false;
            }
            else
            {
                onBreak = false;
                float currentScore = GameManager.Instance.Points;

                //swap2 = !swap2;
                oldRandomSet = randomSet;
                while(oldRandomSet == randomSet)
                {
                    do
                    {
                        randomSet = UnityEngine.Random.Range(0, 5);
                        if(currentScore < 10000 && randomSet == 2) { randomSet = 0; }
                    } while(randomSet == 0 || randomSet == 5);
                }

                if(randomSet == 2) //if cop with spike, make it shorter than the rest
                    _scenario[1].StartScore = currentScore + 120;
                else if(randomSet == 4)
                    _scenario[1].StartScore = currentScore + 200;
                else
                    _scenario[1].StartScore = currentScore + 400;

                print("change obstacle "+randomSet);
                foreach (DistanceSpawner obs in Set1)
                {
                    if(randomSet == 1)
                        obs.Spawning = true;
                    else
                        obs.Spawning = false;
                }
                foreach (DistanceSpawner obs in Set2)
                {
                    if (randomSet == 2)
                        obs.Spawning = true;
                    else
                        obs.Spawning = false;
                }
                foreach (DistanceSpawner obs in Set3)
                {
                    if (randomSet == 3)
                        obs.Spawning = true;
                    else
                        obs.Spawning = false;
                }
                foreach (DistanceSpawner obs in Set4)
                {
                    if (randomSet == 4)
                        obs.Spawning = true;
                    else
                        obs.Spawning = false;
                }
            }
        }

        IEnumerator OnABreak()
        {
            if(randomSet == 1) yield return new WaitForSeconds(3);
            else yield return new WaitForSeconds(0.5f);

            _scenario[1].Status = true;
            Debug.Log("Break End");
            //_scenario[1].StartScore = GameManager.Instance.Points + 20;
        }

        public void ChangeDifficult()
        {
            if (_scenario[2].StartScore > 5000)
                _laneManager.GetComponent<LaneManager>().maxInLane = 2;

            _scenario[2].StartScore += 1500;

            if(_laneManager.transform.localScale.z > 6)
                _laneManager.transform.localScale -= Vector3.forward;
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
