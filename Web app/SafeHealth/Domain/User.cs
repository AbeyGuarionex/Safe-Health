using System;
using System.Collections.Generic;

namespace SafeHealth.Domain
{
    public partial class User
    {
        public User()
        {
            Documents = new HashSet<Document>();
        }

        public string UserCodePk1 { get; set; } = null!;
        public string UserEmailPk2 { get; set; } = null!;
        public string UserPassword { get; set; } = null!;
        public string TelephoneNumber { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string PaternalLastName { get; set; } = null!;
        public string? MaternalLastName { get; set; }
        public string UserOfficeCodeFk { get; set; } = null!;
        public string UserType { get; set; } = null!;
        public string? DoctorLicenseNumber { get; set; }
        public string? DoctorLicenseTitle { get; set; }
        public string? PatientMedicalInsuranceCodeFk { get; set; }
        public string? PolicyNumber { get; set; }

        public virtual MedicalInsurance? PatientMedicalInsuranceCodeFkNavigation { get; set; }
        public virtual Office UserOfficeCodeFkNavigation { get; set; } = null!;
        public virtual ICollection<Document> Documents { get; set; }
    }
}
