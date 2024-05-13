using MahApps.Metro.Controls;


namespace Find_Library
{
    /// <summary>
    /// MapWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MapWindow : MetroWindow
    {
        public MapWindow()
        {
            InitializeComponent();
        }
        public MapWindow(double 위도, double 경도) : this()
        {
            BrsLoc.Address = $"https://google.com/maps/place/{위도},{경도}";
        }
    }
}
