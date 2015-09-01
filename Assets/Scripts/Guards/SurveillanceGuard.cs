using UnityEngine;
using System.Collections;

public class SurveillanceGuard : BaseGuard {

	public SurveyInfo[] surveys;

	private int _currentSurvey = 0;
	private float _slerpValue;
	private float _surveyTimer;
	private Quaternion _snapshotRotation;
	private bool _rotationSnapped;

	//Seeks for Player based on survey info i.e. rotations
	public override void Seek ()
	{
		if (_currentSurvey >= surveys.Length) {
			_currentSurvey = 0;
		}
		
		if (!_rotationSnapped) {
			_snapshotRotation = transform.rotation;
			_rotationSnapped = true;
		}
		
		SurveyInfo survey = surveys [_currentSurvey];
		if(_surveyTimer < survey.surveyTime)
		{
			_slerpValue += Time.deltaTime / (survey.surveyTime * 0.5f);
			_slerpValue = Mathf.Clamp01(_slerpValue);
			_surveyTimer += Time.deltaTime;
			transform.rotation = Quaternion.Slerp(_snapshotRotation,
			                                      Quaternion.Euler(surveys[_currentSurvey].surveyEuler),
			                                      _slerpValue);
			
		}
		else
		{
			_rotationSnapped = false;
			_surveyTimer = 0;
			_currentSurvey ++;
			_slerpValue = 0;
		}
	}

	//Detect unit using line casting and view angle
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
