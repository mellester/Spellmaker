using System;
using System.Linq;
using ImageMagick;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using log4net;
using System.Reflection;
using System.Threading;
using System.Collections.Concurrent;

using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Collections.ObjectModel;
using SheetDownloader;
using System.Windows.Media;
using System.Globalization;
using System.Windows;
using System.Drawing;

namespace Spellmaker
{
    enum questmaker
    {
        SHOWDATA,
        MAKEQUEST,
        MAKEJSON,
    }

    public class Questmaker
    {
        private const int ResizeConstant = 100;
        public static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        readonly int widthOfDeck = 10;
        public static object lockedObject = new Object();
        public static SettingsData settingsData;

        /// <summary>
        /// locks when imamagick is about to be used
        /// </summary>
        static readonly Mutex mutex = new Mutex();
        static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);
        private static readonly object cmdLock = new object();

        public class DoneDeck
        {
           public Deck deck;
        }
        public DoneDeck doneDeck = null;
        static public void Run(Settings settings = null)
        {
            var Helper = new Spellmaker.fakeGui.Helpers(settings);
            ClearFolder(@".\Images\temp");
            settingsData = settings.Primary;
            List<Questmaker> questmakers = new List<Questmaker>();

            List<Sheet> sheets;
            sheets = Sheet.BatchSheetGet(settings); // getsheets from google spreadsheest

            var DictWithAllTheQuets = Split(sheets);

            List<Task> tasks = new List<Task>();


            for (int indexer = 0; indexer < DictWithAllTheQuets.Count; indexer++)
            {
                var index = indexer;
                var maker = new Questmaker();
                questmakers.Add(maker);
                tasks.Add(new Task(() => maker.Questmake(DictWithAllTheQuets[index])));
            }
            while (tasks.Count > 0)
            {
                tasks.Take(1).ToList().ForEach(
                    (a) => { if (a.Status != TaskStatus.Running) a.Start();});
                Task.WhenAny(tasks.ToArray()).Wait();
                tasks.RemoveAll(t => t.IsCompleted);
            }
            MessageBoxResult r = default(MessageBoxResult);
            if (settings.Primary.doneImage.Count > 0)
                r = Helper.AskMessage("Done with my task want me to open up the temp folder?");
            else
            {
                MessageBox.Show("Done with nothing to show for");
                r = MessageBoxResult.Cancel;
            }
            
            if (r == MessageBoxResult.OK)
            {
                string p = Path.GetDirectoryName(settings.Primary.doneImage.First().Item2);
                Process.Start(p);
            }
            /*
            while ((from w in maker.QuestWork where w.jsonTask.IsCompleted select w).Count() == maker.QuestWork.Count);

            */

        }
        private static readonly int MaxNumOfCards = Settings.MaxNumOfCards;
        private static List<Deck> Split(List<Sheet> sheets)
        {
            List<Deck> ret = new List<Deck>();

            foreach (var sheet in sheets)
            {
                int count = sheet.Quests.Count;
                int remaining = sheet.Quests.Count;
                int divide = count / MaxNumOfCards + 1;
                char Letter = 'A';
                int itter = 0;
                while (divide-- >= 1)
                {
                    
                    Letter += (char)(itter);

                    Deck temp = new Deck
                    {
                        name = sheet.Name + '-' + Letter,
                        DirectoryPath = @"Images\temp\" + sheet.Name + '-' + Letter,
                        Quests = sheet.Quests.GetRange(itter * MaxNumOfCards, Math.Min(MaxNumOfCards, remaining)),
                       sheet = sheet,                          

                    };
                    temp.Number = temp.Quests.Count;
                    temp.Width = Math.Min(temp.Number, 10); 
                    temp.Height = Math.Min(temp.Number/10 + 1, 2);
                    remaining -= temp.Quests.Count;
                    itter++;
                    ret.Add(temp);
                }
            }

            return ret;
        }

        private DoneDeck Questmake(Deck deck)
        {
            this.RenderQuest(deck);
            return this.doneDeck;
           // SaveDeck.SaveImage(deck);
           // SaveDeck.SaveJson(deck);

        }


