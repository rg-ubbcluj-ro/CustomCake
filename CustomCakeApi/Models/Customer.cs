namespace CustomCakeApi.Models{
    public class Customer {
        public long Id { get; set; }
        public string Name { get; set; }
        public string? Adress { get; set; }
        public float Budget { get; set; }
        public ICollection<Cake> Cakes  { get; set; }

        public Customer(){
            Cakes = new List<Cake>();
        }
    }
}