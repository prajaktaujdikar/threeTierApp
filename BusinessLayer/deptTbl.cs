using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
namespace BusinessLayer
{
    public class deptTbl
    {
        public int Id { get; set; }
        public string  deptName{ get; set; }

        public List<deptTbl> getAll()
        {
            List<deptTbl> deptlist = new List<deptTbl>();
            using (empDBEntities dept= new empDBEntities())
            {

                deptlist = dept.depts.Select(x => new deptTbl
                {
                    Id = x.Id,
                    deptName = x.deptName
                }).ToList();

                //dept.depts.Select(x => new { Id=x.Id,deptName=x.deptName });


                return deptlist;
            }
        }
    }
}
