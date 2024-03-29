﻿namespace OrderUp_API.Models {
    [Microsoft.EntityFrameworkCore.Index(nameof(Code), IsUnique = true)]
    public class VerificationCode : AbstractEntity {

        [MaxLength(100)]
        public string Code { get; set; }

        [Required]
        [MaxLength(100)]
        public Guid UserID { get; set; }

        public DateTime ExpiryDate { get; set; }

        [Required]
        [MaxLength(15)]
        public string UserType { get; set; }
    }
}
