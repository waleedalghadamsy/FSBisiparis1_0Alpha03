using System;
using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BisaprişÇekirdek.Valıklar.Esansiyel
{
    public class İşyeri : TemelVarlık, IEquatable<İşyeri>
    {
        #region Data Members
        //protected string name; 
        //protected LocationAddress locationAddress;
        //protected Communication contactDetails;
        //protected List<ContactPerson> contactPersons;
        #endregion

        #region Constructors
        public İşyeri(string isim)
        {
            İsim = isim;
            İletişim = new İşyeriİletişim();
        }
        #endregion

        #region Properties
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string İsim { get; set; }
        //public CommercialActivity Activity { get; set; }
        public İşyeriİletişim İletişim { get; set; }
        public bool ÖzelSektörMü { get; set; }
        #endregion

        #region Methods
        //public void Method()
        //{
        //}
        #endregion

        #region IEquatable<Company> Members

        public bool Equals(İşyeri diğer)
        {
            if (!string.IsNullOrWhiteSpace(İsim))
                return string.Compare(İsim, diğer.İsim, true) == 0;
            else
                return false;
        }

        #endregion
    }
}
