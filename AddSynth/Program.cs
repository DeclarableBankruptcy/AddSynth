namespace AddSynth
{
    using NAudio.Wave;
    using System;

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
            Oscillators.Oscillator waveform = new Oscillators.Oscillator(harmonics);

            waveOut.Init(waveform);
            waveOut.Play();

            Console.ReadKey();
        }
    }
}
