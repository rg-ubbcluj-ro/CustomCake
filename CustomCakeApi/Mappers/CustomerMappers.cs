using CustomCakeApi.DTOs;
using CustomCakeApi.Models;

namespace CustomCakeApi.Mappers;
public static class CustomerMappers
{
    public static CustomerDTO ItemToDTO(Customer customer) =>
        new()
        {
            Id = customer.Id,
            Name = customer.Name,
            Adress = customer.Adress,
            Budget = customer.Budget,
            Cakes = customer.Cakes.Select(si => CakeMappers.ItemToDTO(si))
            .ToList()
        };

    public static Customer DTOToItem(CustomerDTO customerDTO) =>
        new Customer
        {
             Id = customerDTO.Id,
            Name = customerDTO.Name,
            Adress = customerDTO.Adress,
            Budget = customerDTO.Budget,
            Cakes = customerDTO.Cakes?.Select(ItemDto => CakeMappers
            .DTOToItem(ItemDto)).ToList(),
        };
}