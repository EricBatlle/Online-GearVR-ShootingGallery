using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.Networking.Types;
using System.Collections;

namespace Prototype.NetworkLobby
{
    public class LobbyServerEntry : MonoBehaviour 
    {
        public Text serverInfoText;
        public Text slotInfo;
        public Button joinButton;

        public MatchInfoSnapshot match;
        public LobbyManager lobbyManager; 

		public void Populate(MatchInfoSnapshot match, LobbyManager lobbyManager, Color c)
		{
            this.match = match;
            serverInfoText.text = match.name;

            slotInfo.text = match.currentSize.ToString() + "/" + match.maxSize.ToString();


            NetworkID networkID = match.networkId;
          
            this.lobbyManager = lobbyManager;

           // joinButton.onClick.RemoveAllListeners();
            //joinButton.onClick.AddListener(() => { JoinMatch(networkID, lobbyManager); });

            GetComponent<Image>().color = c;
        }

        public void JoinMatch(NetworkID networkID, LobbyManager lobbyManager)
        {
           
            lobbyManager.matchMaker.JoinMatch(networkID, "", "", "", 0, 0, lobbyManager.OnMatchJoined);
			lobbyManager.backDelegate = lobbyManager.StopClientClbk;
            lobbyManager._isMatchmaking = true;
            lobbyManager.DisplayIsConnecting();
        }        
    }
}