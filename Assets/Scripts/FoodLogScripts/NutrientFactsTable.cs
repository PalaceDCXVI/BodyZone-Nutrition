using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NutrientFactsTable : MonoBehaviour {

	[System.Serializable]
	public struct NutrientFactsData
	{
		public int Calories;
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
	
	public void SetData(ref NutrientFactsData data)
	{
		//TODO: Set the various text elements based on the values provided by the item selected by the user. 
		//Wait on an artist to create an approriate asset.
	}
}
