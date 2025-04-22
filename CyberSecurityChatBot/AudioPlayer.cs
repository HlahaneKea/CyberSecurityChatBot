using System;
using System.Media;

namespace CyberSecurityChatBot
{
    public class AudioPlayer
    {
        public void PlayGreetingAudio(string filePath)
        {
            try
            {
                using (SoundPlayer player = new SoundPlayer(filePath))
                {
                    player.PlaySync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(" Unable to play audio. Error: " + ex.Message);
            }
        }
    }
}
