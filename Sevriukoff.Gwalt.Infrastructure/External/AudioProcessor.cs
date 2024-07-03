using NAudio.Wave;

namespace Sevriukoff.Gwalt.Infrastructure.External;

public class AudioProcessor
{
    public float[] GetAudioPeaks(Stream audioStream, int numPeaks = 500)
    {
        var peaks = new List<float>();
        
        using (var mp3Reader = new Mp3FileReader(audioStream))
        {
            var waveFormat = mp3Reader.WaveFormat;
            int bytesPerSample = waveFormat.BitsPerSample / 8;
            int sampleRate = waveFormat.SampleRate;
            long totalSamples = mp3Reader.Length / bytesPerSample;
            long samplesPerPeak = totalSamples / numPeaks;
            byte[] buffer = new byte[bytesPerSample];
            int bytesRead;

            for (int i = 0; i < numPeaks; i++)
            {
                long position = i * samplesPerPeak * bytesPerSample;
                mp3Reader.Position = position;
                bytesRead = mp3Reader.Read(buffer, 0, bytesPerSample);
                if (bytesRead == 0)
                {
                    break;
                }
                
                short sample = BitConverter.ToInt16(buffer, 0);
                float sampleValue = sample / 32768f;
                peaks.Add(sampleValue);
            }
        }

        return RoundPeaks(peaks).ToArray();
    }

    private List<float> RoundPeaks(List<float> peaks, int decimals = 2)
    {
        return peaks.Select(p => (float)Math.Round(p, decimals)).ToList();
    }
}