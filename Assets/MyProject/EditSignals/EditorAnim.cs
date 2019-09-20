using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

public class EditorAnim : MonoBehaviour
{
    //public Animation Animation;
    //public Animator Anim;
    public AnimatorController Controller;

    [Range(0, 100)] public float Time;

    // Start is called before the first frame update
    void Start()
    {
        //var name = "teste9";
        //Controller.AddParameter(name, AnimatorControllerParameterType.Trigger);
        //AnimatorStateMachine rootStateMachine = Controller.layers[0].stateMachine;
        //AnimatorState stateA1 = rootStateMachine.AddState(name);
        //var transitionAnimation = rootStateMachine.AddAnyStateTransition(stateA1);
        //transitionAnimation.AddCondition(AnimatorConditionMode.If, 0, name);
        //
        //
        //var returnIdle = stateA1.AddTransition(rootStateMachine.defaultState);
        //returnIdle.hasExitTime = true;
        //returnIdle.exitTime = 1;
        //AnimationClip clip = AssetDatabase.LoadAssetAtPath<AnimationClip>($"Assets/Teste/{name}.anim");
        //stateA1.motion = clip;
        //
        

    }

    // Update is called once per frame
    void Update()
    {

        AnimationClip clip = AssetDatabase.LoadAssetAtPath<AnimationClip>("Assets/Teste/teste9.anim");

        clip.SampleAnimation(this.gameObject, (clip.length/100)*Time);
    }
}
