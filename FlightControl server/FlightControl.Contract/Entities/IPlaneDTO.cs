namespace FlightControl.Contract.Entities
{
    public interface IPlaneDTO
    {
        int PlaneId { get; set; }
        string CompanyName { get; set; }
        int StationId { get; set; }
    }
}