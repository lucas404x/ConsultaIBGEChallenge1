namespace ConsultaIbge.Domain.Entities;

public class Ibge
{
    public Ibge(string id, string state, string city)
    {
        Id = id;
        State = state;
        City = city;
    }

    public string Id { get; private set; }
    public string State { get; private set; }
    public string City { get; private set; }
}
