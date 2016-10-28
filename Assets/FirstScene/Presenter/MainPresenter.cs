using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainPresenter
{

    #region Initialization
    //Views
    private List<IScoreView> listScoreView = null;
    private IPlayerIndicatorsView indicatorsView = null;
    //Models
    private IPlayerModel playerModel = null;

    public MainPresenter(IPlayerModel _pm, List<IScoreView> _lsv, IPlayerIndicatorsView _i)
    {
        playerModel = _pm;
        listScoreView = _lsv;
        indicatorsView = _i;

        foreach (IScoreView sv in listScoreView)
        {
            sv.CoinRaised += model_ScoreUpdated;
        }

        playerModel.playerUpdated += view_InformationUpdated;

        indicatorsView.UpdateIndicators(playerModel.Score, playerModel.Health);
    }



    #endregion Initialization

    #region Event handlers

    private void view_HealthUpdated(object sender, HealthChangedEventArgs e)
    {
        playerModel.Health += e.difference;
    }

    private void model_ScoreUpdated(object sender, CoinRaisedEventArgs e)
    {
        playerModel.Score += e.points;
    }

    private void view_InformationUpdated(object sender, PlayerInformationEventArgs e)
    {
        indicatorsView.UpdateIndicators(e.scores, e.hp);
    }




    #endregion Event handlers
}

