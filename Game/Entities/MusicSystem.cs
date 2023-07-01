using System.Media;
using LibVLCSharp.Shared;


namespace ConsoleDungeonCrawler.Game.Entities;

internal static class MusicSystem
{
  // Note this works on Windows only!!!

  // we have a synchronous method that plays music using the Console.Beep() method
  // we also want to have an asynchronous method that plays music using the Console.Beep() method
  // Finally we also want to us System.Media.SoundPlayer to play music

  // Declare the LibVLCSharp objects
  internal static LibVLC libVLC;
  internal static MediaPlayer BackgroundMPlayer1;
  internal static MediaPlayer BackgroundMPlayer2;

  // Declare the SoundPlayer objects
  internal static SoundPlayer EffectPlayer1 = new();
  internal static SoundPlayer EffectPlayer2 = new();

  // Declare the first few notes of the song, "Mary Had A Little Lamb" For Console Beep.
  internal static Note[] Mary =
  {
    new Note(Tone.B, Duration.QUARTER),
    new Note(Tone.A, Duration.QUARTER),
    new Note(Tone.GbelowC, Duration.QUARTER),
    new Note(Tone.A, Duration.QUARTER),
    new Note(Tone.B, Duration.QUARTER),
    new Note(Tone.B, Duration.QUARTER),
    new Note(Tone.B, Duration.HALF),
    new Note(Tone.A, Duration.QUARTER),
    new Note(Tone.A, Duration.QUARTER),
    new Note(Tone.A, Duration.HALF),
    new Note(Tone.B, Duration.QUARTER),
    new Note(Tone.D, Duration.QUARTER),
    new Note(Tone.D, Duration.HALF)
  };

  // Static Initializer
  static MusicSystem()
  {
    //LibVLCSharp mediaPlayers for Background Music
    // we want only one instance of libVLC.  we can have as many media players as we want.
    libVLC = new(enableDebugLogs: false);
    // we want to loop the background music, so we need to set the input-repeat option
    string[] options = { ":input-repeat=65535" };
    Media media1 = new Media(libVLC, $"{Game.SoundPath}388340__phlair__dungeon-ambiance.wav", FromType.FromPath, options);
    Media media2 = new Media(libVLC, $"{Game.SoundPath}322447__goodlistener__dungeon-2.wav", FromType.FromPath, options);
    BackgroundMPlayer1 = new(media1);
    BackgroundMPlayer2 = new(media2);

    //System.Media.SoundPlayer for Effects
    EffectPlayer1 = new($"{Game.SoundPath}322447__goodlistener__dungeon-2.wav");
    EffectPlayer2 = new($"{Game.SoundPath}580443__bennynz__dungeon_lock_1.wav");
  }

  internal static void PlayBackground()
  {
    BackgroundMPlayer1.Play();
    BackgroundMPlayer2.Play();
  }

  internal static void PlayEffect()
  {
    EffectPlayer2.Play();
  } 

  // Console.Beep methods
  internal static void Play(Note[] tune)
  {
    foreach (Note n in tune)
    {
      if (n.NoteTone == Tone.REST)
        Thread.Sleep((int)n.NoteDuration);
      else
        Console.Beep((int)n.NoteTone, (int)n.NoteDuration);
    }
  }

  // Define the frequencies of notes in an octave, as well as
  // silence (rest).
  internal enum Tone
  {
    REST = 0,
    GbelowC = 196,
    A = 220,
    Asharp = 233,
    B = 247,
    C = 262,
    Csharp = 277,
    D = 294,
    Dsharp = 311,
    E = 330,
    F = 349,
    Fsharp = 370,
    G = 392,
    Gsharp = 415,
  }

  // Define the duration of a note in units of milliseconds.
  internal enum Duration
  {
    WHOLE = 1600,
    HALF = WHOLE / 2,
    QUARTER = HALF / 2,
    EIGHTH = QUARTER / 2,
    SIXTEENTH = EIGHTH / 2,
  }

  // Define a note as a frequency (tone) and the amount of
  // time (duration) the note plays.
  internal struct Note
  {
    Tone toneVal;
    Duration durVal;

    // Define a constructor to create a specific note.
    public Note(Tone frequency, Duration time)
    {
      toneVal = frequency;
      durVal = time;
    }

    // Define properties to return the note's tone and duration.
    internal Tone NoteTone { get { return toneVal; } }
    internal Duration NoteDuration { get { return durVal; } }
  }
}