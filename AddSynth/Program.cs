using System;
using NAudio.Wave;
using System.Linq;

namespace AddSynth.Oscillator
{
    public class Voice : ISampleProvider
    {
        int frequency { get; set; }
        int sampleRate { get; set; }
        int phase { get; set; }
        double amp { get; set; }

        float[] wave;
        double twoPi = Math.PI * 2;

        double[] harmonics;
        public WaveFormat WaveFormat { get; }

        public int Read(float[] buffer, int offset, int count)
        {
            for (int i = 0; i < count; i++)
            {
                buffer[i] = wave[i];
            }
            return count;
        }

        public Voice(
            double[] _harmonics = null, 
            int _rootFrequency = 220, 
            int _sampleRate = 44100, 
            double _amp = 1, 
            int _phase = 0)
        {
            harmonics = _harmonics ?? new double[] { 1 };
            frequency = _rootFrequency;
            sampleRate = _sampleRate;
            amp = _amp;
            phase = _phase;

            WaveFormat = WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, 1);
            wave = generateWave();
        }

        public float[] generateWave()
        {
            float[] wave = new float[sampleRate];
            for (int sampleCount = 0; sampleCount < sampleRate; sampleCount++)
            {
                float sampleValue = 0;
                for (int harmonicIndex = 0; harmonicIndex < harmonics.Length; harmonicIndex++)
                {
                    sampleValue += (float)((harmonics[harmonicIndex] * amp) * Math.Sin((twoPi * (frequency * (harmonicIndex + 1)) / sampleRate) * sampleCount + phase));
                }
                wave[sampleCount] = sampleValue;
            }
            return wave;
        }
    }

    public class Playback
    {
        static void Main()
        {
            playAudio();
        }

        static void playAudio()
        {
            WaveOutEvent waveOut = new WaveOutEvent();
            double[] harmonics = new double[100];
            for (int i = 0; i < 100; i++)
            {
                harmonics[i] = 1d / (i + 1d);
                Console.WriteLine(1d / (i + 1d));
            }
            Voice sineWave = new Voice(harmonics);

            waveOut.Init(sineWave);
            waveOut.Play();

            Console.ReadKey();
        }
    }
}
