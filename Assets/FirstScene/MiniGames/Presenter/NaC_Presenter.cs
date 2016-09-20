using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class NaC_Presenter 
{
    #region Initialization

    private List<ICubeView> cubeViews = null;
    private INaC_Model model = null;
    private IControlView main = null;

    public NaC_Presenter(List<ICubeView> boardView, INaC_Model boardModel, IControlView mainView)
    {
        model = boardModel;
        model.BoardUpdated += model_BoardUpdated;

        cubeViews = boardView;
        foreach (ICubeView view in cubeViews)
        {
            view.FieldUpdated += view_FieldUpdated;
            view.UpdateField(model.GetField(view.GetField().x, view.GetField().y));
        }

        main = mainView;
        main.ResetClicked += main_ResetClicked;
    }



    #endregion Initialization

    #region Event handlers

    private void view_FieldUpdated(object sender, FieldEventArgs e)
    {
        model.UpdateGame(e.Field);
    }

    private void model_BoardUpdated(object sender, BoardEventArgs e)
    {
        foreach (ICubeView view in cubeViews)
        {
            view.UpdateField(e.Board.GetField(view.GetField().x, view.GetField().y));
        }
    }

    private void main_ResetClicked(object sender, EventArgs e)
    {
        model.Reset();
    }

    #endregion Event handlers
}

