﻿namespace Application.DTOs
{
    public class ResponseBooking
    {
        public string Id { get; set; } = default!;
        public string UserFullName { get; set; } = default!;
        public string UserEmail { get; set; } = default!;
        public string UserPhoneNumber { get; set; } = default!;
        public string? InterestedCampus { get; set; }
        public string? InterestedAcademicField { get; set; }
        public string? InterestedSpecialization { get; set; }
        public string? InterestedCourse { get; set; }
    }
}
