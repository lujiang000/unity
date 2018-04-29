using System.Collections;

using UnityEngine;

public class NetworkedGame : MonoBehaviour {

	void Start(){
		RefreshHostList ();
	}
	public void BeServer(){
		Debug.Log ("being server");
		Network.InitializeServer (2, 25000, !Network.HavePublicAddress ());
		MasterServer.RegisterHost ("unity2d", "turn", "test");
	}
	public void RefreshHostList(){
		MasterServer.ClearHostList ();
		MasterServer.RequestHostList ("unity2d");
	}
	void OnDestroy(){
		Network.Disconnect ();
		if (Network.isServer)
			MasterServer.UnregisterHost ();
	}
	public void BeClient(){
		Debug.Log ("being client");
		HostData[] HD = MasterServer.PollHostList ();
		if (HD.Length <= 0)
			return;
		Network.Connect (HD [0]);
	}
	void OnMasterServerEvent(MasterServerEvent msEvent){
		switch (msEvent) {
		case MasterServerEvent.HostListReceived:
			if (Network.isClient || Network.isServer)
				return;
			if (MasterServer.PollHostList ().Length <= 0)
				BeServer ();
			else
				BeClient ();
			break;
		}
	}
}
