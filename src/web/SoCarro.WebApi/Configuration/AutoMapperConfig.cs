using AutoMapper;
using SoCarro.Business.Models;
using SoCarro.WebApi.ViewModels;

namespace SoCarro.WebApi.Configuration;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<Marca, MarcaViewModel>().ReverseMap();
        CreateMap<Endereco, EnderecoViewModel>().ReverseMap();
        CreateMap<Modelo, ModeloViewModel>().ReverseMap();

        CreateMap<ProprietarioViewModel, Proprietario>();
        CreateMap<Proprietario, ProprietarioViewModel>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Endereco));

        CreateMap<VeiculoViewModel, Veiculo>();
        CreateMap<Veiculo, VeiculoViewModel>()
            .ForMember(dest => dest.Renavam, opt => opt.MapFrom(src => src.Renavam));
    }
}
