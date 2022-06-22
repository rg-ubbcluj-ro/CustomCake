namespace CustomCakeApi.Models{
    public class Cake {
        public long Id { get; set; }
        public string Name { get; set; }
        public float Weight { get; set; }
        public ICollection<Ingredient> Ingredients{ get; set; }
        public long CustomerId {get;set;}
        
        public float getPrice(){
            var sum = 0f; 
            foreach(Ingredient ingredient in Ingredients){
               sum += ingredient.Price;
            }
            return sum;
        }
    }
}