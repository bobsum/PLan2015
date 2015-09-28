using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Bismuth.Framework.Audio
{
    public class XactSoundManager : ISoundManager
    {
        private readonly AudioEngine _audioEngine;
        private readonly SoundBank _soundBank;
        private readonly WaveBank _waveBank;

        public XactSoundManager(string settingsFilename, string waveBankFilename, string soundBankFilename)
        {
            _audioEngine = new AudioEngine(settingsFilename);
            _waveBank = new WaveBank(_audioEngine, waveBankFilename);
            _soundBank = new SoundBank(_audioEngine, soundBankFilename);
        }

        public void Update(GameTime gameTime)
        {
            _audioEngine.Update();
        }

        public void PlayCue(string name)
        {
            _soundBank.PlayCue(name);
        }

        public ICue GetCue(string name)
        {
            return new XactCue(_soundBank.GetCue(name));
        }

        public ICue GetCue(string name, bool newInstance)
        {
            return new XactCue(_soundBank.GetCue(name));
        }
    }
}
