using CustomCakeApi.DTOs;
using CustomCakeApi.Models;

namespace CustomCakeApi.Mappers;
public static class CakeMappers
{
    public static CakeDTO ItemToDTO(Cake cake) =>
        new()
        {
            Id = cake.Id,
            Name = cake.Name,
            Weight = cake.Weight,
            Ingredients = cake.Ingredients.Select(si => IngredientMappers.ItemToDTO(si)).ToList(),
            CustomerId = cake.CustomerId
        };

    public static Cake DTOToItem(CakeDTO cakeDTO) =>
        new Cake
        {
             Id = cakeDTO.Id,
            Name = cakeDTO.Name,
            Weight = cakeDTO.Weight,
            Ingredients = cakeDTO.Ingredients.Select(ItemDto => IngredientMappers
            .DTOToItem(ItemDto)).ToList(),
            CustomerId = cakeDTO.CustomerId
        };
}