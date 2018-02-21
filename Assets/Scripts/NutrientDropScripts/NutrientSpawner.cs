using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FOODTYPE{
	NONE,
	LOGFOOD,
	OTHERFOOD,
	NOTFOOD
}

public class NutrientSpawner:MonoBehaviour {
	public	static NutrientSpawner inst;
	public	GameObject		m_spawnZone;				//Zone where food spawns.
	public	GameObject		m_pre_SpawnItem;			//Prefab for a spawned food.
	public	bool			m_active;					//Game active.

	private RectTransform	mp_SZ_rectTransform;		//Spawn zone transform.
	private RectTransform	mp_canvasRectTransform;		//Gameplay Canvas
	private ND_SpawnerInfo	mp_spawnerInfo;				//Copy of the ND_LevelInput's SpawnerInfo.
	private float			mp_spawnTimer;				//How long until the next item spawns.
	private float			mp_levelTimer;				//Total time the level has been playing.
	private List<FOODTYPE>	mp_spawnGroup;				//Randomized group of items to next spawn.
	private int				mp_spawnGroupIndex;			//Index into mp_spawnGroup of the next item to spawn.
	private List<int>		mp_logFoodsYetSpawned;		//The list of which Log Foods haven't been spawned yet. Used if mp_spawnerInfo.m_needAllLogFoods=true.

	private void Awake() {
		if(inst==null) inst=this;
		else {
			Debug.Log("NutrientSpawner destroyed on '"+gameObject.name+"'. Are there duplicates in the scene?");
			DestroyImmediate(this);
		}
	}
	void Start(){
		m_active=false;

		mp_SZ_rectTransform = m_spawnZone.GetComponent<RectTransform>();
		mp_canvasRectTransform = m_spawnZone.GetComponentInParent<Canvas>().GetComponent<RectTransform>();

		mp_levelTimer=0;
		mp_spawnGroup=new List<FOODTYPE>();
		mp_spawnGroupIndex=0;
		mp_logFoodsYetSpawned=new List<int>();

		//timer = mp_spawnerInfo.m_spawnRate + Random.Range(-mp_spawnerInfo.m_spawnRandomness, +mp_spawnerInfo.m_spawnRandomness);
		//currentWantedFoodTimer = WantedFoodTimer;
	}
	
	void Update(){
		if(m_active){
			mp_spawnTimer-=Time.deltaTime;

			if(mp_spawnTimer<=0) {
				//Spawn the next item.
				SpawnFood(mp_spawnGroup[mp_spawnGroupIndex]);
				mp_spawnGroupIndex++;

				//If the entire item group has been spawned, randomize a new group.
				if(mp_spawnGroupIndex==mp_spawnGroup.Count) RandomizeNextItemGroup();   

				// Additional chance to spawn an item for variance.
				if(Random.Range(0.0f, 1.0f)<mp_spawnerInfo.m_additionalSpawnChance){
					SpawnRandomFood();
				}

				//Reset the spawnTimer.
				mp_spawnTimer=mp_spawnerInfo.m_spawnRate+Random.Range(-mp_spawnerInfo.m_spawnRandomness, +mp_spawnerInfo.m_spawnRandomness);
			}

			//Check if the time limit for the level has passed.
			mp_levelTimer+=Time.deltaTime;
			if((mp_spawnerInfo.m_timeLimit!=0)&&(mp_levelTimer>=mp_spawnerInfo.m_timeLimit)){
				ND_GameController.inst.EndLevelTimeLimit();
			}

			//Check if the robot has died from eating too many non-foods.
		}
	}

	public void SpawnObject(Vector3 position){
		// Spawns the food somewhere within the nutrient spawner
		//GameObject newFoodItem = Instantiate(spawnedItem, mp_SZ_rectTransform);
		//newFoodItem.transform.Translate((new Vector2(Random.Range(-mp_SZ_rectTransform.rect.width, mp_SZ_rectTransform.rect.width), Random.Range(-mp_SZ_rectTransform.rect.height, mp_SZ_rectTransform.rect.height)) / 2.0f + mp_SZ_rectTransform.anchoredPosition) * mp_canvasRectTransform.localScale.x);
		
		//float foodSelection = Random.Range(0.0f, 1.0f);
		//NutrientLife.FoodType selectedType = NutrientLife.FoodType.LogFood;
		//Sprite selectedSprite;

		//if (foodSelection >= LoggableFoodSelectionBarrier)
		//{
		//	selectedType = NutrientLife.FoodType.LogFood;
		//	selectedSprite = mp_spawnerInfo.m_logFoods[Random.Range(0, mp_spawnerInfo.m_logFoods.Count)]; //int Random.Range is [inclusive, exclusive];

		//	newFoodItem.GetComponent<NutrientLife>().SetFoodType(selectedType, selectedSprite);
		//}
		//else if (foodSelection >= OtherFoodSelectionBarrier)
		//{
		//	selectedType = NutrientLife.FoodType.NotFood;
		//	selectedSprite = mp_spawnerInfo.m_notFoods[Random.Range(0, mp_spawnerInfo.m_notFoods.Count)];

		//	newFoodItem.GetComponent<NutrientLife>().SetFoodType(selectedType, selectedSprite);
		//}
		//else
		//{
		//	selectedType = NutrientLife.FoodType.OtherFood;
		//	selectedSprite = mp_spawnerInfo.m_otherFoods[Random.Range(0, mp_spawnerInfo.m_otherFoods.Count)];

		//	newFoodItem.GetComponent<NutrientLife>().SetFoodType(selectedType, selectedSprite);
		//}

		//if (selectedSprite == wantedFood.currentWantedFood.sprite)
		//{
		//	currentWantedFoodTimer = WantedFoodTimer;
		//}
	}
	private void SpawnWantedObject(){
		//GameObject newFoodItem = Instantiate(spawnedItem, mp_SZ_rectTransform);
		//newFoodItem.transform.Translate((new Vector2(Random.Range(-mp_SZ_rectTransform.rect.width, mp_SZ_rectTransform.rect.width), Random.Range(-mp_SZ_rectTransform.rect.height, mp_SZ_rectTransform.rect.height)) / 2.0f + mp_SZ_rectTransform.anchoredPosition) * mp_canvasRectTransform.localScale.x);
		//newFoodItem.GetComponent<NutrientLife>().SetFoodType(NutrientLife.FoodType.LogFood, wantedFood.currentWantedFood.sprite);
	}

