using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrabajaYa.Models;
using TrabajaYaAPI.Data.Entities;

namespace TrabajaYaAPI.Data
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            this.CreateMap<UserEntity, UserModel>()
                .ReverseMap();

            this.CreateMap<PublishModel, PublishEntity>()
                .ForMember(des => des.User,  opt => opt.MapFrom(scr => new UserEntity { Id = scr.UserIde }))
                .ReverseMap()
                .ForMember(dest => dest.UserIde, opt => opt.MapFrom(scr => scr.User.Id));
        }
    }
}
