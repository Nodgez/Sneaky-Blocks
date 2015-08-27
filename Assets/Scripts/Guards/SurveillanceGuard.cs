using UnityEngine;
using System.Collections;

public class SurveillanceGuard : BaseGuard {

	public SurveyInfo[] surveys;

	private int currentSurvey = 0;
	private float slerpValue;
	private float surveyTimer;
	private Quaternion snapshotRotation;
	private bool rotationSnapped;

	public override void Seek ()
	{
		if (currentSurvey >= surveys.Length) {
			currentSurvey = 0;
		}
		
		if (!rotationSnapped) {
			snapshotRotation = transform.rotation;
			rotationSnapped = true;
		}
		
		SurveyInfo survey = surveys [currentSurvey];
		if(surveyTimer < survey.surveyTime)
		{
			slerpValue += Time.deltaTime / (survey.surveyTime * 0.5f);
			slerpValue = Mathf.Clamp01(slerpValue);
			surveyTimer += Time.deltaTime;
			transform.rotation = Quaternion.Slerp(snapshotRotation,
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
	}

	public override bool DetectUnit (Vector3 position)
	{
		Vector3 directionToPlayer = position - transform.position;
		float distanceFromUnit = Vector3.Distance (position, transform.position);
		directionToPlayer = directionToPlayer.normalized;
		float dotProduct = 0;
		dotProduct = Vector3.Dot(directionToPlayer,transform.right);
		RaycastHit hit;
		Debug.DrawRay (transform.position, transform.right * detectionRadius, Color.red);
		
		if(Physics.Linecast(transform.position,position,out hit))
		{
			Debug.DrawLine(transform.position,position,Color.yellow);
			if(hit.collider.name == targetName)
			{
				if(distanceFromUnit < detectionRadius && dotProduct > 0.75f)
				{
					return true;
				}
			}
		}
		return false;
	}
}
