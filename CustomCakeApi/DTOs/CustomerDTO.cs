namespace CustomCakeApi.DTOs{
   public class CustomerDTO 
   {
        public long Id { get; set; }
        public string Name { get; set; }
        public string? Adress { get; set; }
        public float Budget { get; set; }
        public ICollection<CakeDTO> Cakes  { get; set;} 
    }
}