using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUtils.Automation.Video {
    public enum ColorAlgorithm {
        DominantColorThief,
        DominantColor,
        AverageColor,
        DominantColorKMeans,
        WeightedBrightestColor
    }
}
