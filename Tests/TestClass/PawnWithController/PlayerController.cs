using Eu4ng.Framework.Game.ControllerSystem;

public class PlayerController : Controller
{
	bool _isOnPossessCalled;
	bool _isOnUnPossessCalled;

	public bool IsOnPossessCalled => _isOnPossessCalled;
	public bool IsOnUnPossessCalled => _isOnUnPossessCalled;

	protected override void OnPossess(Pawn newPawn)
	{
		_isOnPossessCalled = true;
	}

	protected override void OnUnPossess()
	{
		_isOnUnPossessCalled = true;
	}
}
