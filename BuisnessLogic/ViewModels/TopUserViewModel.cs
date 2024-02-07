using BuisnessLogic.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuisnessLogic.ViewModels
{
    public class TopUserViewModel
    {
        public string Id { get; set; }
        public int Count { get; set; }
        public ApplicationUserDto User { get; set; }
    }
}
