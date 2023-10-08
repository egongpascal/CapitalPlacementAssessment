namespace CapitalPlacementAssessment.Domain
{
    public class ResponseClass<T>
    {
        public bool Sucesss { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
