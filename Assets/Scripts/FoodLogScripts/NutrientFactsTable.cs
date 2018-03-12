using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Class or storing an item's nutrient data and updating the text in the log database scene.
public class NutrientFactsTable : MonoBehaviour {


	public enum ServingType
	{
		g,
		mg,
		mL
	}

	[System.Serializable]
	public struct NutrientFactsData
	{
		public int ServingSizeE;
		public string ServingTypeE;
		public int ServingSizeM;
		public ServingType servingType;
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


	public Text ServingSizeE; public Text ServingSizeE1;
	public Text ServingSizeM; public Text ServingSizeM1;
	public Text CaloriesText;
	public Text CaloriesPDVText; 			int caloriesDV = 2000;
	public Text FatsText;
	public Text FatsPDVText; 				int fatsDV = 65; //g
	public Text SaturatedFatsText;
	public Text TransFatsText;
	public Text SaturatedTransFatsPDVText; 	int stFatsDV = 20; //g
	public Text CarbohydratesText;
	public Text CarbohydratesPDVText; 		int CarbohydratesDV = 300; //mg
	public Text FibreText;
	public Text FibrePDVText; 				int fibreDV = 25; //g
	public Text SugarText;
	public Text SugarPDVText; 				int sugarDV = 50; //g? no DV says Canada.ca
	public Text ProteinText;
	public Text CholesterolText;
	public Text CholesterolPDVText; 		int cholesterolDV = 300; //mg
	public Text SodiumText;
	public Text SodiumPDVText; 				int sodiumDV = 2400; //mg
	public Text PotassiumText;
	public Text PotassiumPDVText;			int PotassiumDV = 4500; //mg
	public Text CalciumText;
	public Text CalciumPDVText; 			int calciumDV = 1100; //mg
	public Text IronText;
	public Text IronPDVText; 				int ironDV = 14; //mg;

	void Start()
	{
		
	}
	
	public void SetData(ref NutrientFactsData data)
	{
		ServingSizeE.text 				= data.ServingSizeE.ToString("0");
		ServingSizeE1.text 				= data.ServingSizeE.ToString("0");
		ServingSizeM.text 				= data.ServingSizeM.ToString("0");
		ServingSizeM1.text 				= data.ServingSizeM.ToString("0");
		CaloriesText.text 				= data.Calories.ToString("0");
		FatsText.text 					= data.fats.ToString("0");
		FatsPDVText.text 				= ((data.fats / fatsDV) * 100).ToString("0");
		SaturatedFatsText.text 			= data.SaturatedFats.ToString("0");
		TransFatsText.text 				= data.TransFats.ToString("0");
		SaturatedTransFatsPDVText.text	= (((data.SaturatedFats + data.TransFats) / stFatsDV) * 100).ToString("0");
		CarbohydratesText.text 			= data.Carbohydrates.ToString("0");
		FibreText.text 					= data.Fibre.ToString("0");
		SugarText.text 					= data.Sugar.ToString("0");
		SugarPDVText.text 				= ((data.Sugar / sugarDV) * 100).ToString("0");
		ProteinText.text 				= data.Protein.ToString("0");
		CholesterolText.text			= data.Cholesterol.ToString("0");
		SodiumText.text 				= data.Sodium.ToString("0");
		SodiumPDVText.text 				= ((data.Sodium / sodiumDV) * 100).ToString("0");
		PotassiumText.text 				= data.Potassium.ToString("0");
		PotassiumPDVText.text			= ((data.Potassium / PotassiumDV) * 100).ToString("0");
		CalciumText.text 				= data.Calories.ToString("0");
		CalciumPDVText.text 			= ((data.Calcium / calciumDV) * 100).ToString("0");
		IronText.text 					= data.Iron.ToString("0");
		IronPDVText.text				= ((data.Iron / ironDV) * 100).ToString("0");
	}
}
