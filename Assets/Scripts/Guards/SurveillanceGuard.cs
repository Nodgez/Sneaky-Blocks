using UnityEngine;
using System.Collections;

public class SurveillanceGuard : BaseGuard {

	public SurveyInfo[] surveys;
	public float surveyTimeScale = 0.5f;

	private int _currentSurvey = 0;
	private float _slerpValue;
	private float _surveyTimer;
	private Quaternion _snapshotRotation;
	private bool _rotationSnapped;

	//Seeks for Player based on survey info i.e. rotations
	public override void Seek ()
	{
		trigger_.IsTriggered = losDetector_.DetectTargets ();

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
			_slerpValue += Time.deltaTime / (survey.surveyTime * surveyTimeScale);
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
}
