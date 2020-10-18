using ColorThiefDotNet;
using FileUtils.Shared.Automation;
using NAudio.Wave;
using Nucleus;
using Nucleus.Platform.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using YeelightAPI;
using Color = System.Drawing.Color;

namespace FileUtils.Automation.Video {
    public class YeelightVideo {
        private Bitmap bmpScreen;
        private Bitmap bmpScreenSmall;
        private Bitmap bmpScreenSmallPreview;
        private LockBitmap locker;
        private ColorThief colorThief;

        private Graphics gSmall;
        private Graphics gSmallPreview;
        private Graphics gSnapshot;

        private Device lightDevice;
        private Screen screen;
        private bool bStop;

        private string strIP;
        private ColorAlgorithm colorAlgorithm;
        private bool bGeneratePreview;
        private bool bGenerateAllPreview;
        private int numInterval;
        private bool bBrightnessEnabled;
        private double dColorBrightFactor;
        private double dBrightFactor;

        public Bitmap PreviewImage {
            get { return bmpScreenSmallPreview; }
        }

        private int lastLightBrightness;
        public int LastBrightness {
            get { return lastLightBrightness; }
        }

        private Color lastLightColor;
        public Color LastColor {
            get { return lastLightColor; }
        }

        private Color lastPreviewColor;
        public Color PreviewColor {
            get { return lastPreviewColor; }
        }

        public Dictionary<ColorAlgorithm, Color> ColorsByAlg { get; private set; }

        public string Status { get; private set; }

        private Array colorAlgos;
        public YeelightVideo() {
            ColorsByAlg = new Dictionary<ColorAlgorithm, Color>();

            colorAlgos = Enum.GetValues(typeof(ColorAlgorithm));
            for (int i = 0; i < colorAlgos.Length; i++) {
                ColorAlgorithm colorAlgo = (ColorAlgorithm)colorAlgos.GetValue(i);
                ColorsByAlg.Add(colorAlgo, Color.Black);
            }
        }

        public static int GetBrightness(byte[] colors) {
            double totalBrightness = 0;
            for (int i = 0; i < colors.Length - 3; i += 3) {
                byte b = colors[i];
                byte g = colors[i + 1];
                byte r = colors[i + 2];

                if (b < 10 && g < 10 && r < 10) {
                    totalBrightness += 0; // no color
                    continue;
                }

                double bright = Math.Sqrt(0.299 * (double)r * (double)r + 0.587 * (double)g * (double)g + 0.114 * (double)b * (double)b);
                totalBrightness += bright;
            }

            int totalColors = colors.Length / 3;
            totalBrightness = totalBrightness / (double)totalColors;
            return (int)(1 + totalBrightness * 99 / (int)byte.MaxValue);
        }


        public static Color GetDominantColor(byte[] colors, out int brightness) {
            Dictionary<Color, int?> source = new Dictionary<Color, int?>();

            double totalBrightness = 0;
            for (int i = 0; i < colors.Length - 3; i += 3) {
                byte b = colors[i];
                byte g = colors[i + 1];
                byte r = colors[i + 2];
                if (b < 10 && g < 10 && r < 10) {
                    totalBrightness += 0; // no color
                    continue;
                }

                double bright = Math.Sqrt(0.299 * (double)r * (double)r + 0.587 * (double)g * (double)g + 0.114 * (double)b * (double)b);
                totalBrightness += bright;

                Color color = Color.FromArgb(r, g, b);
                if (!source.ContainsKey(color)) {
                    source[color] = new int?(0);
                }

                Color index1 = color;
                int? nullable1 = source[index1];
                Color index2 = index1;
                int? nullable2 = nullable1;
                int? nullable3 = nullable2.HasValue ? new int?(nullable2.GetValueOrDefault() + 1) : new int?();
                source[index2] = nullable3;
            }

            int totalColors = colors.Length / 3;
            totalBrightness = totalBrightness / (double)totalColors;
            brightness = (int)(1 + totalBrightness * 99 / (int)byte.MaxValue);

            return source.OrderByDescending<KeyValuePair<Color, int?>, int?>((Func<KeyValuePair<Color, int?>, int?>)(p => p.Value)).First<KeyValuePair<Color, int?>>().Key;
        }

