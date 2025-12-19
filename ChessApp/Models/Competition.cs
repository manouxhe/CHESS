using System; //pr utiliser Guid et Datetime
using System.Collections.Generic; //pr ut List<Player> et List<Match>

namespace ChessApp.Models;

public class Competition
{
    public int Id { get; set; }

    public string? Name { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Location { get; set; }

    public List<Player> Players { get; set; } = new List<Player>();   //Dès quon crée une nouvelle compétition on a doffice liste player /match/id
    public List<Match> Matches { get; set; } = new List<Match>();

    // Constructeur vide nécessaire pour plus tard (BDD)
    public Competition() { }

    public Competition(int id, string name, DateTime? start, DateTime? end, string location)
    {
        Id = id;
        Name = name;
        StartDate = start;
        EndDate = end;
        Location = location;
    }
}
