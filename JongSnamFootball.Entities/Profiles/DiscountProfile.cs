﻿using AutoMapper;
using JongSnamFootball.Entities.Models;
using JongSnamFootball.Entities.Request;

namespace JongSnamFootball.Entities.Profiles
{
    public class DiscountProfile : Profile
    {
        public DiscountProfile()
        {
            CreateMap<DiscountModel, FieldRequest>();

            CreateMap<DiscountRequest, DiscountModel>();
        }
    }
}
