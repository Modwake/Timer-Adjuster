using BWModLoader;

namespace timerFix
{
    internal static class Log
    {
        static readonly public ModLogger logger = new ModLogger("[TimerFix]", ModLoader.LogPath + "\\Alternion.txt");
    }
    public class Logger
    {
        /// <summary>
        /// Always logs, no matter the logging level.
        /// </summary>
        /// <param name="message">Message to Log</param>
        public static void log(string message)
        {
            //Just easier to type than Log.logger.Log
            //Will always log, so only use in try{} catch(Exception e) {} when absolutely needed
            Log.logger.Log(message);
        }
    }
}
