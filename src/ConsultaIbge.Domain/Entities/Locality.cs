namespace ConsultaIbge.Domain.Entities;

public class Locality
{
    public Locality(string id, string state, string city)
    {
        Id = id;
        State = state;
        City = city;
    }

    public string Id { get; private set; }
    public string State { get; private set; }
    public string City { get; private set; }
}