	public bool SetSpawnerInfo(ND_SpawnerInfo _info) {
		//Reads the LevelInfo's SpawnerInfo to use. Returns whether a major issue occurred.
		mp_spawnerInfo=_info;
		mp_spawnTimer=mp_spawnerInfo.m_spawnRate + Random.Range(-mp_spawnerInfo.m_spawnRandomness, +mp_spawnerInfo.m_spawnRandomness);
		RandomizeNextItemGroup();

		//If all Log Foods need to be spawned at least once, set up a list of which haven't been spawned yet.
		mp_logFoodsYetSpawned.Clear();
		for(int i=0; i<_info.m_logFoods.Count; i++) mp_logFoodsYetSpawned.Add(i);

		//Check for any issues.	////
		//The percent chance of Log Foods and Not Foods to spawn is more than 100% combined.
		if(_info.m_logFoodPercent+_info.m_notFoodPercent>1.0f){
			Debug.Log("ERROR: NutrientSpawner.SetSpawnerInfo: The Log Food Percent and Not Food Percent add up to over 100% chance. The level will prioritize spawning Log Foods.");
		}

		if(_info.m_logFoods.Count==0) {
			Debug.Log("ERROR: NutrientSpawner.SetSpawnerInfo: The list of Log Foods has nothing in it.");
			return true;
		}

		if((_info.m_notFoods.Count==0)&&(_info.m_notFoodPercent>0)) {
			Debug.Log("ERROR: NutrientSpawner.SetSpawnerInfo: The list of Not Foods has nothing in it, but the Not Food Percent is >0. No Not Foods will be spawned.");
		}

		if(_info.m_spawnRate-_info.m_spawnRandomness<=0)
			Debug.Log("ERROR: NutrientSpawner.SetSpawnerInfo: The Spawn Randomness is too large for the Spawn Rate. This may lead to two items regularly spawned at once.");

		if(_info.m_neededItems<=0)
			Debug.Log("ERROR: NutrientSpawner.SetSpawnerInfo: The number of Needed Items is <=0. The level will end immediately.");

		return false;
	}
	public List<Sprite> GetLogFoods() {
		//Returns the SpawnerInfo's LogFoods.
		if((mp_spawnerInfo!=null)&&(mp_spawnerInfo.m_logFoods!=null)) return mp_spawnerInfo.m_logFoods;
		
		Debug.Log("ERROR: NutrientSpawner.GetLogFoods doesn't have any Log Foods to return.");
		return null;
	}
	public ND_SpawnerInfo GetSpawnerInfo() {
		//Returns the SpawnerInfo.
		return mp_spawnerInfo;
	}
	public void StartGame(){
		//Start the spawner.
		m_active=true;
		mp_levelTimer=0;
		//Reset the spawnTimer.
		mp_spawnTimer=mp_spawnerInfo.m_spawnRate+Random.Range(-mp_spawnerInfo.m_spawnRandomness, +mp_spawnerInfo.m_spawnRandomness);
	}

