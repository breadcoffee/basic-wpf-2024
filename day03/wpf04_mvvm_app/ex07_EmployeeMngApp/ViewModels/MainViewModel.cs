using Caliburn.Micro;
using ex07_EmployeeMngApp.Models;
using Microsoft.Data.SqlClient;
using System.Windows;
using System.Windows.Input;

namespace ex07_EmployeeMngApp.ViewModels
{
    public class MainViewModel : Conductor<object>
    {
        // 멤버변수
        private int id;
        private string empname;
        private decimal salary;
        private string deptname;
        private string addr;

        // List는 정적인 컬렉션, BindableCollection은 동적인 컬렉션
        // MVVM처럼 List를 사용못함
        private BindableCollection<Employees> listEmployees;
        private Employees selectedEmployee;

        #region "속성 프로퍼티"
        public Employees SelectedEmployee
        {
            get => selectedEmployee;
            set
            {
                selectedEmployee = value;
                
                // 데이터를 TextBox들에 전달
                if (selectedEmployee != null)
                {
                    Id = value.Id;
                    EmpName = value.EmpName;
                    Salary = value.Salary;
                    DeptName = value.DeptName;
                    Addr = value.Addr;

                    NotifyOfPropertyChange(() => SelectedEmployee); // View에 데이터가 표시되려면 필수!!
                    NotifyOfPropertyChange(() => Id);
                    NotifyOfPropertyChange(() => EmpName);
                    NotifyOfPropertyChange(() => Salary);
                    NotifyOfPropertyChange(() => DeptName);
                    NotifyOfPropertyChange(() => Addr);
                }
            }
        }
        
        public int Id { get => id; 
            set
            {
                id = value;
                NotifyOfPropertyChange(() => Id);
                NotifyOfPropertyChange(() => CanDelEmployee); // 삭제여부 속성도 변경했다고 공지
            }  
        }
        public string EmpName { get => empname;
            set
            {
                empname = value;
                NotifyOfPropertyChange(() => EmpName);
                NotifyOfPropertyChange(() => CanSaveEmployee);
            }
        }
        public decimal Salary { get => salary;
            set
            {
                salary = value;
                NotifyOfPropertyChange(() => Salary);
                NotifyOfPropertyChange(() => CanSaveEmployee);
            }
        }
        public string DeptName
        {
            get => deptname;
            set
            {
                deptname = value;
                NotifyOfPropertyChange(() => DeptName);
                NotifyOfPropertyChange(() => CanSaveEmployee);
            }
        }
        public string Addr
        {
            get => addr;
            set
            { 
                addr = value;
                NotifyOfPropertyChange(() => Addr);
            }
        }

        // DataGrid에 뿌릴 Employees 테이블 데이터
        public BindableCollection<Employees> ListEmployees
        {
            get => listEmployees;
            set
            {
                listEmployees = value;
                // 값이 변경된 것을 시스템에 알려줌
                NotifyOfPropertyChange(() => ListEmployees); // 필수
            }
        }
        #endregion

        public MainViewModel()
        {
            DisplayName = "직원관리 시스템";
            // 조회 실행
            GetEmployees();
        }

        /// <summary>
        /// Caliburn.Micro가 Xaml의 버튼 x:Name과 동일한 이름의 메서드로 매핑
        /// </summary>
        /// 

        // 저장 버튼 활성화 여부
        public bool CanSaveEmployee
        {
            get
            {
                if(string.IsNullOrEmpty(EmpName) || Salary == 0 || string.IsNullOrEmpty(DeptName))
                    return false;
                else
                    return true;
            }
        }
        public void SaveEmployee()
        {
            using(SqlConnection conn = new SqlConnection(Helpers.Common.CONNSTRING))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                
                if (Id == 0) // 새로 추가할 시
                    cmd.CommandText = Models.Employees.INSERTCT_QUERY;
                else // 수정할 시
                    cmd.CommandText = Models.Employees.UPDATE_QUERY;

                SqlParameter prmEmpName = new SqlParameter("@EmpName", EmpName);
                cmd.Parameters.Add(prmEmpName);
                SqlParameter prmSalary = new SqlParameter("@Salary", Salary);
                cmd.Parameters.Add(prmSalary);
                SqlParameter prmDeptName = new SqlParameter("@DeptName", DeptName);
                cmd.Parameters.Add(prmDeptName);
                SqlParameter prmAddr = new SqlParameter("@Addr", Addr ?? (object)DBNull.Value); // 주소가 빈값일때 컬럼에 null값 사용
                cmd.Parameters.Add(prmAddr);

                if (Id != 0)
                {
                    SqlParameter prmId = new SqlParameter("@Id", Id);
                    cmd.Parameters.Add(prmId);
                }

                var result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    MessageBox.Show("저장성공!");
                }
                else
                {
                    MessageBox.Show("저장실패!");
                }
                GetEmployees(); // 재조회
                NewEmployee(); // 모든 입력 초기화
            }
        }

        public void GetEmployees()
        {
            using (SqlConnection conn = new SqlConnection(Helpers.Common.CONNSTRING))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(Models.Employees.SELECT_QUERY, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                ListEmployees = new BindableCollection<Employees>();

                while (reader.Read())
                {
                    ListEmployees.Add(new Employees()
                    {
                        Id = (int)reader["Id"],
                        EmpName = reader["EmpName"].ToString(),
                        Salary = (decimal)reader["Salary"],
                        DeptName = reader["DeptName"].ToString(),
                        Addr = reader["Addr"].ToString()
                    });
                }
            }
        }

        // 삭제 버튼 활성화 여부
        public bool CanDelEmployee
        {
            get { return Id != 0; } // TextBox Id 속성의 값이 0이면 false, 0이 아닌면 true
        }

        public void DelEmployee()
        {
            if (Id==0)
            {
                MessageBox.Show("삭제할 항목이 없습니다");
                return;
            }

            if(MessageBox.Show("삭제하시겠습니까?", "삭제여부", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
            {
                return;
            }

            using (SqlConnection conn = new SqlConnection(Helpers.Common.CONNSTRING))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(Models.Employees.DELETE_QUERY, conn);
                SqlParameter prmId = new SqlParameter("@Id", Id);
                cmd.Parameters.Add(prmId);
                
                var result = cmd.ExecuteNonQuery();
                if (result >= 0)
                {
                    MessageBox.Show("삭제성공!");
                }
                else
                {
                    MessageBox.Show("삭제실패!");
                }
                GetEmployees();
                NewEmployee();
            }
        }

        public void NewEmployee()
        {
            Id = 0;
            Salary = 0;
            EmpName = DeptName = Addr = "";
            GetEmployees();
        }
    }
}
