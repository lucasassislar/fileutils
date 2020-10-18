using FileUtils.Automation.Video;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ambipoor {
    public partial class MainForm : Form {
        private YeelightVideo yeeLightVideo;
        private Array arrAlgorithms;
        private bool bInitialized;

        public MainForm() {
            InitializeComponent();

            Initialize();

            LoadSave();
        }

        private void LoadSave() {
            string appFolder = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string savePath = Path.Combine(appFolder, "lightsave.json");

            if (File.Exists(savePath)) {
                try {
                    string fileData = File.ReadAllText(savePath);
                    LightSettings lightSettings = JsonConvert.DeserializeObject<LightSettings>(fileData);
                    ApplyLightSettings(lightSettings);
                } catch {
                }
            }
        }

        private void ApplyLightSettings(LightSettings settings) {
            combo_Displays.SelectedIndex = settings.display;
            this.txt_IP.Text = settings.lightIP;
            numeric_Interval.Value = settings.interval;
            check_UpdateBrightness.Checked = settings.updateBrightness;
            num_BrightFactor.Value = (decimal)settings.brightnessFactor;
            combo_Algorithms.SelectedIndex = Array.IndexOf(arrAlgorithms, settings.colorAlgorithm);
            num_colorBrightness.Value = (decimal)settings.colorBrightnessFactor;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
            LightSettings lightSetting = GetLightSettings();

            string appFolder = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string savePath = Path.Combine(appFolder, "lightsave.json");

            if (File.Exists(savePath)) {
                File.Delete(savePath);
            }

            string serialized = JsonConvert.SerializeObject(lightSetting);
            File.WriteAllText(savePath, serialized);
        }

        private LightSettings GetLightSettings() {
            LightSettings lightSettings = new LightSettings();
            lightSettings.display = combo_Displays.SelectedIndex;
            lightSettings.lightIP = this.txt_IP.Text;
            lightSettings.colorAlgorithm = (ColorAlgorithm)arrAlgorithms.GetValue(combo_Algorithms.SelectedIndex);
            lightSettings.interval = (int)numeric_Interval.Value;
            lightSettings.updateBrightness = check_UpdateBrightness.Checked;
            lightSettings.brightnessFactor = (double)num_BrightFactor.Value;
            lightSettings.colorBrightnessFactor = (double)num_colorBrightness.Value;
            return lightSettings;
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
            check_UpdateBrightness.Enabled = true;
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

            if (!yeeLightVideo.Initialize(this.txt_IP.Text, combo_Displays.SelectedIndex,
                (ColorAlgorithm)arrAlgorithms.GetValue(combo_Algorithms.SelectedIndex),
                (int)numeric_Interval.Value,
                (double)num_colorBrightness.Value,
                (double)num_BrightFactor.Value,
                check_UpdateBrightness.Checked,
                check_Preview.Checked,
                check_allPreview.Checked)) {
                MessageBox.Show("Failed connecting to camera IP");
                return;
            }

            bInitialized = true;
            btn_Start.Text = "Stop";

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
            check_UpdateBrightness.Enabled = false;

            yeeLightVideo.Execute();
        }

        private void timer_Preview_Tick(object sender, EventArgs e) {
            picture_Preview.Invalidate();
            panel_Color.BackColor = yeeLightVideo.LastColor;
            panel_PreviewColor.BackColor = yeeLightVideo.PreviewColor;
            track_Brightness.Value = yeeLightVideo.LastBrightness;
            label_Status.Text = yeeLightVideo.Status;
            label_CallsSec.Text = yeeLightVideo.CallsPerSec.ToString();
            label_CallsMin.Text = yeeLightVideo.CallsPerMin.ToString();

            if (check_allPreview.Checked) {
                panel_dominantColor.BackColor = yeeLightVideo.ColorsByAlg[ColorAlgorithm.DominantColor];
                panel_averageColor.BackColor = yeeLightVideo.ColorsByAlg[ColorAlgorithm.AverageColor];
                panel_colorThief.BackColor = yeeLightVideo.ColorsByAlg[ColorAlgorithm.DominantColorThief];
                panel_kMeans.BackColor = yeeLightVideo.ColorsByAlg[ColorAlgorithm.DominantColorKMeans];
                panel_weightedDominant.BackColor = yeeLightVideo.ColorsByAlg[ColorAlgorithm.WeightedBrightestColor];
            }
        }

        private void btn_Detect_Click(object sender, EventArgs e) {
            string strNewIP = YeelightVideo.GetLightIP(txt_Gateway.Text);
            if (!string.IsNullOrEmpty(strNewIP)) {
                txt_IP.Text = strNewIP;
            }
        }


    }
}