        public void RenderQuest(Deck deck)
        {
            this.doneDeck = new DoneDeck();
            this.doneDeck.deck = new Deck();
            //setup
            if (deck == null)
                Debug.Fail("sheet null");
            var name = deck.name;
            string tempDirectoryPath = deck.DirectoryPath;
            doneDeck.deck.DirectoryPath = tempDirectoryPath;
            lock (SettingsData.loc)
            {
               
            }
            lock (lockedObject)
            {
                settingsData.decks.Add(deck);
                settingsData.pathsToWatch.Add(tempDirectoryPath);
                settingsData.DictNameDes.Add(name, new List<KeyValuePair<string, string>>());
                settingsData.sheets.Add(deck.sheet);
            }
            if (Directory.Exists(tempDirectoryPath))
                foreach (FileInfo file in new DirectoryInfo(tempDirectoryPath).GetFiles())
                    file.Delete(); // empty folder


    
            
            deck.pathImage = NewMethod(deck.Quests, name, tempDirectoryPath);
            Settings.Instance.Primary.decks.First(A => A.name == deck.name).pathImage = deck.pathImage;
            if (deck.pathImage == null)
                throw new NotImplementedException();
            Console.WriteLine("Done");

            return;

        }

        private string NewMethod(IEnumerable<Sheet.Quest> quests, string name, string tempDirectoryPath)
        {
 
            semaphoreSlim.Wait();
            bool haslock = true;
            doneDeck.deck.Quests = new List<Sheet.Quest>();
            IEnumerable<Tuple<Object, Image, Bitmap>> singecardmaker()
            {
                foreach (Sheet.Quest quest in quests)
                {
                    Tuple<Object, Image, Bitmap> result;
                    if (!haslock)
                        semaphoreSlim.Wait();
                    try
                    {
                        result = SingleCard.SingleQuest(tempDirectoryPath, quest,
                          value =>
                          {
                              lock (lockedObject)
                              { settingsData.DictNameDes[name].Add(value); }
                          }, settingsData);
                    }
                    finally
                    {
                        haslock = false;
                        semaphoreSlim.Release();
                    }
                    yield return result;
                }
            }
            List<Sheet.Quest> done = new List<Sheet.Quest>();
            var task = this.Doasync(singecardmaker(), tempDirectoryPath: tempDirectoryPath, Sheetname: name);
            try
            {
                return task.Result;
            }
            catch (AggregateException ae)
            {
                foreach (var e in ae.Flatten().InnerExceptions)
                {
                    throw e;
                }
                return "";
            }
        }

