namespace CustomCakeApi.Models{
    public class Ingredient {
        public long Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public long CakeId { get; set; }
    }
}