using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TMPro;

public class NPCControl : MonoBehaviour
{
    public GameObject player;
    public Transform[] path;
    private FSMSystem fsm;
    public TextMeshProUGUI Text;

    public void SetTransition(Transition t) { fsm.PerformTransition(t); }

    public void Start()
    {
        MakeFSM();
    }

    public void FixedUpdate()
    {
        //fsm.ShowStateCount();
      
    }
    public void Update()
    {
        fsm.CurrentState.Reason(player, gameObject);
        fsm.CurrentState.Act(player, gameObject);
    }

    // The NPC has two states: FollowPath and ChasePlayer
    // If it's on the first state and SawPlayer transition is fired, it changes to ChasePlayer
    // If it's on ChasePlayerState and LostPlayer transition is fired, it returns to FollowPath
    private void MakeFSM()
    {
        SleepState sleep = new SleepState();
        sleep.AddTransition(Transition.ClockGo, StateID.Study);

        StudyState study = new StudyState();
        study.AddTransition(Transition.WannaPlayGame, StateID.PlayGame);
        study.AddTransition(Transition.WannaEat, StateID.Eat);

        PlayState play = new PlayState();
        play.AddTransition(Transition.WannaStudy, StateID.Study);
        play.AddTransition(Transition.WannaSleep, StateID.Sleeping);

        EatState eat = new EatState();
        eat.AddTransition(Transition.WannaPlayGame, StateID.PlayGame);
        eat.AddTransition(Transition.WannaStudy, StateID.Study);
        eat.AddTransition(Transition.WannaSleep, StateID.Sleeping);


        fsm = new FSMSystem();
        fsm.AddState(sleep);
        fsm.AddState(study);
        fsm.AddState(play);
        fsm.AddState(eat);
    }
}

public class SleepState : FSMState
{
    public SleepState()
    {
        stateID = StateID.Sleeping;
    }
    public override void Reason(GameObject player, GameObject npc)
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            npc.GetComponent<NPCControl>().SetTransition(Transition.ClockGo);
        }
    }

    public override void Act(GameObject player, GameObject npc)
    {
        npc.GetComponent<NPCControl>().Text.SetText("Sleeping!\n" + "press W key,convert to study state");

    }
}

public class EatState : FSMState
{
    public EatState()
    {
        stateID = StateID.Eat;
    }
    public override void Act(GameObject player, GameObject npc)
    {
        npc.GetComponent<NPCControl>().Text.SetText("Eating!\n" + "press S key,conver to study state\n" + "press P key,convert to Play state\n" + "press L key,convert to sleep state");
    }

    public override void Reason(GameObject player, GameObject npc)
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            npc.GetComponent<NPCControl>().SetTransition(Transition.WannaStudy);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            npc.GetComponent<NPCControl>().SetTransition(Transition.WannaPlayGame);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            npc.GetComponent<NPCControl>().SetTransition(Transition.WannaSleep);

        }

    }
}

public class StudyState : FSMState
{
    public StudyState()
    {
        stateID = StateID.Study;
    }
    public override void Act(GameObject player, GameObject npc)
    {
        npc.GetComponent<NPCControl>().Text.SetText("Studying!\n" + "press P key,convert to play state\n" + "press E key,convert to eat state");
    }

    public override void Reason(GameObject player, GameObject npc)
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            npc.GetComponent<NPCControl>().SetTransition(Transition.WannaPlayGame);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            npc.GetComponent<NPCControl>().SetTransition(Transition.WannaEat);

        }
    }
}

public class PlayState : FSMState
{
    public PlayState()
    {
        stateID = StateID.PlayGame;
    }
    public override void Act(GameObject player, GameObject npc)
    {
        npc.GetComponent<NPCControl>().Text.SetText("Playing!\n" + "press S key,convert to study state\n" + "press P key, convert to sleep state");

    }

    public override void Reason(GameObject player, GameObject npc)
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            npc.GetComponent<NPCControl>().SetTransition(Transition.WannaStudy);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            npc.GetComponent<NPCControl>().SetTransition(Transition.WannaSleep);
        }

    }
}

