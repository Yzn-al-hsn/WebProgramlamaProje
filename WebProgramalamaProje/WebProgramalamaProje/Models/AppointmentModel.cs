﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProgramalamaProje.Models
{
    public class AppointmentModel
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(ApplicationUser))]
        public string ClientId { get; set; }
        [ForeignKey(nameof(DoctorModel))]
        public int DoctorId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public ApplicationUser Client { get; set; }
        public DoctorModel Doctor { get; set; }
    }
}
