namespace ChromeProtocol.Tools.CodeGeneration.Emitting;

public struct EmissionState
{
  public int Level;

  public EmissionState(int level)
  {
    Level = level;
  }

  public EmissionState WithIncreasedLevel(int levelIncrement = 1)
  {
    return new EmissionState(Level + levelIncrement);
  }
}