        public static Color GetAverageColor(byte[] colors, out int brightness) {
            double totalBrightness = 0;
            double totalR = 0;
            double totalG = 0;
            double totalB = 0;

            for (int i = 0; i < colors.Length - 3; i += 3) {
                byte b = colors[i];
                byte g = colors[i + 1];
                byte r = colors[i + 2];

                totalR += r;
                totalG += g;
                totalB += b;

                if (b < 10 && g < 10 && r < 10) {
                    totalBrightness += 0; // no color
                    continue;
                }

                double bright = Math.Sqrt(0.299 * (double)r * (double)r + 0.587 * (double)g * (double)g + 0.114 * (double)b * (double)b);
                totalBrightness += bright;
            }

            int totalColors = colors.Length / 3;
            totalBrightness = totalBrightness / (double)totalColors;
            brightness = (int)(1 + totalBrightness * 99 / (int)byte.MaxValue);

            return Color.FromArgb((byte)(totalR / totalColors), (byte)(totalG / totalColors), (byte)(totalB / totalColors));
        }


        public static Color GetWeightedDominantColor(byte[] colors, out int brightness) {
            double totalBrightness = 0;
            double totalR = 0;
            double totalG = 0;
            double totalB = 0;

            double rWeight = 1 / 3.0;
            double gWeight = 1 / 3.0;
            double bWeight = 1 / 3.0;

            //int totalColors = 0;

            double finalR = 0;
            double finalG = 0;
            double finalB = 0;

            for (int i = 0; i < colors.Length - 3; i += 3) {
                byte b = colors[i];
                byte g = colors[i + 1];
                byte r = colors[i + 2];

                //totalColors++;
                //totalR += Math.Log(256 - r, 255) * 255;
                //totalG += Math.Log(256 - g, 255) * 255;
                //totalB += Math.Log(256 - b, 255) * 255;

                if (double.IsInfinity(totalB)) {
                    Debugger.Break();
                }

                double bright = Math.Sqrt(0.299 * (double)r * (double)r + 0.587 * (double)g * (double)g + 0.114 * (double)b * (double)b);
                totalBrightness += bright;

                double weight = bright / 255.0;

                finalR += r * weight;
            }

            int totalColors = colors.Length / 3;
            totalBrightness = totalBrightness / (double)totalColors;
            brightness = (int)(1 + totalBrightness * 99 / (int)byte.MaxValue);

            //return Color.FromArgb((byte)(totalR / totalColors), (byte)(totalG / totalColors), (byte)(totalB / totalColors));
            return Color.FromArgb((byte)(finalR * 255.0), (byte)(finalG * 255.0), (byte)(finalB * 255.0));
        }

        public static Color GetDominantColor(IEnumerable<Color> colors) {
            Dictionary<Color, int?> source = new Dictionary<Color, int?>();

            foreach (Color color in colors) {
                if (!source.ContainsKey(color)) {
                    source[color] = new int?(0);
                }

                Color index1 = color;
                int? nullable1 = source[index1];
                Color index2 = index1;
                int? nullable2 = nullable1;
                int? nullable3 = nullable2.HasValue ? new int?(nullable2.GetValueOrDefault() + 1) : new int?();
                source[index2] = nullable3;
            }
            return source.OrderByDescending<KeyValuePair<Color, int?>, int?>((Func<KeyValuePair<Color, int?>, int?>)(p => p.Value)).First<KeyValuePair<Color, int?>>().Key;
        }

        public bool Initialize(string strIP, int numScreen,
            ColorAlgorithm colorAlgorithm, int numInterval,
            double dColorBrightFactor,
            double dBrightFactor, bool bBrightnessEnabled,
            bool bGeneratePreview, bool bGenerateAllPreview) {
            this.strIP = strIP;
            this.numInterval = numInterval;
            this.colorAlgorithm = colorAlgorithm;
            this.dColorBrightFactor = dColorBrightFactor;
            this.dBrightFactor = dBrightFactor;
            this.bBrightnessEnabled = bBrightnessEnabled;
            this.bGeneratePreview = bGeneratePreview;
            this.bGenerateAllPreview = bGenerateAllPreview;

            screen = Screen.AllScreens[numScreen];
            bmpScreen = new Bitmap(screen.Bounds.Width, screen.Bounds.Height);
            bmpScreenSmall = new Bitmap(screen.Bounds.Width / 8, screen.Bounds.Height / 8, PixelFormat.Format24bppRgb);
            bmpScreenSmallPreview = new Bitmap(screen.Bounds.Width / 8, screen.Bounds.Height / 8, PixelFormat.Format24bppRgb);

            gSnapshot = Graphics.FromImage(bmpScreen);
            gSmall = Graphics.FromImage(bmpScreenSmall);
            gSmallPreview = Graphics.FromImage(bmpScreenSmallPreview);

            lightDevice = new Device(strIP);

            try {
                AsyncHelpers.RunSync(lightDevice.Connect);
            } catch {
                return false;
            }

            lightDevice.SetBrightness(100);
            locker = new LockBitmap(bmpScreenSmall);

            if (colorAlgorithm == ColorAlgorithm.DominantColorThief ||
                bGenerateAllPreview) {
                colorThief = new ColorThief();
            }

            return true;
        }

