using ManyConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spellmaker
{
    class QuestCommand : ConsoleCommand
    {
        private const int Success = 0;
        private const int Failure = 2;
        //private bool Json = false;
        public QuestCommand()
        {
            IsCommand("makeQuests", "turns A google sheet into a tiled png for Tabletop sim");
            HasLongDescription("running this will Download from a sheet a table for processing.");
            HasOption("j", "update json with file's in local storage",
                (v) => v.Replace(" "," "));
        }

        public override int Run(string[] remainingArguments)
        {
#if DEBUG
#else
            try{
#endif
            var set = Settings.Init(true , remainingArguments); // getting defualt settings

            Console.WriteLine("Done with " + this.Command + " command");
            Console.ReadLine();
            return Success;
#if DEBUG
#else
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                Console.Error.WriteLine(ex.StackTrace);
                throw;
            }
#endif
        }
    }

    class SpellCommand : ConsoleCommand
    {
        private const int Success = 0;
        private const int Failure = 2;
        public SpellCommand()
        {
            IsCommand("makeSpell", "turns A google sheet into a tiled png for Tabletop sim");
            HasLongDescription("This can be used to quickly read a file's contents " +
            "while optionally stripping out the ',' character.");
        }

        public override int Run(string[] remainingArguments)
        {
#if DEBUG
#else
            try
            {
#endif

            return Success;
#if DEBUG
#else
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                Console.Error.WriteLine(ex.StackTrace);
                throw;
            }
#endif
        }
    }

    class DefaultCommand : ConsoleCommand
    {
        private const int Success = 0;
        private const int Failure = 2;
        public DefaultCommand()
        {
            IsCommand("default", "default settings");
            HasLongDescription("uses default settings to do a predifned set of actions");
            HasAdditionalArguments(0, "<Argument1> <Argument2>");
            AllowsAnyAdditionalArguments("Testing");
        }

        public override int Run(string[] remainingArguments)
        {
#if DEBUG
#else
            try
            {
#endif     

            return Success;
#if DEBUG
#else
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                Console.Error.WriteLine(ex.StackTrace);
                Console.Read();
                return Failure;
            }
#endif
        }
    }
    class UploadCommand : ConsoleCommand
    {
        private bool Show = false;
        private bool Json = false;
        private const int Success = 0;
        private const int Failure = 2;
        public UploadCommand()
        {
            IsCommand("upload", "uplaod a picture to google drive");
            HasOption("s", "show file in drive",
                (v) => Show = true);
            HasOption("j", "show file in drive",
                (v) => Json = true);

            HasLongDescription(""
            );
        }

        public override int Run(string[] remainingArguments)
        {
#if DEBUG

            var set = Settings.Init(true, remainingArguments); // getting defualt settings

            Console.WriteLine("Done with " + this.Command + " command");
            Console.ReadLine();
            return Success;

#else
            try
            {
                //Drive.run(remainingArguments);
             Console.WriteLine("Done with " + this.Command + " command");
                return Success;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                Console.Error.WriteLine(ex.StackTrace);
                Console.Read();
                return Failure;
            }
#endif
        }
    }
    class SettingsCommand : ConsoleCommand
    {
        private const int Success = 0;
        private const int Failure = 2;
        public SettingsCommand()
        {
            IsCommand("Settings", "settings util");
            HasLongDescription(""
            );
        }

        public override int Run(string[] remainingArguments)
        {
#if DEBUG

            return Success;

#else
            try
            {
              //  Drive.un(remainingArguments);
             Console.WriteLine("Done with " + this.Command + " command");
                return Success;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                Console.Error.WriteLine(ex.StackTrace);
                Console.Read();
                return Failure;
            }
#endif
        }
    }
}
