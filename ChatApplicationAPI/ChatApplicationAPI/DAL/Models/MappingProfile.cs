using AutoMapper;
using DAL.ModelDTO;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Register, User>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
            CreateMap<MessageDTO, Message>();
        }
    }
}
