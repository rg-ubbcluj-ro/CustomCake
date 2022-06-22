using CustomCakeApi.DTOs;
using CustomCakeApi.Models;

namespace CustomCakeApi.Mappers;

public static class IngredientMappers
{
    public static IngredientDTO ItemToDTO(Ingredient ingredient) =>
        new()
        {
            Id = ingredient.Id,
            Name = ingredient.Name,
            Price = ingredient.Price,
            CakeId = ingredient.CakeId
        };

    public static Ingredient DTOToItem(IngredientDTO ingredientDTO) =>
        new Ingredient
        {
             Id = ingredientDTO.Id,
            Name = ingredientDTO.Name,
            Price = ingredientDTO.Price,
            CakeId = ingredientDTO.CakeId
        };
}