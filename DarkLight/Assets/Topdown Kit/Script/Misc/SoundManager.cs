/// <summary>
/// Sound manager.
/// This script use for play sound effect
/// </summary>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour {
	
	
	[System.Serializable]
	public class SoundGroup{
		public string soundName;
		public AudioClip audioClip;
		
	}
	
	public List<SoundGroup> sound_List = new List<SoundGroup>();
	
	public static SoundManager instance;
	
	public void Start(){
		instance = this;	
	}
	/// <summary>
    /// ≤•∑≈“Ù–ß
    /// </summary>
    /// <param name="_soundName"></param>
	public void PlayingSound(string _soundName){
		AudioSource.PlayClipAtPoint(FindClip(_soundName), TinyTeam.UI.TTUIRoot.Instance.uiCamera.transform.position);
	}
    private AudioClip FindClip(string _soundName)
    {
        SoundGroup sg = sound_List.Find(x => x.soundName == _soundName);
        if (sg!=null)
        {
            return sg.audioClip;
        }
        else
        {
            return null;
        }
    }
	
	private int FindSound(string _soundName){
		int i = 0;
		while( i < sound_List.Count ){
			if(sound_List[i].soundName == _soundName){
				return i;	
			}
			i++;
		}
		return i;
	}
	
}
