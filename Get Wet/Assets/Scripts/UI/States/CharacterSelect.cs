﻿using UnityEngine;
using System.Collections;

public class CharacterSelect : State {

	public override void OnEnter()
	{
		
	}
	
	public override void OnUpdate()
	{
		
	}
	
	public override void OnExit()
	{
	
	}

	public void BackToMenu()
	{
		m_UIManager.ChangeState (m_UIManager.MainMenuName);
	}
	
	public void ChooseWeapon()
	{
		m_UIManager.ChangeState (m_UIManager.WeaponSelectName);
	}

	public void LoadLevel(string levelName)
	{
		Time.timeScale = 1f;
		Application.LoadLevel (levelName);
	}
}
