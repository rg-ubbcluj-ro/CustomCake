namespace CustomCakeApi.DTOs{
    public class IngredientDTO {
        public long Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public long CakeId { get; set; }
    }
}