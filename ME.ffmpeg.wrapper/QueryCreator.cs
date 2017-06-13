using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace ME.ffmpeg.wrapper
{
    public class QueryCreator
    {
        public const string basicCommand =
            "ffmpeg -r 60 -f image2 -s 1920x1080 -i pic%04d.png -vcodec libx264 -crf 25  -pix_fmt yuv420p test.mp4";
        private const int _defaultWidth = 1920;
        private const int _defaultHeight = 1080;

        public string SourcePath { get; private set; }
        public string NamingConvention { get; private set; }
        public int FrameRate { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }


        public QueryCreator(string sourcePath, string NamingConvention)
        {
            this.SourcePath = sourcePath;
            this.NamingConvention = NamingConvention;
        }

        private string GenerateQuery()
        {
            StringBuilder sb = new StringBuilder();
            //sb.Append("ffmpeg ");
            if (FrameRate == 0)
                FrameRate = 30;
            sb.Append("-r " + FrameRate.ToString() + " ");

            sb.Append("-f image2 ");
            if (Width > 0 && Height > 0)
            {
                sb.Append("-s " + Width + "x" + Height + " ");
            }
            else
            {
                sb.Append("-s " + _defaultWidth + "x" + _defaultHeight + " ");
            }
            sb.Append("-i \"" + SourcePath +"\\"+ NamingConvention + "\" ");
            sb.Append("-vcodec libx264 -crf 25 -pix_fmt yuv420p ");
            return sb.ToString();
        }

        public void RenderVideo(string OutputFile)
        {
            string command = GenerateQuery();
            command = command + "\"" + OutputFile + "\"";
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory+"ffmpeg.exe"))
            {
                try
                {
                    Process.Start(AppDomain.CurrentDomain.BaseDirectory +"ffmpeg.exe", @command);
                }
                catch (Exception)
                {
                    throw;
                }

            }
        }

    }
}
