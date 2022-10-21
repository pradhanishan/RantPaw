using AutoMapper;
using RantPaw.Models.DTOS.PostDTOS;
using RantPaw.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RantPaw.DataContext.Mapper
{
    public class PostProfile : Profile
    {

        public PostProfile()
        {
            CreateMap<Post, CreatePostDTO>().ReverseMap();
        }

    }
}
