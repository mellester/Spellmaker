using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using log4net;
using System.Reflection;
using System.IO;
using System.Windows.Forms;
using questGui;
using ImageMagick;
using System.Windows.Media.Imaging;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows;
using System.Drawing;
using Brushes = System.Windows.Media.Brushes;
using Size = System.Windows.Size;
using Point = System.Windows.Point;
using sd = SheetDownloader;
using Google.Apis.Sheets.v4.Data;

namespace Spellmaker
{
    enum Singlecard
    {
        QUEST,
        SPEll,
    }
    /// <summary>
    /// Make a single card 
    /// </summary>
    /// 


    public class SingleCard
    {
        public static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        // lock for command line bat file
        private readonly static object cmdLock = new object();
        

        public struct Quest
        {
            public string name;
            public int Ilevel;
            public string level;
            public string des;
            public CellData Descritpion;
            public static implicit operator Quest(Sheet.Quest q)
            {
                Quest o;
                o.name = q.name;
                o.Ilevel = q.iLevel;
                o.level = q.level;
                o.des = q.des;
                o.Descritpion = q.Descritpion;
                return o;
            }
        };

        // is this class proprly init
        //private static bool init = true;
        /// <summary>
        /// setup any sattic init
        /// </summary>
        /// <returns>True if succes</returns>
        private static readonly object Locker = new object();
        static public void SingleQuestEvent(object sender, EventArgs e)
        {
            lock (Locker)
            {
                SettingsData.Image?.Dispose();
                string tempDir = @".\Images";
                Quest quest = new Quest
                {
                    name = "test name",
                    level = "0 XP",
                    des = "Testing the the image!. aaasfgaskjfgakjsfh ;aslifhk ;oiAWEIOf aw gh ;ls hjaso;g 'kljs m.svói aj"
                };
                var result = SingleQuest(tempDir: tempDir,
                    quest: quest,
                    set: Settings.Instance.Primary,
                    nameDes: new Action<KeyValuePair<string, string>>((a) => { return; }))
                    ;
                SettingsData.Image = result.Item2;
            }
        }

