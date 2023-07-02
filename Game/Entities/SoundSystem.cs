using System.Media;
using LibVLCSharp.Shared;


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

  // Declare the SoundPlayer objects
  private static readonly SoundPlayer SoundEffectPlayer;
  internal static readonly Dictionary<Sound, string> Sounds;


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
    MSounds = new()
    {
      { Sound.Intro, new Media(libVlc, $"{Game.SoundPath}565051__aria_of_winds__hellishambience01.ogg")},
      { Sound.GameTitle, new Media(libVlc, $"{Game.SoundPath}565051__aria_of_winds__hellishambience01.ogg")},
      { Sound.GameWon, new Media(libVlc, $"{Game.SoundPath}516566__kinoton__dark-dungeon-ambience.ogg")},
      { Sound.GameOver, new Media(libVlc, $"{Game.SoundPath}166187__drminky__creepy-dungeon-ambience.ogg")},
      { Sound.GameEnter, new Media(libVlc, $"{Game.SoundPath}97790__cgeffex__dungeon-gates.ogg")},
      { Sound.Ambiance1, new Media(libVlc, $"{Game.SoundPath}388340__phlair__dungeon-ambiance.ogg", FromType.FromPath, options)},
      { Sound.Ambiance2, new Media(libVlc, $"{Game.SoundPath}322447__goodlistener__dungeon-2.ogg", FromType.FromPath, options)},
      { Sound.Door, new Media(libVlc, $"{Game.SoundPath}580442__bennynz__dungeon_lock_2.ogg") },
      { Sound.Stairs, new Media(libVlc, $"{Game.SoundPath}233065__lukeupf__stairs.ogg") },
      { Sound.FootSteps, new Media(libVlc, $"{Game.SoundPath}490951__nox_sound__footsteps_walk.ogg") },
      { Sound.GoblinCackle, new Media(libVlc, $"{Game.SoundPath}202096__spookymodem__goblin-cackle.ogg") },
      { Sound.GoblinScream, new Media(libVlc, $"{Game.SoundPath}202100__spookymodem__goblin-scream.ogg") },
      { Sound.GoblinDeath, new Media(libVlc, $"{Game.SoundPath}249813__spookymodem__goblin-death.ogg") },
      { Sound.SwordSwing, new Media(libVlc, $"{Game.SoundPath}249813__spookymodem__goblin-death.ogg") },
      { Sound.Boss, new Media(libVlc, $"{Game.SoundPath}641931__kbrecordzz__dungeon-boss-aku.ogg") },
      { Sound.BossDeath, new Media(libVlc, $"{Game.SoundPath}210997__zagi2__demonic-vocal-intro.ogg") },
      { Sound.Vendor, new Media(libVlc, $"{Game.SoundPath}75235__creek23__cha-ching.ogg") },
      { Sound.Pickup, new Media(libVlc, $"{Game.SoundPath}209578__zott820__cash-register-purchase.ogg")}

    };

    //System.Media.SoundPlayer for Effects
    SoundEffectPlayer = new();

    // create the sound objects
    // Downside is SoundPlayer does not support any other file types other than .wav
    Sounds = new()
    {
      { Sound.Door, $"{Game.SoundPath}580442__bennynz__dungeon_lock_2.wav" },
      { Sound.GoblinCackle, $"{Game.SoundPath}202096__spookymodem__goblin-cackle.wav" },
      { Sound.GoblinScream, $"{Game.SoundPath}202100__spookymodem__goblin-scream.wav" },
      { Sound.GoblinDeath, $"{Game.SoundPath}249813__spookymodem__goblin-death.wav" },
      { Sound.Pickup, $"{Game.SoundPath}209578__zott820__cash-register-purchase.wav" }
    };
  }

  internal static void PlayTitle()
  {
    BackgroundMPlayer1.Stop();
    BackgroundMPlayer1.Play(MSounds[Sound.GameTitle]);
    //BackgroundMPlayer2.Play(MSounds[Sound.Ambiance2]);
  }

  internal static void PlayEnter()
  {
    BackgroundMPlayer1.Stop();
    BackgroundMPlayer1.Play(MSounds[Sound.GameEnter]);
    //BackgroundMPlayer2.Play(MSounds[Sound.Ambiance2]);
  }

  internal static void PlayBackground()
  {
    BackgroundMPlayer1.Play(MSounds[Sound.Ambiance1]);
    BackgroundMPlayer2.Play(MSounds[Sound.Ambiance2]);
  }

  internal static void PlayEffect(Media media)
  {
    MediaEffectPlayer.Play(media);
  }

  internal static void PlayEffect(string location)
  {
    SoundEffectPlayer.SoundLocation = location;
    SoundEffectPlayer.Play();
  }
}