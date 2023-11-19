using Eu4ng.Framework.Game.ControllerSystem;

public class Character : Pawn
{
	bool _isOnPossessedCalled;
	bool _isOnUnPossessedCalled;

	public bool IsOnPossessedCalled => _isOnPossessedCalled;
	public bool IsOnUnPossessedCalled => _isOnUnPossessedCalled;

	protected override void OnPossessed(Controller newController)
	{
		_isOnPossessedCalled = true;
	}

	protected override void OnUnPossessed()
	{
		_isOnUnPossessedCalled = true;
	}
}
