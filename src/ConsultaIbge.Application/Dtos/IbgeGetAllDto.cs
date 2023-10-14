namespace ConsultaIbge.Application.Dtos;

public class IbgeGetAllDto
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public string Query { get; set; } = null;
}
