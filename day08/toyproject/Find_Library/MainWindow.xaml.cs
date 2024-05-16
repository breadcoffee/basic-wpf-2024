using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ControlzEx.Standard;
using Find_Library.Models;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json.Linq;

namespace Find_Library
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public bool CboFlag = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            InitComboDateFromDB();
        }

        private void InitComboDateFromDB()
        {
            using (SqlConnection conn = new SqlConnection(Helpers.Common.CONNSTRING))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(Models.LibraryLocation.GETDATE_QUERY, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet dSet = new DataSet();
                adapter.Fill(dSet);
                List<string> saveDates = new List<string>();

                foreach (DataRow row in dSet.Tables[0].Rows)
                {
                    saveDates.Add(Convert.ToString(row["Save_Date"]));
                }
                CboReqDate.ItemsSource = saveDates;
            }
        }

        private void BtnReqRealtime_Click(object sender, RoutedEventArgs e)
        {
            CboFlag = false;
            RefeshData();
        }

        private async void RefeshData()
        {
            string openApiUri = "https://smart.incheon.go.kr/server/rest/services/Hosted/%EB%8F%84%EC%84%9C%EA%B4%80%EB%B0%8F%EC%84%9C%EC%A0%90%EC%A0%95%EB%B3%B4/FeatureServer/0/query?outFields=*&where=1%3D1&f=geojson";
            string result = string.Empty;

            // WebRequest, WebResponse 객체
            WebRequest req = null;
            WebResponse res = null;
            StreamReader reader = null;

            try
            {
                req = WebRequest.Create(openApiUri);
                res = await req.GetResponseAsync();
                reader = new StreamReader(res.GetResponseStream());
                result = reader.ReadToEnd();

                //await this.ShowMessageAsync("결과", result);
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("오류", $"OpenAPI 조회오류 {ex.Message}");
            }

            var jsonResult = JObject.Parse(result);
            var data = jsonResult["features"];
            var jsonArray = data as JArray; // json자체에서 []안에 들어간 배열데이터만 JArray 변환가능

            var libraryLocations = new List<LibraryLocation>();
            
            foreach (var item in jsonArray)
            {
                if (CboFlag == true)
                {
                    if (item["properties"]["구분"].ToString() == CboReqDate.SelectedItem.ToString().TrimEnd())
                    {
                        libraryLocations.Add(new LibraryLocation()
                        {
                            Id = 0,
                            주요_매출원 = item["properties"]["주요_매출원"] == null ? "" : Convert.ToString(item["properties"]["주요_매출원"]),
                            휴무일 = item["properties"]["휴무일"] == null ? "" : Convert.ToString(item["properties"]["휴무일"]),
                            개점일_개관일 = item["properties"]["개점일_개관일"] == null ? "" : Convert.ToString(item["properties"]["개점일_개관일"]),
                            운영시간 = item["properties"]["운영시간"] == null ? "" : Convert.ToString(item["properties"]["운영시간"]),
                            이름 = Convert.ToString(item["properties"]["이름"]),
                            전화번호 = item["properties"]["전화번호"] == null ? "" : Convert.ToString(item["properties"]["전화번호"]),
                            주소 = item["properties"]["주소"] == null ? "" : Convert.ToString(item["properties"]["주소"]),
                            웹사이트 = item["properties"]["웹사이트"] == null ? "" : Convert.ToString(item["properties"]["웹사이트"]),
                            상세주소 = item["properties"]["상세주소"] == null ? "" : Convert.ToString(item["properties"]["상세주소"]),
                            대표자_운영주체 = item["properties"]["대표자_운영주체"] == null ? "" : Convert.ToString(item["properties"]["대표자_운영주체"]),
                            총면적_연면적_m2_ = item["properties"]["총면적_연면적_m2_"] == null ? "" : Convert.ToString(item["properties"]["총면적_연면적_m2_"]),
                            구분 = Convert.ToString(item["properties"]["구분"]),
                            좌석수 = string.IsNullOrEmpty(item["properties"]["좌석수"].ToString()) ? 0 : Convert.ToInt32(item["properties"]["좌석수"]),
                            경도 = Convert.ToDouble(item["properties"]["경도"]),
                            보유도서량 = item["properties"]["보유도서량"] == null ? "" : Convert.ToString(item["properties"]["보유도서량"]),
                            E_mail = item["properties"]["e_mail"] != null ? "" : Convert.ToString(item["properties"]["e_mail"]),
                            위도 = Convert.ToDouble(item["properties"]["위도"]),
                            공간유형 = Convert.ToString(item["properties"]["공간유형"]),
                            Objectid = Convert.ToInt32(item["properties"]["objectid"]),
                        });
                    }
                }
                else
                {
                    libraryLocations.Add(new LibraryLocation()
                    {
                        Id = 0,
                        주요_매출원 = item["properties"]["주요_매출원"] == null ? "" : Convert.ToString(item["properties"]["주요_매출원"]),
                        휴무일 = item["properties"]["휴무일"] == null ? "" : Convert.ToString(item["properties"]["휴무일"]),
                        개점일_개관일 = item["properties"]["개점일_개관일"] == null ? "" : Convert.ToString(item["properties"]["개점일_개관일"]),
                        운영시간 = item["properties"]["운영시간"] == null ? "" : Convert.ToString(item["properties"]["운영시간"]),
                        이름 = Convert.ToString(item["properties"]["이름"]),
                        전화번호 = item["properties"]["전화번호"] == null ? "" : Convert.ToString(item["properties"]["전화번호"]),
                        주소 = item["properties"]["주소"] == null ? "" : Convert.ToString(item["properties"]["주소"]),
                        웹사이트 = item["properties"]["웹사이트"] == null ? "" : Convert.ToString(item["properties"]["웹사이트"]),
                        상세주소 = item["properties"]["상세주소"] == null ? "" : Convert.ToString(item["properties"]["상세주소"]),
                        대표자_운영주체 = item["properties"]["대표자_운영주체"] == null ? "" : Convert.ToString(item["properties"]["대표자_운영주체"]),
                        총면적_연면적_m2_ = item["properties"]["총면적_연면적_m2_"] == null ? "" : Convert.ToString(item["properties"]["총면적_연면적_m2_"]),
                        구분 = Convert.ToString(item["properties"]["구분"]),
                        좌석수 = string.IsNullOrEmpty(item["properties"]["좌석수"].ToString()) ? 0 : Convert.ToInt32(item["properties"]["좌석수"]),
                        경도 = Convert.ToDouble(item["properties"]["경도"]),
                        보유도서량 = item["properties"]["보유도서량"] == null ? "" : Convert.ToString(item["properties"]["보유도서량"]),
                        E_mail = item["properties"]["e_mail"] != null ? "" : Convert.ToString(item["properties"]["e_mail"]),
                        위도 = Convert.ToDouble(item["properties"]["위도"]),
                        공간유형 = Convert.ToString(item["properties"]["공간유형"]),
                        Objectid = Convert.ToInt32(item["properties"]["objectid"]),
                    });
                }
            }
            this.DataContext = libraryLocations;
            StsResult.Content = $"OpenAPI {libraryLocations.Count}건 조회완료!";
        }

        private async void BtnSaveData_Click(object sender, RoutedEventArgs e)
        {
            if (GrdResult.SelectedItems.Count == 0)
            {
                await this.ShowMessageAsync("저장오류", $"실시간 조회 후 저장하십시오");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(Helpers.Common.CONNSTRING))
                {
                    conn.Open();

                    var insRes = 0;
                    foreach (LibraryLocation item in GrdResult.SelectedItems)
                    {
                        SqlCommand cmd = new SqlCommand(Models.LibraryLocation.INSERT_QUERY, conn);
                        cmd.Parameters.AddWithValue("@Id", item.Id);
                        cmd.Parameters.AddWithValue("@주요_매출원", item.주요_매출원);
                        cmd.Parameters.AddWithValue("@휴무일", item.휴무일);
                        cmd.Parameters.AddWithValue("@개점일_개관일", item.개점일_개관일);
                        cmd.Parameters.AddWithValue("@운영시간", item.운영시간);
                        cmd.Parameters.AddWithValue("@이름", item.이름);
                        cmd.Parameters.AddWithValue("@전화번호", item.전화번호);
                        cmd.Parameters.AddWithValue("@주소", item.주소);
                        cmd.Parameters.AddWithValue("@웹사이트", item.웹사이트);
                        cmd.Parameters.AddWithValue("@상세주소", item.상세주소);
                        cmd.Parameters.AddWithValue("@대표자_운영주체", item.대표자_운영주체);
                        cmd.Parameters.AddWithValue("@총면적_연면적_m2_", item.총면적_연면적_m2_);
                        cmd.Parameters.AddWithValue("@구분", item.구분);
                        cmd.Parameters.AddWithValue("@좌석수", item.좌석수);
                        cmd.Parameters.AddWithValue("@경도", item.경도);
                        cmd.Parameters.AddWithValue("@보유도서량", item.보유도서량);
                        cmd.Parameters.AddWithValue("@E_mail", item.E_mail);
                        cmd.Parameters.AddWithValue("@위도", item.위도);
                        cmd.Parameters.AddWithValue("@공간유형", item.공간유형);
                        cmd.Parameters.AddWithValue("@Objectid", item.Objectid);

                        insRes += cmd.ExecuteNonQuery();
                    }

                    if (insRes > 0)
                    {
                        await this.ShowMessageAsync("저장", "DB저장성공!");
                        StsResult.Content = $"DB저장 {insRes}건 성공!";
                    }
                }
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("저장오류", $"저장오류 {ex.Message}");
            }

            InitComboDateFromDB();
        }

        private async void CboReqDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CboFlag = true;
            //await this.ShowMessageAsync("결과", $"{CboReqDate.SelectedItem}");
            RefeshData();
        }

        private void GrdResult_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var curItem = GrdResult.SelectedItem as LibraryLocation;
            var mapWindow = new MapWindow(curItem.위도, curItem.경도);
            mapWindow.Owner = this;
            mapWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            mapWindow.ShowDialog();
        }
    }
}