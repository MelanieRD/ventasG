using System.ComponentModel.DataAnnotations;

namespace ventasG.Models
{
    public class employee
    {
        public int Id { get; set; }
 
        public string FullName { get; set; }
        

        public int Companyid { get; set; }

        public virtual Company Company { get; set; }


    }
}