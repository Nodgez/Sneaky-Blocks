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
		//Ensure transform is directly at the checkpooint position
		transform.position = this.Position;

		if (currentSurvey >= surveys.Length) {
			currentSurvey = 0;
			return true;
		}

		//take a snapshot of the current rotation for slerping
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

/// <summary>
/// A struct of informtation for surveillance
/// </summary>
[System.Serializable]
public struct SurveyInfo
{
	public float surveyTime;
	public Vector3 surveyEuler;
}