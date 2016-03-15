using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class WizardGUI : NetworkBehaviour {
	private WizardStats wizard;
	private SpellCaster caster;
	public Texture hp, mana, bg, oom, cd;
	public GUISkin skin, other;


	// Use this for initialization
	void Start () {
		wizard = GetComponent<WizardStats> ();
		caster = GetComponent<SpellCaster> ();
	}

	void OnGUI() {
		if (!isLocalPlayer) 
			return;

		drawSpellIcon(new Rect(Screen.width/2-400, Screen.height-60, 60, 60), caster.spells[0]);
		drawSpellIcon(new Rect(Screen.width/2-300, Screen.height-60, 60, 60), caster.spells[1]);
		drawSpellIcon(new Rect(Screen.width/2+240, Screen.height-60, 60, 60), caster.spells[2]);
		drawSpellIcon(new Rect(Screen.width/2+340, Screen.height-60, 60, 60), caster.spells[3]);
		
		GUI.DrawTexture (new Rect(Screen.width/2-200, Screen.height-64, 400, 30), bg, ScaleMode.StretchToFill);
		GUI.DrawTexture (new Rect(Screen.width/2-200, Screen.height-64, 400*(wizard.hp/wizard.maxHP), 30), hp, ScaleMode.StretchToFill);


		GUI.DrawTexture (new Rect(Screen.width/2-200, Screen.height-32, 400, 30), bg, ScaleMode.StretchToFill);
		GUI.DrawTexture (new Rect(Screen.width/2-200, Screen.height-32, 400*(wizard.mana/wizard.maxMana), 30), mana, ScaleMode.StretchToFill);
		
		GUI.skin = other;
		GUI.Label(new Rect(Screen.width/2-200, Screen.height-64, 400, 30), ""+wizard.hp.ToString("F0")+"/"+wizard.maxHP.ToString("F0"));
		GUI.Label(new Rect(Screen.width/2-200, Screen.height-32, 400, 30), ""+wizard.mana.ToString("F0")+"/"+wizard.maxMana.ToString("F0"));
	}


	void drawSpellIcon(Rect rect, SpellRoot spell) {
		GUI.skin = skin;
		GUI.Label (new Rect(rect.x-20, rect.y-30, 100, 30), ""+spell.spellName);
		GUI.Label (new Rect(rect.x-20, rect.y-60, 100, 30), ""+spell.key.ToString());
		GUI.DrawTexture (rect, spell.icon, ScaleMode.StretchToFill);
		if(spell.cooldownTimer > 0) {
			GUI.DrawTexture (rect, cd, ScaleMode.StretchToFill);
			GUI.Box (rect, ""+spell.cooldownTimer.ToString("F1"));
		}
		if(wizard.mana < spell.manaCost) {
			GUI.DrawTexture (rect, oom, ScaleMode.StretchToFill);
		}
	}
}