        public static string GetLightIP(string strGateway) {
            IPAddress ipAddress = IPAddress.Parse(strGateway);
            byte[] address = ipAddress.GetAddressBytes();

            for (int i = 0; i < 255; i++) {
                address[3] = (byte)i;
                IPAddress newIP = new IPAddress(address);
                Console.WriteLine($"Looking for light in {newIP.ToString()}");

                Device lightDevice = new Device(newIP.ToString());
                lightDevice.Connect();

                int sleeps = 10;
                for (; ; ) {
                    Thread.Sleep(100);
                    sleeps--;

                    if (sleeps <= 0) {
                        break;
                    }

                    if (lightDevice.IsConnected) {
                        return newIP.ToString();
                    }
                }
            }

            return null;
        }

        public void Execute() {
            callsPerSecStop = Stopwatch.StartNew();
            callsPerMinStop = Stopwatch.StartNew();

            Thread thread = new Thread(LightThread);
            thread.Start();
        }

        public void Stop() {
            callsPerSecStop.Stop();
            callsPerMinStop.Stop();
            bStop = true;
        }

        private Stopwatch callsPerSecStop;
        private Stopwatch callsPerMinStop;
        private int numCalls;
        private int numCallsMin;

        public double CallsPerSec { get; private set; }
        public double CallsPerMin { get; private set; }



        private Color ParseFrame(ColorAlgorithm colorAlg, out int brightness) {
            brightness = 0;
            Color color = new Color();
            switch (colorAlg) {
                case ColorAlgorithm.DominantColor: {
                    // lock bits
                    locker.LockBits();
                    color = GetDominantColor(locker.Pixels, out brightness);
                    locker.UnlockBits();
                }
                break;
                case ColorAlgorithm.AverageColor: {
                    // lock bits
                    locker.LockBits();
                    color = GetAverageColor(locker.Pixels, out brightness);
                    locker.UnlockBits();
                }
                break;
                case ColorAlgorithm.DominantColorThief: {
                    locker.LockBits();
                    brightness = GetBrightness(locker.Pixels);
                    locker.UnlockBits();

                    QuantizedColor qColor = colorThief.GetColor(bmpScreenSmall);
                    color = Color.FromArgb(qColor.Color.R, qColor.Color.G, qColor.Color.B);
                }
                break;
                case ColorAlgorithm.WeightedBrightestColor: {
                    return Color.Black;

                    locker.LockBits();
                    color = GetWeightedDominantColor(locker.Pixels, out brightness);
                    locker.UnlockBits();
                }
                break;
                case ColorAlgorithm.DominantColorKMeans: {
                    locker.LockBits();
                    brightness = GetBrightness(locker.Pixels);
                    locker.UnlockBits();

                    // copy colors
                    byte[] pixels = locker.Pixels;
                    int totalColors = pixels.Length / 3;
                    List<Color> colorList = new List<Color>(totalColors);

                    for (int i = 0; i < pixels.Length - 3; i += 3) {
                        byte b = pixels[i];
                        byte g = pixels[i + 1];
                        byte r = pixels[i + 2];

                        Color nColor = Color.FromArgb(255,
                            (byte)(r * dColorBrightFactor),
                            (byte)(g * dColorBrightFactor),
                            (byte)(b * dColorBrightFactor));

                        //l = 0.75;

                        //// convert back to rgb
                        //l *= dColorBrightFactor;
                        //Color nColor = ColorUtil.HSL2RGB(h, s, l);

                        // skip dark pixels
                        //if (b < 50 && g < 50 && r < 50) {
                        //    continue;
                        //}

                        // skip dark pixels
                        //if (KCluster.EuclideanDistance(nColor, Color.Black) >= 200 &&
                        //    KCluster.EuclideanDistance(nColor, Color.White) >= 200) {
                        //}

                        // TODO: skipping pixels make the closest color fluctuate
                        colorList.Add(nColor);
                    }

                    const int k = 1;

                    KMeansClusteringCalculator clustering = new KMeansClusteringCalculator();
                    IList<Color> dominantColours = clustering.Calculate(k, colorList, 5.0d);
                    color = dominantColours.FirstOrDefault();

                    // convert to HSV
                    double h, s, l;
                    //ColorUtil.RGB2HSL(b, g, r, out h, out s, out l);

                    double[] hsl = SimpleColorTransforms.RgBtoHsl(color);

                    Color newColor = SimpleColorTransforms.HsLtoRgb(hsl[0], hsl[1], hsl[2]);

                    int x = -1;
                }
                break;
            }
            return color;
        }

