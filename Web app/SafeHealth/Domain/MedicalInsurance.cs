using System;
using System.Collections.Generic;

namespace SafeHealth.Domain
{
    public partial class MedicalInsurance
    {
        public MedicalInsurance()
        {
            Users = new HashSet<User>();
            OfficeCodeFk1s = new HashSet<Office>();
        }

        public string MedicalInsuranceCodePk { get; set; } = null!;
        public string Name { get; set; } = null!;

        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<Office> OfficeCodeFk1s { get; set; }
    }
}
