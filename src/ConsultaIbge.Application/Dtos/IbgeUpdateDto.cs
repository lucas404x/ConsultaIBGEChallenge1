namespace ConsultaIbge.Application.Dtos;

public class IbgeUpdateDto
{
    public IbgeUpdateDto(string id, string state, string city)
    {
        Id = id;
        State = state;
        City = city;
    }

    public string Id { get; set; }
    public string State { get; set; }
    public string City { get; set; }
}
