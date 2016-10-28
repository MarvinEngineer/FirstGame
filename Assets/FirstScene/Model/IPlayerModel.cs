using UnityEngine;
using System.Collections;
using System;

public interface IPlayerModel
{
    float Health { get; set; }
    int Score { get; set; }
    void Resurrect();
    void Resurrect(float hp);
    void Kill();

    event EventHandler<PlayerInformationEventArgs> playerUpdated;
}
