using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace TheMaze.WinForms.Services
{
    public class SoundService
    {
        private const string CLOSED = "/Resources/Audio/closed.wav";
        private const string ITEM = "/Resources/Audio/item.wav";
        private const string KEY = "/Resources/Audio/key.wav";
        private const string LOSE = "/Resources/Audio/lose.wav";
        private const string TRAP = "/Resources/Audio/trap.wav";
        private const string WIN = "/Resources/Audio/win.wav";

        //private SoundPlayer soundPlayer;
        private readonly string basePath;

        public SoundService()
        {
            //soundPlayer = new SoundPlayer();
            basePath = $"{Directory.GetParent(Environment.CurrentDirectory).Parent.FullName}";
        }

        public void PlayClosedSound()
        {
            using (var soundPlayer = new SoundPlayer())
            {
                soundPlayer.SoundLocation = $"{basePath}{CLOSED}";
                soundPlayer.Play();
            }
        }

        public void PlayItemSound()
        {
            using (var soundPlayer = new SoundPlayer())
            {
                soundPlayer.SoundLocation = $"{basePath}{ITEM}";
                soundPlayer.Play();
            }
        }

        public void PlayKeySound()
        {
            using (var soundPlayer = new SoundPlayer())
            {
                soundPlayer.SoundLocation = $"{basePath}{KEY}";
                soundPlayer.Play();
            }
        }

        public void PlayLoseSound()
        {
            using (var soundPlayer = new SoundPlayer())
            {
                soundPlayer.SoundLocation = $"{basePath}{LOSE}";
                soundPlayer.Play();
            }
        }

        public void PlayWinSound()
        {
            using (var soundPlayer = new SoundPlayer())
            {
                soundPlayer.SoundLocation = $"{basePath}{WIN}";
                soundPlayer.Play();
            }
        }

        public void PlayTrapSound()
        {
            using (var soundPlayer = new SoundPlayer())
            {
                soundPlayer.SoundLocation = $"{basePath}{TRAP}";
                soundPlayer.Play();
            }
        }
    }
}