        async private Task<string> Doasync(IEnumerable<Tuple<Object, Image, Bitmap>> enumerable, string tempDirectoryPath, string Sheetname)
        { 
            try
            {
                MagickImageCollection Vertical_images = new MagickImageCollection();
                MagickImageCollection Horiontal_images = new MagickImageCollection();
                void AppendHorizontally()
                {
                    if (Horiontal_images.Count >= widthOfDeck)
                    {
                        Vertical_images.Add(Horiontal_images.AppendHorizontally());
                        Horiontal_images.Clear();
                    }
                }
                MagickImageInfo info = new MagickImageInfo(@"Images\\quest.png");
                int count = 0;

                foreach (var pair in  enumerable)
                {
                    var quest = (SingleCard.Quest)pair.Item1;
                    count++;
                    await semaphoreSlim.WaitAsync();
                    try
                    {
                        var image = new MagickImage((ImageToByte(pair.Item3)));
                        pair.Item3.Dispose();
                        if (image.Height > (info.Height / 2))
                            image.Resize(new Percentage(ResizeConstant));
                        Horiontal_images.Add(image);
                        AppendHorizontally();
                    }
                    finally
                    {
                        semaphoreSlim.Release();
                    }
                }
                int extra_to_add = 10 - (count + 2) % widthOfDeck;
                if (extra_to_add == 10) { extra_to_add = 0; }
                if (count < 9)
                    extra_to_add += 10;
                Console.WriteLine("extra_to_add =" + extra_to_add.ToString());
                for (int Number = extra_to_add; Number-- > 0;)
                {
                    AppendHorizontally();
                    var image = new MagickImage(new MagickColor(0, 0, 0, 0), info.Width, info.Height);
                    image.Resize(new Percentage(ResizeConstant));
                    // adds empty image to collection
                    Horiontal_images.Add(image);
                }
                AppendHorizontally();
                {
                    MagickImage image1;
                    MagickImage image2;

                    try
                    {
                         image1 = secondtoLastimage(Sheetname);
                         image2 = new MagickImage(@".\Images\questquestion2.jpg");
                    }
                    catch
                    {
                        log.Error("shomthing went wrong");
                        throw;
                    }
                    try
                    {
                       image1.Resize(image2.Width, image2.Height);
                    }
                    catch
                    {
                        log.Error("shomthing went wrong");
                        throw;
                    }
                    image1.Write(".\\Images\\temp\\" + Sheetname + ".png");
                    // adds last hidden back to collection
                    Horiontal_images.Add(image1);
                    Horiontal_images.Add(image2);
                }

                Vertical_images.Add(Horiontal_images.AppendHorizontally());
                // some constanst
                // doWork(quests, info, filesindirectory, filesNamewithouExt);
                var result = Vertical_images.AppendVertically();
                result.Settings.SetDefine(MagickFormat.Jpeg, "sampling-factor", "4:2:0");
                result.Interlace = Interlace.Plane;
                result.Quality = 75;
                //result.ColorSpace = ColorSpace.RGB;
                Vertical_images.Dispose();
                Horiontal_images.Dispose();
                string path;
                {
                    Console.WriteLine("Writing " + Sheetname + " to Image with name:" + Sheetname + ".png");
                    // Save the result
                    path = "Images\\temp\\" + Sheetname + ".jpg";
                    using (var mem = new MemoryStream())
                    {
                        result.Write(mem); // write picture to stream
                        try
                        {
                            result.Write(path);
                        }
                        catch
                        {
                            throw;
                        }
                        lock (lockedObject)
                        {
                            settingsData.doneImage.Add(new Tuple<string, string>(Sheetname, path));
                        }
                    }
                }
                return path;
            }
            catch
            {
                log.Error("shomthing went wrong");
                throw;
            }
        }


        private static MagickImage secondtoLastimage(string name)
        {
            FormattedText formattedText1 = new FormattedText(name + "\n Done on " + DateTime.Now.Date.ToString("d"), CultureInfo.GetCultureInfo("en-us"),
                FlowDirection.LeftToRight,
                new Typeface("Verdana"),
                foreground: System.Windows.Media.Brushes.Black,
                emSize: 30)
            {
                MaxTextWidth = 600,
                MaxTextHeight = 500
            };
            formattedText1.SetFontSize(60, 0, name.Length);
            var image1 = new MagickImage(ImageToByte(Grapich.Render(
                inputFile: @".\Images\quest.png", text: formattedText1,
                position: new System.Windows.Point(250, 350))));
            return image1;
        }

        public static string Trim(string name)
        {

            const string pattern = "[\\s\"]+";
            name = Regex.Replace(name, pattern, "");
            name = name.Replace(':', '_');
            name = name.Replace('?', '_');
            name = name.Replace('@', '_');
            return name;
        }
        private static void ClearFolder(string FolderName)
        {
            DirectoryInfo dir = new DirectoryInfo(FolderName);
            System.IO.Directory.CreateDirectory(FolderName);
            foreach (FileInfo fi in dir.GetFiles())
            {
                try
                {
                    fi.Delete();
                }
                catch (Exception) { } // Ignore all exceptions
            }

            foreach (DirectoryInfo di in dir.GetDirectories())
            {
                ClearFolder(di.FullName);
                try
                {
                    di.Delete();
                }
                catch (Exception) { } // Ignore all exceptions
            }
        }


        public static byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }
    }
}
