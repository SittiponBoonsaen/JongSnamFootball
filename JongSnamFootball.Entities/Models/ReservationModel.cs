﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace JongSnamFootball.Entities.Models
{
    public class ReservationModel
    {
        [Column("id_reservation")]
        public int Id { get; set; }

        [Column("id_member_in_reservation")]
        public int IdMember { get; set; }

        [Column("id_field_in_reservation")]
        public int IdField { get; set; }

        [Column("startTime_reservation")]
        public DateTime StartTime { get; set; }

        [Column("stopTime_reservation")]
        public DateTime StopTime { get; set; }

        [Column("status_reservation")]
        public string Status { get; set; }

    }
}