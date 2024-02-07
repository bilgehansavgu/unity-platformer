using UnityEngine;
using System.Collections.Generic;

public class ComboSystem : MonoBehaviour, IComboSystem
{
    [SerializeField] private AnimationController animationController; // Serialized field for AnimationController
    public event System.Action OnAnimationFinished;
    private List<string> currentCombo = new List<string>();
    private Dictionary<List<string>, string> comboResultAnimations = new Dictionary<List<string>, string>();

    private void Start()
    {
        DefineCombos();
    }
    public void Initialize(AnimationController controller)
    {
        animationController = controller;
    }
    private void DefineCombos()
    {
        List<string> comboSequence1 = new List<string> { "CrossPunch", "LightJab", "CrossPunch" };
        comboResultAnimations.Add(comboSequence1, "jump_and_ground_slam_R_animation");

        List<string> comboSequence2 = new List<string> { "LightJab", "LightJab", "CrossPunch" };
        comboResultAnimations.Add(comboSequence2, "chain_punch_R_animation");

    }
    public void OnCrossPunch()
    {
        Debug.Log("Cross Punch Added to combo waitlist");

        currentCombo.Add("CrossPunch");
        PlayAnimation("cross_punch_R_animation");
        CheckCombos();
    }
    public void OnLightJab()
    {
        Debug.Log("Light Jab Added to combo waitlist");

        currentCombo.Add("LightJab");
        PlayAnimation("light_jab_animation");
        CheckCombos();
    }
    private void CheckCombos()
    {
        foreach (var comboSequence in comboResultAnimations.Keys)
        {
            if (IsComboMatch(comboSequence))
            {
                string comboResultAnimationName = comboResultAnimations[comboSequence];
                PlayAnimation(comboResultAnimationName);
                currentCombo.Clear();
                
                if (OnAnimationFinished != null)
                    OnAnimationFinished();
                return;
            }
        }
    }

    private bool IsComboMatch(List<string> comboSequence)
    {
        // Check if the current combo matches the specified sequence
        if (currentCombo.Count < comboSequence.Count)
            return false;

        for (int i = 0; i < comboSequence.Count; i++)
        {
            if (currentCombo[i] != comboSequence[i])
                return false;
        }

        return true;
    }
    private void PlayAnimation(string animationName)
    {
        if (animationController != null)
        {
            animationController.PlayAnimation(animationName);
        }
        else
        {
            Debug.LogWarning("AnimationController reference is null in ComboSystem. Cannot play animation.");
        }
    }
}
