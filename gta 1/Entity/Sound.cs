using gta_1.Properties;
using NAudio.Wave;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading.Tasks;

namespace gta_1
{
    internal class Sound
    {
        private static readonly byte[][] Music = new byte[1][];
        private static readonly int SoundAmount = 13;
        private static readonly byte[][] SoundEffects = new byte[SoundAmount][];
        private static readonly TimeSpan[] DelayBetweenSameSounds = new TimeSpan[SoundAmount];
        private static readonly DateTime[] SoundLastPlayedTime = new DateTime[SoundAmount];
        private static readonly ConcurrentQueue<int> SoundQueue = new ConcurrentQueue<int>();

        public static void Initialize()
        {
            Music[0] = Resources.gta_1;

            SoundEffects[0] = Resources.bonus;
            SoundEffects[1] = Resources.car_door_open;
            SoundEffects[2] = Resources.car_door_shut;
            SoundEffects[3] = Resources.corner_skid_noise;
            SoundEffects[4] = Resources.police_or_fire_siren;
            SoundEffects[5] = Resources.gunshot;
            SoundEffects[6] = Resources.punch_to_body;
            SoundEffects[7] = Resources.crowd_in_panic;
            SoundEffects[8] = Resources.death_cry_of_person;
            SoundEffects[9] = Resources.player_running__single_footstep_;
            SoundEffects[10] = Resources.start_engine;
            SoundEffects[11] = Resources.normal_car_revs;
            SoundEffects[12] = Resources.squashed_pedestrian;

            for (int i = 0; i < SoundAmount; i++)
            {
                DelayBetweenSameSounds[i] = new TimeSpan(0, 0, 0, 0, 100);
                SoundLastPlayedTime[i] = DateTime.Now;

                if (i == 3)
                    DelayBetweenSameSounds[i] = new TimeSpan(0, 0, 0, 0, 1000);
            }
    }

        public static void EnqueueSound(int soundIndex)
        {
            if (DateTime.Now - SoundLastPlayedTime[soundIndex] < DelayBetweenSameSounds[soundIndex])
                return;

            SoundLastPlayedTime[soundIndex] = DateTime.Now;
            SoundQueue.Enqueue(soundIndex);
            PlayNextSound();
        }

        public static void SoundHandler(IEntity entity)
        {
            if (entity is Vehicle)
            {
                if (entity.Position != Game.player.Position)
                    return;

                EnqueueSound(11);

                if (!entity.IsMoving(entity.MovingVector))
                    return;

                if (entity.MovingVector == entity.LastMovingVector)
                    return;
                
                EnqueueSound(3);
            }
            else if (entity is Player)
            {
                if (!entity.IsMoving(entity.MovingVector))
                    return;

                EnqueueSound(9);
            }
        }

        private static void PlayNextSound()
        {
            if (SoundQueue.TryDequeue(out int soundIndex))
                Task.Run(() => PlaySoundAsync(soundIndex));
        }

        private static async Task PlaySoundAsync(int soundIndex)
        {
            using (WaveOutEvent soundOutput = new WaveOutEvent())
            using (Mp3FileReader fileReaderSound = new Mp3FileReader(new MemoryStream(SoundEffects[soundIndex])))
            {
                soundOutput.Init(fileReaderSound);
                soundOutput.Volume = Options.SoundVolume;
                soundOutput.Play();

                while (soundOutput.PlaybackState == PlaybackState.Playing)
                {
                    if (Tools.CancellationToken.IsCancellationRequested)
                    {
                        soundOutput?.Stop();
                        soundOutput?.Dispose();
                        fileReaderSound?.Dispose();
                        break;
                    }

                    await Task.Delay(100);
                }
            }
        }

        public static IWavePlayer MusicOutput;
        public static MemoryStream MemoryStreamMusic;
        public static void PlayMusic(int soundTrackIndex)
        {
            MusicOutput?.Dispose();
            MemoryStreamMusic?.Dispose();
            MemoryStreamMusic = new MemoryStream(Music[soundTrackIndex]);
            MusicOutput = new WaveOutEvent();
            MusicOutput.Init(new Mp3FileReader(MemoryStreamMusic));
            MusicOutput.Volume = Options.MusicVolume;
            MusicOutput.Play();
        }
    }
}