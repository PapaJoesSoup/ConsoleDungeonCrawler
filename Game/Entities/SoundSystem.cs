﻿using LibVLCSharp.Shared;


namespace ConsoleDungeonCrawler.Game.Entities;

internal static class SoundSystem
{
  // Note this works on Windows only!!!
  // We want to use LibVLCSharp to play layered sound effects and background sounds
  // We can also System.Media.SoundPlayer to play individual effects

  // Declare the LibVLCSharp objects
  private static readonly LibVLC libVlc;
  private static readonly MediaPlayer BackgroundMPlayer1;
  private static readonly MediaPlayer BackgroundMPlayer2;
  private static readonly MediaPlayer MediaEffectPlayer;
  internal static readonly Dictionary<Sound, Media> MSounds;

  // Static Initializer
  static SoundSystem()
  {
    // In order to play multiple sounds at once, we need to use LibVLCSharp.  SoundPlayer will not allow this.
    //LibVLCSharp mediaPlayers for Background Music
    // we want only one instance of libVLC.  we can have as many media players as we want.
    libVlc = new();

    // we want to loop the background music, so create an input-repeat option
    string[] options = { ":input-repeat=65535" };

    // create the media players
    BackgroundMPlayer1 = new(libVlc);
    BackgroundMPlayer2 = new(libVlc);
    MediaEffectPlayer = new(libVlc);

    // create the media objects
    //Note, it seems the VLC library does not like looping OGG files (seek issues).
    //I had to convert them to WAV or MP3 files so they loop correctly.
    MSounds = new()
    {
      { Sound.Intro, new Media(libVlc, $"{Game.SoundPath}Ogg/456384__lost_dream__intro.ogg")},
      { Sound.GameTitle, new Media(libVlc, $"{Game.SoundPath}Mp3/565051__aria_of_winds__hellishambience.mp3", FromType.FromPath, options)},
      { Sound.GameWon, new Media(libVlc, $"{Game.SoundPath}Ogg/516566__kinoton__dark-dungeon-ambience.ogg")},
      { Sound.GameOver, new Media(libVlc, $"{Game.SoundPath}Ogg/166187__drminky__creepy-dungeon-ambience.ogg")},
      { Sound.GameEnter, new Media(libVlc, $"{Game.SoundPath}Ogg/97790__cgeffex__dungeon-gates.ogg")},
      { Sound.Ambiance1, new Media(libVlc, $"{Game.SoundPath}Wav/388340__phlair__dungeon-ambiance.wav", FromType.FromPath, options)},
      { Sound.Ambiance2, new Media(libVlc, $"{Game.SoundPath}Wav/322447__goodlistener__dungeon-2.wav", FromType.FromPath, options)},
      { Sound.Door, new Media(libVlc, $"{Game.SoundPath}Ogg/580442__bennynz__dungeon_lock_2.ogg") },
      { Sound.Stairs, new Media(libVlc, $"{Game.SoundPath}Ogg/233065__lukeupf__stairs.ogg") },
      { Sound.FootSteps, new Media(libVlc, $"{Game.SoundPath}Ogg/490951__nox_footsteps1.ogg") },
      { Sound.GoblinCackle, new Media(libVlc, $"{Game.SoundPath}Ogg/202096__spookymodem__goblin-cackle.ogg") },
      { Sound.GoblinScream, new Media(libVlc, $"{Game.SoundPath}Ogg/202100__spookymodem__goblin-scream.ogg") },
      { Sound.GoblinDeath, new Media(libVlc, $"{Game.SoundPath}Ogg/249813__spookymodem__goblin-death.ogg") },
      { Sound.SwordSwing, new Media(libVlc, $"{Game.SoundPath}Ogg/268227__xxchr0nosxx__swing.ogg") },
      { Sound.RangedAttack, new Media(libVlc, $"{Game.SoundPath}Ogg/547041__eponn__hit-swing-sword-small-3.ogg") },
      { Sound.Boss, new Media(libVlc, $"{Game.SoundPath}Ogg/641931__kbrecordzz__dungeon-boss-aku.ogg") },
      { Sound.BossDeath, new Media(libVlc, $"{Game.SoundPath}Ogg/210997__zagi2__demonic-vocal-intro.ogg") },
      { Sound.Vendor, new Media(libVlc, $"{Game.SoundPath}Ogg/75235__creek23__cha-ching.ogg") },
      { Sound.Pickup, new Media(libVlc, $"{Game.SoundPath}Ogg/209578__zott820__cash-register-purchase.ogg")}

    };
  }

  internal static MediaPlayer GetPlayer()
  {
    return new(libVlc);
  }


  internal static void PlayTitle()
  {
    BackgroundMPlayer1.Stop();
    BackgroundMPlayer1.Play(MSounds[Sound.GameTitle]);
    BackgroundMPlayer2.Play(MSounds[Sound.Ambiance1]);
  }

  internal static void PlayEnter()
  {
    BackgroundMPlayer1.Stop();
    BackgroundMPlayer2.Stop();
    PlayEffect(MSounds[Sound.GameEnter]);
  }

  internal static void PlayBackground()
  {
    //BackgroundMPlayer1.Play(MSounds[Sound.Ambiance1]);
    //BackgroundMPlayer2.Play(MSounds[Sound.Ambiance2]);
  }

  internal static void PlayEffect(Media media)
  {
    MediaEffectPlayer.Play(media);
  }
}