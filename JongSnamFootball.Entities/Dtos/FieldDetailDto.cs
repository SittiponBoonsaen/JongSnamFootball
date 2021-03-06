﻿using System.Collections.Generic;
using JongSnamFootball.Entities.Models;

namespace JongSnamFootball.Entities.Dtos
{
    public class FieldDetailDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Size { get; set; }

        public bool IsOpen { get; set; }

        public virtual DiscountModel DiscountModel { get; set; }

        public virtual ICollection<ImageFieldModel> ImageFieldModel { get; set; }

        public bool Active { get; set; }
    }
}
