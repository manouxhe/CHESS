using System; //pr utiliser Guid et Datetime
using System.Collections.Generic; //pr ut List<Player> et List<Match>

namespace ChessApp.Models;

public class Competition
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; }
    public DateTime Date { get; set; }
    public string Location { get; set; }

    public List<Player> Players { get; set; } = new List<Player>();   //Dès quon crée une nouvelle compétition on a doffice liste player /match/id
    public List<Match> Matches { get; set; } = new List<Match>();

    public Competition(string name, DateTime date, string location)  //constructeur 
    {
        Name = name;
        Date = date;
        Location = location;
    }
}
