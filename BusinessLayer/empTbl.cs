using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
namespace BusinessLayer
{
    public class empTbl
    {
        public int Id { get; set; }
        public string empName { get; set; }
        public int empAge { get; set; }
        public decimal empSalary { get; set; }
        public int deptId { get; set; }
        public string deptName { get; set; }


        public List<empTbl> getAll()
        {
            List<empTbl> emplist = new List<empTbl>();
            using (empDBEntities emp=new empDBEntities())
            {
                emplist = emp.EmpTbls.Select(x => new empTbl
                {
                    Id = x.Id,
                    empName = x.empName,
                    empAge = x.empAge,
                    empSalary = x.empSalary,
                    deptId = x.deptId,
                    deptName = x.dept.deptName
                }).ToList();

                
                return emplist;
            }
        }

        public void AddEmp(empTbl empData)
        {
            using (empDBEntities emp=new empDBEntities())
            {
                EmpTbl etbl = new EmpTbl();
                etbl.empName=empData.empName;
                etbl.empAge = empData.empAge;
                etbl.empSalary =Convert.ToDecimal(empData.empSalary);
                etbl.deptId = empData.deptId;

                emp.EmpTbls.Add(etbl);
                emp.SaveChanges();
            }
        }

        public void Delemp(empTbl empData)
        {
            using (empDBEntities emp=new empDBEntities())
            {
                EmpTbl empDel = emp.EmpTbls.Where(x=>x.Id==empData.Id).FirstOrDefault();
                emp.EmpTbls.Remove(empDel);
                emp.SaveChanges();
            }
        }

        public void UpdateEmp(empTbl empData)
        {
            using (empDBEntities emp=new empDBEntities())
            {

                EmpTbl etbl = emp.EmpTbls.Where(x=>x.Id==empData.Id).FirstOrDefault();
                etbl.empName = empData.empName;
                etbl.empAge = empData.empAge;
                etbl.empSalary = Convert.ToDecimal(empData.empSalary);
                etbl.deptId = empData.deptId;

                emp.SaveChanges();
            }
        }
    }

    
}
