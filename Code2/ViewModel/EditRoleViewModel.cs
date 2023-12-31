﻿using System.ComponentModel.DataAnnotations;

namespace Code2.ViewModel
{
    public class EditRoleViewModel
    {
        public string Id { get; set; }
        [Display(Name = "Role")]
        [Required(ErrorMessage = "Role name is required")]
        public string RoleName { get; set; }
        public List<string> Users { get; set; } = new();

    }
}
