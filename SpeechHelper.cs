using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;

namespace BillSwap
{

    public sealed class SpeechHelper : IDisposable
    {
        private readonly SpeechSynthesizer synth;

        private int Clamp(int value, int min, int max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }

        public SpeechHelper(int volume = 100, int rate = 0)
        {
            synth = new SpeechSynthesizer();
            synth.Volume = Clamp(volume, 0, 100);
            synth.Rate = Clamp(rate, -10, 10);  // -10..10
        }

        // Async (non-blocking)
        public void SpeakAsync(string text) => synth.SpeakAsync(text);

        // Blocking (waits until finished)
        public void Speak(string text) => synth.Speak(text);

        // Change voice (optional)
        public void SetVoiceByGender(VoiceGender gender)
        {
            try
            {
                synth.SelectVoiceByHints(gender, VoiceAge.Adult);
            }
            catch
            {
                // Fallback silently if requested voice isn't installed
            }
        }

        public string[] GetInstalledVoiceNames()
        {
            var list = new System.Collections.Generic.List<string>();
            foreach (var v in synth.GetInstalledVoices())
                list.Add(v.VoiceInfo.Name);
            return list.ToArray();
        }

        public void Dispose()
        {
            synth?.Dispose();
        }
    }

}
