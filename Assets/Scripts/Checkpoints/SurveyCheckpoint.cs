using UnityEngine;
using System.Collections;

public class SurveyCheckpoint : BaseCheckpoint{
	
	public SurveyInfo[] surveys;
	private int currentSurvey = 0;
	private float surveyTimer = 0;
	private Quaternion sanpshotRotation;
	private bool rotationSnapped = false;
	private float slerpValue;

	public override bool ApplyCheckpointAction (Transform transform)
	{
		transform.position = this.Position;

		if (currentSurvey >= surveys.Length) {
			currentSurvey = 0;
			return true;
		}

		if (!rotationSnapped) {
			sanpshotRotation = transform.rotation;
			rotationSnapped = true;
		}

		SurveyInfo survey = surveys [currentSurvey];
		if(surveyTimer < survey.surveyTime)
		{
			slerpValue += Time.deltaTime / (survey.surveyTime * 0.5f);
			slerpValue = Mathf.Clamp01(slerpValue);
			surveyTimer += Time.deltaTime;
			transform.rotation = Quaternion.Slerp(sanpshotRotation,
			                                      Quaternion.Euler(surveys[currentSurvey].surveyEuler),
			                                      slerpValue);

		}
		else
		{
			rotationSnapped = false;
			surveyTimer = 0;
			currentSurvey ++;
			slerpValue = 0;
		}

		return false;
	}
}

[System.Serializable]
public struct SurveyInfo
{
	public float surveyTime;
	public Vector3 surveyEuler;
}