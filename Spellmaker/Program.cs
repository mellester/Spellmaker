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
        private string[] Args;
        private Settings settings;
        private questGui.Form1 Form;
        private questGui.FileForm1 fileForm;
        private Thread thread1;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public Program()
        {
            this.settings = Settings.Init();
            var settingsData = settings.Primary;
            Form = new questGui.Form1(settingsData);
            Settings.form = Form;
            fileForm = new questGui.FileForm1(settingsData);
        }
        [STAThread]
        public static int Main(string[] args)
        {
            bool show_help = false;
            List<string> names = new List<string>();
            int repeat = 1;
            OptionSet options = new OptionSet() {
            "Usage: greet [OPTIONS]+ message",
            "Greet a list of individuals with an optional message.",
            "If no message is specified, a generic greeting is used.",
            "",
            "Options:",
            { "n|name=", "the {NAME} of of the sheet.",
              v => names.Add (v) },
            { "r|repeat=",
                "the number of {TIMES} to repeat the greeting.\n" +
                    "this must be an integer.",
              (int v) => repeat = v },
            { "v", "increase nothing",
              v => {  ; } },
            { "h|help",  "show this message and exit",
              v => show_help = v != null },
            };
            List<string> extra;
            try
            {
                extra = options.Parse(args);
            }
            catch (OptionException e)
            {
                Console.Write("greet: ");
                Console.WriteLine(e.Message);
                Console.WriteLine("Try `greet --help' for more information.");
                return 1;
            }
            Grapich.Do();
            if (show_help)
            {
                options.WriteOptionDescriptions(Console.Out);
                return 0;
            };

            var program = new Program();
            XmlConfigurator.Configure();
            program.Commands = GetCommands();
            program.Args = args;
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
           Application.Run(Form);
        }
        private void Dispatch()
        {
            Exit = ConsoleCommandDispatcher.DispatchCommand(Commands, Args, Console.Out);
            return;
        }

        public static IEnumerable<ConsoleCommand> GetCommands()
        {
            return ConsoleCommandDispatcher.FindCommandsInSameAssemblyAs(typeof(Program));
        }
    }
}
