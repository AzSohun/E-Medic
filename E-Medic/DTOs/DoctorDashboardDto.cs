namespace E_Medic.DTOs
{
    public class DoctorDashboardDto
    {
        public int TodaysAppointmentsCount { get; set; }
        public int TotalAppointmentsCount { get; set; }
        public decimal TotalEarnings { get; set; }
        public IEnumerable<AppointmentDto> TodaysQueue { get; set; } = new List<AppointmentDto>();
    }
}
}
