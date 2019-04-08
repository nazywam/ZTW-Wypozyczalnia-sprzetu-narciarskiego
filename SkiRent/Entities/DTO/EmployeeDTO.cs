﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SkiRent.Extensions;

namespace SkiRent.Entities.DTO
{
    public class EmployeeDTO
    {
        public int ID { get; set; }

        [Required]
        [StringLength(30)]
		[DisplayName("Imię")]
		public string FirstName { get; set; }
		[Required]
		[StringLength(30)]
		[DisplayName("Nazwisko")]
		public string LastName { get; set; }
		[Required]
		[StringLength(30)]
		[DisplayName("Adres")]
		public string Address { get; set; }
		[DisplayName("Telefon")]
		[DataType(DataType.PhoneNumber)]
		public int PhoneNumber { get; set; }
		[DisplayName("Pensja")]
		[DataType(DataType.Currency)]
		public decimal? Salary { get; set; }
        [Required]
        [StringLength(30)]
        [DisplayName("Login")]
		public string Login { get; set; }
		[IfCreateRequired]
		[StringLength(30)]
		[DisplayName("Hasło")]
		public string Password { get; set; }
		public int PermissionLevel { get; set; }
		[DisplayName("Administrator")]
		public bool IsAdmin
		{
			get { return PermissionLevel == 1; }
			set { PermissionLevel = value ? 1 : 0; }
		}
		public DateTime EmploymentDate { get; set; }
        public virtual HashSet<OrderDTO> Orders { get; set; }
    }
}