        private void LightThread() {
            Color lastColor = new Color();
            int counter = 0;
            int lastBrightness = -1;
            int lastRGB = -1;
            for (; ; ) {
                if (bStop) {
                    break;
                }

                Thread.Sleep(TimeSpan.FromMilliseconds(numInterval));
                Console.WriteLine($"Tick {counter++}");
                Status = "OK";

                if (callsPerSecStop.ElapsedMilliseconds > 10000) {
                    callsPerSecStop.Restart();
                    CallsPerSec = (numCalls / 10.0);
                    numCallsMin += numCalls;
                    numCalls = 0;
                }

                if (callsPerMinStop.ElapsedMilliseconds > 60000) {
                    callsPerMinStop.Restart();
                    CallsPerMin = numCallsMin;
                    numCallsMin = 0;
                }

                // snapshot display and copy to bitmap
                //g.CopyFromScreen(-1920, 0, 0, 0, new Size(1920, 1080));
                Rectangle bounds = screen.Bounds;
                gSnapshot.CopyFromScreen(bounds.X, bounds.Y, 0, 0, new Size(bounds.Width, bounds.Height));
                // scale bitmap down 8x
                gSmall.DrawImage(bmpScreen, new Rectangle(0, 0, bmpScreenSmall.Width, bmpScreenSmall.Height));
                if (bGeneratePreview) {
                    gSmallPreview.DrawImage(bmpScreen, new Rectangle(0, 0, bmpScreenSmall.Width, bmpScreenSmall.Height));
                }
                //bmpScreenSmall.Save($"F:\\Nuke\\img{counter}.png");

                int brightness;
                Color color = ParseFrame(colorAlgorithm, out brightness);
                lastPreviewColor = color;

                if (this.bGenerateAllPreview) {
                    for (int i = 0; i < colorAlgos.Length; i++) {
                        ColorAlgorithm colorAlgo = (ColorAlgorithm)colorAlgos.GetValue(i);
                        if (colorAlgo == colorAlgorithm) {
                            ColorsByAlg[colorAlgo] = color;
                        } else {

                            int subBrightness;
                            Color subColor = ParseFrame(colorAlgo, out subBrightness);
                            ColorsByAlg[colorAlgo] = subColor;
                        }
                    }
                }

                int red = color.R;
                int green = color.G;
                int blue = color.B;
                int num2 = red << 16 | green << 8 | blue;

                if (bBrightnessEnabled) {
                    if (brightness != lastBrightness) {
                        int finalBrightness = (int)(brightness * dBrightFactor);
                        finalBrightness = MathUtil.Clamp(finalBrightness, 1, 100);

                        int deltaBright = Math.Abs(finalBrightness - lastBrightness);
                        const int minDeltaBright = 13;

                        if (deltaBright > minDeltaBright) {
                            lastBrightness = finalBrightness;

                            lastLightBrightness = finalBrightness;
                            try {
                                AsyncHelpers.RunSync(() => {
                                    numCalls++;
                                    return lightDevice.SetBrightness(finalBrightness);
                                });
                            } catch (Exception ex) {
                                Status = ex.Message;
                            }

                            Console.WriteLine($"New brightness: {finalBrightness}");
                        }
                    }
                }

                if (num2 != lastRGB) {
                    lastRGB = num2;

                    int deltaR = Math.Abs(red - lastColor.R);
                    int deltaG = Math.Abs(green - lastColor.G);
                    int deltaB = Math.Abs(blue - lastColor.B);

                    const int minDeltaColor = 40;

                    if (deltaR > minDeltaColor ||
                        deltaG > minDeltaColor ||
                        deltaB > minDeltaColor) {

                        //if (deltaR > 5 ||
                        //   deltaG > 5 ||
                        //   deltaB > 5) {
                        lastColor = color;
                        lastLightColor = color;
                        // dont change if the delta is too small

                        try {
                            AsyncHelpers.RunSync(() => {
                                numCalls++;
                                return lightDevice.SetRGBColor(red, green, blue);
                            });
                        } catch (Exception ex) {
                            Status = ex.Message;
                        }
                        Console.WriteLine($"New color: {red}, {green}, {blue}");
                    }
                }
                //string strRepeat = StringUtil.RepeatCharacter('■', peakValueBars);
                //Console.WriteLine($"({peakValueCent}) {strRepeat}");
            }

            lightDevice.Disconnect();
        }
    }
}
