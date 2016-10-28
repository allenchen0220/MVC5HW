namespace CRMWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    [MetadataType(typeof(客戶聯絡人MetaData))]
    public partial class 客戶聯絡人 : IValidatableObject
    {
        private 客戶資料Entities db = new 客戶資料Entities();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
             
            //throw new NotImplementedException();
            if(db.客戶聯絡人.Where(s =>s.Email ==Email).FirstOrDefault() != null)
            {
                yield return new ValidationResult("同一個客戶下的聯絡人，其 Email 不能重複", new string[] { "Email" });
            }
        }
    }

    public partial class 客戶聯絡人MetaData
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int 客戶Id { get; set; }
        
        [StringLength(50, ErrorMessage="{0} 欄位長度必須為{2} ~ {1} 個字元!", MinimumLength =2)]
        [Required]
        public string 職稱 { get; set; }
        
        [StringLength(50, ErrorMessage = "{0} 欄位長度必須為{2} ~ {1} 個字元!", MinimumLength = 2)]
        [Required]
        public string 姓名 { get; set; }
        
        [StringLength(250, ErrorMessage = "{0} 欄位長度必須為{2} ~ {1} 個字元!", MinimumLength = 2)]
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email格式不正確.")]
        public string Email { get; set; }
        
        [StringLength(50, ErrorMessage = "{0} 欄位長度必須為{2} ~ {1} 個字元!", MinimumLength = 2)]
        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        [RegularExpression(@"\d{4}-\d{6}", ErrorMessage = "電話格式不正確 ( e.g. 0911-111111 ).")]
        public string 手機 { get; set; }
        
        [StringLength(50, ErrorMessage = "{0} 欄位長度必須為{2} ~ {1} 個字元!", MinimumLength = 2)]
        public string 電話 { get; set; }
        [Required]
        public bool 是否已刪除 { get; set; }
    
        public virtual 客戶資料 客戶資料 { get; set; }
    }
}
