using UnityEngine;
using System.Collections;

public interface IPlayerIndicatorsView
{
    int CurrentScore    { get; set; }

    float CurrentHealth    { get; set; }

    void UpdateIndicators(int score, float health);
}
