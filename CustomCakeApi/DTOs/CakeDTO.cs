namespace CustomCakeApi.DTOs{
   public class CakeDTO {
        public long Id { get; set; }
        public string Name { get; set; }
        public float Weight { get; set; }
        
        public ICollection<IngredientDTO> Ingredients{ get; set; }
        public long CustomerId {get;set;}

        public CakeDTO(){
            Ingredients = new List<IngredientDTO>();
        }
    }
}