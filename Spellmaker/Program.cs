using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using log4net;
using log4net.Config;
using System.Windows.Forms;
using ManyConsole;
using Mono.Options;

namespace Spellmaker
{
    public class Program
    {
        int Exit = 0;
        private IEnumerable<ConsoleCommand> Commands;
        private Settings settings;
        private Spellmaker.fakeGui.Form1 Form;
        private Spellmaker.fakeGui.FileForm1 fileForm;
        public string[] Args;
        private Thread thread1;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public Program(bool noGui, string [] Args)
        {
            this.settings = Settings.Init(noGui, Args);
            this.Args = Args;
            var settingsData = settings.Primary;
            Form = new Spellmaker.fakeGui.Form1(noGui, settingsData);
            Settings.form = Form;
            fileForm = new Spellmaker.fakeGui.FileForm1(noGui, settingsData);

        }
        [STAThread]
        public static int Main(string[] args)
        {
            bool show_help = false;
            List<string> names = new List<string>();
            int repeat = 1;
            bool noGui =  false;
            OptionSet options = new OptionSet() {
            "Usage: greet [OPTIONS]+ message",
            "Greet a list of individuals with an optional message.",
            "If no message is specified, a generic greeting is used.",
            "",
            "Options:",
            { "n|name=", "the {NAME} of of the sheet.",
              v => names.Add (v) },
            { "no-gui", "Disables the gui",
              v => noGui = true },
            { "r|repeat=",
                "the number of {TIMES} to repeat the greeting.\n" +
                    "this must be an integer.",
              (int v) => repeat = v },
            { "v", "does nothing yet",
              v => {  ; } },
            { "h|help",  "show this message and exit",
              v => show_help = v != null },
            };
            List<string> extra;
            try
            {
                extra = options.Parse(args);
                var length = "1suImPSisAvCgPiN6DlqDfaQbNPCx0HjpqtCbyi4Ifss".Length;
                var index = extra.FindIndex(x => x.Length == length);/*
                if (extra.Count != 0 && index == -1) {
                    throw new OptionException("unkown option \"" + extra[0] + "\"", extra[0]);
                }*/
            }
            catch (OptionException e)
            {
                Console.Write("greet: ");
                Console.WriteLine(e.Message);
                string currentExecutable =
System.AppDomain.CurrentDomain.FriendlyName;
                Console.WriteLine($"Try `{currentExecutable} --help' for more information.");
                return 1;
            }
            Grapich.Do();
            if (show_help)
            {
                options.WriteOptionDescriptions(Console.Out);
                return 0;
            };

            var program = new Program(noGui, args);
            XmlConfigurator.Configure();
            program.Commands = GetCommands();
            log.Info("starting program with args: " + String.Join(" ",args));
            SettingsData.refreshImage += SingleCard.SingleQuestEvent;
            Console.WriteLine("Starting Gui");
            var thread = new Thread(new ThreadStart(() =>
            {
                Console.WriteLine("Awaiting selection of sheets");
                try
                {
                    program.Form.promise.Task.Wait();
                    Settings.SaveSettings(LocalDatacs.SheetSelected());

                    // getting defualt settings
                    Questmaker.Run(program.settings);
                    Console.ReadLine();
                }
                catch (AggregateException e)
                {
                    if (e.InnerException.GetType() != typeof(System.Threading.Tasks.TaskCanceledException))
                        throw e;
                    Console.WriteLine("program closed");
                }
            }));
            thread.Start();
            program.thread1 = new Thread(new ThreadStart(() =>
            {
                var file = new FilerWatcher(program.settings, program.fileForm);
            }));
            Drive.Show();
            if (names.Count == 0)
                names = Settings.Instance.Primary.defaultSheets;

              program.Gui(names);

            
            
            
            //thread.Join();
            
            return program.Exit; 
        }
        
        private void Gui(IList<string> names)
        {
            fileForm.Show();
            if (names.Count == 0)
            {
               ;
            }else
            {
               Form.Names = names ;
            }
            
            thread1.Start();
            Form.Run();
           
        }
        private void Dispatch()
        {
            Exit = ConsoleCommandDispatcher.DispatchCommand(Commands, this.Args, Console.Out);
            return;
        }

        public static IEnumerable<ConsoleCommand> GetCommands()
        {
            return ConsoleCommandDispatcher.FindCommandsInSameAssemblyAs(typeof(Program));
        }
    }
}
