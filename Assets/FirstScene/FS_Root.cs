using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FS_Root : MonoBehaviour {

    [SerializeField]    private GameObject ScoreSphereRoot;
    [SerializeField]    private GameObject indicators;


    private IPlayerModel playerModel;
    private MainPresenter presenter;
    private List<IScoreView> scoreView;

    void Start()
    {
        playerModel = new PlayerModel();
        scoreView = new List<IScoreView>();

        ScoreSphereRoot.GetComponentsInChildren(true, scoreView);
        presenter = new MainPresenter(playerModel, scoreView, indicators.GetComponent<PlayerIndicatorsView>());
    }

    // Update is called once per frame
    void Update () {
	
	}
}
