using MediCare.Models;
using Microsoft.EntityFrameworkCore;

namespace MediCare.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Receptionist> Receptionists { get; set; }
        public DbSet<Billing> Billings { get; set; }
        public DbSet<PatientNote> PatientNotes { get; set; }
        public DbSet<Specialization> Specializations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            base.OnModelCreating(modelBuilder);

            // Seed Roles
            modelBuilder.Entity<Role>().HasData(SeedingData.GetRoles());

            // Seed Users
            modelBuilder.Entity<User>().HasData(SeedingData.GetUsers());

            // Seed Specializations
            modelBuilder.Entity<Specialization>().HasData(SeedingData.GetSpecializations());

            // Seed Doctors
            modelBuilder.Entity<Doctor>().HasData(SeedingData.GetDoctors());

            // Seed Receptionists
            modelBuilder.Entity<Receptionist>().HasData(SeedingData.GetReceptionists());

            // Seed Patients
            modelBuilder.Entity<Patient>().HasData(SeedingData.GetPatients());

            // Seed Appointments
            modelBuilder.Entity<Appointment>().HasData(SeedingData.GetAppointments());

            // Seed PatientNotes
            modelBuilder.Entity<PatientNote>().HasData(SeedingData.GetPatientNotes());

            // Seed Billings
            modelBuilder.Entity<Billing>().HasData(SeedingData.GetBillings());
        }

        public static class SeedingData
        {
            public static List<Role> GetRoles()
            {
                return new List<Role>
                {
                    new Role 
                    { 
                        RoleId = 1, 
                        RoleName = "Admin" 
                    },
                    new Role 
                    { 
                        RoleId = 2, 
                        RoleName = "Doctor" 
                    },
                    new Role 
                    { 
                        RoleId = 3, 
                        RoleName = "Receptionist" 
                    },
                    new Role 
                    { 
                        RoleId = 4, 
                        RoleName = "Patient" 
                    }
                };
            }

            public static List<User> GetUsers()
            {
                return new List<User>
                {
                    new User
                    {
                        UserId = 1, 
                        FirstName = "Admin", 
                        LastName = "User", 
                        Email = "admin@medicare.com",
                        DateOfBirth = new DateTime(1990, 1, 1), 
                        DateOfJoining = new DateTime(2023, 1, 1),
                        MobileNo = "1234567890", 
                        EmergencyNo = "0987654321",
                        Password = "password", 
                        RoleId = 1,
                        Active = true, 
                        CreatedBy = "Seed", 
                        CreatedAt = new DateTime(2023, 1, 1)
                    },
                    new User
                    {
                        UserId = 2, 
                        FirstName = "Doctor", 
                        LastName = "One", 
                        Email = "doctor1@medicare.com",
                        DateOfBirth = new DateTime(1985, 5, 15), 
                        DateOfJoining = new DateTime(2023, 2, 1),
                        MobileNo = "9876543210", 
                        EmergencyNo = "1029384756", 
                        Password = "password", 
                        RoleId = 2,
                        Active = true, 
                        CreatedBy = "Seed", 
                        CreatedAt = new DateTime(2023, 1, 1)
                    },
                    new User
                    {
                        UserId = 3, 
                        FirstName = "Receptionist", 
                        LastName = "One", 
                        Email = "receptionist1@medicare.com",
                        DateOfBirth = new DateTime(1992, 8, 20), 
                        DateOfJoining = new DateTime(2023, 3, 1),
                        MobileNo = "5551234567", 
                        EmergencyNo = "7776543210", 
                        Password = "password", 
                        RoleId = 3,
                        Active = true, 
                        CreatedBy = "Seed", 
                        CreatedAt = new DateTime(2023, 1, 1)
                    },
                    new User
                    {
                        UserId = 4, 
                        FirstName = "Patient", 
                        LastName = "One", 
                        Email = "patient1@medicare.com",
                        DateOfBirth = new DateTime(1980, 10, 10), 
                        DateOfJoining = new DateTime(2023, 4, 1),
                        MobileNo = "1112223333", 
                        EmergencyNo = "4445556666", 
                        Password = "password", 
                        RoleId = 4,
                        Active = true, 
                        CreatedBy = "Seed", 
                        CreatedAt = new DateTime(2023, 1, 1)
                    },
                    new User
                    {
                        UserId = 5, 
                        FirstName = "Doctor", 
                        LastName = "Two", 
                        Email = "doctor2@medicare.com",
                        DateOfBirth = new DateTime(1978, 03, 22), 
                        DateOfJoining = new DateTime(2023, 05, 01),
                        MobileNo = "1234567899", 
                        EmergencyNo = "9876543211", 
                        Password = "password", 
                        RoleId = 2,
                        Active = true, 
                        CreatedBy = "Seed", 
                        CreatedAt = new DateTime(2023, 1, 1)
                    },
                    new User
                    {
                        UserId = 6, 
                        FirstName = "Patient", 
                        LastName = "Two", 
                        Email = "patient2@medicare.com",
                        DateOfBirth = new DateTime(1995, 06, 30), 
                        DateOfJoining = new DateTime(2023, 06, 01),
                        MobileNo = "1234567898", 
                        EmergencyNo = "9876543212", 
                        Password = "password", 
                        RoleId = 4,
                        Active = true, 
                        CreatedBy = "Seed", 
                        CreatedAt = new DateTime(2023, 1, 1)
                    }
                };
            }

            public static List<Specialization> GetSpecializations()
            {
                return new List<Specialization>
                {
                    new Specialization 
                    { 
                        SpecializationId = 1, 
                        SpecializationName = "Cardiology" 
                    },
                    new Specialization 
                    { 
                        SpecializationId = 2, 
                        SpecializationName = "Dermatology" 
                    },
                    new Specialization 
                    { 
                        SpecializationId = 3, 
                        SpecializationName = "Orthopedics" 
                    }
                };
            }

            public static List<Doctor> GetDoctors()
            {
                return new List<Doctor>
                {
                    new Doctor 
                    { 
                        DoctorId = 1, 
                        UserId = 2, 
                        SpecializationId = 1, 
                        Qualification = "MD Cardiology", 
                        LicenseNumber = "LC123", 
                        CreatedAt = new DateTime(2023, 2, 1) 
                    },
                    new Doctor 
                    { 
                        DoctorId = 2, 
                        UserId = 5, 
                        SpecializationId = 2, 
                        Qualification = "MD Dermatology", 
                        LicenseNumber = "LC456", 
                        CreatedAt = new DateTime(2023, 5, 1) 
                    }
                };
            }

            public static List<Receptionist> GetReceptionists()
            {
                return new List<Receptionist>
                {
                    new Receptionist 
                    { 
                        ReceptionistId = 1, 
                        UserId = 3, 
                        Qualification = "Bachelor's Degree", 
                        CreatedAt = new DateTime(2023, 3, 1) 
                    }
                };
            }

            public static List<Patient> GetPatients()
            {
                return new List<Patient>
                {
                    new Patient
                    {
                        PatientId = 1, 
                        FirstName = "Patient", 
                        LastName = "One", 
                        DateOfBirth = new DateTime(1980, 10, 10),
                        Gender = "Male", 
                        AadharNo = "123456789012", 
                        Address = "123 Main St", 
                        City = "Anytown",
                        MobileNo = "1112223333", 
                        Email = "patient1@medicare.com", 
                        Active = true, 
                        CreatedBy = "Seed",
                        CreatedAt = new DateTime(2023, 4, 1)
                    },
                    new Patient
                    {
                        PatientId = 2, 
                        FirstName = "Patient", 
                        LastName = "Two", 
                        DateOfBirth = new DateTime(1995, 06, 30),
                        Gender = "Female", 
                        AadharNo = "987654321098", 
                        Address = "456 Oak Ave", 
                        City = "Anycity",
                        MobileNo = "1234567898", 
                        Email = "patient2@medicare.com", 
                        Active = true, 
                        CreatedBy = "Seed",
                        CreatedAt = new DateTime(2023, 6, 1)
                    }
                };
            }

            public static List<Appointment> GetAppointments()
            {
                return new List<Appointment>
            {
                new Appointment
                {
                    AppointmentId = 1, 
                    PatientId = 1, 
                    DoctorId = 1, 
                    ReceptionistId = 1,
                    AppointmentStarts = new DateTime(2023, 7, 10, 10, 0, 0),
                    AppointmentEnds = new DateTime(2023, 7, 10, 11, 0, 0),
                    Status = "Scheduled", 
                    AppointmentDescription = "Regular checkup", 
                    CreatedBy = "Seed",
                    CreatedAt = new DateTime(2023, 7, 1)
                },
                new Appointment
                {
                    AppointmentId = 2, 
                    PatientId = 2, 
                    DoctorId = 2, 
                    ReceptionistId = 1,
                    AppointmentStarts = new DateTime(2023, 7, 11, 14, 0, 0),
                    AppointmentEnds = new DateTime(2023, 7, 11, 15, 0, 0),
                    Status = "Scheduled", 
                    AppointmentDescription = "Skin consultation", 
                    CreatedBy = "Seed",
                    CreatedAt = new DateTime(2023, 7, 1)
                }
            };
            }

            public static List<PatientNote> GetPatientNotes()
            {
                return new List<PatientNote>
                {
                    new PatientNote
                    {
                        PatientNoteId = 1, 
                        AppointmentId = 1, 
                        PatientId = 1,
                        NoteText = "Patient reported mild chest pain. Recommended ECG.",
                        CreatedBy = "Doctor1", 
                        CreatedAt = new DateTime(2023, 7, 10, 11, 0, 0)
                    },
                    new PatientNote
                    {
                        PatientNoteId = 2, 
                        AppointmentId = 2, 
                        PatientId = 2,
                        NoteText = "Patient has a rash on their arms. Prescribed topical cream.",
                        CreatedBy = "Doctor2", 
                        CreatedAt = new DateTime(2023, 7, 11, 15, 0, 0)
                    }
                };
            }

            public static List<Billing> GetBillings()
            {
                return new List<Billing>
                {
                    new Billing
                    {
                        BillingId = 1,
                        AppointmentId = 1,
                        PatientId = 1,
                        Amount = 150.00m,
                        BillingDate = new DateTime(2023, 7, 10),
                        PaymentStatus = "Paid" // Added PaymentStatus
                    },
                    new Billing
                    {
                        BillingId = 2,
                        AppointmentId = 2,
                        PatientId = 2,
                        Amount = 100.00m,
                        BillingDate = new DateTime(2023, 7, 11),
                        PaymentStatus = "Pending" // Added PaymentStatus
                    },
                    new Billing
                    {
                        BillingId = 3,
                        AppointmentId = 1,
                        PatientId = 1,
                        Amount = 50.00m,
                        BillingDate = new DateTime(2023, 7, 10),
                        PaymentStatus = "Paid" // Added PaymentStatus
                    }
                };
            }

        }
    }
}
