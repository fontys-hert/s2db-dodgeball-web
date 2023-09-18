namespace S2DodgeBall.Web.Models;

public class BallIndexViewModel
{
    public int HealthPoints { get; set; } = 5;
    public string? Direction { get; set; }

    public bool GotHit { get; set; }
    public bool DidDodge { get; set; }
}