        /// <summary>
        /// Runs a .bat file in the ImageMagcikfolder making a [quest.name].png then moves it to Images\temp  
        /// </summary>
        /// <param name="quest.name"></param>
        /// <param name="quest.level"></param>
        /// <param name="quest.des"></param>
        public static Tuple<Object, Image, Bitmap> SingleQuest(string tempDir, Quest quest,Action<KeyValuePair<string,string>> nameDes, SettingsData set)
        {
            int rand = new Random().Next(1337);
            // if (quest.name == "Coin for a Coin")
            //throw new NotImplementedException();
            System.Windows.Media.FontFamily FF;
            if (quest.Descritpion?.TextFormatRuns?.First()?.Format?.FontFamily != null)
                FF = new System.Windows.Media.FontFamily(
                    quest.Descritpion.TextFormatRuns.First().Format.FontFamily);
            else
                FF = new System.Windows.Media.FontFamily(set.MyObjects[2].Item2.Font);

            var readSettings1 = set.MyObjects[0].Item2;
            var readSettings2 = set.MyObjects[1].Item2;
            var readSettings3 = set.MyObjects[2].Item2;
            readSettings1.TextGravity = Gravity.West;
            readSettings2.TextGravity = Gravity.Center;
            FlowDocument flowDoc = new FlowDocument
            {
                FontSize = readSettings3.FontPointsize,
                TextAlignment = System.Windows.TextAlignment.Left
                //FontSize = 1,
                //FontFamily = FF,
            };
            Paragraph myPara = new Paragraph();
            textFormatrun(quest, ref myPara, readSettings3);
            flowDoc.Blocks.Add(myPara);
            if (quest.name.Length > 45)
                readSettings2.FontPointsize -= 10;
            var sz = new Size((readSettings3.Width.GetValueOrDefault(0)), readSettings3.Height.GetValueOrDefault(0));
            using (var image = new MagickImage(set.blankpng))
            {
                image.Format = MagickFormat.Jpeg;
                image.Interlace = Interlace.Plane;
                image.Quality = 75;
                using (var bm = FlowDocumentToBitmap(flowDoc, sz))
                using (var caption1 = new MagickImage($"caption:{quest.level} ", readSettings1))
                using (var caption2 = new MagickImage($"caption:{quest.name} ", readSettings2))
                using (var caption3 = new MagickImage(bm))
                {
                    // Add the caption layer on top of the background image
                    // at position 590,450
                    (int x, int y) = (set.MyObjects[0].Item3.X, set.MyObjects[0].Item3.Y);
                    image.Composite(caption1, x, y, CompositeOperator.Over);
                    (x, y) = (set.MyObjects[1].Item3.X, set.MyObjects[1].Item3.Y);
                    image.Composite(caption2, x, y, CompositeOperator.Over);
                    caption3.Shear(10, 0);
                    caption3.ColorFuzz = new Percentage(50);
                    caption3.Transparent(MagickColors.White);
                    (x, y) = (set.MyObjects[2].Item3.X, set.MyObjects[2].Item3.Y);
                    image.Composite(caption3, x, y, CompositeOperator.Over);
                    bm.Close();
                }
                nameDes(new KeyValuePair<string, string>(quest.name, Bald(quest.des)));
                // move created png away to folder
                //forst remove whitespace from name
                quest.name = Questmaker.Trim(quest.name);
                //string fileName = "singleQuest" + rand + ".png";
                //string sourcePath = @"ImageMagick";
                string targetPath = tempDir;
                //string sourceFile = System.IO.Path.Combine(sourcePath, fileName);
                string destFile = System.IO.Path.Combine(targetPath, quest.name + ".png");
                
                try
                {
                    if (!System.IO.Directory.Exists(targetPath))
                    {
                        System.IO.Directory.CreateDirectory(targetPath);
                    }
                    if (System.IO.File.Exists(destFile))
                    {
                        System.IO.File.Delete(destFile);
                    }

                    try
                    {
                        var I = new MemoryStream();
                        
                        image.Write(I);
                        var img = Image.FromStream(I);
                        Bitmap bitmap = new Bitmap(img);
                        image.Strip(); // strips image of all profiles and comments
                        img.Save(destFile);
                        Tuple<Object, Image, Bitmap> pair;
                        pair = new Tuple<Object, Image, Bitmap>(quest, img, bitmap);
                        SettingsData.addToObservableImageSet(pair);
                        return pair;

                    }
                    catch  // copy blank if does not exist
                    {
                        System.IO.File.Copy(@"Images\quest.png", destFile);
                    }
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return new Tuple<Object, Image, Bitmap>(new Quest(),null,null);
        }
        private struct ImaToAdd
        {

        }
        private static void textFormatrun(Quest quest, ref Paragraph myPara, MagickReadSettings readSettings3)
        {
            
            if (quest.Descritpion == null || quest.Descritpion.TextFormatRuns == null)
            {
                myPara.Inlines.Add(new Run(quest.des));
                CheckImages(quest, ref myPara);
                return;
            }
            CellData cellData = quest.Descritpion;
            string ret = cellData.FormattedValue;
            if (ret == null)
                throw new NullReferenceException("Celldata.FormattedValue was empty");
            if (ret.Contains("single"))
                Debug.Assert(true);
            {
                for (var itter = 0; itter < cellData.TextFormatRuns.Count; ++itter)
                {

                    var run = cellData.TextFormatRuns[itter];
                    int length;
                    int StartIndex = run.StartIndex.GetValueOrDefault(0);
                    if (itter == cellData.TextFormatRuns.Count - 1)
                        length = cellData.FormattedValue.Length - StartIndex;
                    else
                        length = cellData.TextFormatRuns[itter + 1].StartIndex.GetValueOrDefault(0)
                            - StartIndex;
                    string substring = cellData.FormattedValue.Substring(
                        StartIndex, length
                        );
                    //var persistendRun = new Span(myTextPointer1, myTextPointer2);
                    var persistendRun = new Run(substring);
                    TextFormat format = run.Format;
                    bool Bold = format.Bold.GetValueOrDefault(false);
                    if (Bold)
                        persistendRun.FontWeight = FontWeights.SemiBold;
                    else
                        persistendRun.FontWeight = FontWeights.Normal;
                    bool Italic = format.Bold.GetValueOrDefault(false);
                    if (Italic)
                        persistendRun.FontStyle = FontStyles.Italic;
                    else
                        persistendRun.FontStyle = FontStyles.Normal;
                    

                    if (format.Strikethrough.HasValue)
                        if (format.Strikethrough.Value)
                            persistendRun.TextDecorations = TextDecorations.Strikethrough;
                        else
                            persistendRun.TextDecorations = null;
                    if (format.Underline.HasValue)
                        if (format.Strikethrough.Value)
                            persistendRun.TextDecorations = TextDecorations.Underline;
                        else
                            persistendRun.TextDecorations = null;
                    if (format.ForegroundColor != null)
                            persistendRun.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(
                                (byte)(format.ForegroundColor.Red.GetValueOrDefault(0) * 255),
                                (byte)(format.ForegroundColor.Green.GetValueOrDefault(0) * 255),
                                (byte)(format.ForegroundColor.Blue.GetValueOrDefault(0) * 255)
                                ));
                        else
                            persistendRun.Foreground = null;
                    var theRun = new Run(substring)
                    {
                        FontWeight = persistendRun.FontWeight,
                        TextDecorations = persistendRun.TextDecorations,
                        FontStyle = persistendRun.FontStyle,
                        Foreground = persistendRun.Foreground,
                        //FontSize = persistendRun.FontSize
                    };
                    //if (format.FontSize.HasValue)
                     //   theRun.FontSize = format.FontSize.Value;
                    myPara.Inlines.Add(theRun);
                }
                CheckImages(quest, ref myPara);
            }           
            
        }

        private static void CheckImages(Quest quest1, ref Paragraph paragraph)
        {

        }

        public static string EvalBald(Match match)
        {
            string des = match.ToString();
            des = des.Replace("[b]", "");
            return des;
        }
        private static string Bald(string des)
        {
            if (des == null)
                return "";
            string patrren = "\\[b\\].*?\\[-\\]";
            des = Regex.Replace(des, patrren, new MatchEvaluator(EvalBald));
            des = des.Replace("[-]", "");
            return des;
        }

        public static string EvalBold(Match match)
        {
            string des = match.ToString();
            des = des.Replace("[b]", "<b>");
   
            des = des.Replace("[-]", "</b>");
            return des;
        }
        private static string Bold(string des)
        {
            if (des == null)
                return "";
            string patrren = "\\[b\\].*?\\[-\\]";
            des = des.Replace("&", "&amp;amp;");
            des = Regex.Replace(des, patrren, new MatchEvaluator(EvalBold));
            des = des.Replace("[-]", "");
            des = des.Replace("\"", "\"\"");
            return des;
        }

        public static Task<int> RunProcess(string fileName, string args)
        {
            using (var process = new Process
            {
                StartInfo =
        {
            FileName = fileName, Arguments = args,
            UseShellExecute = false, CreateNoWindow = true,
            RedirectStandardOutput = true, RedirectStandardError = true,
            WindowStyle = ProcessWindowStyle.Hidden
        },
                EnableRaisingEvents = true
            })
            {
                var task = RunProcess(process);
                task.Wait();
                return task;
            }
        }

        private static Task<int> RunProcess(Process process)
        {
            var tcs = new TaskCompletionSource<int>();

            process.Exited += (s, ea) =>
            {
                tcs.SetResult(process.ExitCode);
            };
            process.OutputDataReceived += (s, ea) =>
            {
                if (ea.Data != null)
                {
                    Console.WriteLine(ea.Data);
                }
            };
            process.ErrorDataReceived += (s, ea) =>
            {
                if (ea.Data != null)
                {
                    Console.WriteLine("ERR" + ea.Data);
                }
            }; ;

            bool started = process.Start();
            if (!started)
            {
                //you may allow for the process to be re-used (started = false) 
                //but I'm not sure about the guarantees of the Exited event in such a case
                throw new InvalidOperationException("Could not start process: " + process);
            }

            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            return tcs.Task;
        }
        public static Stream FlowDocumentToBitmap(FlowDocument document, Size size)
        {
            document = CloneDocument(document);
            document.Background = Brushes.Transparent;
            var paginator = ((IDocumentPaginatorSource)document).DocumentPaginator;
            paginator.PageSize = size;
            
            var visual = new DrawingVisual();
            using (var drawingContext = visual.RenderOpen())
            {
                // draw white background
                //drawingContext.DrawRectangle(Brushes.Transparent, null, new Rect(size));
            }
            visual.Children.Add(paginator.GetPage(0).Visual);

            var renderTarget = new RenderTargetBitmap((int)size.Width *2, (int)size.Height *2,
                                                200, 200, PixelFormats.Pbgra32);
            renderTarget.Render(visual);
            //renderTarget.Render(drawingVisual);
            PngBitmapEncoder jpgEncoder = new PngBitmapEncoder();
            jpgEncoder.Frames.Add(BitmapFrame.Create(renderTarget));
            MemoryStream outputStream = new MemoryStream();            
            jpgEncoder.Save(outputStream);
            outputStream.Seek(0,SeekOrigin.Begin);
            return outputStream;
            
            
            //return imageArray;

        }

        public static FlowDocument CloneDocument(FlowDocument document)
        {
            var copy = new FlowDocument();
            var sourceRange = new TextRange(document.ContentStart, document.ContentEnd);
            var targetRange = new TextRange(copy.ContentStart, copy.ContentEnd);

            using (var stream = new MemoryStream())
            {
                sourceRange.Save(stream, System.Windows.DataFormats.XamlPackage);
                targetRange.Load(stream, System.Windows.DataFormats.XamlPackage);
            }

            return copy;
        }
       




    }
}
