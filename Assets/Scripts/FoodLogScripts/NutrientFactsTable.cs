using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Class or storing an item's nutrient data and updating the text in the log database scene.
public class NutrientFactsTable : MonoBehaviour {

	[System.Serializable]
	public struct NutrientFactsData
	{
		public int Calories;
		public float fats;
		public float SaturatedFats;
		public float TransFats;
		public float Carbohydrates;
		public float Fibre;
		public float Sugar;
		public float Protein;
		public float Cholesterol;
		public float Sodium;
		public float Potassium;
		public float Calcium;
		public float Iron;
	}


	public Text CaloriesText;
	public Text SaturatedFatsText;
	public Text TransFatsText;
	public Text CarbohydratesText;
	public Text FibreText;
	public Text SugarText;
	public Text ProteinText;
	public Text CholesterolText;
	public Text SodiumText;
	public Text PotassiumText;
	public Text CalciumText;
	public Text IronText;

	void Start()
	{
		
	}
	
	public void SetData(ref NutrientFactsData data)
	{
		//TODO: Set the various text elements based on the values provided by the item selected by the user. 
		//Wait on an artist to create an approriate asset.
		CaloriesText.text 		= "Calories " 		+ data.Calories;
		SaturatedFatsText.text 	= "SaturatedFats " 	+ data.SaturatedFats;
		TransFatsText.text 		= "TransFats " 		+ data.TransFats;
		CarbohydratesText.text 	= "Carbohydrates " 	+ data.Carbohydrates;
		FibreText.text 			= "Fibre " 			+ data.Fibre;
		SugarText.text 			= "Sugar " 			+ data.Sugar;
		ProteinText.text 		= "Protein "		+ data.Protein;
		CholesterolText.text	= "Choleterol " 	+ data.Cholesterol;
		SodiumText.text 		= "Sodium " 		+ data.Sodium;
		PotassiumText.text 		= "Potassium " 		+ data.Potassium;
		CalciumText.text 		= "Calcium " 		+ data.Calories;
		IronText.text 			= "Iron " 			+ data.Iron;
	}
}
