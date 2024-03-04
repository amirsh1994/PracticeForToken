using AutoMapper;

namespace PracticeForToken.Profiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<Eshop.DomainModel.Models.User, PracticeForToken.Dto.LoginDto>().ReverseMap();
        }
    }
}
