using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class ComboSystem : MonoBehaviour, IComboSystem
{
    [SerializeField] private AnimationController animationController;
    public event System.Action OnAnimationFinished;
    private List<string> currentCombo = new List<string>();
    private Dictionary<List<string>, string> comboResultAnimations = new Dictionary<List<string>, string>();
    private bool isPlayingComboAnimation = false;
    private Coroutine comboInputCoroutine;
    private PlayerInputHandler inputHandler;
    
    private void Start()
    {
        inputHandler = PlayerInputHandler.Instance;
        DefineCombos();
        
    }
    private void FixedUpdate()
    {
        if (inputHandler.CrossPunchTriggered)
        {
            AddInputToCombo("CrossPunch");
            PlayAnimation("cross_punch_R_animation");
        }
        if (inputHandler.LightJabTriggered)
        {
            AddInputToCombo("LightJab");
            PlayAnimation("light_jab_animation");
        }
    }
    private void DefineCombos()
    {
        List<string> comboSequence1 = new List<string> { "CrossPunch", "LightJab", "CrossPunch" };
        comboResultAnimations.Add(comboSequence1, "jump_and_ground_slam_R_animation");

        List<string> comboSequence2 = new List<string> { "LightJab", "LightJab", "CrossPunch" };
        comboResultAnimations.Add(comboSequence2, "chain_punch_R_animation");
    }
    private void AddInputToCombo(string input)
    {
        if (isPlayingComboAnimation)
        {
            return;
        }
        currentCombo.Add(input);
        
        if (comboInputCoroutine != null)
        {
            StopCoroutine(comboInputCoroutine);
        }
        comboInputCoroutine = StartCoroutine(ComboInputDelay());
    }

    private IEnumerator ComboInputDelay()
    {
        yield return new WaitForSeconds(3f);
        CheckCombos();
    }
    
    // Animation dalgasını bu fonksiyondan çıkar true false dönsün
    // Combolar objeleştirilecek
    // Input bilgi akışı eventler üzerinden yapılcak.(ON..  eventlere subsribe olcak)
    private void CheckCombos()
    {
        foreach (var comboSequence in comboResultAnimations.Keys)
        {
            if (IsComboMatch(comboSequence))
            {
                string comboResultAnimationName = comboResultAnimations[comboSequence];
                Debug.Log("Matching combo sequence: " + string.Join(" -> ", comboSequence) + " -> " + comboResultAnimationName);
                PlayAnimation(comboResultAnimationName);
                currentCombo.Clear();
                return;
            }
        }
        Debug.Log("No matching combo found.");
        currentCombo.Clear();
    }
    
    private bool IsComboMatch(List<string> comboSequence)
    {
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
