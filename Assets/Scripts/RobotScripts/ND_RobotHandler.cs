using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Keeps track of all the different Log Foods that the robot wants to eat.
/// </summary>

public class ND_RobotHandler:MonoBehaviour{
	public static ND_RobotHandler inst;
	public List<int>	m_wantedFoods;			//Indices into NutrientSpawner.mp_spawnerInfo.m_logFoods of which foods the robot wants.
	public int			m_wantedFoodIndex;
	public Image		m_robotWantedFoodImage;
	public Image		m_progressBarFill;
	public Animator		m_animRobot;			//Robot animator.

	private int			m_badFoodsEaten;		//Number of bad foods eaten.

	private void Awake() {
		if(inst==null) inst=this;
		else {
			Debug.Log("WantedFood destroyed on '"+gameObject.name+"'. Are there duplicates in the scene?");
			DestroyImmediate(this);
		}
	}
	void Start(){
		if(m_wantedFoods==null) m_wantedFoods=new List<int>();
		m_wantedFoodIndex=-1;

		m_robotWantedFoodImage.gameObject.SetActive(false);
		m_badFoodsEaten=0;
	}
	public void SetWantedFoods() {
		//Creates a list of the robot's wanted foods based on NutrientSpawner.mp_spawnerInfo.m_logFoods.
		int _logFoodCount=NutrientSpawner.inst.GetLogFoods().Count;
		int _neededItems=NutrientSpawner.inst.GetSpawnerInfo().m_neededItems;
		bool _needAllLogFoods=NutrientSpawner.inst.GetSpawnerInfo().m_needAllLogFoods;

		List<int> _tempList=new List<int>();
		m_wantedFoods.Clear();

		//If one of each Log Food is required, add those in first.
		if(_needAllLogFoods)
			for(int i=0; i<_logFoodCount; i++)
				_tempList.Add(i);

		//Fill remaining spaces with random Log Foods.
		while(_tempList.Count<_neededItems)
			_tempList.Add(Random.Range(0, _logFoodCount));

		//Shorten the list if the number of Log Foods was longer than the number of needed items.
		while(_tempList.Count>_neededItems)
			_tempList.RemoveAt(_tempList.Count-1);

		//Randomize the list into the final wanted foods list.
		while(_tempList.Count>0) {
			int _picked=Random.Range(0, _tempList.Count);
			m_wantedFoods.Add(_tempList[_picked]);
			_tempList.RemoveAt(_picked);
		}

		//Show the first wanted food on the robot.
		m_wantedFoodIndex=-1;
		m_robotWantedFoodImage.gameObject.SetActive(true);
		NextFood();
	}

	public void Update(){
		m_animRobot.SetFloat("MoveSpeed", NutrientBucket.inst.GetMoveSpeed());
	}

	public void NextFood(){
		//Count a wanted food as obtained and show the next one. End the game if over.
		m_wantedFoodIndex++;
		UpdateProgressBar();

		if(m_wantedFoodIndex>=m_wantedFoods.Count) {
			//End level.
			m_robotWantedFoodImage.gameObject.SetActive(false);
			ND_GameController.inst.EndLevelSuccess();
		}
		else {
			m_robotWantedFoodImage.sprite=NutrientSpawner.inst.GetLogFoods()[m_wantedFoods[m_wantedFoodIndex]];
		}
	}

	public float GetCompletionPercentage(){
		//Returns how far into the number of wanted foods the robot has eaten.
		if(m_wantedFoods.Count==0) return 0;

		float _result=(float)m_wantedFoodIndex/(float)m_wantedFoods.Count;
		
		if(_result<0) return 0;
		else if(_result>1) return 1;
		return _result;
	}
	public int GetWantedFoodIndex(){
		//Get which food into NutrientSpawner.mp_spawnerInfo.m_logFoods the robot currently wants.
		if((m_wantedFoods.Count>0)&&(m_wantedFoodIndex>=0)&&(m_wantedFoodIndex<m_wantedFoods.Count))
			return m_wantedFoods[m_wantedFoodIndex];

		Debug.Log("ERROR: WantedFood.GetWantedFoodIndex: The WantedFood isn't ready to be asked which food the robot wants.");
		return -1;
	}
	public void EatBadFood() {
		//A non-food was eaten.
		m_badFoodsEaten++;

		if((ND_GameController.inst.m_levelInput.m_foodDropLevelInput.m_robotInfo.m_robotHealth>0)&&(m_badFoodsEaten>=ND_GameController.inst.m_levelInput.m_foodDropLevelInput.m_robotInfo.m_robotHealth)){
			//Robot has eaten too much trash. Game over.
			ND_GameController.inst.EndLevelRobotDead();
		}
	}

	private void UpdateProgressBar() {
		//Update the progress bar based on the completion percentage.
		m_progressBarFill.fillAmount=GetCompletionPercentage();
	}
}