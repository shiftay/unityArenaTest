using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour 
{

	public Sprite[] spriteChoices;
	public int choice = 0;
	public Image img;
	public Slider Red;
	public Slider Green;
	public Slider Blue;
	public Image laserC;
	
	void Start()
	{
		img.sprite = spriteChoices[choice];
	}

	void Update()
	{
		//color.color = GM.Instance.color;
		GameManager.Instance.skinChoice = choice;
	}

	public void Next()
	{
		choice++;
		if(choice >= spriteChoices.Length)
		{
			choice = 0;
		}
		img.sprite = spriteChoices[choice];
	}
	
	public void Back()
	{
		choice--;
		if(choice < 0)
		{
			choice = spriteChoices.Length - 1;
		}
		img.sprite = spriteChoices[choice];
	}
}