	public void SpawnFood(FOODTYPE _type){
		//Spawn a food item.

		//Spawn the food somewhere within the nutrient spawner.
		GameObject _newFoodItem = Instantiate(m_pre_SpawnItem, mp_SZ_rectTransform);
		_newFoodItem.transform.Translate((new Vector2(Random.Range(-mp_SZ_rectTransform.rect.width, mp_SZ_rectTransform.rect.width), Random.Range(-mp_SZ_rectTransform.rect.height, mp_SZ_rectTransform.rect.height))/2.0f+mp_SZ_rectTransform.anchoredPosition)*mp_canvasRectTransform.localScale.x);

		//Set the food's information and sprite.
		int _spriteIndex;
		if(_type==FOODTYPE.LOGFOOD) {
			if(Random.Range(0.0f,1.0f)<mp_spawnerInfo.m_neededFoodChance) { //Spawn the food the robot currently needs.
				int _wantedFoodIndex=ND_RobotHandler.inst.GetWantedFoodIndex();
				if(_wantedFoodIndex>0) _spriteIndex=_wantedFoodIndex;
				else _spriteIndex=Random.Range(0,mp_spawnerInfo.m_logFoods.Count);
			}
			else if((mp_spawnerInfo.m_needAllLogFoods)&&(mp_logFoodsYetSpawned.Count>0)){    //If each Log Food needs to be spawned at least once, use one of the unused ones.
				int _LFNYSIndex=Random.Range(0,mp_logFoodsYetSpawned.Count);
				_spriteIndex=mp_logFoodsYetSpawned[_LFNYSIndex];
				mp_logFoodsYetSpawned.RemoveAt(_LFNYSIndex);
			}
			else{	//Else, pick a random Log Food.
				_spriteIndex=Random.Range(0,mp_spawnerInfo.m_logFoods.Count);
			}
			_newFoodItem.GetComponent<NutrientLife>().SetFoodType(_type, _spriteIndex, mp_spawnerInfo.m_logFoods[_spriteIndex]);
		}
		else if(_type==FOODTYPE.OTHERFOOD) {
			_spriteIndex=Random.Range(0,mp_spawnerInfo.m_otherFoods.Count);
			_newFoodItem.GetComponent<NutrientLife>().SetFoodType(_type, _spriteIndex, mp_spawnerInfo.m_otherFoods[_spriteIndex]);
		}
		else if(_type==FOODTYPE.NOTFOOD) {
			_spriteIndex=Random.Range(0,mp_spawnerInfo.m_notFoods.Count);
			_newFoodItem.GetComponent<NutrientLife>().SetFoodType(_type, _spriteIndex, mp_spawnerInfo.m_notFoods[_spriteIndex]);
		}
	}
	public void SpawnRandomFood(){
		//Spawn a random food item.
		bool _spawned=false;

		while(!_spawned) {
			int _choice=Random.Range(0,3);
			if((_choice==0)&&(mp_spawnerInfo.m_logFoods.Count>0)) {
				SpawnFood(FOODTYPE.LOGFOOD);
				_spawned=true;
			}
			else if((_choice==1)&&(mp_spawnerInfo.m_otherFoods.Count>0)) {
				SpawnFood(FOODTYPE.OTHERFOOD);
				_spawned=true;
			}
			else if((_choice==2)&&(mp_spawnerInfo.m_notFoods.Count>0)) {
				SpawnFood(FOODTYPE.NOTFOOD);
				_spawned=true;
			}
		}		
	}

	private void RandomizeNextItemGroup() {
		//Generates a new spawnGroup of items to start spawning.
		if(mp_spawnGroup==null) mp_spawnGroup=new List<FOODTYPE>();
		mp_spawnGroup.Clear();
		mp_spawnGroupIndex=0;

		//Make a list of all items needed to spawn according to the logFood and notFood percentages.
		List<FOODTYPE> _fillList=new List<FOODTYPE>();
		if(mp_spawnerInfo.m_logFoods.Count>0)	//Add in all log foods.
			for(int i=0; i<Mathf.CeilToInt(mp_spawnerInfo.m_logFoodPercent*mp_spawnerInfo.m_spawnGroupSize); i++) _fillList.Add(FOODTYPE.LOGFOOD);
		if(mp_spawnerInfo.m_notFoods.Count>0)	//Add in all not foods.
			for(int i=0; i<Mathf.CeilToInt(mp_spawnerInfo.m_notFoodPercent*mp_spawnerInfo.m_spawnGroupSize); i++) _fillList.Add(FOODTYPE.NOTFOOD);
		if(mp_spawnerInfo.m_otherFoods.Count>0)	//Add in remaining Other foods.
			while(_fillList.Count<mp_spawnerInfo.m_spawnGroupSize) _fillList.Add(FOODTYPE.OTHERFOOD);
		else //If there were no Other foods to add in, add in Log foods instead.
			while(_fillList.Count<mp_spawnerInfo.m_spawnGroupSize) _fillList.Add(FOODTYPE.LOGFOOD);

		//Trim the _fillList if it is too long.
		while(_fillList.Count>mp_spawnerInfo.m_spawnGroupSize) {
			_fillList.RemoveAt(_fillList.Count-1);
		}
		
		//Randomize the _fillList into the actual mp_spawnGroup of items.
		while(_fillList.Count>0) {
			int _selection=Random.Range(0, _fillList.Count);
			mp_spawnGroup.Add(_fillList[_selection]);
			_fillList.RemoveAt(_selection);
		}
	}

	public void Pause(bool _pause=true) {
		m_active=!_pause;
	}
	public float GetLevelTimer() {
		//Returns the time left in the game.
		return mp_levelTimer;
	}
}
