using System;
using System.Collections.Generic;

namespace SafeHealth.Domain
{
    public partial class Office
    {
        public Office()
        {
            Users = new HashSet<User>();
            MedicalInsuranceCodeFk2s = new HashSet<MedicalInsurance>();
        }

        public string OfficeCodePk { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string TelephoneNumber { get; set; } = null!;
        public string AddressFirstLine { get; set; } = null!;
        public string? AddressSecondtLine { get; set; }
        public string City { get; set; } = null!;
        public string PostalCode { get; set; } = null!;

        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<MedicalInsurance> MedicalInsuranceCodeFk2s { get; set; }
    }
}
