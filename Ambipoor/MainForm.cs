using FileUtils.Automation.Video;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ambipoor {
    public partial class MainForm : Form {
        private YeelightVideo yeeLightVideo;
        private Array arrAlgorithms;
        private bool bInitialized;
        private UdpClient broadcaster;

        public MainForm() {
            InitializeComponent();

            Initialize();
        }

        private void Initialize() {

            Screen[] allScreens = Screen.AllScreens;
            for (int i = 0; i < allScreens.Length; i++) {
                Screen scr = allScreens[i];
                string strScreen = $"{i + 1} - {scr.Bounds.Width}x{scr.Bounds.Height}";
                combo_Displays.Items.Add(strScreen);
            }
            combo_Displays.SelectedIndex = 0;

            arrAlgorithms = Enum.GetValues(typeof(ColorAlgorithm));
            for (int i = 0; i < arrAlgorithms.Length; i++) {
                object algo = arrAlgorithms.GetValue(i);
                combo_Algorithms.Items.Add(algo);
            }
            combo_Algorithms.SelectedIndex = 0;

            yeeLightVideo = new YeelightVideo();
        }

        private void EnableForm() {
            combo_Displays.Enabled = true;
            txt_IP.Enabled = true;
            combo_Algorithms.Enabled = true;
            numeric_Interval.Enabled = true;
            check_Preview.Enabled = true;
        }

        private void btn_Start_Click(object sender, EventArgs e) {
            if (bInitialized) {
                timer_Preview.Enabled = false;
                btn_Start.Text = "Start";
                yeeLightVideo.Stop();
                yeeLightVideo = new YeelightVideo();
                GC.Collect();
                bInitialized = false;
                EnableForm();
                return;
            }

            bInitialized = true;
            btn_Start.Text = "Stop";
            yeeLightVideo.Initialize(this.txt_IP.Text, combo_Displays.SelectedIndex,
                (ColorAlgorithm)arrAlgorithms.GetValue(combo_Algorithms.SelectedIndex),
                (int)numeric_Interval.Value,
                check_Preview.Checked);

            timer_Preview.Enabled = check_Preview.Checked;
            if (check_Preview.Checked) {
                picture_Preview.Image = yeeLightVideo.PreviewImage;
                timer_Preview.Interval = (int)numeric_Interval.Value;
            }

            combo_Displays.Enabled = false;
            txt_IP.Enabled = false;
            combo_Algorithms.Enabled = false;
            numeric_Interval.Enabled = false;
            check_Preview.Enabled = false;

            yeeLightVideo.Execute();
        }

        private void timer_Preview_Tick(object sender, EventArgs e) {
            picture_Preview.Invalidate();
            panel_Color.BackColor = yeeLightVideo.LastColor;
            track_Brightness.Value = yeeLightVideo.LastBrightness;
        }

        private void btn_Detect_Click(object sender, EventArgs e) {
            string strNewIP = YeelightVideo.GetLightIP(txt_Gateway.Text);
            if (!string.IsNullOrEmpty(strNewIP)) {
                txt_IP.Text = strNewIP;
            }            
        }
    }
}
